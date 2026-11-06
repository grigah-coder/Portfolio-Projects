package com.grigah.appgbib.model;

/**
 * Contrat commun aux personnes capables d'emprunter des ouvrages.
 */
public interface Emprunteur {

    /**
     * Tente d'enregistrer un nouvel emprunt.
     *
     * @return {@code true} si l'emprunt a été accepté, {@code false} si la limite est atteinte.
     */
    boolean emprunter();

    /**
     * Tente d'enregistrer un retour.
     *
     * @return {@code true} si un emprunt a été rendu, {@code false} si aucun emprunt n'est en cours.
     */
    boolean retourner();

    /**
     * @return le nombre d'emprunts actuellement en cours.
     */
    int getEmpruntsEnCours();
}
