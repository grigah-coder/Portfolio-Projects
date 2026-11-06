using System.Globalization;
using GestionDentaire.Models;

namespace GestionDentaire.Services;

/// <summary>
/// Cœur métier de l'application : CRUD des patients, recherche, tri,
/// statistiques et persistance. Le formulaire ne manipule jamais la liste
/// directement, ce qui garde les règles au même endroit et testable.
/// </summary>
public sealed class PatientService
{
    private readonly List<Patient> patients = new();
    private readonly PatientCsvRepository repository;

    /// <summary>Crée le service avec le dépôt CSV fourni (ou le dépôt par défaut).</summary>
    public PatientService(PatientCsvRepository? repository = null)
    {
        this.repository = repository ?? new PatientCsvRepository();
    }

    /// <summary>Patients actuellement en mémoire (lecture seule).</summary>
    public IReadOnlyList<Patient> Patients => patients;

    /// <summary>Emplacement du fichier de persistance.</summary>
    public string CheminFichier => repository.CheminFichier;

    /// <summary>Charge le fichier et remplace le contenu en mémoire.</summary>
    public ResultatChargement Charger()
    {
        var resultat = repository.Charger();

        patients.Clear();
        patients.AddRange(resultat.Patients);

        return resultat;
    }

    /// <summary>Enregistre l'état courant dans le fichier CSV.</summary>
    public void Enregistrer() => repository.Enregistrer(patients);

    /// <summary>Ajoute un patient et renvoie l'objet créé (référence générée).</summary>
    public Patient Ajouter(
        string nom,
        string prenom,
        string telephone,
        int age,
        StatutRendezVous statut,
        DateTime dateRendezVous)
    {
        var patient = new Patient(
            ProchaineReference(),
            nom,
            prenom,
            telephone,
            age,
            statut,
            dateRendezVous);

        patients.Add(patient);
        return patient;
    }

    /// <summary>Remplace les données d'un patient existant.</summary>
    /// <exception cref="InvalidOperationException">Référence inconnue.</exception>
    public Patient Modifier(
        string reference,
        string nom,
        string prenom,
        string telephone,
        int age,
        StatutRendezVous statut,
        DateTime dateRendezVous)
    {
        var index = IndexDe(reference);
        var modifie = patients[index].Modifier(nom, prenom, telephone, age, statut, dateRendezVous);

        patients[index] = modifie;
        return modifie;
    }

    /// <summary>Supprime un patient ; renvoie false si la référence est inconnue.</summary>
    public bool Supprimer(string reference)
    {
        var index = patients.FindIndex(
            patient => string.Equals(patient.Reference, reference, StringComparison.OrdinalIgnoreCase));

        if (index < 0)
        {
            return false;
        }

        patients.RemoveAt(index);
        return true;
    }

    /// <summary>Recherche un patient par sa référence.</summary>
    public Patient? Trouver(string reference) =>
        patients.FirstOrDefault(
            patient => string.Equals(patient.Reference, reference, StringComparison.OrdinalIgnoreCase));

    /// <summary>Filtre les patients selon un texte et un champ ; accents ignorés.</summary>
    public IReadOnlyList<Patient> Filtrer(string? texte, ChampRecherche champ)
    {
        var recherche = Normalisation.PourRecherche(texte);

        if (recherche.Length == 0)
        {
            return patients.ToList();
        }

        return patients
            .Where(patient => Correspond(patient, champ, recherche))
            .ToList();
    }

    /// <summary>Renvoie une copie triée de la source, sans modifier l'ordre interne.</summary>
    public IReadOnlyList<Patient> Trier(
        IEnumerable<Patient> source,
        ChampTri critere,
        bool descendant) =>
        critere switch
        {
            ChampTri.Prenom => TrierPar(source, patient => patient.Prenom, descendant),
            ChampTri.Age => TrierPar(source, patient => patient.Age, descendant),
            ChampTri.DateRendezVous => TrierPar(source, patient => patient.DateRendezVous, descendant),
            ChampTri.Statut => TrierPar(source, patient => patient.StatutLibelle, descendant),
            _ => TrierPar(source, patient => patient.Nom, descendant)
        };

    /// <summary>Calcule les indicateurs affichés par le bouton « Statistiques ».</summary>
    public StatistiquesPatients CalculerStatistiques()
    {
        if (patients.Count == 0)
        {
            return StatistiquesPatients.Vides;
        }

        var maintenant = DateTime.Now;

        var prochain = patients
            .Where(patient =>
                patient.DateRendezVous >= maintenant &&
                patient.Statut is not StatutRendezVous.Annule and not StatutRendezVous.Termine)
            .Select(patient => (DateTime?)patient.DateRendezVous)
            .Min();

        return new StatistiquesPatients(
            Total: patients.Count,
            AgeMoyen: Math.Round(patients.Average(patient => patient.Age), 1),
            Juniors: patients.Count(patient => patient.Age <= 18),
            Adultes: patients.Count(patient => patient.Age is >= 19 and <= 35),
            Seniors: patients.Count(patient => patient.Age >= 36),
            ParStatut: patients
                .GroupBy(patient => patient.Statut)
                .ToDictionary(groupe => groupe.Key, groupe => groupe.Count()),
            ProchainRendezVous: prochain);
    }

    private static IReadOnlyList<Patient> TrierPar<TKey>(
        IEnumerable<Patient> source,
        Func<Patient, TKey> cle,
        bool descendant)
    {
        var ordonne = descendant ? source.OrderByDescending(cle) : source.OrderBy(cle);

        // La référence départage les égalités : le tri reste stable et lisible.
        return ordonne
            .ThenBy(patient => patient.Reference, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    private static bool Correspond(Patient patient, ChampRecherche champ, string recherche)
    {
        var valeurs = champ switch
        {
            ChampRecherche.Nom => new[] { patient.Nom },
            ChampRecherche.Prenom => new[] { patient.Prenom },
            ChampRecherche.Telephone => new[] { patient.Telephone },
            ChampRecherche.Statut => new[] { patient.StatutLibelle },
            _ => new[]
            {
                patient.Reference,
                patient.Nom,
                patient.Prenom,
                patient.Telephone,
                patient.StatutLibelle,
                patient.DateRendezVous.ToString("dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture)
            }
        };

        return valeurs.Any(
            valeur => Normalisation.PourRecherche(valeur).Contains(recherche, StringComparison.Ordinal));
    }

    private int IndexDe(string reference)
    {
        var index = patients.FindIndex(
            patient => string.Equals(patient.Reference, reference, StringComparison.OrdinalIgnoreCase));

        if (index < 0)
        {
            throw new InvalidOperationException($"Aucun patient ne porte la référence « {reference} ».");
        }

        return index;
    }

    /// <summary>Génère la prochaine référence libre au format REF-001.</summary>
    private string ProchaineReference()
    {
        var numeroMaximum = 0;

        foreach (var patient in patients)
        {
            if (int.TryParse(
                    patient.Reference.AsSpan(4),
                    NumberStyles.None,
                    CultureInfo.InvariantCulture,
                    out var numero) &&
                numero > numeroMaximum)
            {
                numeroMaximum = numero;
            }
        }

        return $"REF-{numeroMaximum + 1:D3}";
    }
}
