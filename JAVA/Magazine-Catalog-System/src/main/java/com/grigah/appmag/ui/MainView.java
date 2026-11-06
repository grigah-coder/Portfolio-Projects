package com.grigah.appmag.ui;

import com.grigah.appmag.model.Article;
import com.grigah.appmag.model.Categorie;
import com.grigah.appmag.service.MagasinService;
import com.grigah.appmag.service.Statistiques;
import javafx.application.Platform;
import javafx.beans.property.ReadOnlyObjectWrapper;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.ButtonBar;
import javafx.scene.control.ButtonType;
import javafx.scene.control.ChoiceDialog;
import javafx.scene.control.Label;
import javafx.scene.control.ListCell;
import javafx.scene.control.ListView;
import javafx.scene.control.Menu;
import javafx.scene.control.MenuBar;
import javafx.scene.control.MenuItem;
import javafx.scene.control.SeparatorMenuItem;
import javafx.scene.control.SplitPane;
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
import java.util.Comparator;
import java.util.List;
import java.util.Locale;
import java.util.Objects;
import java.util.Optional;

/**
 * Vue principale du magasin : catégories à gauche, articles à droite.
 */
public final class MainView extends VBox {

    private final MagasinService service;

    private final ObservableList<Categorie> categories = FXCollections.observableArrayList();
    private final ListView<Categorie> listeCategories = new ListView<>(categories);
    private final ObservableList<Article> articles = FXCollections.observableArrayList();
    private final TableView<Article> tableArticles = new TableView<>(articles);

    private final TextField recherche = new TextField();
    private final Label titreCategorie = new Label("Articles");
    private final Label infosCategorie = new Label();
    private final Label statistiquesLabel = new Label();
    private final Label statutLabel = new Label("Prêt.");

    private final Button btnCategorieAjouter = new Button("Ajouter");
    private final Button btnCategorieModifier = new Button("Modifier");
    private final Button btnCategorieSupprimer = new Button("Supprimer");
    private final Button btnArticleAjouter = new Button("Ajouter");
    private final Button btnArticleModifier = new Button("Modifier");
    private final Button btnArticleSupprimer = new Button("Supprimer");
    private final Button btnArticleDeplacer = new Button("Déplacer");
    private final Button btnEnregistrer = new Button("Enregistrer");

    public MainView(MagasinService service) {
        this.service = Objects.requireNonNull(service, "Le service est obligatoire.");

        configurerListeCategories();
        configurerTableArticles();
        configurerRecherche();
        configurerActions();

        getChildren().addAll(
                creerMenu(),
                creerEntete(),
                creerContenu(),
                creerPied());
        VBox.setVgrow(getChildren().get(2), Priority.ALWAYS);

        mettreAJourBoutons();
        mettreAJourStatistiques();
    }

    /**
     * Charge les données depuis le dépôt et rafraîchit l'interface.
     */
    public void chargerDonnees() {
        try {
            service.charger();
            rafraichirCategories(null);
            statut("Données chargées depuis " + service.getCheminDonnees());
        } catch (IOException exception) {
            afficherArticles(categorieSelectionnee());
            alert(Alert.AlertType.ERROR, "Chargement impossible", exception.getMessage());
            statut("Erreur de chargement. Les données actuellement en mémoire sont conservées.");
        }
    }

    private void configurerListeCategories() {
        listeCategories.getStyleClass().add("liste-categories");
        listeCategories.setPlaceholder(new Label("Aucune catégorie. Cliquez sur « Ajouter »."));
        listeCategories.setCellFactory(vue -> new ListeCategorieCell());
        listeCategories.getSelectionModel().selectedItemProperty().addListener(
                (observable, ancienne, selection) -> {
                    afficherArticles(selection);
                    mettreAJourBoutons();
                });
    }

