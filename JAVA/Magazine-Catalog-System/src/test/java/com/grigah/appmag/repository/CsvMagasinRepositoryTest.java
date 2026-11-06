package com.grigah.appmag.repository;

import com.grigah.appmag.model.Article;
import com.grigah.appmag.model.Categorie;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.io.TempDir;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.List;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.junit.jupiter.api.Assertions.assertTrue;

class CsvMagasinRepositoryTest {

    @TempDir
    Path dossierTemporaire;

    @Test
    void sauvegardeEtRechargeLesCategoriesEtLeursArticles() throws IOException {
        Path fichier = dossierTemporaire.resolve("magasin.csv");
        CsvMagasinRepository repository = new CsvMagasinRepository(fichier);

        Categorie informatique = new Categorie(1, "Informatique", 5);
        informatique.ajouterArticle(new Article(1, "Clavier", 49.99));
        informatique.ajouterArticle(new Article(2, "Souris", 24.5));
        Categorie livres = new Categorie(2, "Livres", 10);

        repository.sauvegarder(List.of(informatique, livres));

        List<Categorie> categories = repository.charger();
        assertEquals(2, categories.size());

        Categorie rechargee = categories.get(0);
        assertEquals("Informatique", rechargee.getNom());
        assertEquals(5, rechargee.getCapacite());
        assertEquals(2, rechargee.getNombreArticles());
        assertEquals("Clavier", rechargee.getArticles().get(0).getLibelle());
        assertEquals(49.99, rechargee.getArticles().get(0).getPrix());

        assertTrue(categories.get(1).getArticles().isEmpty());
        assertEquals(10, categories.get(1).getCapacite());
    }

    @Test
    void echappeLesPointsVirgulesEtGuillemets() throws IOException {
        Path fichier = dossierTemporaire.resolve("magasin.csv");
        CsvMagasinRepository repository = new CsvMagasinRepository(fichier);

        Categorie categorie = new Categorie(1, "Info;Gaming", 5);
        categorie.ajouterArticle(new Article(1, "Souris \"pro\"", 39.9));
        repository.sauvegarder(List.of(categorie));

        Categorie rechargee = repository.charger().get(0);
        assertEquals("Info;Gaming", rechargee.getNom());
        assertEquals("Souris \"pro\"", rechargee.getArticles().get(0).getLibelle());
    }

    @Test
    void unFichierAbsentRetourneUneListeVide() throws IOException {
        CsvMagasinRepository repository =
                new CsvMagasinRepository(dossierTemporaire.resolve("absent.csv"));
        assertTrue(repository.charger().isEmpty());
    }

    @Test
    void unArticleOrphelinEstSignale() throws IOException {
        Path fichier = dossierTemporaire.resolve("orphelin.csv");
        Files.writeString(fichier, """
                type;id;nom;capacite
                ART;9;1;Clavier;49.99
                """);

        CsvMagasinRepository repository = new CsvMagasinRepository(fichier);
        assertThrows(IOException.class, repository::charger);
    }
}
