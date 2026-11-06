package com.grigah.appmag.ui;

import com.grigah.appmag.model.Categorie;
import javafx.beans.binding.Bindings;
import javafx.geometry.Insets;
import javafx.scene.control.Button;
import javafx.scene.control.ButtonType;
import javafx.scene.control.Dialog;
import javafx.scene.control.Label;
import javafx.scene.control.Spinner;
import javafx.scene.control.TextField;
import javafx.scene.layout.ColumnConstraints;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.Priority;

/**
 * Boîte de dialogue de création ou de modification d'une catégorie.
 */
public final class CategorieDialog extends Dialog<CategorieFormData> {

    private final TextField nom = new TextField();
    private final Spinner<Integer> capacite =
            new Spinner<>(1, Categorie.CAPACITE_MAX, 10);

    public CategorieDialog(Categorie categorie) {
        boolean modification = categorie != null;

        setTitle(modification ? "Modifier une catégorie" : "Ajouter une catégorie");
        setHeaderText(modification
                ? "Mettez à jour la catégorie."
                : "Renseignez le nom et la capacité de la nouvelle catégorie.");
        getDialogPane().getStyleClass().add("magasin-dialog");
        getDialogPane().setPrefWidth(460);
        getDialogPane().getButtonTypes().addAll(ButtonType.OK, ButtonType.CANCEL);

        Button enregistrer = (Button) getDialogPane().lookupButton(ButtonType.OK);
        enregistrer.setText("Enregistrer");
        Button annuler = (Button) getDialogPane().lookupButton(ButtonType.CANCEL);
        annuler.setText("Annuler");

        configurerChamps();

        if (modification) {
            nom.setText(categorie.getNom());
            capacite.getValueFactory().setValue(categorie.getCapacite());
        }

        enregistrer.disableProperty().bind(Bindings.createBooleanBinding(
                () -> nom.getText() == null || nom.getText().isBlank(),
                nom.textProperty()));

        setResultConverter(button -> button == ButtonType.OK
                ? new CategorieFormData(nom.getText().trim(), capacite.getValue())
                : null);
    }

    private void configurerChamps() {
        nom.setPromptText("Ex. Informatique");
        capacite.setEditable(true);
        capacite.getStyleClass().add("spinner-capacite");

        GridPane grille = new GridPane();
        grille.setHgap(14);
        grille.setVgap(14);
        grille.setPadding(new Insets(22));

        ColumnConstraints labels = new ColumnConstraints();
        labels.setMinWidth(90);
        ColumnConstraints champs = new ColumnConstraints();
        champs.setHgrow(Priority.ALWAYS);
        grille.getColumnConstraints().addAll(labels, champs);

        grille.addRow(0, new Label("Nom"), nom);
        grille.addRow(1, new Label("Capacité"), capacite);
        grille.add(new Label("entre 1 et " + Categorie.CAPACITE_MAX), 1, 2);

        GridPane.setHgrow(nom, Priority.ALWAYS);
        GridPane.setHgrow(capacite, Priority.ALWAYS);

        getDialogPane().setContent(grille);
    }
}