    private void configurerTableArticles() {
        tableArticles.getStyleClass().add("table-articles");
        tableArticles.setColumnResizePolicy(TableView.CONSTRAINED_RESIZE_POLICY_FLEX_LAST_COLUMN);
        tableArticles.setPlaceholder(new Label(
                "Sélectionnez une catégorie, puis ajoutez un article."));

        TableColumn<Article, Integer> colonneId = new TableColumn<>("Réf.");
        colonneId.setCellValueFactory(cellule ->
                new ReadOnlyObjectWrapper<>(cellule.getValue().getId()));
        colonneId.setPrefWidth(70);
        colonneId.setMaxWidth(110);

        TableColumn<Article, String> colonneLibelle = new TableColumn<>("Libellé");
        colonneLibelle.setCellValueFactory(cellule ->
                new ReadOnlyObjectWrapper<>(cellule.getValue().getLibelle()));
        colonneLibelle.setPrefWidth(320);

        TableColumn<Article, String> colonnePrix = new TableColumn<>("Prix");
        colonnePrix.setCellValueFactory(cellule ->
                new ReadOnlyObjectWrapper<>(Formats.prix(cellule.getValue().getPrix())));
        colonnePrix.setPrefWidth(140);

        tableArticles.getColumns().setAll(List.of(colonneId, colonneLibelle, colonnePrix));

        tableArticles.getSelectionModel().selectedItemProperty().addListener(
                (observable, ancienne, selection) -> mettreAJourBoutons());

        tableArticles.setRowFactory(vue -> {
            TableRow<Article> ligne = new TableRow<>();
            ligne.setOnMouseClicked(evenement -> {
                if (evenement.getClickCount() == 2 && !ligne.isEmpty()) {
                    modifierArticle();
                }
            });
            return ligne;
        });
    }

    private void configurerRecherche() {
        recherche.setPromptText("Rechercher un article…");
        recherche.setPrefWidth(240);
        recherche.getStyleClass().add("champ-recherche");
        recherche.textProperty().addListener(
                (observable, ancien, texte) -> afficherArticles(categorieSelectionnee()));
    }

    private void configurerActions() {
        btnCategorieAjouter.setOnAction(evenement -> ajouterCategorie());
        btnCategorieModifier.setOnAction(evenement -> modifierCategorie());
        btnCategorieSupprimer.setOnAction(evenement -> supprimerCategorie());

        btnArticleAjouter.setOnAction(evenement -> ajouterArticle());
        btnArticleModifier.setOnAction(evenement -> modifierArticle());
        btnArticleSupprimer.setOnAction(evenement -> supprimerArticle());
        btnArticleDeplacer.setOnAction(evenement -> deplacerArticle());
        btnEnregistrer.setOnAction(evenement -> enregistrer());

        btnCategorieAjouter.getStyleClass().add("primary");
        btnArticleAjouter.getStyleClass().add("primary");
        btnCategorieSupprimer.getStyleClass().add("danger");
        btnArticleSupprimer.getStyleClass().add("danger");
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
        MenuItem nouvelleCategorie = new MenuItem("Nouvelle catégorie");
        nouvelleCategorie.setAccelerator(
                new KeyCodeCombination(KeyCode.N, KeyCombination.CONTROL_DOWN));
        nouvelleCategorie.setOnAction(evenement -> ajouterCategorie());
        MenuItem nouvelArticle = new MenuItem("Nouvel article");
        nouvelArticle.setOnAction(evenement -> ajouterArticle());
        edition.getItems().addAll(nouvelleCategorie, nouvelArticle);

        Menu aide = new Menu("Aide");
        MenuItem apropos = new MenuItem("À propos");
        apropos.setOnAction(evenement -> apropos());
        aide.getItems().add(apropos);

        return new MenuBar(fichier, edition, aide);
    }

    private VBox creerEntete() {
        Label titre = new Label("AppMag");
        titre.getStyleClass().add("app-title");
        Label sousTitre = new Label("Gestion des catégories et des articles du magasin");
        sousTitre.getStyleClass().add("app-subtitle");

        VBox titres = new VBox(2, titre, sousTitre);
        Region espace = new Region();
        HBox.setHgrow(espace, Priority.ALWAYS);
        statistiquesLabel.getStyleClass().add("stats-chip");

        HBox entete = new HBox(16, titres, espace, statistiquesLabel);
        entete.setAlignment(Pos.CENTER_LEFT);
        entete.getStyleClass().add("header");

        return new VBox(entete);
    }

    private SplitPane creerContenu() {
        SplitPane contenu = new SplitPane(creerPanneauCategories(), creerPanneauArticles());
        contenu.setDividerPositions(0.32);
        contenu.getStyleClass().add("contenu");
        VBox.setVgrow(contenu, Priority.ALWAYS);
        return contenu;
    }

