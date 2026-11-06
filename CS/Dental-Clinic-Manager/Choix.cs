namespace GestionDentaire;

/// <summary>
/// Élément affiché dans une liste déroulante : associe une valeur typée
/// (statut, champ de recherche, critère de tri) à son libellé français.
/// </summary>
/// <typeparam name="T">Type de la valeur transportée.</typeparam>
public sealed record Choix<T>(T Valeur, string Libelle)
{
    /// <summary>ComboBox affiche cette valeur quand DisplayMember n'est pas défini.</summary>
    public override string ToString() => Libelle;
}
