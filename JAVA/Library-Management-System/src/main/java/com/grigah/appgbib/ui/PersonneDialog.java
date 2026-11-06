package com.grigah.appgbib.ui;

import com.grigah.appgbib.model.Personne;
import com.grigah.appgbib.model.PersonneType;
import javafx.beans.binding.Bindings;
import javafx.geometry.Insets;
import javafx.scene.control.Button;
import javafx.scene.control.ButtonType;
import javafx.scene.control.ComboBox;
import javafx.scene.control.Dialog;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.scene.layout.ColumnConstraints;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.Priority;

/**
 * Boîte de dialogue de création ou de modification d'une personne.
 */
public final class PersonneDialog extends Dialog<PersonneFormData> {

    private final ComboBox<PersonneType> type = new ComboBox<>();
    private final TextField nom = new TextField();
    private final TextField prenom = new TextField();
    private final TextField role = new TextField();
    private final Label roleLabel = new Label("Fonction");

    public PersonneDialog(Personne personne) {
        boolean modification = personne != null;

        setTitle(modification ? "Modifier une personne" : "Ajouter une personne");
        setHeaderText(modification
                ? "Mettez à jour les informations de la personne."
                : "Renseignez les informations de la nouvelle personne.");
        getDialogPane().getStyleClass().add("personne-dialog");
        getDialogPane().setPrefWidth(480);
        getDialogPane().getButtonTypes().addAll(ButtonType.OK, ButtonType.CANCEL);

        Button enregistrer = (Button) getDialogPane().lookupButton(ButtonType.OK);
        enregistrer.setText("Enregistrer");
        Button annuler = (Button) getDialogPane().lookupButton(ButtonType.CANCEL);
        annuler.setText("Annuler");

        configurerChamps();
        configurerGrille();

        if (modification) {
            type.setValue(personne.getType());
            type.setDisable(true);
            nom.setText(personne.getNom());
            prenom.setText(personne.getPrenom());
            role.setText(personne.getRole());
        } else {
            type.setValue(PersonneType.LECTEUR);
        }

        enregistrer.disableProperty().bind(Bindings.createBooleanBinding(
                () -> type.getValue() == null
                        || nom.getText().isBlank()
                        || prenom.getText().isBlank()
                        || role.getText().isBlank(),
                type.valueProperty(),
                nom.textProperty(),
                prenom.textProperty(),
                role.textProperty()));

        setResultConverter(button -> button == ButtonType.OK
                ? new PersonneFormData(
                        type.getValue(),
                        nom.getText().trim(),
                        prenom.getText().trim(),
                        role.getText().trim())
                : null);
    }

    private void configurerChamps() {
        type.getItems().setAll(PersonneType.values());
        type.setMaxWidth(Double.MAX_VALUE);

        nom.setPromptText("Ex. Grigah");
        prenom.setPromptText("Ex. Hamza");
        role.setPromptText("Ex. Étudiant, Enseignant, Bibliothécaire…");

        type.valueProperty().addListener((observable, ancien, valeur) -> {
            boolean lecteur = valeur == PersonneType.LECTEUR;
            roleLabel.setText(lecteur ? "Fonction" : "Statut");
            role.setPromptText(lecteur
                    ? "Ex. Étudiant, Enseignant…"
                    : "Ex. Bibliothécaire, Manager…");
        });
    }

    private void configurerGrille() {
        GridPane grille = new GridPane();
        grille.setHgap(14);
        grille.setVgap(14);
        grille.setPadding(new Insets(22));

        ColumnConstraints labels = new ColumnConstraints();
        labels.setMinWidth(90);
        ColumnConstraints champs = new ColumnConstraints();
        champs.setHgrow(Priority.ALWAYS);
        grille.getColumnConstraints().addAll(labels, champs);

        grille.addRow(0, new Label("Type"), type);
        grille.addRow(1, new Label("Nom"), nom);
        grille.addRow(2, new Label("Prénom"), prenom);
        grille.addRow(3, roleLabel, role);

        GridPane.setHgrow(type, Priority.ALWAYS);
        GridPane.setHgrow(nom, Priority.ALWAYS);
        GridPane.setHgrow(prenom, Priority.ALWAYS);
        GridPane.setHgrow(role, Priority.ALWAYS);

        getDialogPane().setContent(grille);
    }
}
