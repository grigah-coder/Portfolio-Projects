namespace GestionDentaire.Models;

/// <summary>Indicateurs agrégés calculés à la demande sur la patientèle.</summary>
/// <param name="Total">Nombre de patients suivis.</param>
/// <param name="AgeMoyen">Âge moyen, arrondi au dixième.</param>
/// <param name="Juniors">Patients de 18 ans ou moins.</param>
/// <param name="Adultes">Patients de 19 à 35 ans.</param>
/// <param name="Seniors">Patients de 36 ans ou plus.</param>
/// <param name="ParStatut">Répartition des rendez-vous par statut.</param>
/// <param name="ProchainRendezVous">Premier rendez-vous à venir non annulé.</param>
public sealed record StatistiquesPatients(
    int Total,
    double AgeMoyen,
    int Juniors,
    int Adultes,
    int Seniors,
    IReadOnlyDictionary<StatutRendezVous, int> ParStatut,
    DateTime? ProchainRendezVous)
{
    /// <summary>Statistiques neutres, utilisées quand la patientèle est vide.</summary>
    public static StatistiquesPatients Vides { get; } = new(
        Total: 0,
        AgeMoyen: 0,
        Juniors: 0,
        Adultes: 0,
        Seniors: 0,
        ParStatut: new Dictionary<StatutRendezVous, int>(),
        ProchainRendezVous: null);
}
