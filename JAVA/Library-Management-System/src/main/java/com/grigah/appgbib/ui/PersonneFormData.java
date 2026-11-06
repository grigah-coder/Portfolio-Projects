package com.grigah.appgbib.ui;

import com.grigah.appgbib.model.PersonneType;

/**
 * Données saisies dans le formulaire d'ajout ou de modification.
 */
public record PersonneFormData(PersonneType type, String nom, String prenom, String role) {
}
