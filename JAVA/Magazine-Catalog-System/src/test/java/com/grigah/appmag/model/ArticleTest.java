package com.grigah.appmag.model;

import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNotEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;

class ArticleTest {

    @Test
    void lePrixEstArrondiAuCentime() {
        Article article = new Article(1, "Clavier", 19.999);
        assertEquals(20.0, article.getPrix());
    }

    @Test
    void lesChampsSontValides() {
        assertThrows(IllegalArgumentException.class, () -> new Article(0, "Clavier", 10));
        assertThrows(IllegalArgumentException.class, () -> new Article(1, " ", 10));
        assertThrows(IllegalArgumentException.class, () -> new Article(1, "Clavier", -1));
        assertThrows(IllegalArgumentException.class,
                () -> new Article(1, "Clavier", Double.NaN));
    }

    @Test
    void leLibelleEtLePrixSontModifiables() {
        Article article = new Article(1, "Clavier", 10);
        article.setLibelle("Clavier mécanique");
        article.setPrix(89.5);

        assertEquals("Clavier mécanique", article.getLibelle());
        assertEquals(89.5, article.getPrix());
    }

    @Test
    void lEgaliteEstBaseeSurLIdentifiant() {
        Article premier = new Article(7, "Souris", 25);
        Article second = new Article(7, "Souris sans fil", 39);
        Article troisieme = new Article(8, "Souris", 25);

        assertEquals(premier, second);
        assertEquals(premier.hashCode(), second.hashCode());
        assertNotEquals(premier, troisieme);
    }
}
