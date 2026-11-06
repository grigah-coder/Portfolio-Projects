package com.grigah.appmag.repository;

import com.grigah.appmag.model.Article;
import com.grigah.appmag.model.Categorie;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.StandardOpenOption;
import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import java.util.Objects;

/**
 * Dépôt CSV lisible par un humain.
 *
 * <p>Format :</p>
 * <pre>
 * CAT;id;nom;capacite
 * ART;idCategorie;idArticle;libelle;prix
 * </pre>
 */
public final class CsvMagasinRepository implements MagasinRepository {

    private static final String ENTETE = "type;id;nom;capacite";

    private final Path fichier;

    public CsvMagasinRepository(Path fichier) {
        this.fichier = Objects.requireNonNull(fichier, "Le fichier de données est obligatoire.");
    }

    @Override
    public List<Categorie> charger() throws IOException {
        if (!Files.exists(fichier)) {
            return List.of();
        }

        Map<Integer, Categorie> categories = new LinkedHashMap<>();
        List<String[]> lignesArticles = new ArrayList<>();

        try (BufferedReader reader = Files.newBufferedReader(fichier, StandardCharsets.UTF_8)) {
            String ligne;
            int numeroLigne = 0;

            while ((ligne = reader.readLine()) != null) {
                numeroLigne++;
                if (ligne.isBlank() || ligne.startsWith("#")) {
                    continue;
                }
                if (numeroLigne == 1 && ligne.startsWith("type;")) {
                    continue;
                }

                List<String> champs = Csv.analyser(ligne, numeroLigne);
                String type = champs.get(0);

                if ("CAT".equals(type)) {
                    verifierNombreColonnes(champs, 4, numeroLigne);
                    Categorie categorie = construireCategorie(champs, numeroLigne);
                    categories.put(categorie.getId(), categorie);
                } else if ("ART".equals(type)) {
                    verifierNombreColonnes(champs, 5, numeroLigne);
                    lignesArticles.add(champs.toArray(String[]::new));
                } else {
                    throw new IOException("Ligne " + numeroLigne
                            + " invalide : type inconnu « " + type + " ».");
                }
            }
        }

        int numeroLigneArticle = 0;
        for (String[] champs : lignesArticles) {
            numeroLigneArticle++;
            int idCategorie = lireEntier(champs[1], "identifiant de catégorie", numeroLigneArticle);
            Categorie categorie = categories.get(idCategorie);
            if (categorie == null) {
                throw new IOException("Article " + numeroLigneArticle
                        + " : la catégorie " + idCategorie + " n'existe pas.");
            }
            Article article = construireArticle(champs, numeroLigneArticle);
            if (!categorie.ajouterArticle(article)) {
                throw new IOException("Article " + numeroLigneArticle
                        + " : catégorie pleine ou article en double.");
            }
        }

        return new ArrayList<>(categories.values());
    }

    @Override
    public void sauvegarder(List<Categorie> categories) throws IOException {
        Path parent = fichier.getParent();
        if (parent != null) {
            Files.createDirectories(parent);
        }

        try (BufferedWriter writer = Files.newBufferedWriter(
                fichier,
                StandardCharsets.UTF_8,
                StandardOpenOption.CREATE,
                StandardOpenOption.TRUNCATE_EXISTING,
                StandardOpenOption.WRITE)) {
            writer.write(ENTETE);
            writer.newLine();
            for (Categorie categorie : categories) {
                writer.write(String.join(";",
                        "CAT",
                        Integer.toString(categorie.getId()),
                        Csv.echapper(categorie.getNom()),
                        Integer.toString(categorie.getCapacite())));
                writer.newLine();

                for (Article article : categorie.getArticles()) {
                    writer.write(String.join(";",
                            "ART",
                            Integer.toString(categorie.getId()),
                            Integer.toString(article.getId()),
                            Csv.echapper(article.getLibelle()),
                            Double.toString(article.getPrix())));
                    writer.newLine();
                }
            }
        }
    }

    @Override
    public Path getChemin() {
        return fichier;
    }

    private static void verifierNombreColonnes(List<String> champs, int attendu, int numeroLigne)
            throws IOException {
        if (champs.size() != attendu) {
            throw new IOException("Ligne " + numeroLigne + " invalide : "
                    + attendu + " colonnes attendues.");
        }
    }

    private static Categorie construireCategorie(List<String> champs, int numeroLigne)
            throws IOException {
        try {
            int id = Integer.parseInt(champs.get(1));
            String nom = champs.get(2);
            int capacite = Integer.parseInt(champs.get(3));
            return new Categorie(id, nom, capacite);
        } catch (IllegalArgumentException exception) {
            throw new IOException("Ligne " + numeroLigne + " invalide : "
                    + exception.getMessage(), exception);
        }
    }

    private static Article construireArticle(String[] champs, int numeroLigne) throws IOException {
        try {
            int id = Integer.parseInt(champs[2]);
            String libelle = champs[3];
            double prix = Double.parseDouble(champs[4]);
            return new Article(id, libelle, prix);
        } catch (IllegalArgumentException exception) {
            throw new IOException("Article " + numeroLigne + " invalide : "
                    + exception.getMessage(), exception);
        }
    }

    private static int lireEntier(String valeur, String champ, int numeroLigne) throws IOException {
        try {
            return Integer.parseInt(valeur);
        } catch (NumberFormatException exception) {
            throw new IOException("Article " + numeroLigne + " : " + champ + " invalide.", exception);
        }
    }
}
