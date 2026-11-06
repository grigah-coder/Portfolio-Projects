namespace GestionDentaire.Models;

/// <summary>Étape du rendez-vous dans le parcours de soins du patient.</summary>
public enum StatutRendezVous
{
    EnAttente,
    Confirme,
    Reporte,
    Annule,
    Termine
}

/// <summary>Libellés français, ordre d'affichage et lecture tolérante du statut.</summary>
public static class StatutRendezVousExtensions
{
    private static readonly IReadOnlyDictionary<StatutRendezVous, string> Libelles =
        new Dictionary<StatutRendezVous, string>
        {
            [StatutRendezVous.EnAttente] = "En attente",
            [StatutRendezVous.Confirme] = "Confirmé",
            [StatutRendezVous.Reporte] = "Reporté",
            [StatutRendezVous.Annule] = "Annulé",
            [StatutRendezVous.Termine] = "Traité"
        };

    /// <summary>Statuts proposés dans l'interface, dans un ordre logique de parcours.</summary>
    public static IReadOnlyList<StatutRendezVous> Tous { get; } = new[]
    {
        StatutRendezVous.EnAttente,
        StatutRendezVous.Confirme,
        StatutRendezVous.Reporte,
        StatutRendezVous.Annule,
        StatutRendezVous.Termine
    };

    /// <summary>Libellé français du statut.</summary>
    public static string Libelle(this StatutRendezVous statut) => Libelles[statut];

    /// <summary>
    /// Convertit un texte en statut. Les accents et la casse sont ignorés,
    /// et les anciens libellés (« validé », « traité »…) restent acceptés
    /// pour relire les fichiers produits par la version précédente.
    /// </summary>
    public static bool EssayerAnalyser(string? texte, out StatutRendezVous statut)
    {
        statut = StatutRendezVous.EnAttente;

        if (string.IsNullOrWhiteSpace(texte))
        {
            return false;
        }

        var normalise = Normalisation.PourRecherche(texte);

        // Alias historiques : ancien projet et vocabulaire courant du cabinet.
        var alias = normalise switch
        {
            "valide" or "confirme" => StatutRendezVous.Confirme,
            "reporte" => StatutRendezVous.Reporte,
            "annule" => StatutRendezVous.Annule,
            "traite" or "termine" => StatutRendezVous.Termine,
            "en attente" or "attente" => StatutRendezVous.EnAttente,
            _ => (StatutRendezVous?)null
        };

        if (alias is not null)
        {
            statut = alias.Value;
            return true;
        }

        foreach (var valeur in Tous)
        {
            if (normalise == Normalisation.PourRecherche(valeur.ToString()))
            {
                statut = valeur;
                return true;
            }
        }

        return false;
    }
}
