using System.Globalization;
using System.Text.RegularExpressions;

namespace GestionDentaire.Models;

/// <summary>Donnée patient invalide (nom vide, âge hors limites, etc.).</summary>
public sealed class PatientInvalideException(string message) : Exception(message);

/// <summary>
/// Patient du cabinet. L'objet est immuable : une modification produit une
/// nouvelle instance via <see cref="Modifier"/>, ce qui évite les états
/// incohérents et rend les règles de validation impossibles à contourner.
/// </summary>
public sealed class Patient
{
    public const int AgeMinimum = 0;
    public const int AgeMaximum = 120;
    public const int LongueurNomMaximum = 60;

    private static readonly Regex FormatReference = new(@"^REF-\d{3,6}$", RegexOptions.Compiled);
    private static readonly Regex FormatTelephone = new(@"^[0-9+().\s-]{6,25}$", RegexOptions.Compiled);

    /// <summary>Crée un patient validé ; lève <see cref="PatientInvalideException"/> sinon.</summary>
    public Patient(
        string reference,
        string nom,
        string prenom,
        string telephone,
        int age,
        StatutRendezVous statut,
        DateTime dateRendezVous)
    {
        Reference = ValiderReference(reference);
        Nom = ValiderNom(nom, "Le nom");
        Prenom = ValiderNom(prenom, "Le prénom");
        Telephone = ValiderTelephone(telephone);
        Age = ValiderAge(age);
        Statut = statut;
        DateRendezVous = dateRendezVous;
    }

    public string Reference { get; }

    public string Nom { get; }

    public string Prenom { get; }

    public string Telephone { get; }

    public int Age { get; }

    public StatutRendezVous Statut { get; }

    public DateTime DateRendezVous { get; }

    /// <summary>Nom et prénom assemblés, utilisés par la grille et les messages.</summary>
    public string NomComplet => $"{Nom} {Prenom}".Trim();

    /// <summary>Libellé français du statut, exposé pour la liaison de données.</summary>
    public string StatutLibelle => Statut.Libelle();

    /// <summary>Crée une copie modifiée du patient en conservant sa référence.</summary>
    public Patient Modifier(
        string nom,
        string prenom,
        string telephone,
        int age,
        StatutRendezVous statut,
        DateTime dateRendezVous) =>
        new(Reference, nom, prenom, telephone, age, statut, dateRendezVous);

    public override string ToString() => $"{Reference} — {NomComplet}";

    private static string ValiderReference(string reference)
    {
        var propre = reference?.Trim() ?? string.Empty;

        if (!FormatReference.IsMatch(propre))
        {
            throw new PatientInvalideException(
                "La référence doit respecter le format REF-001.");
        }

        return propre;
    }

    private static string ValiderNom(string valeur, string champ)
    {
        var propre = valeur?.Trim() ?? string.Empty;

        if (propre.Length == 0)
        {
            throw new PatientInvalideException($"{champ} est obligatoire.");
        }

        if (propre.Length > LongueurNomMaximum)
        {
            throw new PatientInvalideException(
                $"{champ} ne doit pas dépasser {LongueurNomMaximum} caractères.");
        }

        return propre;
    }

    private static string ValiderTelephone(string valeur)
    {
        var propre = valeur?.Trim() ?? string.Empty;

        if (propre.Length == 0)
        {
            return string.Empty;
        }

        var chiffres = propre.Count(char.IsDigit);

        if (!FormatTelephone.IsMatch(propre) || chiffres < 6)
        {
            throw new PatientInvalideException(
                "Le téléphone doit contenir au moins 6 chiffres et uniquement " +
                "des chiffres, espaces, points, tirets, parenthèses ou un « + ».");
        }

        return propre;
    }

    private static int ValiderAge(int age)
    {
        if (age is < AgeMinimum or > AgeMaximum)
        {
            throw new PatientInvalideException(
                $"L'âge doit être compris entre {AgeMinimum} et {AgeMaximum} ans.");
        }

        return age;
    }

    /// <summary>Date au format d'échange du fichier CSV (invariant).</summary>
    public string DatePourFichier =>
        DateRendezVous.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
}
