using System.Globalization;
using System.Text;
using GestionDentaire.Models;

namespace GestionDentaire.Services;

/// <summary>Résultat d'un chargement : patients valides et lignes ignorées.</summary>
/// <param name="Patients">Patients relus avec succès.</param>
/// <param name="Avertissements">Explications des lignes ignorées ou corrigées.</param>
public sealed record ResultatChargement(
    IReadOnlyList<Patient> Patients,
    IReadOnlyList<string> Avertissements)
{
    /// <summary>Chargement vide (aucun fichier ou fichier absent).</summary>
    public static ResultatChargement Vide { get; } = new(
        Array.Empty<Patient>(),
        Array.Empty<string>());
}

/// <summary>
/// Persistance des patients dans un fichier CSV (séparateur « ; »).
/// L'écriture est atomique : le contenu est écrit dans un fichier temporaire
/// puis déplacé sur l'original, afin qu'une coupure ne laisse jamais un
/// fichier à moitié réécrit.
/// </summary>
public sealed class PatientCsvRepository
{
    private const string Entete = "reference;nom;prenom;telephone;age;statut;date";

    private static readonly string[] FormatsDateAcceptes =
    {
        "yyyy-MM-dd HH:mm",
        "yyyy-MM-dd HH:mm:ss",
        "dd/MM/yyyy HH:mm",
        "dd/MM/yyyy HH:mm:ss"
    };

    /// <summary>Crée le dépôt ; le chemin par défaut est dans le profil utilisateur.</summary>
    /// <param name="cheminFichier">
    /// Chemin du CSV. Par défaut : <c>%USERPROFILE%\.gestiondentaire\patients.csv</c>.
    /// </param>
    public PatientCsvRepository(string? cheminFichier = null)
    {
        CheminFichier = string.IsNullOrWhiteSpace(cheminFichier)
            ? Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".gestiondentaire",
                "patients.csv")
            : cheminFichier;
    }

    /// <summary>Emplacement du fichier de données.</summary>
    public string CheminFichier { get; }

    /// <summary>Lit le fichier ; les lignes invalides sont signalées, pas fatales.</summary>
    public ResultatChargement Charger()
    {
        if (!File.Exists(CheminFichier))
        {
            return ResultatChargement.Vide;
        }

        var patients = new List<Patient>();
        var avertissements = new List<string>();
        var references = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        var lignes = File.ReadAllLines(CheminFichier, Encoding.UTF8);

        for (var index = 0; index < lignes.Length; index++)
        {
            var ligne = lignes[index].Trim();
            if (ligne.Length == 0)
            {
                continue;
            }

            // L'en-tête est facultatif : un fichier sans en-tête reste lisible.
            if (index == 0 && ligne.StartsWith("reference;", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            var champs = Decouper(ligne);
            if (champs.Count != 7)
            {
                avertissements.Add(
                    $"Ligne {index + 1} ignorée : {champs.Count} colonne(s) au lieu de 7.");
                continue;
            }

            // L'âge est validé à part pour produire un message français plutôt
            // que le texte d'erreur .NET (« input string was not in a correct format »).
            if (!int.TryParse(champs[4], NumberStyles.Integer, CultureInfo.InvariantCulture, out var age))
            {
                avertissements.Add($"Ligne {index + 1} ignorée : âge « {champs[4]} » invalide.");
                continue;
            }

            try
            {
                if (!StatutRendezVousExtensions.EssayerAnalyser(champs[5], out var statut))
                {
                    statut = StatutRendezVous.EnAttente;
                    avertissements.Add(
                        $"Ligne {index + 1} : statut « {champs[5]} » inconnu, remplacé par « En attente ».");
                }

                var patient = new Patient(
                    reference: champs[0],
                    nom: champs[1],
                    prenom: champs[2],
                    telephone: champs[3],
                    age: age,
                    statut: statut,
                    dateRendezVous: LireDate(champs[6]));

                if (!references.Add(patient.Reference))
                {
                    avertissements.Add(
                        $"Ligne {index + 1} ignorée : référence « {patient.Reference} » déjà présente.");
                    continue;
                }

                patients.Add(patient);
            }
            catch (Exception exception)
                when (exception is PatientInvalideException or FormatException)
            {
                avertissements.Add($"Ligne {index + 1} ignorée : {exception.Message}");
            }
        }

        return new ResultatChargement(patients, avertissements);
    }

    /// <summary>Réécrit intégralement le fichier de données.</summary>
    public void Enregistrer(IEnumerable<Patient> patients)
    {
        var dossier = Path.GetDirectoryName(CheminFichier);
        if (!string.IsNullOrEmpty(dossier))
        {
            Directory.CreateDirectory(dossier);
        }

        var contenu = new StringBuilder();
        contenu.AppendLine(Entete);

        foreach (var patient in patients)
        {
            contenu.AppendJoin(
                ';',
                Echapper(patient.Reference),
                Echapper(patient.Nom),
                Echapper(patient.Prenom),
                Echapper(patient.Telephone),
                patient.Age.ToString(CultureInfo.InvariantCulture),
                Echapper(patient.StatutLibelle),
                patient.DatePourFichier);
            contenu.AppendLine();
        }

        // Écriture atomique : fichier temporaire puis remplacement.
        var temporaire = CheminFichier + ".tmp";
        File.WriteAllText(temporaire, contenu.ToString(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
        File.Move(temporaire, CheminFichier, overwrite: true);
    }

    /// <summary>Ajoute les guillemets nécessaires autour d'un champ CSV.</summary>
    private static string Echapper(string valeur)
    {
        if (valeur.IndexOfAny(new[] { ';', '"', '\r', '\n' }) < 0)
        {
            return valeur;
        }

        return $"\"{valeur.Replace("\"", "\"\"")}\"";
    }

    /// <summary>Découpe une ligne en respectant les champs entre guillemets.</summary>
    private static List<string> Decouper(string ligne)
    {
        var champs = new List<string>();
        var courant = new StringBuilder();
        var entreGuillemets = false;

        for (var index = 0; index < ligne.Length; index++)
        {
            var caractere = ligne[index];

            if (entreGuillemets)
            {
                if (caractere != '"')
                {
                    courant.Append(caractere);
                }
                else if (index + 1 < ligne.Length && ligne[index + 1] == '"')
                {
                    courant.Append('"');
                    index++;
                }
                else
                {
                    entreGuillemets = false;
                }

                continue;
            }

            switch (caractere)
            {
                case '"':
                    entreGuillemets = true;
                    break;
                case ';':
                    champs.Add(courant.ToString());
                    courant.Clear();
                    break;
                default:
                    courant.Append(caractere);
                    break;
            }
        }

        champs.Add(courant.ToString());
        return champs;
    }

    /// <summary>Lit une date au format du fichier, avec repli sur les formats français.</summary>
    private static DateTime LireDate(string texte)
    {
        var valeur = texte.Trim();

        if (DateTime.TryParseExact(
                valeur,
                FormatsDateAcceptes,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var date))
        {
            return date;
        }

        if (DateTime.TryParse(valeur, CultureInfo.GetCultureInfo("fr-FR"), DateTimeStyles.None, out date) ||
            DateTime.TryParse(valeur, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
            return date;
        }

        throw new FormatException($"date « {valeur} » illisible");
    }
}
