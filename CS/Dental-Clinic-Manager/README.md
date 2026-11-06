# Gestion Dentaire

Application Windows Forms pour gérer les fiches patients et leurs rendez-vous dans un cabinet dentaire.

## Fonctionnalités

- Créer, modifier et supprimer une fiche patient avec validation des champs.
- Rechercher par référence, nom, prénom, téléphone ou statut, sans tenir compte des accents ni de la casse.
- Trier les résultats par nom, prénom, âge, rendez-vous ou statut.
- Consulter les statistiques du cabinet et les prochains rendez-vous.
- Enregistrer les données en CSV et être averti avant de quitter avec des modifications non enregistrées.

## Prérequis et lancement

- Windows 10 ou ultérieur.
- .NET 10 SDK.

Depuis ce dossier, lancez :

```powershell
dotnet run --project 'GestionDentaire.csproj'
```

Pour compiler l'application :

```powershell
dotnet build 'GestionDentaire.csproj' --configuration Release
```

## Données

Les fiches sont enregistrées dans `%USERPROFILE%\.gestiondentaire\patients.csv`. Le fichier est créé au premier enregistrement. Les lignes CSV invalides sont ignorées au chargement et signalées dans la barre d'état.

## Organisation du code

- `Models/` contient les patients, statuts, règles de validation, recherche et statistiques.
- `Services/PatientService.cs` orchestre les opérations métier.
- `Services/PatientCsvRepository.cs` lit et écrit le fichier CSV.
- `MainForm.cs` gère les interactions de la fenêtre et délègue les règles métier au service.

## Raccourcis clavier

- `Ctrl+S` : enregistrer.
- `Ctrl+N` : nouvelle fiche.
- `F5` : recharger depuis le fichier.
- `Delete` dans la liste : supprimer le patient sélectionné.
