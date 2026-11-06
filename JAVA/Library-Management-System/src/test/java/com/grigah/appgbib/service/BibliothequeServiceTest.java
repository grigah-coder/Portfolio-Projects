package com.grigah.appgbib.service;

import com.grigah.appgbib.model.Lecteur;
import com.grigah.appgbib.model.Personne;
import com.grigah.appgbib.repository.PersonneRepository;
import org.junit.jupiter.api.Test;

import java.io.IOException;
import java.nio.file.Path;
import java.util.ArrayList;
import java.util.List;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertFalse;
import static org.junit.jupiter.api.Assertions.assertTrue;

class BibliothequeServiceTest {

    @Test
    void ajouteModifieSupprimeEtCalculeLesStatistiques() throws IOException {
        MemoireRepository repository = new MemoireRepository();
        BibliothequeService service = new BibliothequeService(repository);
        service.charger();

        Lecteur lecteur = service.ajouterLecteur("Grigah", "Hamza", "Étudiant");
        service.ajouterEmploye("Fadili", "Samir", "Manager");
        service.emprunter(lecteur);

        Statistiques statistiques = service.statistiques();
        assertEquals(2, statistiques.total());
        assertEquals(1, statistiques.lecteurs());
        assertEquals(1, statistiques.employes());
        assertEquals(1, statistiques.empruntsEnCours());

        service.modifier(lecteur, "Grigah", "Hamza", "Enseignant");
        assertEquals("Enseignant", lecteur.getRole());

        service.sauvegarder();
        assertFalse(repository.donnees.isEmpty());

        assertTrue(service.supprimer(lecteur));
        assertEquals(1, service.getPersonnes().size());
    }

    @Test
    void rechargeLeProchainIdentifiantDepuisLeDepot() throws IOException {
        MemoireRepository repository = new MemoireRepository();
        repository.donnees.add(new Lecteur(10, "Grigah", "Hamza", "Étudiant"));

        BibliothequeService service = new BibliothequeService(repository);
        service.charger();
        Personne nouvelle = service.ajouterEmploye("Fadili", "Samir", "Manager");

        assertEquals(11, nouvelle.getId());
    }

    private static final class MemoireRepository implements PersonneRepository {

        private List<Personne> donnees = new ArrayList<>();

        @Override
        public List<Personne> charger() {
            return new ArrayList<>(donnees);
        }

        @Override
        public void sauvegarder(List<Personne> personnes) {
            donnees = new ArrayList<>(personnes);
        }

        @Override
        public Path getChemin() {
            return Path.of("memoire");
        }
    }
}
