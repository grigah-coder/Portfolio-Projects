using System.Globalization;
using GestionDentaire.Models;
using GestionDentaire.Services;

namespace GestionDentaire;

/// <summary>
/// Fenêtre principale : fiche patient à gauche, liste et outils de recherche
/// à droite. Toute la logique métier est déléguée à <see cref="PatientService"/>.
/// </summary>
public partial class MainForm : Form
{
    private static readonly CultureInfo CultureFr = CultureInfo.GetCultureInfo("fr-FR");

    private static readonly Font PoliceStatut = new("Segoe UI Semibold", 9.5F, FontStyle.Bold);

    private static readonly IReadOnlyDictionary<StatutRendezVous, Color> CouleursStatut =
        new Dictionary<StatutRendezVous, Color>
        {
            [StatutRendezVous.EnAttente] = Color.FromArgb(14, 116, 144),
            [StatutRendezVous.Confirme] = Color.FromArgb(4, 120, 87),
            [StatutRendezVous.Reporte] = Color.FromArgb(180, 83, 9),
            [StatutRendezVous.Annule] = Color.FromArgb(185, 28, 28),
            [StatutRendezVous.Termine] = Color.FromArgb(71, 85, 105)
        };

    private readonly PatientService service = new();

    /// <summary>Empêche les rafraîchissements pendant la construction de l'UI.</summary>
    private bool interfacePrete;

    /// <summary>Indique que des ajouts/modifications ne sont pas encore enregistrés.</summary>
    private bool modificationsEnAttente;

    private bool triDescendant;

    private int indexColonneStatut = -1;

    public MainForm()
    {
        InitializeComponent();
        ConfigurerInterface();
    }

    /// <summary>
    /// Réduit la fenêtre si l'écran est plus petit que la taille de conception
    /// (portables 1366 × 768 ou écrans mis à l'échelle 125 % notamment) : la
    /// barre d'état et les boutons restent ainsi visibles.
    /// </summary>
    private void AjusterALEcran()
    {
        var zone = Screen.FromControl(this).WorkingArea;
        var echelle = DeviceDpi / 96f;

        if (echelle <= 0f)
        {
            echelle = 1f;
        }

        // Width/Height sont exprimés en unités logiques, WorkingArea en pixels
        // physiques : la comparaison se fait donc après mise à l'échelle.
        if (Width * echelle > zone.Width)
        {
            Width = (int)(zone.Width / echelle) - 2;
        }

        if (Height * echelle > zone.Height)
        {
            Height = (int)(zone.Height / echelle) - 2;
        }
    }

    /// <summary>Saisie validée de la fiche, prête à être envoyée au service.</summary>
    private sealed record SaisiePatient(
        string Nom,
        string Prenom,
        string Telephone,
        int Age,
        StatutRendezVous Statut,
        DateTime DateRendezVous);

    /// <summary>Applique les styles, remplit les listes déroulantes et la grille.</summary>
    private void ConfigurerInterface()
    {
        DoubleBuffered = true;
        statusBar.ShowItemToolTips = true;

        var icone = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        if (icone is not null)
        {
            Icon = icone;
        }

        lblDate.Text = DateTime.Now.ToString("dddd d MMMM yyyy", CultureFr);
        lblChemin.Text = $"Données : {Raccourcir(service.CheminFichier)}";
        lblChemin.ToolTipText = service.CheminFichier;

        cmbStatut.DataSource = StatutRendezVousExtensions.Tous
            .Select(statut => new Choix<StatutRendezVous>(statut, statut.Libelle()))
            .ToList();
        cmbStatut.DisplayMember = nameof(Choix<StatutRendezVous>.Libelle);
        cmbStatut.ValueMember = nameof(Choix<StatutRendezVous>.Valeur);

        cmbChampRecherche.DataSource = new List<Choix<ChampRecherche>>
        {
            new(ChampRecherche.Tous, "Tous les champs"),
            new(ChampRecherche.Nom, "Nom"),
            new(ChampRecherche.Prenom, "Prénom"),
            new(ChampRecherche.Telephone, "Téléphone"),
            new(ChampRecherche.Statut, "Statut")
        };
        cmbChampRecherche.DisplayMember = nameof(Choix<ChampRecherche>.Libelle);
        cmbChampRecherche.ValueMember = nameof(Choix<ChampRecherche>.Valeur);

        cmbTri.DataSource = new List<Choix<ChampTri>>
        {
            new(ChampTri.Nom, "Trier par nom"),
            new(ChampTri.Prenom, "Trier par prénom"),
            new(ChampTri.Age, "Trier par âge"),
            new(ChampTri.DateRendezVous, "Trier par rendez-vous"),
            new(ChampTri.Statut, "Trier par statut")
        };
        cmbTri.DisplayMember = nameof(Choix<ChampTri>.Libelle);
        cmbTri.ValueMember = nameof(Choix<ChampTri>.Valeur);

        ConfigurerGrille();
        ViderFiche();

        interfacePrete = true;
    }

