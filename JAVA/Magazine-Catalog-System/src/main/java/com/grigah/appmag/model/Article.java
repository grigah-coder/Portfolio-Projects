package com.grigah.appmag.model;

import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.Objects;

/**
 * Article vendu par le magasin.
 *
 * <p>L'identifiant est immuable : il est attribué par le service et ne change plus.
 * Le prix est arrondi au centime à chaque affectation.</p>
 */
public final class Article implements Comparable<Article> {

    private final int id;
    private String libelle;
    private double prix;

    public Article(int id, String libelle, double prix) {
        this.id = Validation.entierPositif(id, "L'identifiant");
        setLibelle(libelle);
        setPrix(prix);
    }

    public int getId() {
        return id;
    }

    public String getLibelle() {
        return libelle;
    }

    public void setLibelle(String libelle) {
        this.libelle = Validation.texteObligatoire(libelle, "Le libellé");
    }

    public double getPrix() {
        return prix;
    }

    public void setPrix(double prix) {
        double valide = Validation.montantPositifOuNul(prix, "Le prix");
        this.prix = BigDecimal.valueOf(valide)
                .setScale(2, RoundingMode.HALF_UP)
                .doubleValue();
    }

    @Override
    public int compareTo(Article autre) {
        return Integer.compare(id, autre.id);
    }

    @Override
    public boolean equals(Object autre) {
        if (this == autre) {
            return true;
        }
        if (!(autre instanceof Article article)) {
            return false;
        }
        return id == article.id;
    }

    @Override
    public int hashCode() {
        return Objects.hash(id);
    }

    @Override
    public String toString() {
        return libelle + " (réf. " + id + ")";
    }
}
