package com.grigah.appmag.service;

import com.grigah.appmag.model.Article;
import com.grigah.appmag.model.Categorie;
import com.grigah.appmag.model.Validation;
import com.grigah.appmag.repository.MagasinRepository;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;

/**
 * Orchestre les cas d'usage du magasin et conserve les données en mémoire
 * jusqu'à la prochaine sauvegarde.
 */
public final class MagasinService {

    private final MagasinRepository repository;
    private final List<Categorie> categories = new ArrayList<>();
    private int prochainIdCategorie = 1;
    private int prochainIdArticle = 1;

    public MagasinService(MagasinRepository repository) {
        this.repository = Objects.requireNonNull(repository, "Le dépôt est obligatoire.");
    }

    /**
     * Charge les catégories depuis le dépôt et calcule les prochains identifiants.
     */
    public void charger() throws IOException {
        List<Categorie> chargees = repository.charger();
        categories.clear();
        categories.addAll(chargees);

        prochainIdCategorie = categories.stream()
                .mapToInt(Categorie::getId)
                .max()
                .orElse(0) + 1;

        prochainIdArticle = categories.stream()
                .flatMap(categorie -> categorie.getArticles().stream())
                .mapToInt(Article::getId)
                .max()
                .orElse(0) + 1;
    }

    public List<Categorie> getCategories() {
        return List.copyOf(categories);
    }

    public Categorie ajouterCategorie(String nom, int capacite) {
        Categorie categorie = new Categorie(prochainIdCategorie++, nom, capacite);
        categories.add(categorie);
        return categorie;
    }

    public void modifierCategorie(Categorie categorie, String nom, int capacite) {
        exigerCategorie(categorie);

        // On valide les deux champs avant de modifier quoi que ce soit.
        String nomValide = Validation.texteObligatoire(nom, "Le nom");
        int capaciteValide = Validation.entierEntre(
                capacite, 1, Categorie.CAPACITE_MAX, "La capacité");
        if (capaciteValide < categorie.getNombreArticles()) {
            throw new IllegalArgumentException(
                    "La capacité ne peut pas être inférieure au nombre d'articles ("
                            + categorie.getNombreArticles() + ").");
        }

        categorie.setNom(nomValide);
        categorie.setCapacite(capaciteValide);
    }

    /**
     * Supprime une catégorie et, par conséquent, tous ses articles.
     *
     * @return le nombre d'articles supprimés avec la catégorie.
     */
    public int supprimerCategorie(Categorie categorie) {
        exigerCategorie(categorie);
        categories.remove(categorie);
        return categorie.getNombreArticles();
    }

    public Article ajouterArticle(Categorie categorie, String libelle, double prix) {
        exigerCategorie(categorie);

        String libelleValide = Validation.texteObligatoire(libelle, "Le libellé");
        double prixValide = Validation.montantPositifOuNul(prix, "Le prix");
        if (categorie.estPleine()) {
            throw new IllegalStateException("La catégorie « " + categorie.getNom()
                    + " » est pleine (" + categorie.getCapacite() + " articles maximum).");
        }

        Article article = new Article(prochainIdArticle++, libelleValide, prixValide);
        if (!categorie.ajouterArticle(article)) {
            throw new IllegalStateException("L'article n'a pas pu être ajouté à la catégorie.");
        }
        return article;
    }

    public void modifierArticle(Article article, String libelle, double prix) {
        Objects.requireNonNull(article, "L'article est obligatoire.");

        String libelleValide = Validation.texteObligatoire(libelle, "Le libellé");
        double prixValide = Validation.montantPositifOuNul(prix, "Le prix");

        article.setLibelle(libelleValide);
        article.setPrix(prixValide);
    }

    public boolean supprimerArticle(Categorie categorie, Article article) {
        exigerCategorie(categorie);
        return categorie.retirerArticle(article);
    }

    /**
     * Déplace un article d'une catégorie vers une autre.
     *
     * @return {@code true} si le déplacement a réussi.
     */
    public boolean deplacerArticle(Article article, Categorie source, Categorie destination) {
        exigerCategorie(source);
        exigerCategorie(destination);
        Objects.requireNonNull(article, "L'article est obligatoire.");

        if (source.equals(destination) || destination.estPleine()) {
            return false;
        }
        if (!source.retirerArticle(article)) {
            return false;
        }
        if (!destination.ajouterArticle(article)) {
            source.ajouterArticle(article);
            return false;
        }
        return true;
    }

    public void sauvegarder() throws IOException {
        repository.sauvegarder(categories);
    }

    public Statistiques statistiques() {
        int nombreArticles = 0;
        double valeurTotale = 0;

        for (Categorie categorie : categories) {
            nombreArticles += categorie.getNombreArticles();
            valeurTotale += categorie.getValeurTotale();
        }

        return new Statistiques(categories.size(), nombreArticles, valeurTotale);
    }

    public String getCheminDonnees() {
        return repository.getChemin().toString();
    }

    private void exigerCategorie(Categorie categorie) {
        Objects.requireNonNull(categorie, "La catégorie est obligatoire.");
        if (!categories.contains(categorie)) {
            throw new IllegalArgumentException("Cette catégorie n'existe pas dans le magasin.");
        }
    }
}