    /// <summary>Colonnes explicites : la grille affiche des propriétés calculées.</summary>
    private void ConfigurerGrille()
    {
        dgvPatients.AutoGenerateColumns = false;
        dgvPatients.Columns.Clear();

        dgvPatients.DefaultCellStyle.BackColor = Color.White;
        dgvPatients.DefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42);
        dgvPatients.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 242, 254);
        dgvPatients.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
        dgvPatients.DefaultCellStyle.Font = new Font("Segoe UI", 9.75F);
        dgvPatients.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);

        dgvPatients.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

        dgvPatients.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
        dgvPatients.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
        dgvPatients.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 250, 252);
        dgvPatients.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(71, 85, 105);
        dgvPatients.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        dgvPatients.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
        dgvPatients.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

        AjouterColonne("Reference", "Référence", 110, DataGridViewAutoSizeColumnMode.None, estMonospace: true);
        AjouterColonne("NomComplet", "Patient", 220, DataGridViewAutoSizeColumnMode.Fill, poids: 30);
        AjouterColonne("Age", "Âge", 62, DataGridViewAutoSizeColumnMode.None, alignementCentre: true);
        AjouterColonne("Telephone", "Téléphone", 130, DataGridViewAutoSizeColumnMode.Fill, poids: 20);
        AjouterColonne("StatutLibelle", "Statut", 110, DataGridViewAutoSizeColumnMode.Fill, poids: 18);
        AjouterColonne("DateRendezVous", "Rendez-vous", 140, DataGridViewAutoSizeColumnMode.None, format: "dd/MM/yy HH:mm");

        indexColonneStatut = dgvPatients.Columns["StatutLibelle"]?.Index ?? -1;
    }

    private void AjouterColonne(
        string propriete,
        string entete,
        int largeur,
        DataGridViewAutoSizeColumnMode mode,
        float poids = 100F,
        bool estMonospace = false,
        bool alignementCentre = false,
        string? format = null)
    {
        var colonne = new DataGridViewTextBoxColumn
        {
            Name = propriete,
            DataPropertyName = propriete,
            HeaderText = entete,
            Width = largeur,
            MinimumWidth = Math.Min(largeur, 80),
            AutoSizeMode = mode,
            FillWeight = poids,
            SortMode = DataGridViewColumnSortMode.NotSortable
        };

        if (estMonospace)
        {
            colonne.DefaultCellStyle.Font = new Font("Consolas", 9.5F);
            colonne.DefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
        }

        if (alignementCentre)
        {
            colonne.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        if (format is not null)
        {
            colonne.DefaultCellStyle.Format = format;
        }

        dgvPatients.Columns.Add(colonne);
    }

    /* ---------------------------------------------------------------------
       Chargement et persistance
       --------------------------------------------------------------------- */

    private void MainForm_Load(object? sender, EventArgs e)
    {
        AjusterALEcran();
        ChargerPatients();
        txtNom.Focus();
    }

    private void ChargerPatients()
    {
        try
        {
            var resultat = service.Charger();
            modificationsEnAttente = false;
            RafraichirListe();
            ViderFiche();

            if (resultat.Avertissements.Count == 0)
            {
                AfficherMessage($"{service.Patients.Count} patient(s) chargé(s).");
            }
            else
            {
                AfficherMessage(
                    $"{service.Patients.Count} patient(s) chargé(s), " +
                    $"{resultat.Avertissements.Count} ligne(s) ignorée(s) — survolez pour le détail.",
                    erreur: true);
                lblMessage.ToolTipText = string.Join(Environment.NewLine, resultat.Avertissements);
            }
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
        {
            AfficherMessage($"Chargement impossible : {exception.Message}", erreur: true);
        }
    }

    /// <summary>Enregistre l'état courant ; renvoie false en cas d'échec d'écriture.</summary>
    private bool Enregistrer(bool silencieux = false)
    {
        try
        {
            service.Enregistrer();
            modificationsEnAttente = false;
            MettreAJourBoutons();

            if (!silencieux)
            {
                AfficherMessage("Données enregistrées dans le fichier CSV.");
            }

            return true;
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
        {
            AfficherMessage($"Enregistrement impossible : {exception.Message}", erreur: true);
            return false;
        }
    }

    private void btnEnregistrer_Click(object? sender, EventArgs e) => Enregistrer();

    private void btnRecharger_Click(object? sender, EventArgs e)
    {
        if (modificationsEnAttente)
        {
            var reponse = MessageBox.Show(
                "Les modifications non enregistrées seront perdues. Continuer ?",
                "Recharger les données",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (reponse != DialogResult.Yes)
            {
                return;
            }
        }

        ChargerPatients();
    }

    private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (!modificationsEnAttente)
        {
            return;
        }

        var reponse = MessageBox.Show(
            "Des modifications ne sont pas enregistrées. Voulez-vous les enregistrer avant de quitter ?",
            "Gestion Dentaire",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button1);

        switch (reponse)
        {
            case DialogResult.Cancel:
                e.Cancel = true;
                break;
            case DialogResult.Yes when !Enregistrer(silencieux: true):
                e.Cancel = true;
                break;
        }
    }

    /* ---------------------------------------------------------------------
       Fiche patient
       --------------------------------------------------------------------- */

    private void btnAjouter_Click(object? sender, EventArgs e)
    {
        if (!EssayerLireSaisie(out var saisie))
        {
            return;
        }

        try
        {
            var patient = service.Ajouter(
                saisie.Nom,
                saisie.Prenom,
                saisie.Telephone,
                saisie.Age,
                saisie.Statut,
                saisie.DateRendezVous);

            modificationsEnAttente = true;
            RafraichirListe();
            Selectionner(patient.Reference);
            RemplirFiche(patient);
            AfficherMessage($"Patient {patient.Reference} ajouté. Pensez à enregistrer.");
        }
        catch (PatientInvalideException exception)
        {
            AfficherMessage(exception.Message, erreur: true);
        }
    }

    private void btnModifier_Click(object? sender, EventArgs e)
    {
        var reference = txtReference.Text.Trim();

        if (reference.Length == 0)
        {
            AfficherMessage("Sélectionnez d'abord un patient dans la liste.", erreur: true);
            return;
        }

        if (!EssayerLireSaisie(out var saisie))
        {
            return;
        }

        try
        {
            var patient = service.Modifier(
                reference,
                saisie.Nom,
                saisie.Prenom,
                saisie.Telephone,
                saisie.Age,
                saisie.Statut,
                saisie.DateRendezVous);

            modificationsEnAttente = true;
            RafraichirListe();
            Selectionner(patient.Reference);
            RemplirFiche(patient);
            AfficherMessage($"Fiche {patient.Reference} mise à jour. Pensez à enregistrer.");
        }
        catch (PatientInvalideException exception)
        {
            AfficherMessage(exception.Message, erreur: true);
        }
        catch (InvalidOperationException exception)
        {
            AfficherMessage(exception.Message, erreur: true);
        }
    }

    private void btnSupprimer_Click(object? sender, EventArgs e)
    {
        var patient = service.Trouver(txtReference.Text.Trim());

        if (patient is null)
        {
            AfficherMessage("Sélectionnez d'abord un patient dans la liste.", erreur: true);
            return;
        }

        var reponse = MessageBox.Show(
            $"Supprimer définitivement {patient.NomComplet} ({patient.Reference}) ?",
            "Confirmation",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2);

        if (reponse != DialogResult.Yes)
        {
            return;
        }

        if (service.Supprimer(patient.Reference))
        {
            modificationsEnAttente = true;
            RafraichirListe();
            ViderFiche();
            AfficherMessage($"Patient {patient.Reference} supprimé. Pensez à enregistrer.");
        }
        else
        {
            AfficherMessage("Patient introuvable : la liste a peut-être changé.", erreur: true);
        }
    }

    private void btnNouveau_Click(object? sender, EventArgs e) => ViderFiche();

    private void RemplirFiche(Patient patient)
    {
        txtReference.Text = patient.Reference;
        txtNom.Text = patient.Nom;
        txtPrenom.Text = patient.Prenom;
        txtTelephone.Text = patient.Telephone;
        txtAge.Text = patient.Age.ToString(CultureInfo.CurrentCulture);
        cmbStatut.SelectedValue = patient.Statut;
        dtpDateRdv.Value = patient.DateRendezVous;
        MettreAJourBoutons();
    }

    private void ViderFiche()
    {
        txtReference.Clear();
        txtNom.Clear();
        txtPrenom.Clear();
        txtTelephone.Clear();
        txtAge.Clear();
        cmbStatut.SelectedIndex = 0;
        dtpDateRdv.Value = DateParDefaut();
        MettreAJourBoutons();
    }

    private static DateTime DateParDefaut()
    {
        var maintenant = DateTime.Now.AddHours(1);
        return new DateTime(maintenant.Year, maintenant.Month, maintenant.Day, maintenant.Hour, 0, 0);
    }

    private bool EssayerLireSaisie(out SaisiePatient saisie)
    {
        saisie = null!;

        if (txtNom.Text.Trim().Length == 0)
        {
            AfficherMessage("Le nom est obligatoire.", erreur: true);
            txtNom.Focus();
            return false;
        }

        if (txtPrenom.Text.Trim().Length == 0)
        {
            AfficherMessage("Le prénom est obligatoire.", erreur: true);
            txtPrenom.Focus();
            return false;
        }

        if (!int.TryParse(txtAge.Text.Trim(), NumberStyles.Integer, CultureInfo.CurrentCulture, out var age))
        {
            AfficherMessage("L'âge doit être un nombre entier.", erreur: true);
            txtAge.Focus();
            return false;
        }

        var statut = cmbStatut.SelectedValue is StatutRendezVous valeur
            ? valeur
            : StatutRendezVous.EnAttente;

        saisie = new SaisiePatient(
            Nom: txtNom.Text.Trim(),
            Prenom: txtPrenom.Text.Trim(),
            Telephone: txtTelephone.Text.Trim(),
            Age: age,
            Statut: statut,
            DateRendezVous: dtpDateRdv.Value);

        return true;
    }

    /* ---------------------------------------------------------------------
       Liste, recherche et tri
       --------------------------------------------------------------------- */

    private void RafraichirListe()
    {
        if (!interfacePrete)
        {
            return;
        }

        var champ = cmbChampRecherche.SelectedValue is ChampRecherche champSelectionne
            ? champSelectionne
            : ChampRecherche.Tous;

        var critere = cmbTri.SelectedValue is ChampTri triSelectionne
            ? triSelectionne
            : ChampTri.Nom;

        var resultat = service.Trier(
            service.Filtrer(txtRecherche.Text, champ),
            critere,
            triDescendant);

        dgvPatients.DataSource = resultat.ToList();
        dgvPatients.ClearSelection();

        lblVide.Visible = resultat.Count == 0;
        if (lblVide.Visible)
        {
            lblVide.Text = service.Patients.Count == 0
                ? "Aucun patient enregistré.\r\nRenseignez la fiche à gauche puis cliquez sur « Ajouter »."
                : "Aucun patient ne correspond à votre recherche.\r\nModifiez le texte ou cliquez sur « Réinitialiser ».";
            lblVide.BringToFront();
        }

        var total = service.Patients.Count;
        lblTotal.Text = $"{total} patient{(total > 1 ? "s" : string.Empty)}";
        lblResultat.Text = resultat.Count == total
            ? $"{total} patient{(total > 1 ? "s" : string.Empty)} affiché{(total > 1 ? "s" : string.Empty)}"
            : $"{resultat.Count} résultat{(resultat.Count > 1 ? "s" : string.Empty)} sur {total}";

        MettreAJourBoutons();
    }

    private void MettreAJourBoutons()
    {
        var reference = txtReference.Text.Trim();
        var patientSelectionne = reference.Length > 0;

        btnModifier.Enabled = patientSelectionne;
        btnSupprimer.Enabled = patientSelectionne;
        btnEnregistrer.Text = modificationsEnAttente ? "Enregistrer •" : "Enregistrer";
        btnEnregistrer.Enabled = modificationsEnAttente;
    }

    private void Selectionner(string reference)
    {
        foreach (DataGridViewRow ligne in dgvPatients.Rows)
        {
            if (ligne.DataBoundItem is Patient patient &&
                string.Equals(patient.Reference, reference, StringComparison.OrdinalIgnoreCase))
            {
                ligne.Selected = true;
                dgvPatients.CurrentCell = ligne.Cells[0];
                return;
            }
        }
    }

    private void txtRecherche_TextChanged(object? sender, EventArgs e) => RafraichirListe();

    private void filtre_SelectedIndexChanged(object? sender, EventArgs e) => RafraichirListe();

    private void btnRechercher_Click(object? sender, EventArgs e) => RafraichirListe();

    private void btnReinitialiser_Click(object? sender, EventArgs e)
    {
        txtRecherche.Clear();
        cmbChampRecherche.SelectedIndex = 0;
        cmbTri.SelectedIndex = 0;
        triDescendant = false;
        MettreAJourBoutonTri();
        RafraichirListe();
        txtRecherche.Focus();
    }

    private void btnInverserTri_Click(object? sender, EventArgs e)
    {
        triDescendant = !triDescendant;
        MettreAJourBoutonTri();
        RafraichirListe();
    }

    private void MettreAJourBoutonTri() =>
        btnInverserTri.Text = triDescendant ? "Décroissant" : "Croissant";

    private void dgvPatients_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        if (dgvPatients.Rows[e.RowIndex].DataBoundItem is Patient patient)
        {
            RemplirFiche(patient);
        }
    }

    private void dgvPatients_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex != indexColonneStatut)
        {
            return;
        }

        if (dgvPatients.Rows[e.RowIndex].DataBoundItem is not Patient patient)
        {
            return;
        }

        if (CouleursStatut.TryGetValue(patient.Statut, out var couleur))
        {
            e.CellStyle.ForeColor = couleur;
            e.CellStyle.SelectionForeColor = couleur;
            e.CellStyle.Font = PoliceStatut;
        }
    }

    /* ---------------------------------------------------------------------
       Statistiques et raccourcis
       --------------------------------------------------------------------- */

    private void btnStatistiques_Click(object? sender, EventArgs e)
    {
        var statistiques = service.CalculerStatistiques();

        if (statistiques.Total == 0)
        {
            MessageBox.Show(
                "Aucun patient n'est enregistré pour le moment.",
                "Statistiques",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        var lignes = new List<string>
        {
            $"Patientèle : {statistiques.Total} patient(s)",
            $"Âge moyen : {statistiques.AgeMoyen:F1} ans".Replace('.', ','),
            string.Empty,
            "Tranches d'âge",
            $"• 0 – 18 ans : {statistiques.Juniors}",
            $"• 19 – 35 ans : {statistiques.Adultes}",
            $"• 36 ans et + : {statistiques.Seniors}",
            string.Empty,
            "Rendez-vous par statut"
        };

        lignes.AddRange(
            StatutRendezVousExtensions.Tous.Select(statut =>
                $"• {statut.Libelle()} : {statistiques.ParStatut.GetValueOrDefault(statut)}"));

        lignes.Add(string.Empty);
        lignes.Add(statistiques.ProchainRendezVous is { } prochain
            ? $"Prochain rendez-vous : {prochain.ToString("dddd d MMMM yyyy 'à' HH:mm", CultureFr)}"
            : "Prochain rendez-vous : aucun rendez-vous à venir.");

        MessageBox.Show(
            string.Join(Environment.NewLine, lignes),
            "Statistiques du cabinet",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    private void MainForm_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.S)
        {
            e.SuppressKeyPress = true;
            Enregistrer();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            e.SuppressKeyPress = true;
            ViderFiche();
            txtNom.Focus();
        }
        else if (e.KeyCode == Keys.F5)
        {
            e.SuppressKeyPress = true;
            ChargerPatients();
        }
        else if (e.KeyCode == Keys.Delete && dgvPatients.ContainsFocus)
        {
            e.SuppressKeyPress = true;
            btnSupprimer_Click(sender, EventArgs.Empty);
        }
    }

    /// <summary>Affiche un message dans la barre d'état, en rouge si erreur.</summary>
    private void AfficherMessage(string texte, bool erreur = false)
    {
        lblMessage.Text = texte;
        lblMessage.ForeColor = erreur
            ? Color.FromArgb(185, 28, 28)
            : Color.FromArgb(100, 116, 139);
        lblMessage.ToolTipText = texte;
    }

    /// <summary>Raccourcit un chemin pour la barre d'état.</summary>
    private static string Raccourcir(string chemin)
    {
        var profil = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        return chemin.StartsWith(profil, StringComparison.OrdinalIgnoreCase)
            ? "%USERPROFILE%" + chemin[profil.Length..]
            : chemin;
    }
}
