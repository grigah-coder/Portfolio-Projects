package com.grigah.appgbib.model;

/**
 * Petites validations partagées par le modèle.
 */
public final class Validation {

    private Validation() {
    }

    public static String texteObligatoire(String valeur, String champ) {
        if (valeur == null || valeur.isBlank()) {
            throw new IllegalArgumentException(champ + " est obligatoire.");
        }
        return valeur.trim();
    }

    public static int entierPositif(int valeur, String champ) {
        if (valeur <= 0) {
            throw new IllegalArgumentException(champ + " doit être supérieur à zéro.");
        }
        return valeur;
    }

    public static int entierEntre(int valeur, int minimum, int maximum, String champ) {
        if (valeur < minimum || valeur > maximum) {
            throw new IllegalArgumentException(
                    champ + " doit être compris entre " + minimum + " et " + maximum + ".");
        }
        return valeur;
    }
}
