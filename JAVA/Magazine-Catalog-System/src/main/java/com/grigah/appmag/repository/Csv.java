package com.grigah.appmag.repository;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

/**
 * Utilitaires CSV minimalistes (séparateur « ; », échappement par guillemets).
 */
final class Csv {

    private Csv() {
    }

    static String echapper(String valeur) {
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

    static List<String> analyser(String ligne, int numeroLigne) throws IOException {
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
