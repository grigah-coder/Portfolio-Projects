package com.grigah.appmag;

import com.grigah.appmag.repository.CsvMagasinRepository;
import com.grigah.appmag.repository.MagasinRepository;
import com.grigah.appmag.service.MagasinService;
import com.grigah.appmag.ui.MainView;
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
        MagasinRepository repository = new CsvMagasinRepository(cheminDonnees());
        MagasinService service = new MagasinService(repository);
        MainView vue = new MainView(service);

        Scene scene = new Scene(vue, 1180, 760);
        scene.getStylesheets().add(
                App.class.getResource("app.css").toExternalForm());

        stage.setTitle("AppMag — Gestion de magasin");
        stage.setMinWidth(980);
        stage.setMinHeight(620);
        stage.setScene(scene);
        stage.show();

        vue.chargerDonnees();
    }

    private Path cheminDonnees() {
        return Path.of(System.getProperty("user.home"), ".appmag", "magasin.csv");
    }

    public static void main(String[] args) {
        launch(args);
    }
}
