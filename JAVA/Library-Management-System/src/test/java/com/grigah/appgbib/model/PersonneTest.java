package com.grigah.appgbib.model;

import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertFalse;
import static org.junit.jupiter.api.Assertions.assertNotEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.junit.jupiter.api.Assertions.assertTrue;

class PersonneTest {

    @Test
    void unLecteurRespecteLaLimiteDEmprunts() {
        Lecteur lecteur = new Lecteur(1, "Grigah", "Hamza", "Étudiant");

        assertTrue(lecteur.emprunter());
        assertTrue(lecteur.emprunter());
        assertTrue(lecteur.emprunter());
        assertFalse(lecteur.emprunter());
        assertEquals(Lecteur.MAX_EMPRUNTS, lecteur.getEmpruntsEnCours());

        assertTrue(lecteur.retourner());
        assertEquals(2, lecteur.getEmpruntsEnCours());
    }

    @Test
    void unRetourSansEmpruntEstRefuse() {
        Lecteur lecteur = new Lecteur(1, "Grigah", "Hamza", "Étudiant");
        assertFalse(lecteur.retourner());
    }

    @Test
    void lesChampsObligatoiresSontValides() {
        assertThrows(IllegalArgumentException.class,
                () -> new Employe(1, " ", "Hamza", "Manager"));
        assertThrows(IllegalArgumentException.class,
                () -> new Employe(1, "Grigah", "", "Manager"));
        assertThrows(IllegalArgumentException.class,
                () -> new Employe(1, "Grigah", "Hamza", " "));
    }

    @Test
    void lEgaliteEstBaseeSurLIdentifiant() {
        Personne premier = new Lecteur(7, "Grigah", "Hamza", "Étudiant");
        Personne second = new Employe(7, "Autre", "Personne", "Manager");
        Personne troisieme = new Employe(8, "Autre", "Personne", "Manager");

        assertEquals(premier, second);
        assertEquals(premier.hashCode(), second.hashCode());
        assertNotEquals(premier, troisieme);
    }

    @Test
    void laPresentationEstAdapteeAuType() {
        Personne lecteur = new Lecteur(1, "Grigah", "Hamza", "Étudiant");
        Personne employe = new Employe(2, "Fadili", "Samir", "Manager");

        assertTrue(lecteur.sePresenter().contains("lecteur"));
        assertTrue(employe.sePresenter().contains("employé"));
    }
}
