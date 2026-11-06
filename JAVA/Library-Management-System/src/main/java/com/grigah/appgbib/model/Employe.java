package com.grigah.appgbib.model;

/**
 * Employé de la bibliothèque (bibliothécaire, manager, etc.).
 */
public final class Employe extends Personne {

    private String statut;

    public Employe(int id, String nom, String prenom, String statut) {
        super(id, nom, prenom);
        setStatut(statut);
    }

    public String getStatut() {
        return statut;
    }

    public void setStatut(String statut) {
        this.statut = Validation.texteObligatoire(statut, "Le statut");
    }

    @Override
    public PersonneType getType() {
        return PersonneType.EMPLOYE;
    }

    @Override
    public String getRole() {
        return statut;
    }

    @Override
    public String sePresenter() {
        return "Je suis " + getNomComplet() + ", employé (" + statut + ").";
    }
}
