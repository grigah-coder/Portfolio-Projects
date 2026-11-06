package com.grigah.appmag.service;

import java.math.BigDecimal;
import java.math.RoundingMode;

/**
 * Indicateurs affichés dans l'interface.
 */
public record Statistiques(int categories, int articles, double valeurTotale) {

    /**
     * @return la valeur totale arrondie au centime.
     */
    public double valeurArrondie() {
        return BigDecimal.valueOf(valeurTotale)
                .setScale(2, RoundingMode.HALF_UP)
                .doubleValue();
    }
}
