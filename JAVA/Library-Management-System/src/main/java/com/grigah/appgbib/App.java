package com.grigah.appgbib;

import com.grigah.appgbib.repository.CsvPersonneRepository;
import com.grigah.appgbib.repository.PersonneRepository;
import com.grigah.appgbib.service.BibliothequeService;
import com.grigah.appgbib.ui.MainView;
import javafx.application.Application;
import javafx.scene.Scene;
import javafx.stage.Stage;

import java.nio.file.Path;

/**
 * Point d'entrée JavaFX de l'application.
 */
public final class App extends Application {

    @Override
    public void start(Stage stage) {
        PersonneRepository repository = new CsvPersonneRepository(cheminDonnees());
        BibliothequeService service = new BibliothequeService(repository);
        MainView vue = new MainView(service);

        Scene scene = new Scene(vue, 1180, 760);
        scene.getStylesheets().add(
                App.class.getResource("app.css").toExternalForm());

        stage.setTitle("AppGBib — Gestion de bibliothèque");
        stage.setMinWidth(980);
        stage.setMinHeight(620);
        stage.setScene(scene);
        stage.show();

        vue.chargerDonnees();
    }

    private Path cheminDonnees() {
        return Path.of(System.getProperty("user.home"), ".appgbib", "personnes.csv");
    }

    public static void main(String[] args) {
        launch(args);
    }
}