    private VBox creerPanneauCategories() {
        Label titre = new Label("Catégories");
        titre.getStyleClass().add("section-titre");

        Region espace = new Region();
        HBox.setHgrow(espace, Priority.ALWAYS);

        HBox entete = new HBox(8, titre, espace, btnCategorieAjouter);
        entete.setAlignment(Pos.CENTER_LEFT);

        FlowPane actions = new FlowPane(8, 8,
                btnCategorieModifier,
                btnCategorieSupprimer,
                btnEnregistrer);
        actions.setAlignment(Pos.CENTER_LEFT);

        VBox panneau = new VBox(12, entete, listeCategories, actions);
        panneau.setPadding(new Insets(18, 14, 14, 20));
        panneau.getStyleClass().add("panneau");
        VBox.setVgrow(listeCategories, Priority.ALWAYS);
        return panneau;
    }

    private VBox creerPanneauArticles() {
        titreCategorie.getStyleClass().add("section-titre");
        infosCategorie.getStyleClass().add("section-infos");

        VBox titres = new VBox(2, titreCategorie, infosCategorie);
        Region espace = new Region();
        HBox.setHgrow(espace, Priority.ALWAYS);

        HBox entete = new HBox(12, titres, espace, recherche);
        entete.setAlignment(Pos.CENTER_LEFT);

        FlowPane actions = new FlowPane(8, 8,
                btnArticleAjouter,
                btnArticleModifier,
                btnArticleSupprimer,
                btnArticleDeplacer);
        actions.setAlignment(Pos.CENTER_LEFT);

        VBox carte = new VBox(tableArticles);
        carte.getStyleClass().add("card");
        VBox.setVgrow(tableArticles, Priority.ALWAYS);
        VBox.setVgrow(carte, Priority.ALWAYS);

        VBox panneau = new VBox(12, entete, actions, carte);
        panneau.setPadding(new Insets(18, 20, 14, 14));
        panneau.getStyleClass().add("panneau");
        return panneau;
    }

    private HBox creerPied() {
        Region espace = new Region();
        HBox.setHgrow(espace, Priority.ALWAYS);
        Label version = new Label("AppMag 2.0");

        HBox pied = new HBox(12, statutLabel, espace, version);
        pied.setAlignment(Pos.CENTER_LEFT);
        pied.getStyleClass().add("status-bar");
        return pied;
    }

    private Categorie categorieSelectionnee() {
        return listeCategories.getSelectionModel().getSelectedItem();
    }

    private void rafraichirCategories(Categorie aSelectionner) {
        Categorie selection = aSelectionner != null ? aSelectionner : categorieSelectionnee();

        categories.setAll(service.getCategories().stream()
                .sorted(Comparator.naturalOrder())
                .toList());

        if (selection != null) {
            categories.stream()
                    .filter(categorie -> categorie.getId() == selection.getId())
                    .findFirst()
                    .ifPresent(categorie -> listeCategories.getSelectionModel().select(categorie));
        }

        if (listeCategories.getSelectionModel().getSelectedItem() == null) {
            afficherArticles(null);
        } else {
            afficherArticles(listeCategories.getSelectionModel().getSelectedItem());
        }

        mettreAJourStatistiques();
        mettreAJourBoutons();
    }

    private void afficherArticles(Categorie categorie) {
        String filtre = normaliserRecherche(recherche.getText());

        if (categorie == null) {
            articles.clear();
            titreCategorie.setText("Articles");
            infosCategorie.setText("Aucune catégorie sélectionnée.");
            return;
        }

        List<Article> visibles = categorie.getArticles().stream()
                .filter(article -> filtre.isEmpty()
                        || normaliserRecherche(article.getLibelle()).contains(filtre))
                .sorted(Comparator.naturalOrder())
                .toList();

        articles.setAll(visibles);
        titreCategorie.setText(categorie.getNom());
        infosCategorie.setText(String.format(
                "%d article%s sur %d · valeur %s · prix moyen %s",
                categorie.getNombreArticles(),
                categorie.getNombreArticles() > 1 ? "s" : "",
                categorie.getCapacite(),
                Formats.prix(categorie.getValeurTotale()),
                Formats.prix(categorie.getPrixMoyen())));
    }

