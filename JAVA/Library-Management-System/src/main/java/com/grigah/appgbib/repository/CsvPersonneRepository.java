package com.grigah.appgbib.repository;

import com.grigah.appgbib.model.Employe;
import com.grigah.appgbib.model.Lecteur;
import com.grigah.appgbib.model.Personne;
import com.grigah.appgbib.model.PersonneType;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.StandardOpenOption;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;

/**
 * Dépôt CSV lisible par un humain.
 *
 * <p>Le format utilise le point-virgule comme séparateur. Les champs contenant
 * un séparateur, un guillemet ou un saut de ligne sont échappés entre guillemets.</p>
 */
public final class CsvPersonneRepository implements PersonneRepository {

    private static final String ENTETE = "type;id;nom;prenom;role;emprunts";

    private final Path fichier;

    public CsvPersonneRepository(Path fichier) {
        this.fichier = Objects.requireNonNull(fichier, "Le fichier de données est obligatoire.");
    }

    @Override
    public List<Personne> charger() throws IOException {
        if (!Files.exists(fichier)) {
            return List.of();
        }

        List<Personne> personnes = new ArrayList<>();
        try (BufferedReader reader = Files.newBufferedReader(fichier, StandardCharsets.UTF_8)) {
            String ligne;
            int numeroLigne = 0;
            boolean enteteLue = false;

            while ((ligne = reader.readLine()) != null) {
                numeroLigne++;
                if (ligne.isBlank()) {
                    continue;
                }
                if (!enteteLue) {
                    enteteLue = true;
                    if (ligne.startsWith("type;")) {
                        continue;
                    }
                }

                List<String> champs = analyserLigne(ligne, numeroLigne);
                if (champs.size() != 6) {
                    throw new IOException("Ligne " + numeroLigne + " invalide : 6 colonnes attendues.");
                }

                try {
                    personnes.add(construirePersonne(champs));
                } catch (IllegalArgumentException exception) {
                    throw new IOException(
                            "Ligne " + numeroLigne + " invalide : " + exception.getMessage(), exception);
                }
            }
        }
        return personnes;
    }

    @Override
    public void sauvegarder(List<Personne> personnes) throws IOException {
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
            for (Personne personne : personnes) {
                int emprunts = personne instanceof Lecteur lecteur
                        ? lecteur.getEmpruntsEnCours()
                        : 0;
                writer.write(String.join(";",
                        personne.getType().name(),
                        Integer.toString(personne.getId()),
                        echapper(personne.getNom()),
                        echapper(personne.getPrenom()),
                        echapper(personne.getRole()),
                        Integer.toString(emprunts)));
                writer.newLine();
            }
        }
    }

    @Override
    public Path getChemin() {
        return fichier;
    }

    private static Personne construirePersonne(List<String> champs) {
        PersonneType type = PersonneType.valueOf(champs.get(0));
        int id = Integer.parseInt(champs.get(1));
        String nom = champs.get(2);
        String prenom = champs.get(3);
        String role = champs.get(4);
        int emprunts = Integer.parseInt(champs.get(5));

        return switch (type) {
            case LECTEUR -> new Lecteur(id, nom, prenom, role, emprunts);
            case EMPLOYE -> new Employe(id, nom, prenom, role);
        };
    }

    private static String echapper(String valeur) {
        if (valeur == null) {
            return "";
        }
        boolean necessiteGuillemets = valeur.indexOf(';') >= 0
                || valeur.indexOf('"') >= 0
                || valeur.indexOf('\n') >= 0
                || valeur.indexOf('\r') >= 0;
        if (!necessiteGuillemets) {
            return valeur;
        }
        return '"' + valeur.replace("\"", "\"\"") + '"';
    }

    private static List<String> analyserLigne(String ligne, int numeroLigne) throws IOException {
        List<String> champs = new ArrayList<>();
        StringBuilder courant = new StringBuilder();
        boolean entreGuillemets = false;

        for (int i = 0; i < ligne.length(); i++) {
            char caractere = ligne.charAt(i);
            if (entreGuillemets) {
                if (caractere == '"') {
                    if (i + 1 < ligne.length() && ligne.charAt(i + 1) == '"') {
                        courant.append('"');
                        i++;
                    } else {
                        entreGuillemets = false;
                    }
                } else {
                    courant.append(caractere);
                }
            } else if (caractere == '"') {
                entreGuillemets = true;
            } else if (caractere == ';') {
                champs.add(courant.toString());
                courant.setLength(0);
            } else {
                courant.append(caractere);
            }
        }

        if (entreGuillemets) {
            throw new IOException("Ligne " + numeroLigne + " invalide : guillemet non fermé.");
        }

        champs.add(courant.toString());
        return champs;
    }
}
