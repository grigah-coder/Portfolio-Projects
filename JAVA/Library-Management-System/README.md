# AppGBib

Application de gestion de bibliothèque en JavaFX, réalisée dans le cadre du portfolio de Hamza Grigah.

## Fonctionnalités

- Ajout, modification et suppression de lecteurs et d'employés.
- Recherche instantanée par nom, prénom, type ou rôle.
- Gestion des emprunts et des retours pour les lecteurs (3 emprunts maximum).
- Tableau de bord avec le nombre de personnes, de lecteurs, d'employés et d'emprunts.
- Sauvegarde et rechargement des données dans un fichier CSV local.
- Interface JavaFX moderne avec thème CSS intégré.

## Prérequis

- JDK 25
- Maven 3.9+ (ou le wrapper `mvnw` fourni)

## Lancer l'application

```powershell
.\mvnw.cmd javafx:run
```

Ou, si Maven est installé globalement :

```powershell
mvn javafx:run
```

## Compiler et tester

```powershell
.\mvnw.cmd clean test
.\mvnw.cmd clean package
```

## Données

Les données sont enregistrées dans :

```text
%USERPROFILE%\.appgbib\personnes.csv
```

Le fichier est créé automatiquement au premier enregistrement.

## Architecture

```text
src/main/java/com/grigah/appgbib/
├── App.java                     Point d'entrée JavaFX
├── model/                       Domaine métier
│   ├── Personne.java            Classe abstraite de base
│   ├── Lecteur.java             Lecteur + gestion des emprunts
│   ├── Employe.java             Employé de la bibliothèque
│   ├── Emprunteur.java          Contrat d'emprunt
│   ├── PersonneType.java        Enumération des types
│   └── Validation.java          Validations partagées
├── repository/                  Persistance
│   ├── PersonneRepository.java  Abstraction du dépôt
│   └── CsvPersonneRepository.java
├── service/                     Cas d'usage
│   ├── BibliothequeService.java
│   └── Statistiques.java
└── ui/                          Interface JavaFX
    ├── MainView.java            Fenêtre principale
    ├── PersonneDialog.java      Formulaire d'ajout/modification
    └── PersonneFormData.java    DTO du formulaire
```

## Choix techniques

- **Séparation des responsabilités** : le modèle ne dépend pas de JavaFX, la persistance est abstraite par `PersonneRepository`, et l'interface ne manipule que le service.
- **OOP** : `Personne` est abstraite, `Lecteur` implémente `Emprunteur`, les identifiants sont immuables et les validations sont centralisées.
- **Tests** : tests unitaires JUnit 5 sur le modèle, le dépôt CSV et le service.
- **Persistance** : CSV lisible par un humain, avec échappement des séparateurs et des guillemets.

## Auteur

Hamza Grigah — [github.com/grigah-coder](https://github.com/grigah-coder)
