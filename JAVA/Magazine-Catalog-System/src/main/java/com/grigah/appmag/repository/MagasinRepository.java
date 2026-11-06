package com.grigah.appmag.repository;

import com.grigah.appmag.model.Categorie;

import java.io.IOException;
import java.nio.file.Path;
import java.util.List;

/**
 * Abstraction de la persistance du magasin.
 */
public interface MagasinRepository {

    List<Categorie> charger() throws IOException;

    void sauvegarder(List<Categorie> categories) throws IOException;

    Path getChemin();
}
