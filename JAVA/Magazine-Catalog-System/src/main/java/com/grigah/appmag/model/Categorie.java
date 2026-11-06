package com.grigah.appmag.model;

import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.Optional;

/**
 * Catégorie d'articles.
 *
 * <p>Une catégorie possède une capacité maximale. La liste interne n'est jamais
 * exposée directement : les appelants passent par {@link #ajouterArticle(Article)}
 * et {@link #retirerArticle(Article)}, qui garantissent les invariants.</p>
 */
public final class Categorie implements Comparable<Categorie> {

    public static final int CAPACITE_MAX = 100;

    private final int id;
    private final List<Article> articles = new ArrayList<>();
    private String nom;
    private int capacite;

    public Categorie(int id, String nom, int capacite) {
        this.id = Validation.entierPositif(id, "L'identifiant");
        setNom(nom);
        setCapacite(capacite);
    }

    public int getId() {
        return id;
    }

    public String getNom() {
        return nom;
    }

    public void setNom(String nom) {
        this.nom = Validation.texteObligatoire(nom, "Le nom");
    }

    public int getCapacite() {
        return capacite;
    }

    /**
     * Modifie la capacité de la catégorie.
     *
     * @throws IllegalArgumentException si la nouvelle capacité est inférieure
     *                                  au nombre d'articles déjà présents.
     */
    public void setCapacite(int capacite) {
        int valide = Validation.entierEntre(capacite, 1, CAPACITE_MAX, "La capacité");
        if (valide < articles.size()) {
            throw new IllegalArgumentException(
                    "La capacité ne peut pas être inférieure au nombre d'articles ("
                            + articles.size() + ").");
        }
        this.capacite = valide;
    }

    public List<Article> getArticles() {
        return List.copyOf(articles);
    }

    public int getNombreArticles() {
        return articles.size();
    }

    public boolean estPleine() {
        return articles.size() >= capacite;
    }

    /**
     * Ajoute un article si la catégorie n'est pas pleine et si l'article n'y figure pas déjà.
     *
     * @return {@code true} si l'article a été ajouté.
     */
    public boolean ajouterArticle(Article article) {
        Objects.requireNonNull(article, "L'article est obligatoire.");
        if (estPleine() || articles.contains(article)) {
            return false;
        }
        return articles.add(article);
    }

    public boolean retirerArticle(Article article) {
        return articles.remove(article);
    }

    public Optional<Article> trouverArticle(int idArticle) {
        return articles.stream()
                .filter(article -> article.getId() == idArticle)
                .findFirst();
    }

    /**
     * @return la valeur cumulée des articles de la catégorie.
     */
    public double getValeurTotale() {
        return articles.stream().mapToDouble(Article::getPrix).sum();
    }

    /**
     * @return le prix moyen des articles, ou {@code 0} si la catégorie est vide.
     */
    public double getPrixMoyen() {
        return articles.isEmpty() ? 0 : getValeurTotale() / articles.size();
    }

    /**
     * @return l'article le plus cher, ou {@code null} si la catégorie est vide.
     */
    public Article getArticleLePlusCher() {
        return articles.stream()
                .max((premier, second) -> Double.compare(premier.getPrix(), second.getPrix()))
                .orElse(null);
    }

    @Override
    public int compareTo(Categorie autre) {
        return nom.compareToIgnoreCase(autre.nom);
    }

    @Override
    public boolean equals(Object autre) {
        if (this == autre) {
            return true;
        }
        if (!(autre instanceof Categorie categorie)) {
            return false;
        }
        return id == categorie.id;
    }

    @Override
    public int hashCode() {
        return Objects.hash(id);
    }

    @Override
    public String toString() {
        return nom;
    }
}
