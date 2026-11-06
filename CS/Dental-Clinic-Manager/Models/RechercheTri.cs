namespace GestionDentaire.Models;

/// <summary>Champ sur lequel porte la recherche textuelle.</summary>
public enum ChampRecherche
{
    Tous,
    Nom,
    Prenom,
    Telephone,
    Statut
}

/// <summary>Critère de tri de la liste des patients.</summary>
public enum ChampTri
{
    Nom,
    Prenom,
    Age,
    DateRendezVous,
    Statut
}
