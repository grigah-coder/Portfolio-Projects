package com.grigah.appmag.service;

import com.grigah.appmag.model.Article;
import com.grigah.appmag.model.Categorie;
import com.grigah.appmag.repository.MagasinRepository;
import org.junit.jupiter.api.Test;

import java.io.IOException;
import java.nio.file.Path;
import java.util.ArrayList;
import java.util.List;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertFalse;
import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.junit.jupiter.api.Assertions.assertTrue;

class MagasinServiceTest {

    @Test
    void ajouteModifieSupprimeEtCalculeLesStatistiques() throws IOException {
        MemoireRepository repository = new MemoireRepository();
        MagasinService service = new MagasinService(repository);
        service.charger();

        Categorie informatique = service.ajouterCategorie("Informatique", 3);
        Categorie livres = service.ajouterCategorie("Livres", 5);
        Article clavier = service.ajouterArticle(informatique, "Clavier", 50);
        service.ajouterArticle(informatique, "Souris", 25);
        service.ajouterArticle(livres, "Roman", 15);

        Statistiques statistiques = service.statistiques();
        assertEquals(2, statistiques.categories());
        assertEquals(3, statistiques.articles());
        assertEquals(90, statistiques.valeurTotale());

        service.modifierArticle(clavier, "Clavier mécanique", 80);
        assertEquals("Clavier mécanique", clavier.getLibelle());
        assertEquals(80, clavier.getPrix());

        service.modifierCategorie(informatique, "Matériel", 4);
        assertEquals("Matériel", informatique.getNom());
        assertEquals(4, informatique.getCapacite());

        service.sauvegarder();
        assertFalse(repository.donnees.isEmpty());

        assertTrue(service.supprimerArticle(informatique, clavier));
        assertEquals(2, service.statistiques().articles());
    }

    @Test
    void refuseDépasserLaCapaciteDuneCategorie() {
        MagasinService service = new MagasinService(new MemoireRepository());
        Categorie categorie = service.ajouterCategorie("Informatique", 1);
        service.ajouterArticle(categorie, "Clavier", 50);

        assertThrows(IllegalStateException.class,
                () -> service.ajouterArticle(categorie, "Souris", 25));
    }

    @Test
    void refuseDeDiminuerLaCapaciteSousLeNombreDArticles() {
        MagasinService service = new MagasinService(new MemoireRepository());
        Categorie categorie = service.ajouterCategorie("Informatique", 3);
        service.ajouterArticle(categorie, "Clavier", 50);
        service.ajouterArticle(categorie, "Souris", 25);

        assertThrows(IllegalArgumentException.class,
                () -> service.modifierCategorie(categorie, "Informatique", 1));
    }

    @Test
    void supprimeUneCategorieEtSesArticles() {
        MagasinService service = new MagasinService(new MemoireRepository());
        Categorie categorie = service.ajouterCategorie("Informatique", 3);
        service.ajouterArticle(categorie, "Clavier", 50);
        service.ajouterArticle(categorie, "Souris", 25);

        assertEquals(2, service.supprimerCategorie(categorie));
        assertTrue(service.getCategories().isEmpty());
    }

    @Test
    void deplaceUnArticleVersUneAutreCategorie() {
        MagasinService service = new MagasinService(new MemoireRepository());
        Categorie source = service.ajouterCategorie("Informatique", 3);
        Categorie destination = service.ajouterCategorie("Promotions", 3);
        Article article = service.ajouterArticle(source, "Clavier", 50);

        assertTrue(service.deplacerArticle(article, source, destination));
        assertTrue(source.getArticles().isEmpty());
        assertEquals(1, destination.getNombreArticles());

        Categorie pleine = service.ajouterCategorie("Pleine", 1);
        service.ajouterArticle(pleine, "Souris", 25);
        assertFalse(service.deplacerArticle(article, destination, pleine));
        assertEquals(1, destination.getNombreArticles());
    }

    @Test
    void rechargeLesProchainsIdentifiantsDepuisLeDepot() throws IOException {
        MemoireRepository repository = new MemoireRepository();
        Categorie existante = new Categorie(10, "Informatique", 5);
        existante.ajouterArticle(new Article(42, "Clavier", 50));
        repository.donnees.add(existante);

        MagasinService service = new MagasinService(repository);
        service.charger();

        Categorie nouvelle = service.ajouterCategorie("Livres", 5);
        Article article = service.ajouterArticle(nouvelle, "Roman", 15);

        assertEquals(11, nouvelle.getId());
        assertEquals(43, article.getId());
    }

    private static final class MemoireRepository implements MagasinRepository {

        private List<Categorie> donnees = new ArrayList<>();

        @Override
        public List<Categorie> charger() {
            return new ArrayList<>(donnees);
        }

        @Override
        public void sauvegarder(List<Categorie> categories) {
            donnees = new ArrayList<>(categories);
        }

        @Override
        public Path getChemin() {
            return Path.of("memoire");
        }
    }
}
