package com.grigah.appgbib.service;

import com.grigah.appgbib.model.Employe;
import com.grigah.appgbib.model.Lecteur;
import com.grigah.appgbib.model.Personne;
import com.grigah.appgbib.model.Validation;
import com.grigah.appgbib.repository.PersonneRepository;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;

/**
 * Orchestre les cas d'usage de la bibliothèque et conserve les données en mémoire
 * jusqu'à la prochaine sauvegarde.
 */
public final class BibliothequeService {

    private final PersonneRepository repository;
    private final List<Personne> personnes = new ArrayList<>();
    private int prochainId = 1;

    public BibliothequeService(PersonneRepository repository) {
        this.repository = Objects.requireNonNull(repository, "Le dépôt est obligatoire.");
    }

    /**
     * Charge les personnes depuis le dépôt et calcule le prochain identifiant.
     */
    public void charger() throws IOException {
        List<Personne> chargees = repository.charger();
        personnes.clear();
        personnes.addAll(chargees);
        prochainId = personnes.stream()
                .mapToInt(Personne::getId)
                .max()
                .orElse(0) + 1;
    }

    public List<Personne> getPersonnes() {
        return List.copyOf(personnes);
    }

    public Lecteur ajouterLecteur(String nom, String prenom, String fonction) {
        Lecteur lecteur = new Lecteur(prochainId++, nom, prenom, fonction);
        personnes.add(lecteur);
        return lecteur;
    }

    public Employe ajouterEmploye(String nom, String prenom, String statut) {
        Employe employe = new Employe(prochainId++, nom, prenom, statut);
        personnes.add(employe);
        return employe;
    }

    public void modifier(Personne personne, String nom, String prenom, String role) {
        Objects.requireNonNull(personne, "La personne est obligatoire.");
        if (!personnes.contains(personne)) {
            throw new IllegalArgumentException("Cette personne n'existe pas dans la bibliothèque.");
        }

        // On valide les trois champs avant de modifier quoi que ce soit.
        String nomValide = Validation.texteObligatoire(nom, "Le nom");
        String prenomValide = Validation.texteObligatoire(prenom, "Le prénom");
        String roleValide = Validation.texteObligatoire(role, "Le rôle");

        personne.setNom(nomValide);
        personne.setPrenom(prenomValide);
        if (personne instanceof Lecteur lecteur) {
            lecteur.setFonction(roleValide);
        } else if (personne instanceof Employe employe) {
            employe.setStatut(roleValide);
        } else {
            throw new IllegalArgumentException("Type de personne non pris en charge.");
        }
    }

    public boolean supprimer(Personne personne) {
        return personnes.remove(personne);
    }

    public boolean emprunter(Personne personne) {
        return personne instanceof Lecteur lecteur && lecteur.emprunter();
    }

    public boolean retourner(Personne personne) {
        return personne instanceof Lecteur lecteur && lecteur.retourner();
    }

    public void sauvegarder() throws IOException {
        repository.sauvegarder(personnes);
    }

    public Statistiques statistiques() {
        int lecteurs = 0;
        int employes = 0;
        int emprunts = 0;

        for (Personne personne : personnes) {
            if (personne instanceof Lecteur lecteur) {
                lecteurs++;
                emprunts += lecteur.getEmpruntsEnCours();
            } else if (personne instanceof Employe) {
                employes++;
            }
        }

        return new Statistiques(personnes.size(), lecteurs, employes, emprunts);
    }

    public String getCheminDonnees() {
        return repository.getChemin().toString();
    }
}
