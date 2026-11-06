package com.grigah.appgbib.repository;

import com.grigah.appgbib.model.Personne;

import java.io.IOException;
import java.nio.file.Path;
import java.util.List;

/**
 * Abstraction de la persistance des personnes.
 */
public interface PersonneRepository {

    List<Personne> charger() throws IOException;

    void sauvegarder(List<Personne> personnes) throws IOException;

    Path getChemin();
}
