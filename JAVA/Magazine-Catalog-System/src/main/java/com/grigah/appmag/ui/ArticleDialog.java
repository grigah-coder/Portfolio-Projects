package com.grigah.appmag.ui;

import com.grigah.appmag.model.Article;
import javafx.beans.binding.Bindings;
import javafx.geometry.Insets;
import javafx.scene.control.Button;
import javafx.scene.control.ButtonType;
import javafx.scene.control.Dialog;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.scene.layout.ColumnConstraints;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.Priority;

import java.util.Locale;
import java.util.OptionalDouble;

/**
 * Boîte de dialogue de création ou de modification d'un article.
 */
public final class ArticleDialog extends Dialog<ArticleFormData> {

    private final TextField libelle = new TextField();
    private final TextField prix = new TextField();
    private final Label erreurPrix = new Label();

    public ArticleDialog(Article article) {
        boolean modification = article != null;

        setTitle(modification ? "Modifier un article" : "Ajouter un article");
        setHeaderText(modification
                ? "Mettez à jour l'article."
                : "Renseignez le libellé et le prix du nouvel article.");
        getDialogPane().getStyleClass().add("magasin-dialog");
        getDialogPane().setPrefWidth(460);
        getDialogPane().getButtonTypes().addAll(ButtonType.OK, ButtonType.CANCEL);

        Button enregistrer = (Button) getDialogPane().lookupButton(ButtonType.OK);
        enregistrer.setText("Enregistrer");
        Button annuler = (Button) getDialogPane().lookupButton(ButtonType.CANCEL);
        annuler.setText("Annuler");

        configurerChamps();

        if (modification) {
            libelle.setText(article.getLibelle());
            prix.setText(String.format(Locale.FRANCE, "%.2f", article.getPrix()));
        }

        enregistrer.disableProperty().bind(Bindings.createBooleanBinding(
                () -> libelle.getText() == null
                        || libelle.getText().isBlank()
                        || analyserPrix(prix.getText()).isEmpty(),
                libelle.textProperty(),
                prix.textProperty()));

        setResultConverter(button -> {
            if (button != ButtonType.OK) {
                return null;
            }
            return new ArticleFormData(
                    libelle.getText().trim(),
                    analyserPrix(prix.getText()).orElseThrow());
        });
    }

    private void configurerChamps() {
        libelle.setPromptText("Ex. Ordinateur portable");
        prix.setPromptText("Ex. 1299,99");
        erreurPrix.getStyleClass().add("erreur-champ");

        prix.textProperty().addListener((observable, ancien, texte) -> {
            boolean vide = texte == null || texte.isBlank();
            boolean invalide = !vide && analyserPrix(texte).isEmpty();
            erreurPrix.setText(invalide ? "Saisissez un montant positif (ex. 1299,99)." : "");
        });

        GridPane grille = new GridPane();
        grille.setHgap(14);
        grille.setVgap(14);
        grille.setPadding(new Insets(22));

        ColumnConstraints labels = new ColumnConstraints();
        labels.setMinWidth(90);
        ColumnConstraints champs = new ColumnConstraints();
        champs.setHgrow(Priority.ALWAYS);
        grille.getColumnConstraints().addAll(labels, champs);

        grille.addRow(0, new Label("Libellé"), libelle);
        grille.addRow(1, new Label("Prix (€)"), prix);
        grille.add(erreurPrix, 1, 2);

        GridPane.setHgrow(libelle, Priority.ALWAYS);
        GridPane.setHgrow(prix, Priority.ALWAYS);

        getDialogPane().setContent(grille);
    }

    private static OptionalDouble analyserPrix(String texte) {
        if (texte == null || texte.isBlank()) {
            return OptionalDouble.empty();
        }
        try {
            double valeur = Double.parseDouble(texte.trim().replace(" ", "").replace(',', '.'));
            return valeur >= 0 && Double.isFinite(valeur)
                    ? OptionalDouble.of(valeur)
                    : OptionalDouble.empty();
        } catch (NumberFormatException exception) {
            return OptionalDouble.empty();
        }
    }
}
