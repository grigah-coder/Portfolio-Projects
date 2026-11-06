package com.grigah.appgbib.model;

import java.util.Objects;

/**
 * Classe de base de toutes les personnes enregistrées dans la bibliothèque.
 *
 * <p>L'identifiant est attribué une seule fois par le service et ne change plus.
 * Les champs textuels sont validés à chaque affectation.</p>
 */
public abstract class Personne implements Comparable<Personne> {

    private final int id;
    private String nom;
    private String prenom;

    protected Personne(int id, String nom, String prenom) {
        this.id = Validation.entierPositif(id, "L'identifiant");
        setNom(nom);
        setPrenom(prenom);
    }

    public final int getId() {
        return id;
    }

    public final String getNom() {
        return nom;
    }

    public final void setNom(String nom) {
        this.nom = Validation.texteObligatoire(nom, "Le nom");
    }

    public final String getPrenom() {
        return prenom;
    }

    public final void setPrenom(String prenom) {
        this.prenom = Validation.texteObligatoire(prenom, "Le prénom");
    }

    public final String getNomComplet() {
        return nom + " " + prenom;
    }

    /**
     * @return le type concret de la personne.
     */
    public abstract PersonneType getType();

    /**
     * @return la fonction du lecteur ou le statut de l'employé.
     */
    public abstract String getRole();

    /**
     * @return une phrase de présentation adaptée au type de personne.
     */
    public abstract String sePresenter();

    @Override
    public final boolean equals(Object autre) {
        if (this == autre) {
            return true;
        }
        if (!(autre instanceof Personne personne)) {
            return false;
        }
        return id == personne.id;
    }

    @Override
    public final int hashCode() {
        return Objects.hash(id);
    }

    @Override
    public final int compareTo(Personne autre) {
        return Integer.compare(id, autre.id);
    }

    @Override
    public String toString() {
        return "ID " + id + " — " + getNomComplet();
    }
}
