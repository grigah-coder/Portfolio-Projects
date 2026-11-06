package com.grigah.appgbib.repository;

import com.grigah.appgbib.model.Employe;
import com.grigah.appgbib.model.Lecteur;
import com.grigah.appgbib.model.Personne;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.io.TempDir;

import java.io.IOException;
import java.nio.file.Path;
import java.util.List;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertInstanceOf;
import static org.junit.jupiter.api.Assertions.assertTrue;

class CsvPersonneRepositoryTest {

    @TempDir
    Path dossierTemporaire;

    @Test
    void sauvegardeEtRechargeLesPersonnes() throws IOException {
        Path fichier = dossierTemporaire.resolve("personnes.csv");
        CsvPersonneRepository repository = new CsvPersonneRepository(fichier);

        Lecteur lecteur = new Lecteur(1, "Grigah", "Hamza", "Étudiant");
        lecteur.emprunter();
        Employe employe = new Employe(2, "Fadili", "Samir", "Manager");

        repository.sauvegarder(List.of(lecteur, employe));
        List<Personne> personnes = repository.charger();

        assertEquals(2, personnes.size());
        Lecteur lecteurCharge = assertInstanceOf(Lecteur.class, personnes.get(0));
        assertEquals(1, lecteurCharge.getEmpruntsEnCours());
        assertEquals("Grigah", lecteurCharge.getNom());
        assertInstanceOf(Employe.class, personnes.get(1));
        assertEquals("Manager", personnes.get(1).getRole());
    }

    @Test
    void echappeLesPointsVirgulesEtGuillemets() throws IOException {
        Path fichier = dossierTemporaire.resolve("personnes.csv");
        CsvPersonneRepository repository = new CsvPersonneRepository(fichier);

        Employe employe = new Employe(1, "Nom;Test", "Prénom \"Test\"", "Rôle;Spécial");
        repository.sauvegarder(List.of(employe));

        List<Personne> personnes = repository.charger();
        assertEquals(1, personnes.size());
        assertEquals("Nom;Test", personnes.get(0).getNom());
        assertEquals("Prénom \"Test\"", personnes.get(0).getPrenom());
        assertEquals("Rôle;Spécial", personnes.get(0).getRole());
    }

    @Test
    void unFichierAbsentRetourneUneListeVide() throws IOException {
        CsvPersonneRepository repository =
                new CsvPersonneRepository(dossierTemporaire.resolve("absent.csv"));
        assertTrue(repository.charger().isEmpty());
    }
}