    private static String normaliserRecherche(String valeur) {
        if (valeur == null || valeur.isBlank()) {
            return "";
        }
        String sansAccents = Normalizer.normalize(valeur.trim(), Normalizer.Form.NFD)
                .replaceAll("\\p{M}+", "");
        return sansAccents.toLowerCase(Locale.ROOT);
    }

    private void ajouterCategorie() {
        new CategorieDialog(null).showAndWait().ifPresent(donnees -> {
            try {
                Categorie categorie = service.ajouterCategorie(donnees.nom(), donnees.capacite());
                rafraichirCategories(categorie);
                statut("Catégorie ajoutée : " + categorie.getNom());
            } catch (IllegalArgumentException exception) {
                alert(Alert.AlertType.WARNING, "Saisie invalide", exception.getMessage());
            }
        });
    }

    private void modifierCategorie() {
        Categorie selection = categorieSelectionnee();
        if (selection == null) {
            return;
        }

        new CategorieDialog(selection).showAndWait().ifPresent(donnees -> {
            try {
                service.modifierCategorie(selection, donnees.nom(), donnees.capacite());
                rafraichirCategories(selection);
                statut("Catégorie modifiée : " + selection.getNom());
            } catch (IllegalArgumentException exception) {
                alert(Alert.AlertType.WARNING, "Saisie invalide", exception.getMessage());
            }
        });
    }

    private void supprimerCategorie() {
        Categorie selection = categorieSelectionnee();
        if (selection == null) {
            return;
        }

        int nombreArticles = selection.getNombreArticles();
        String details = nombreArticles == 0
                ? "Cette catégorie ne contient aucun article."
                : "Ses " + nombreArticles + " article(s) seront également supprimés.";

        if (confirmer("Supprimer une catégorie",
                "Supprimer « " + selection.getNom() + " » ?",
                details + " Pensez à enregistrer après la suppression.")) {
            int supprimes = service.supprimerCategorie(selection);
            rafraichirCategories(null);
            statut("Catégorie supprimée : " + selection.getNom()
                    + (supprimes > 0 ? " (" + supprimes + " article(s))" : ""));
        }
    }

    private void ajouterArticle() {
        Categorie categorie = categorieSelectionnee();
        if (categorie == null) {
            alert(Alert.AlertType.INFORMATION,
                    "Aucune catégorie",
                    "Sélectionnez d'abord une catégorie.");
            return;
        }
        if (categorie.estPleine()) {
            alert(Alert.AlertType.WARNING,
                    "Catégorie pleine",
                    "La catégorie « " + categorie.getNom() + " » est limitée à "
                            + categorie.getCapacite() + " articles.");
            return;
        }

        new ArticleDialog(null).showAndWait().ifPresent(donnees -> {
            try {
                Article article = service.ajouterArticle(
                        categorie, donnees.libelle(), donnees.prix());
                rafraichirCategories(categorie);
                tableArticles.getSelectionModel().select(article);
                statut("Article ajouté : " + article.getLibelle());
            } catch (IllegalArgumentException | IllegalStateException exception) {
                alert(Alert.AlertType.WARNING, "Ajout impossible", exception.getMessage());
            }
        });
    }

    private void modifierArticle() {
        Article selection = tableArticles.getSelectionModel().getSelectedItem();
        if (selection == null) {
            return;
        }

        new ArticleDialog(selection).showAndWait().ifPresent(donnees -> {
            try {
                service.modifierArticle(selection, donnees.libelle(), donnees.prix());
                rafraichirCategories(categorieSelectionnee());
                tableArticles.getSelectionModel().select(selection);
                statut("Article modifié : " + selection.getLibelle());
            } catch (IllegalArgumentException exception) {
                alert(Alert.AlertType.WARNING, "Saisie invalide", exception.getMessage());
            }
        });
    }

    private void supprimerArticle() {
        Article selection = tableArticles.getSelectionModel().getSelectedItem();
        Categorie categorie = categorieSelectionnee();
        if (selection == null || categorie == null) {
            return;
        }

        if (confirmer("Supprimer un article",
                "Supprimer « " + selection.getLibelle() + " » ?",
                "Cette action est irréversible. Pensez à enregistrer après la suppression.")) {
            service.supprimerArticle(categorie, selection);
            rafraichirCategories(categorie);
            statut("Article supprimé : " + selection.getLibelle());
        }
    }

