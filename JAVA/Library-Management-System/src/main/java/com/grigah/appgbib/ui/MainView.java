package com.grigah.appgbib.ui;

import com.grigah.appgbib.model.Lecteur;
import com.grigah.appgbib.model.Personne;
import com.grigah.appgbib.model.PersonneType;
import com.grigah.appgbib.service.BibliothequeService;
import com.grigah.appgbib.service.Statistiques;
import javafx.application.Platform;
import javafx.beans.property.ReadOnlyObjectWrapper;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.collections.transformation.FilteredList;
import javafx.collections.transformation.SortedList;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.ButtonBar;
import javafx.scene.control.ButtonType;
import javafx.scene.control.Label;
import javafx.scene.control.Menu;
import javafx.scene.control.MenuBar;
import javafx.scene.control.MenuItem;
import javafx.scene.control.SeparatorMenuItem;
import javafx.scene.control.TableCell;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableRow;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javafx.scene.input.KeyCode;
import javafx.scene.input.KeyCodeCombination;
import javafx.scene.input.KeyCombination;
import javafx.scene.layout.HBox;
import javafx.scene.layout.FlowPane;
import javafx.scene.layout.Priority;
import javafx.scene.layout.Region;
import javafx.scene.layout.VBox;

import java.io.IOException;
import java.text.Normalizer;
import java.util.List;
import java.util.Locale;
import java.util.Objects;

/**
 * Vue principale de l'application : tableau, formulaire, recherche et actions.
 */
public final class MainView extends VBox {

    private final BibliothequeService service;
    private final ObservableList<Personne> personnes = FXCollections.observableArrayList();
    private final FilteredList<Personne> personnesFiltrees = new FilteredList<>(personnes, personne -> true);
    private final SortedList<Personne> personnesTriees = new SortedList<>(personnesFiltrees);
    private final TableView<Personne> table;
    private final TextField recherche = new TextField();
    private final Label statutLabel = new Label("Prêt.");
    private final Label statistiquesLabel = new Label();

    private final Button btnAjouter = new Button("Ajouter");
    private final Button btnModifier = new Button("Modifier");
    private final Button btnSupprimer = new Button("Supprimer");
    private final Button btnEmprunter = new Button("Emprunter");
    private final Button btnRetourner = new Button("Retourner");
    private final Button btnEnregistrer = new Button("Enregistrer");

    public MainView(BibliothequeService service) {
        this.service = Objects.requireNonNull(service, "Le service est obligatoire.");
        this.table = creerTable();

        personnesTriees.comparatorProperty().bind(table.comparatorProperty());
        table.setItems(personnesTriees);

        styliserBoutons();
        getChildren().addAll(creerEntete(), creerCentre(), creerPied());
        VBox.setVgrow(getChildren().get(1), Priority.ALWAYS);

        configurerRecherche();
        configurerActions();
        mettreAJourBoutons();
        mettreAJourStatistiques();
    }

    /**
     * Charge les données depuis le dépôt et rafraîchit l'interface.
     */
    public void chargerDonnees() {
        try {
            service.charger();
            rafraichir();
            statut("Données chargées depuis " + service.getCheminDonnees());
        } catch (IOException exception) {
            rafraichir();
            alert(Alert.AlertType.ERROR, "Chargement impossible", exception.getMessage());
            statut("Erreur de chargement. Les données actuellement en mémoire sont conservées.");
        }
    }

