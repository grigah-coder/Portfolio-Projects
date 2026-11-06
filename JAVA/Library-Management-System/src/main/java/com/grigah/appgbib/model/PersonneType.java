package com.grigah.appgbib.model;

/**
 * Types de personnes gérées par l'application.
 */
public enum PersonneType {
    LECTEUR("Lecteur"),
    EMPLOYE("Employé");

    private final String libelle;

    PersonneType(String libelle) {
        this.libelle = libelle;
    }

    public String getLibelle() {
        return libelle;
    }

    @Override
    public String toString() {
        return libelle;
    }
}
