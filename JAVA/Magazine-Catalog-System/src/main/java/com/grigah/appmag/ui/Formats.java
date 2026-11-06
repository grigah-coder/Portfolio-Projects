package com.grigah.appmag.ui;

import java.text.NumberFormat;
import java.util.Locale;

/**
 * Formats partagés par l'interface (prix et nombres en français).
 */
public final class Formats {

    private static final Locale FRANCAIS = Locale.FRANCE;

    private Formats() {
    }

    public static String prix(double montant) {
        return NumberFormat.getCurrencyInstance(FRANCAIS).format(montant);
    }

    public static String nombre(int valeur) {
        return NumberFormat.getIntegerInstance(FRANCAIS).format(valeur);
    }
}