    private TableView<Personne> creerTable() {
        TableView<Personne> tableView = new TableView<>();
        tableView.getStyleClass().add("table-personnes");
        tableView.setColumnResizePolicy(TableView.CONSTRAINED_RESIZE_POLICY_FLEX_LAST_COLUMN);
        tableView.setPlaceholder(new Label(
                "Aucune personne enregistrée. Cliquez sur « Ajouter » pour commencer."));

        TableColumn<Personne, Integer> colonneId = new TableColumn<>("ID");
        colonneId.setCellValueFactory(cellule ->
                new ReadOnlyObjectWrapper<>(cellule.getValue().getId()));
        colonneId.setPrefWidth(60);
        colonneId.setMaxWidth(80);

        TableColumn<Personne, String> colonneNom = new TableColumn<>("Nom");
        colonneNom.setCellValueFactory(cellule ->
                new ReadOnlyObjectWrapper<>(cellule.getValue().getNom()));
        colonneNom.setPrefWidth(150);

        TableColumn<Personne, String> colonnePrenom = new TableColumn<>("Prénom");
        colonnePrenom.setCellValueFactory(cellule ->
                new ReadOnlyObjectWrapper<>(cellule.getValue().getPrenom()));
        colonnePrenom.setPrefWidth(150);

        TableColumn<Personne, PersonneType> colonneType = new TableColumn<>("Type");
        colonneType.setCellValueFactory(cellule ->
                new ReadOnlyObjectWrapper<>(cellule.getValue().getType()));
        colonneType.setPrefWidth(110);

        TableColumn<Personne, String> colonneRole = new TableColumn<>("Rôle");
        colonneRole.setCellValueFactory(cellule ->
                new ReadOnlyObjectWrapper<>(cellule.getValue().getRole()));
        colonneRole.setPrefWidth(150);

        TableColumn<Personne, Integer> colonneEmprunts = new TableColumn<>("Emprunts");
        colonneEmprunts.setCellValueFactory(cellule -> new ReadOnlyObjectWrapper<>(
                cellule.getValue() instanceof Lecteur lecteur
                        ? lecteur.getEmpruntsEnCours()
                        : null));
        colonneEmprunts.setPrefWidth(90);
        colonneEmprunts.setCellFactory(colonne -> new TableCell<>() {
            @Override
            protected void updateItem(Integer valeur, boolean vide) {
                super.updateItem(valeur, vide);
                if (vide) {
                    setText(null);
                } else {
                    setText(valeur == null ? "—" : valeur.toString());
                }
            }
        });

        TableColumn<Personne, String> colonnePresentation = new TableColumn<>("Présentation");
        colonnePresentation.setCellValueFactory(cellule ->
                new ReadOnlyObjectWrapper<>(cellule.getValue().sePresenter()));
        colonnePresentation.setPrefWidth(360);

        tableView.getColumns().setAll(List.of(
                colonneId,
                colonneNom,
                colonnePrenom,
                colonneType,
                colonneRole,
                colonneEmprunts,
                colonnePresentation));

        tableView.getSelectionModel().selectedItemProperty().addListener(
                (observable, ancienne, selection) -> mettreAJourBoutons());

        tableView.setRowFactory(vue -> {
            TableRow<Personne> ligne = new TableRow<>();
            ligne.setOnMouseClicked(evenement -> {
                if (evenement.getClickCount() == 2 && !ligne.isEmpty()) {
                    modifier();
                }
            });
            return ligne;
        });

        return tableView;
    }

    private VBox creerEntete() {
        Label titre = new Label("AppGBib");
        titre.getStyleClass().add("app-title");
        Label sousTitre = new Label("Gestion des lecteurs, des employés et des emprunts");
        sousTitre.getStyleClass().add("app-subtitle");

        VBox titres = new VBox(2, titre, sousTitre);
        Region espace = new Region();
        HBox.setHgrow(espace, Priority.ALWAYS);
        statistiquesLabel.getStyleClass().add("stats-chip");

        HBox entete = new HBox(16, titres, espace, statistiquesLabel);
        entete.setAlignment(Pos.CENTER_LEFT);
        entete.getStyleClass().add("header");

        return new VBox(creerMenu(), entete, creerBarreOutils());
    }

    private MenuBar creerMenu() {
        Menu fichier = new Menu("Fichier");
        MenuItem enregistrer = new MenuItem("Enregistrer");
        enregistrer.setAccelerator(new KeyCodeCombination(KeyCode.S, KeyCombination.CONTROL_DOWN));
        enregistrer.setOnAction(evenement -> enregistrer());
        MenuItem recharger = new MenuItem("Recharger depuis le disque");
        recharger.setOnAction(evenement -> recharger());
        MenuItem quitter = new MenuItem("Quitter");
        quitter.setAccelerator(new KeyCodeCombination(KeyCode.Q, KeyCombination.CONTROL_DOWN));
        quitter.setOnAction(evenement -> Platform.exit());
        fichier.getItems().addAll(enregistrer, recharger, new SeparatorMenuItem(), quitter);

        Menu edition = new Menu("Édition");
        MenuItem ajouter = new MenuItem("Ajouter une personne");
        ajouter.setAccelerator(new KeyCodeCombination(KeyCode.N, KeyCombination.CONTROL_DOWN));
        ajouter.setOnAction(evenement -> ajouter());
        MenuItem modifier = new MenuItem("Modifier la sélection");
        modifier.setOnAction(evenement -> modifier());
        MenuItem supprimer = new MenuItem("Supprimer la sélection");
        supprimer.setAccelerator(new KeyCodeCombination(KeyCode.DELETE));
        supprimer.setOnAction(evenement -> supprimer());
        edition.getItems().addAll(ajouter, modifier, new SeparatorMenuItem(), supprimer);

        Menu aide = new Menu("Aide");
        MenuItem apropos = new MenuItem("À propos");
        apropos.setOnAction(evenement -> apropos());
        aide.getItems().add(apropos);

        return new MenuBar(fichier, edition, aide);
    }

