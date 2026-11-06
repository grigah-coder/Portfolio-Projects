package com.grigah.appgbib.model;

/**
 * Lecteur de la bibliothèque. Un lecteur peut avoir plusieurs emprunts en cours,
 * dans la limite définie par {@link #MAX_EMPRUNTS}.
 */
public final class Lecteur extends Personne implements Emprunteur {

    public static final int MAX_EMPRUNTS = 3;

    private String fonction;
    private int empruntsEnCours;

    public Lecteur(int id, String nom, String prenom, String fonction) {
        this(id, nom, prenom, fonction, 0);
    }

    public Lecteur(int id, String nom, String prenom, String fonction, int empruntsEnCours) {
        super(id, nom, prenom);
        setFonction(fonction);
        setEmpruntsEnCours(empruntsEnCours);
    }

    public String getFonction() {
        return fonction;
    }

    public void setFonction(String fonction) {
        this.fonction = Validation.texteObligatoire(fonction, "La fonction");
    }

    @Override
    public int getEmpruntsEnCours() {
        return empruntsEnCours;
    }

    public void setEmpruntsEnCours(int empruntsEnCours) {
        this.empruntsEnCours = Validation.entierEntre(
                empruntsEnCours, 0, MAX_EMPRUNTS, "Le nombre d'emprunts");
    }

    @Override
    public boolean emprunter() {
        if (empruntsEnCours >= MAX_EMPRUNTS) {
            return false;
        }
        empruntsEnCours++;
        return true;
    }

    @Override
    public boolean retourner() {
        if (empruntsEnCours == 0) {
            return false;
        }
        empruntsEnCours--;
        return true;
    }

    @Override
    public PersonneType getType() {
        return PersonneType.LECTEUR;
    }

    @Override
    public String getRole() {
        return fonction;
    }

    @Override
    public String sePresenter() {
        return "Je suis " + getNomComplet() + ", lecteur (" + fonction + ").";
    }
}
