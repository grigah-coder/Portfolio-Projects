package com.grigah.appmag.model;

import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertFalse;
import static org.junit.jupiter.api.Assertions.assertNull;
import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.junit.jupiter.api.Assertions.assertTrue;

class CategorieTest {

    @Test
    void laCapaciteLimiteLeNombreDArticles() {
        Categorie categorie = new Categorie(1, "Informatique", 2);
        Article premier = new Article(1, "Clavier", 50);
        Article second = new Article(2, "Souris", 25);
        Article troisieme = new Article(3, "Écran", 200);

        assertTrue(categorie.ajouterArticle(premier));
        assertTrue(categorie.ajouterArticle(second));
        assertFalse(categorie.ajouterArticle(troisieme));
        assertTrue(categorie.estPleine());
        assertEquals(2, categorie.getNombreArticles());
    }

    @Test
    void unArticleEnDoubleEstRefuse() {
        Categorie categorie = new Categorie(1, "Informatique", 5);
        Article article = new Article(1, "Clavier", 50);

        assertTrue(categorie.ajouterArticle(article));
        assertFalse(categorie.ajouterArticle(article));
        assertEquals(1, categorie.getNombreArticles());
    }

    @Test
    void calculeLaValeurLePrixMoyenEtLArticleLePlusCher() {
        Categorie categorie = new Categorie(1, "Informatique", 5);
        categorie.ajouterArticle(new Article(1, "Clavier", 100));
        categorie.ajouterArticle(new Article(2, "Souris", 50));

        assertEquals(150, categorie.getValeurTotale());
        assertEquals(75, categorie.getPrixMoyen());
        assertEquals("Clavier", categorie.getArticleLePlusCher().getLibelle());
    }

    @Test
    void uneCategorieVideNAdmetNiPrixMoyenNiArticleLePlusCher() {
        Categorie categorie = new Categorie(1, "Vide", 5);

        assertEquals(0, categorie.getPrixMoyen());
        assertNull(categorie.getArticleLePlusCher());
    }

    @Test
    void laCapaciteNePeutPasDescendreSousLeNombreDArticles() {
        Categorie categorie = new Categorie(1, "Informatique", 5);
        categorie.ajouterArticle(new Article(1, "Clavier", 100));
        categorie.ajouterArticle(new Article(2, "Souris", 50));

        assertThrows(IllegalArgumentException.class, () -> categorie.setCapacite(1));
        categorie.setCapacite(2);
        assertEquals(2, categorie.getCapacite());
    }

    @Test
    void unArticlePeutEtreRetire() {
        Categorie categorie = new Categorie(1, "Informatique", 5);
        Article article = new Article(1, "Clavier", 100);

        categorie.ajouterArticle(article);
        assertTrue(categorie.retirerArticle(article));
        assertFalse(categorie.retirerArticle(article));
        assertTrue(categorie.getArticles().isEmpty());
    }
}