    private VBox creerBarreOutils() {
        recherche.setPromptText("Rechercher par nom, prénom, type ou rôle…");
        recherche.setPrefWidth(320);
        recherche.setMaxWidth(Double.MAX_VALUE);
        recherche.getStyleClass().add("champ-recherche");

        FlowPane actions = new FlowPane(8, 8,
                btnAjouter,
                btnModifier,
                btnSupprimer,
                btnEmprunter,
                btnRetourner,
                btnEnregistrer);
        actions.setAlignment(Pos.CENTER_LEFT);

        HBox rechercheLigne = new HBox(recherche);
        HBox.setHgrow(recherche, Priority.ALWAYS);

        VBox barre = new VBox(10, actions, rechercheLigne);
        barre.setFillWidth(true);
        barre.getStyleClass().add("toolbar");
        return barre;
    }

    private VBox creerCentre() {
        VBox carte = new VBox(table);
        carte.getStyleClass().add("card");
        VBox.setVgrow(table, Priority.ALWAYS);
        VBox.setVgrow(carte, Priority.ALWAYS);

        VBox conteneur = new VBox(carte);
        conteneur.setPadding(new Insets(18, 24, 18, 24));
        VBox.setVgrow(conteneur, Priority.ALWAYS);
        return conteneur;
    }

    private HBox creerPied() {
        Region espace = new Region();
        HBox.setHgrow(espace, Priority.ALWAYS);
        Label version = new Label("AppGBib 2.0");

        HBox pied = new HBox(12, statutLabel, espace, version);
        pied.setAlignment(Pos.CENTER_LEFT);
        pied.getStyleClass().add("status-bar");
        return pied;
    }

    private void styliserBoutons() {
        btnAjouter.getStyleClass().add("primary");
        btnSupprimer.getStyleClass().add("danger");
        btnEmprunter.getStyleClass().add("accent");
        btnRetourner.getStyleClass().add("accent");
    }

    private void configurerRecherche() {
        recherche.textProperty().addListener((observable, ancien, texte) -> {
            String filtre = normaliserRecherche(texte);
            personnesFiltrees.setPredicate(personne -> filtre.isEmpty()
                    || contient(personne.getNom(), filtre)
                    || contient(personne.getPrenom(), filtre)
                    || contient(personne.getType().getLibelle(), filtre)
                    || contient(personne.getRole(), filtre));
        });
    }

    private static boolean contient(String valeur, String filtre) {
        return valeur != null && normaliserRecherche(valeur).contains(filtre);
    }

    private static String normaliserRecherche(String valeur) {
        if (valeur == null || valeur.isBlank()) {
            return "";
        }
        String sansAccents = Normalizer.normalize(valeur.trim(), Normalizer.Form.NFD)
                .replaceAll("\\p{M}+", "");
        return sansAccents.toLowerCase(Locale.ROOT);
    }

    private void configurerActions() {
        btnAjouter.setOnAction(evenement -> ajouter());
        btnModifier.setOnAction(evenement -> modifier());
        btnSupprimer.setOnAction(evenement -> supprimer());
        btnEmprunter.setOnAction(evenement -> emprunter());
        btnRetourner.setOnAction(evenement -> retourner());
        btnEnregistrer.setOnAction(evenement -> enregistrer());
    }

    private void ajouter() {
        new PersonneDialog(null).showAndWait().ifPresent(donnees -> {
            try {
                Personne personne = switch (donnees.type()) {
                    case LECTEUR -> service.ajouterLecteur(
                            donnees.nom(), donnees.prenom(), donnees.role());
                    case EMPLOYE -> service.ajouterEmploye(
                            donnees.nom(), donnees.prenom(), donnees.role());
                };
                rafraichir();
                table.getSelectionModel().select(personne);
                statut("Personne ajoutée : " + personne.getNomComplet());
            } catch (IllegalArgumentException exception) {
                alert(Alert.AlertType.WARNING, "Saisie invalide", exception.getMessage());
            }
        });
    }

    private void modifier() {
        Personne selection = table.getSelectionModel().getSelectedItem();
        if (selection == null) {
            return;
        }

        new PersonneDialog(selection).showAndWait().ifPresent(donnees -> {
            try {
                service.modifier(selection, donnees.nom(), donnees.prenom(), donnees.role());
                rafraichir();
                table.getSelectionModel().select(selection);
                statut("Personne modifiée : " + selection.getNomComplet());
            } catch (IllegalArgumentException exception) {
                alert(Alert.AlertType.WARNING, "Saisie invalide", exception.getMessage());
            }
        });
    }

