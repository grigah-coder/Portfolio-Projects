# AppMag

Application de gestion de magasin en JavaFX, réalisée dans le cadre du portfolio de Hamza Grigah.

## Fonctionnalités

- Gestion des catégories : ajout, modification, suppression et capacité maximale.
- Gestion des articles : ajout, modification, suppression et déplacement entre catégories.
- Recherche instantanée dans les articles d'une catégorie.
- Indicateurs : nombre de catégories, nombre d'articles, valeur totale, valeur et prix moyen par catégorie.
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
%USERPROFILE%\.appmag\magasin.csv
```

Le fichier est créé automatiquement au premier enregistrement.

## Architecture

```text
src/main/java/com/grigah/appmag/
├── App.java                     Point d'entrée JavaFX
├── model/                       Domaine métier
│   ├── Article.java             Article (identifiant immuable, prix arrondi)
│   ├── Categorie.java           Catégorie et règle de capacité
│   └── Validation.java          Validations partagées
├── repository/                  Persistance
│   ├── MagasinRepository.java   Abstraction du dépôt
│   ├── CsvMagasinRepository.java
│   └── Csv.java                 Lecture/écriture CSV
├── service/                     Cas d'usage
│   ├── MagasinService.java
│   └── Statistiques.java
└── ui/                          Interface JavaFX
    ├── MainView.java            Fenêtre principale
    ├── CategorieDialog.java     Formulaire de catégorie
    ├── ArticleDialog.java       Formulaire d'article
    ├── Formats.java             Formatage des prix et des nombres
    ├── CategorieFormData.java   DTO du formulaire de catégorie
    └── ArticleFormData.java     DTO du formulaire d'article
```

## Choix techniques

- **Encapsulation** : la liste d'articles d'une catégorie n'est jamais exposée ;
  `ajouterArticle` et `retirerArticle` garantissent la capacité maximale et l'unicité.
- **Immutabilité des identifiants** : les identifiants sont attribués par le service et ne changent plus.
- **Séparation des responsabilités** : le modèle ignore JavaFX, la persistance est abstraite
  par `MagasinRepository`, et l'interface ne manipule que le service.
- **Tests** : JUnit 5 sur le modèle, le dépôt CSV et le service.
- **Persistance** : CSV lisible par un humain, avec échappement des séparateurs et des guillemets.

## Auteur

Hamza Grigah — [github.com/grigah-coder](https://github.com/grigah-coder)
