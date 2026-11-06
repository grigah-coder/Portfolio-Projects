namespace GestionDentaire;

/// <summary>
/// Utilitaires de comparaison de texte : la recherche et la lecture des
/// libellés ignorent les accents et la casse (« Confirme » = « Confirmé »).
/// </summary>
public static class Normalisation
{
    /// <summary>Retire les diacritiques et passe le texte en minuscules.</summary>
    public static string SansAccents(string? texte)
    {
        if (string.IsNullOrWhiteSpace(texte))
        {
            return string.Empty;
        }

        var decompose = texte.Trim().ToLowerInvariant().Normalize(System.Text.NormalizationForm.FormD);
        var resultat = new System.Text.StringBuilder(decompose.Length);

        foreach (var caractere in decompose)
        {
            var categorie = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(caractere);
            if (categorie != System.Globalization.UnicodeCategory.NonSpacingMark)
            {
                resultat.Append(caractere);
            }
        }

        return resultat.ToString();
    }

    /// <summary>Forme normalisée et compactée utilisée par les recherches.</summary>
    public static string PourRecherche(string? texte) =>
        string.Join(' ', SansAccents(texte).Split(' ', StringSplitOptions.RemoveEmptyEntries));
}