    private void supprimer() {
        Personne selection = table.getSelectionModel().getSelectedItem();
        if (selection == null) {
            return;
        }

        if (confirmer(
                "Supprimer une personne",
                "Supprimer " + selection.getNomComplet() + " ?",
                "Cette action est irréversible. Pensez à enregistrer après la suppression.")) {
            service.supprimer(selection);
            rafraichir();
            statut("Personne supprimée : " + selection.getNomComplet());
        }
    }

    private void emprunter() {
        Personne selection = table.getSelectionModel().getSelectedItem();
        if (!(selection instanceof Lecteur lecteur)) {
            return;
        }

        if (service.emprunter(lecteur)) {
            rafraichir();
            table.getSelectionModel().select(lecteur);
            statut("Emprunt enregistré pour " + lecteur.getNomComplet()
                    + " (" + lecteur.getEmpruntsEnCours() + "/" + Lecteur.MAX_EMPRUNTS + ").");
        } else {
            alert(Alert.AlertType.WARNING,
                    "Limite atteinte",
                    "Un lecteur ne peut pas avoir plus de " + Lecteur.MAX_EMPRUNTS
                            + " emprunts en cours.");
        }
    }

    private void retourner() {
        Personne selection = table.getSelectionModel().getSelectedItem();
        if (!(selection instanceof Lecteur lecteur)) {
            return;
        }

        if (service.retourner(lecteur)) {
            rafraichir();
            table.getSelectionModel().select(lecteur);
            statut("Retour enregistré pour " + lecteur.getNomComplet()
                    + " (" + lecteur.getEmpruntsEnCours() + "/" + Lecteur.MAX_EMPRUNTS + ").");
        } else {
            alert(Alert.AlertType.INFORMATION,
                    "Aucun emprunt",
                    "Ce lecteur n'a aucun emprunt en cours.");
        }
    }

    private void enregistrer() {
        try {
            service.sauvegarder();
            statut("Données enregistrées dans " + service.getCheminDonnees());
        } catch (IOException exception) {
            alert(Alert.AlertType.ERROR, "Enregistrement impossible", exception.getMessage());
        }
    }

    private void recharger() {
        if (confirmer(
                "Recharger les données",
                "Recharger depuis le disque ?",
                "Les modifications non enregistrées seront perdues.")) {
            chargerDonnees();
        }
    }

    private void apropos() {
        alert(Alert.AlertType.INFORMATION,
                "À propos",
                "AppGBib 2.0\n"
                        + "Gestion de bibliothèque en JavaFX.\n\n"
                        + "Projet portfolio de Hamza Grigah.");
    }

    private void rafraichir() {
        personnes.setAll(service.getPersonnes());
        table.refresh();
        mettreAJourStatistiques();
        mettreAJourBoutons();
    }

    private void mettreAJourStatistiques() {
        Statistiques statistiques = service.statistiques();
        statistiquesLabel.setText(String.format(
                "%d personnes  ·  %d lecteurs  ·  %d employés  ·  %d emprunts",
                statistiques.total(),
                statistiques.lecteurs(),
                statistiques.employes(),
                statistiques.empruntsEnCours()));
    }

    private void mettreAJourBoutons() {
        Personne selection = table.getSelectionModel().getSelectedItem();
        boolean aSelection = selection != null;
        boolean lecteur = selection instanceof Lecteur;

        btnModifier.setDisable(!aSelection);
        btnSupprimer.setDisable(!aSelection);
        btnEmprunter.setDisable(!lecteur);
        btnRetourner.setDisable(!lecteur);
    }

    private void statut(String message) {
        statutLabel.setText(message);
    }

    private boolean confirmer(String titre, String entete, String contenu) {
        ButtonType confirmer = new ButtonType("Confirmer", ButtonBar.ButtonData.OK_DONE);
        ButtonType annuler = new ButtonType("Annuler", ButtonBar.ButtonData.CANCEL_CLOSE);

        Alert alerte = new Alert(Alert.AlertType.CONFIRMATION, contenu, confirmer, annuler);
        alerte.setTitle(titre);
        alerte.setHeaderText(entete);
        initialiserProprietaire(alerte);

        return alerte.showAndWait().filter(button -> button == confirmer).isPresent();
    }

    private void alert(Alert.AlertType type, String titre, String message) {
        Alert alerte = new Alert(type);
        alerte.setTitle(titre);
        alerte.setHeaderText(null);
        alerte.setContentText(message == null ? "" : message);
        initialiserProprietaire(alerte);
        alerte.showAndWait();
    }

    private void initialiserProprietaire(Alert alerte) {
        if (getScene() != null && getScene().getWindow() != null) {
            alerte.initOwner(getScene().getWindow());
        }
    }
}