    private void deplacerArticle() {
        Article selection = tableArticles.getSelectionModel().getSelectedItem();
        Categorie source = categorieSelectionnee();
        if (selection == null || source == null) {
            return;
        }

        List<Categorie> destinations = service.getCategories().stream()
                .filter(categorie -> !categorie.equals(source))
                .toList();

        if (destinations.isEmpty()) {
            alert(Alert.AlertType.INFORMATION,
                    "Aucune destination",
                    "Créez une autre catégorie pour déplacer cet article.");
            return;
        }

        ChoiceDialog<Categorie> dialogue = new ChoiceDialog<>(destinations.get(0), destinations);
        dialogue.setTitle("Déplacer un article");
        dialogue.setHeaderText("Déplacer « " + selection.getLibelle() + " » vers :");
        dialogue.setContentText("Catégorie :");
        initialiserProprietaire(dialogue);

        Optional<Categorie> destination = dialogue.showAndWait();
        if (destination.isEmpty()) {
            return;
        }

        if (service.deplacerArticle(selection, source, destination.get())) {
            rafraichirCategories(source);
            statut("Article déplacé vers " + destination.get().getNom() + ".");
        } else {
            alert(Alert.AlertType.WARNING,
                    "Déplacement impossible",
                    "La catégorie « " + destination.get().getNom() + " » est peut-être pleine.");
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
        if (confirmer("Recharger les données",
                "Recharger depuis le disque ?",
                "Les modifications non enregistrées seront perdues.")) {
            chargerDonnees();
        }
    }

    private void apropos() {
        alert(Alert.AlertType.INFORMATION,
                "À propos",
                "AppMag 2.0\n"
                        + "Gestion de magasin en JavaFX.\n\n"
                        + "Projet portfolio de Hamza Grigah.");
    }

    private void mettreAJourStatistiques() {
        Statistiques statistiques = service.statistiques();
        statistiquesLabel.setText(String.format(
                "%s catégorie%s  ·  %s article%s  ·  %s",
                Formats.nombre(statistiques.categories()),
                statistiques.categories() > 1 ? "s" : "",
                Formats.nombre(statistiques.articles()),
                statistiques.articles() > 1 ? "s" : "",
                Formats.prix(statistiques.valeurArrondie())));
    }

    private void mettreAJourBoutons() {
        Categorie categorie = categorieSelectionnee();
        Article article = tableArticles.getSelectionModel().getSelectedItem();
        boolean categorieChoisie = categorie != null;

        btnCategorieModifier.setDisable(!categorieChoisie);
        btnCategorieSupprimer.setDisable(!categorieChoisie);
        btnArticleAjouter.setDisable(!categorieChoisie || categorie.estPleine());
        btnArticleModifier.setDisable(article == null);
        btnArticleSupprimer.setDisable(article == null);
        btnArticleDeplacer.setDisable(article == null || service.getCategories().size() < 2);
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

    private void initialiserProprietaire(javafx.scene.control.Dialog<?> dialogue) {
        if (getScene() != null && getScene().getWindow() != null) {
            dialogue.initOwner(getScene().getWindow());
        }
    }

    /**
     * Cellule de la liste des catégories : nom et occupation.
     */
    private static final class ListeCategorieCell extends ListCell<Categorie> {

        private final Label nom = new Label();
        private final Label occupation = new Label();
        private final VBox contenu = new VBox(2, nom, occupation);

        private ListeCategorieCell() {
            nom.getStyleClass().add("categorie-nom");
            occupation.getStyleClass().add("categorie-occupation");
            contenu.setPadding(new Insets(4, 0, 4, 0));
        }

        @Override
        protected void updateItem(Categorie categorie, boolean vide) {
            super.updateItem(categorie, vide);

            if (vide || categorie == null) {
                setText(null);
                setGraphic(null);
                return;
            }

            nom.setText(categorie.getNom());
            occupation.setText(categorie.getNombreArticles()
                    + " / " + categorie.getCapacite() + " articles");
            setText(null);
            setGraphic(contenu);
        }
    }
}
