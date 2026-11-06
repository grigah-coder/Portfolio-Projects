#nullable disable

namespace GestionDentaire
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        private void InitializeComponent()
        {
            this.panelEntete = new System.Windows.Forms.Panel();
            this.panelAccent = new System.Windows.Forms.Panel();
            this.lblTitre = new System.Windows.Forms.Label();
            this.lblSousTitre = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.tableCorps = new System.Windows.Forms.TableLayoutPanel();
            this.panneauFiche = new System.Windows.Forms.Panel();
            this.tableFiche = new System.Windows.Forms.TableLayoutPanel();
            this.lblSectionFiche = new System.Windows.Forms.Label();
            this.lblAide = new System.Windows.Forms.Label();
            this.lblReference = new System.Windows.Forms.Label();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.lblNom = new System.Windows.Forms.Label();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.lblPrenom = new System.Windows.Forms.Label();
            this.txtPrenom = new System.Windows.Forms.TextBox();
            this.lblTelephone = new System.Windows.Forms.Label();
            this.txtTelephone = new System.Windows.Forms.TextBox();
            this.lblAge = new System.Windows.Forms.Label();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.lblStatut = new System.Windows.Forms.Label();
            this.cmbStatut = new System.Windows.Forms.ComboBox();
            this.lblDateRdv = new System.Windows.Forms.Label();
            this.dtpDateRdv = new System.Windows.Forms.DateTimePicker();
            this.flowActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.btnModifier = new System.Windows.Forms.Button();
            this.btnSupprimer = new System.Windows.Forms.Button();
            this.btnNouveau = new System.Windows.Forms.Button();
            this.btnEnregistrer = new System.Windows.Forms.Button();
            this.btnRecharger = new System.Windows.Forms.Button();
            this.panneauListe = new System.Windows.Forms.Panel();
            this.tableListe = new System.Windows.Forms.TableLayoutPanel();
            this.tableOutils = new System.Windows.Forms.TableLayoutPanel();
            this.txtRecherche = new System.Windows.Forms.TextBox();
            this.cmbChampRecherche = new System.Windows.Forms.ComboBox();
            this.btnRechercher = new System.Windows.Forms.Button();
            this.btnReinitialiser = new System.Windows.Forms.Button();
            this.cmbTri = new System.Windows.Forms.ComboBox();
            this.btnInverserTri = new System.Windows.Forms.Button();
            this.lblResultat = new System.Windows.Forms.Label();
            this.btnStatistiques = new System.Windows.Forms.Button();
            this.dgvPatients = new System.Windows.Forms.DataGridView();
            this.lblVide = new System.Windows.Forms.Label();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.lblTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblChemin = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelEntete.SuspendLayout();
            this.tableCorps.SuspendLayout();
            this.panneauFiche.SuspendLayout();
            this.tableFiche.SuspendLayout();
            this.flowActions.SuspendLayout();
            this.panneauListe.SuspendLayout();
            this.tableListe.SuspendLayout();
            this.tableOutils.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatients)).BeginInit();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEntete
            // 
            this.panelEntete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.panelEntete.Controls.Add(this.panelAccent);
            this.panelEntete.Controls.Add(this.lblTitre);
            this.panelEntete.Controls.Add(this.lblSousTitre);
            this.panelEntete.Controls.Add(this.lblDate);
            this.panelEntete.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEntete.Location = new System.Drawing.Point(0, 0);
            this.panelEntete.Name = "panelEntete";
            this.panelEntete.Size = new System.Drawing.Size(1280, 84);
            this.panelEntete.TabIndex = 0;
            // 
            // panelAccent
            // 
            this.panelAccent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(182)))), ((int)(((byte)(212)))));
            this.panelAccent.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelAccent.Location = new System.Drawing.Point(0, 0);
            this.panelAccent.Name = "panelAccent";
            this.panelAccent.Size = new System.Drawing.Size(4, 84);
            this.panelAccent.TabIndex = 0;
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.ForeColor = System.Drawing.Color.White;
            this.lblTitre.Location = new System.Drawing.Point(24, 15);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(214, 36);
            this.lblTitre.TabIndex = 1;
            this.lblTitre.Text = "Gestion Dentaire";
            // 
            // lblSousTitre
            // 
            this.lblSousTitre.AutoSize = true;
            this.lblSousTitre.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSousTitre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSousTitre.Location = new System.Drawing.Point(27, 53);
            this.lblSousTitre.Name = "lblSousTitre";
            this.lblSousTitre.Size = new System.Drawing.Size(347, 17);
            this.lblSousTitre.TabIndex = 2;
            this.lblSousTitre.Text = "Cabinet dentaire · dossier patients et suivi des rendez-vous";
            // 
            // lblDate
            // 
            this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblDate.Location = new System.Drawing.Point(948, 32);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(308, 20);
            this.lblDate.TabIndex = 3;
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableCorps
            // 
            this.tableCorps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.tableCorps.ColumnCount = 2;
            this.tableCorps.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 416F));
            this.tableCorps.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableCorps.Controls.Add(this.panneauFiche, 0, 0);
            this.tableCorps.Controls.Add(this.panneauListe, 1, 0);
            this.tableCorps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableCorps.Location = new System.Drawing.Point(0, 84);
            this.tableCorps.Name = "tableCorps";
            this.tableCorps.Padding = new System.Windows.Forms.Padding(16);
            this.tableCorps.RowCount = 1;
            this.tableCorps.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableCorps.Size = new System.Drawing.Size(1280, 650);
            this.tableCorps.TabIndex = 1;
            // 
            // panneauFiche
            // 
            this.panneauFiche.AutoScroll = true;
            this.panneauFiche.BackColor = System.Drawing.Color.White;
            this.panneauFiche.Controls.Add(this.tableFiche);
            this.panneauFiche.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panneauFiche.Location = new System.Drawing.Point(19, 19);
            this.panneauFiche.Margin = new System.Windows.Forms.Padding(3, 3, 16, 3);
            this.panneauFiche.Name = "panneauFiche";
            this.panneauFiche.Padding = new System.Windows.Forms.Padding(20);
            this.panneauFiche.Size = new System.Drawing.Size(397, 612);
            this.panneauFiche.TabIndex = 0;
            // 
            // tableFiche
            // 
            this.tableFiche.ColumnCount = 2;
            this.tableFiche.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableFiche.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableFiche.Controls.Add(this.lblSectionFiche, 0, 0);
            this.tableFiche.Controls.Add(this.lblAide, 0, 1);
            this.tableFiche.Controls.Add(this.lblReference, 0, 2);
            this.tableFiche.Controls.Add(this.txtReference, 0, 3);
            this.tableFiche.Controls.Add(this.lblNom, 0, 4);
            this.tableFiche.Controls.Add(this.lblPrenom, 1, 4);
            this.tableFiche.Controls.Add(this.txtNom, 0, 5);
            this.tableFiche.Controls.Add(this.txtPrenom, 1, 5);
            this.tableFiche.Controls.Add(this.lblTelephone, 0, 6);
            this.tableFiche.Controls.Add(this.lblAge, 1, 6);
            this.tableFiche.Controls.Add(this.txtTelephone, 0, 7);
            this.tableFiche.Controls.Add(this.txtAge, 1, 7);
            this.tableFiche.Controls.Add(this.lblStatut, 0, 8);
            this.tableFiche.Controls.Add(this.lblDateRdv, 1, 8);
            this.tableFiche.Controls.Add(this.cmbStatut, 0, 9);
            this.tableFiche.Controls.Add(this.dtpDateRdv, 1, 9);
            this.tableFiche.Controls.Add(this.flowActions, 0, 10);
            this.tableFiche.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableFiche.Location = new System.Drawing.Point(20, 20);
            this.tableFiche.Name = "tableFiche";
            this.tableFiche.RowCount = 11;
            this.tableFiche.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableFiche.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableFiche.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableFiche.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableFiche.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableFiche.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableFiche.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableFiche.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableFiche.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableFiche.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableFiche.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableFiche.SetColumnSpan(this.lblSectionFiche, 2);
            this.tableFiche.SetColumnSpan(this.lblAide, 2);
            this.tableFiche.SetColumnSpan(this.lblReference, 2);
            this.tableFiche.SetColumnSpan(this.txtReference, 2);
            this.tableFiche.SetColumnSpan(this.flowActions, 2);
            this.tableFiche.Size = new System.Drawing.Size(357, 572);
            this.tableFiche.TabIndex = 0;
            // 
            // lblSectionFiche
            // 
            this.lblSectionFiche.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSectionFiche.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSectionFiche.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSectionFiche.Location = new System.Drawing.Point(3, 0);
            this.lblSectionFiche.Name = "lblSectionFiche";
            this.lblSectionFiche.Size = new System.Drawing.Size(351, 32);
            this.lblSectionFiche.TabIndex = 0;
            this.lblSectionFiche.Text = "Fiche patient";
            this.lblSectionFiche.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblAide
            // 
            this.lblAide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAide.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAide.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAide.Location = new System.Drawing.Point(3, 32);
            this.lblAide.Name = "lblAide";
            this.lblAide.Size = new System.Drawing.Size(351, 34);
            this.lblAide.TabIndex = 1;
            this.lblAide.Text = "Sélectionnez un patient dans la liste ou créez une nouvelle fiche.";
            this.lblAide.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblReference
            // 
            this.lblReference.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReference.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReference.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblReference.Location = new System.Drawing.Point(3, 66);
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(351, 22);
            this.lblReference.TabIndex = 2;
            this.lblReference.Text = "Référence";
            this.lblReference.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtReference
            // 
            this.txtReference.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtReference.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReference.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReference.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReference.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.txtReference.Location = new System.Drawing.Point(3, 91);
            this.txtReference.Name = "txtReference";
            this.txtReference.PlaceholderText = "Générée automatiquement à l'ajout";
            this.txtReference.ReadOnly = true;
            this.txtReference.Size = new System.Drawing.Size(351, 23);
            this.txtReference.TabIndex = 3;
            this.txtReference.TabStop = false;
            // 
            // lblNom
            // 
            this.lblNom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblNom.Location = new System.Drawing.Point(3, 118);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(351, 22);
            this.lblNom.TabIndex = 4;
            this.lblNom.Text = "Nom *";
            this.lblNom.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtNom
            // 
            this.txtNom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNom.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNom.Location = new System.Drawing.Point(3, 143);
            this.txtNom.MaxLength = 60;
            this.txtNom.Name = "txtNom";
            this.txtNom.PlaceholderText = "Ex. Alaoui";
            this.txtNom.Size = new System.Drawing.Size(351, 25);
            this.txtNom.TabIndex = 5;
            // 
            // lblPrenom
            // 
            this.lblPrenom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrenom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrenom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPrenom.Location = new System.Drawing.Point(3, 170);
            this.lblPrenom.Name = "lblPrenom";
            this.lblPrenom.Size = new System.Drawing.Size(351, 22);
            this.lblPrenom.TabIndex = 6;
            this.lblPrenom.Text = "Prénom *";
            this.lblPrenom.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtPrenom
            // 
            this.txtPrenom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrenom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPrenom.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrenom.Location = new System.Drawing.Point(3, 195);
            this.txtPrenom.MaxLength = 60;
            this.txtPrenom.Name = "txtPrenom";
            this.txtPrenom.PlaceholderText = "Ex. Yasmine";
            this.txtPrenom.Size = new System.Drawing.Size(351, 25);
            this.txtPrenom.TabIndex = 7;
            // 
            // lblTelephone
            // 
            this.lblTelephone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTelephone.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTelephone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTelephone.Location = new System.Drawing.Point(3, 222);
            this.lblTelephone.Name = "lblTelephone";
            this.lblTelephone.Size = new System.Drawing.Size(351, 22);
            this.lblTelephone.TabIndex = 8;
            this.lblTelephone.Text = "Téléphone";
            this.lblTelephone.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtTelephone
            // 
            this.txtTelephone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelephone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTelephone.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelephone.Location = new System.Drawing.Point(3, 247);
            this.txtTelephone.MaxLength = 25;
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.PlaceholderText = "Ex. 06 12 34 56 78";
            this.txtTelephone.Size = new System.Drawing.Size(351, 25);
            this.txtTelephone.TabIndex = 9;
            // 
            // lblAge
            // 
            this.lblAge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAge.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblAge.Location = new System.Drawing.Point(3, 274);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(351, 22);
            this.lblAge.TabIndex = 10;
            this.lblAge.Text = "Âge *";
            this.lblAge.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtAge
            // 
            this.txtAge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAge.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAge.Location = new System.Drawing.Point(3, 299);
            this.txtAge.MaxLength = 3;
            this.txtAge.Name = "txtAge";
            this.txtAge.PlaceholderText = "Ex. 34";
            this.txtAge.Size = new System.Drawing.Size(351, 25);
            this.txtAge.TabIndex = 11;
            // 
            // lblStatut
            // 
            this.lblStatut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatut.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStatut.Location = new System.Drawing.Point(3, 326);
            this.lblStatut.Name = "lblStatut";
            this.lblStatut.Size = new System.Drawing.Size(351, 22);
            this.lblStatut.TabIndex = 12;
            this.lblStatut.Text = "Statut du rendez-vous";
            this.lblStatut.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // cmbStatut
            // 
            this.cmbStatut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbStatut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatut.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatut.FormattingEnabled = true;
            this.cmbStatut.IntegralHeight = false;
            this.cmbStatut.Location = new System.Drawing.Point(3, 351);
            this.cmbStatut.Name = "cmbStatut";
            this.cmbStatut.Size = new System.Drawing.Size(351, 25);
            this.cmbStatut.TabIndex = 13;
            // 
            // lblDateRdv
            // 
            this.lblDateRdv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDateRdv.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateRdv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblDateRdv.Location = new System.Drawing.Point(3, 378);
            this.lblDateRdv.Name = "lblDateRdv";
            this.lblDateRdv.Size = new System.Drawing.Size(351, 22);
            this.lblDateRdv.TabIndex = 14;
            this.lblDateRdv.Text = "Date et heure du rendez-vous";
            this.lblDateRdv.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // dtpDateRdv
            // 
            this.dtpDateRdv.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpDateRdv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpDateRdv.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateRdv.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateRdv.Location = new System.Drawing.Point(3, 403);
            this.dtpDateRdv.Name = "dtpDateRdv";
            this.dtpDateRdv.ShowUpDown = true;
            this.dtpDateRdv.Size = new System.Drawing.Size(351, 25);
            this.dtpDateRdv.TabIndex = 15;
            // 
            // flowActions
            // 
            this.flowActions.Controls.Add(this.btnAjouter);
            this.flowActions.Controls.Add(this.btnModifier);
            this.flowActions.Controls.Add(this.btnSupprimer);
            this.flowActions.Controls.Add(this.btnNouveau);
            this.flowActions.Controls.Add(this.btnEnregistrer);
            this.flowActions.Controls.Add(this.btnRecharger);
            this.flowActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowActions.Location = new System.Drawing.Point(0, 431);
            this.flowActions.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.flowActions.Name = "flowActions";
            this.flowActions.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.flowActions.Size = new System.Drawing.Size(357, 141);
            this.flowActions.TabIndex = 16;
            // 
            // btnAjouter
            // 
            this.btnAjouter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(116)))), ((int)(((byte)(144)))));
            this.btnAjouter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAjouter.FlatAppearance.BorderSize = 0;
            this.btnAjouter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(94)))), ((int)(((byte)(117)))));
            this.btnAjouter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAjouter.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAjouter.ForeColor = System.Drawing.Color.White;
            this.btnAjouter.Location = new System.Drawing.Point(0, 8);
            this.btnAjouter.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(168, 38);
            this.btnAjouter.TabIndex = 0;
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.UseVisualStyleBackColor = false;
            // 
            // btnModifier
            // 
            this.btnModifier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnModifier.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModifier.FlatAppearance.BorderSize = 0;
            this.btnModifier.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnModifier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModifier.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModifier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnModifier.Location = new System.Drawing.Point(176, 8);
            this.btnModifier.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(168, 38);
            this.btnModifier.TabIndex = 1;
            this.btnModifier.Text = "Modifier";
            this.btnModifier.UseVisualStyleBackColor = false;
            // 
            // btnSupprimer
            // 
            this.btnSupprimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnSupprimer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSupprimer.FlatAppearance.BorderSize = 0;
            this.btnSupprimer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.btnSupprimer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSupprimer.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupprimer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnSupprimer.Location = new System.Drawing.Point(0, 54);
            this.btnSupprimer.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.btnSupprimer.Name = "btnSupprimer";
            this.btnSupprimer.Size = new System.Drawing.Size(168, 38);
            this.btnSupprimer.TabIndex = 2;
            this.btnSupprimer.Text = "Supprimer";
            this.btnSupprimer.UseVisualStyleBackColor = false;
            // 
            // btnNouveau
            // 
            this.btnNouveau.BackColor = System.Drawing.Color.White;
            this.btnNouveau.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNouveau.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnNouveau.FlatAppearance.BorderSize = 1;
            this.btnNouveau.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNouveau.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNouveau.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNouveau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnNouveau.Location = new System.Drawing.Point(176, 54);
            this.btnNouveau.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.btnNouveau.Name = "btnNouveau";
            this.btnNouveau.Size = new System.Drawing.Size(168, 38);
            this.btnNouveau.TabIndex = 3;
            this.btnNouveau.Text = "Nouvelle fiche";
            this.btnNouveau.UseVisualStyleBackColor = false;
            // 
            // btnEnregistrer
            // 
            this.btnEnregistrer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnEnregistrer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnregistrer.FlatAppearance.BorderSize = 0;
            this.btnEnregistrer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnEnregistrer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnregistrer.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnregistrer.ForeColor = System.Drawing.Color.White;
            this.btnEnregistrer.Location = new System.Drawing.Point(0, 100);
            this.btnEnregistrer.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.btnEnregistrer.Name = "btnEnregistrer";
            this.btnEnregistrer.Size = new System.Drawing.Size(168, 38);
            this.btnEnregistrer.TabIndex = 4;
            this.btnEnregistrer.Text = "Enregistrer";
            this.btnEnregistrer.UseVisualStyleBackColor = false;
            // 
            // btnRecharger
            // 
            this.btnRecharger.BackColor = System.Drawing.Color.White;
            this.btnRecharger.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRecharger.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnRecharger.FlatAppearance.BorderSize = 1;
            this.btnRecharger.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnRecharger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecharger.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecharger.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnRecharger.Location = new System.Drawing.Point(176, 100);
            this.btnRecharger.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.btnRecharger.Name = "btnRecharger";
            this.btnRecharger.Size = new System.Drawing.Size(168, 38);
            this.btnRecharger.TabIndex = 5;
            this.btnRecharger.Text = "Recharger";
            this.btnRecharger.UseVisualStyleBackColor = false;
            // 
            // panneauListe
            // 
            this.panneauListe.BackColor = System.Drawing.Color.White;
            this.panneauListe.Controls.Add(this.tableListe);
            this.panneauListe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panneauListe.Location = new System.Drawing.Point(435, 19);
            this.panneauListe.Margin = new System.Windows.Forms.Padding(3);
            this.panneauListe.Name = "panneauListe";
            this.panneauListe.Padding = new System.Windows.Forms.Padding(20, 16, 20, 20);
            this.panneauListe.Size = new System.Drawing.Size(826, 612);
            this.panneauListe.TabIndex = 1;
            // 
            // tableListe
            // 
            this.tableListe.ColumnCount = 1;
            this.tableListe.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableListe.Controls.Add(this.tableOutils, 0, 0);
            this.tableListe.Controls.Add(this.dgvPatients, 0, 1);
            this.tableListe.Controls.Add(this.lblVide, 0, 1);
            this.tableListe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableListe.Location = new System.Drawing.Point(20, 16);
            this.tableListe.Name = "tableListe";
            this.tableListe.RowCount = 2;
            this.tableListe.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tableListe.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableListe.Size = new System.Drawing.Size(786, 576);
            this.tableListe.TabIndex = 0;
            // 
            // tableOutils
            // 
            this.tableOutils.ColumnCount = 6;
            this.tableOutils.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.6667F));
            this.tableOutils.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.6667F));
            this.tableOutils.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.6667F));
            this.tableOutils.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.6667F));
            this.tableOutils.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.6667F));
            this.tableOutils.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.6665F));
            this.tableOutils.Controls.Add(this.txtRecherche, 0, 0);
            this.tableOutils.Controls.Add(this.cmbChampRecherche, 3, 0);
            this.tableOutils.Controls.Add(this.btnRechercher, 4, 0);
            this.tableOutils.Controls.Add(this.btnReinitialiser, 5, 0);
            this.tableOutils.Controls.Add(this.cmbTri, 0, 1);
            this.tableOutils.Controls.Add(this.btnInverserTri, 2, 1);
            this.tableOutils.Controls.Add(this.lblResultat, 3, 1);
            this.tableOutils.Controls.Add(this.btnStatistiques, 5, 1);
            this.tableOutils.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableOutils.Location = new System.Drawing.Point(0, 0);
            this.tableOutils.Margin = new System.Windows.Forms.Padding(0);
            this.tableOutils.Name = "tableOutils";
            this.tableOutils.RowCount = 2;
            this.tableOutils.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableOutils.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableOutils.SetColumnSpan(this.txtRecherche, 3);
            this.tableOutils.SetColumnSpan(this.cmbTri, 2);
            this.tableOutils.SetColumnSpan(this.lblResultat, 2);
            this.tableOutils.Size = new System.Drawing.Size(786, 96);
            this.tableOutils.TabIndex = 0;
            // 
            // txtRecherche
            // 
            this.txtRecherche.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRecherche.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRecherche.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecherche.Location = new System.Drawing.Point(3, 3);
            this.txtRecherche.Name = "txtRecherche";
            this.txtRecherche.PlaceholderText = "Rechercher un patient (nom, téléphone, référence…)";
            this.txtRecherche.Size = new System.Drawing.Size(387, 25);
            this.txtRecherche.TabIndex = 0;
            // 
            // cmbChampRecherche
            // 
            this.cmbChampRecherche.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbChampRecherche.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChampRecherche.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChampRecherche.FormattingEnabled = true;
            this.cmbChampRecherche.IntegralHeight = false;
            this.cmbChampRecherche.Location = new System.Drawing.Point(395, 3);
            this.cmbChampRecherche.Margin = new System.Windows.Forms.Padding(2, 3, 8, 8);
            this.cmbChampRecherche.Name = "cmbChampRecherche";
            this.cmbChampRecherche.Size = new System.Drawing.Size(120, 25);
            this.cmbChampRecherche.TabIndex = 1;
            // 
            // btnRechercher
            // 
            this.btnRechercher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(116)))), ((int)(((byte)(144)))));
            this.btnRechercher.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRechercher.FlatAppearance.BorderSize = 0;
            this.btnRechercher.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(94)))), ((int)(((byte)(117)))));
            this.btnRechercher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRechercher.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRechercher.ForeColor = System.Drawing.Color.White;
            this.btnRechercher.Location = new System.Drawing.Point(525, 3);
            this.btnRechercher.Margin = new System.Windows.Forms.Padding(2, 3, 8, 8);
            this.btnRechercher.Name = "btnRechercher";
            this.btnRechercher.Size = new System.Drawing.Size(120, 33);
            this.btnRechercher.TabIndex = 2;
            this.btnRechercher.Text = "Rechercher";
            this.btnRechercher.UseVisualStyleBackColor = false;
            // 
            // btnReinitialiser
            // 
            this.btnReinitialiser.BackColor = System.Drawing.Color.White;
            this.btnReinitialiser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReinitialiser.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnReinitialiser.FlatAppearance.BorderSize = 1;
            this.btnReinitialiser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnReinitialiser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReinitialiser.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReinitialiser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnReinitialiser.Location = new System.Drawing.Point(655, 3);
            this.btnReinitialiser.Margin = new System.Windows.Forms.Padding(2, 3, 0, 8);
            this.btnReinitialiser.Name = "btnReinitialiser";
            this.btnReinitialiser.Size = new System.Drawing.Size(131, 33);
            this.btnReinitialiser.TabIndex = 3;
            this.btnReinitialiser.Text = "Réinitialiser";
            this.btnReinitialiser.UseVisualStyleBackColor = false;
            // 
            // cmbTri
            // 
            this.cmbTri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTri.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTri.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTri.FormattingEnabled = true;
            this.cmbTri.IntegralHeight = false;
            this.cmbTri.Location = new System.Drawing.Point(3, 47);
            this.cmbTri.Name = "cmbTri";
            this.cmbTri.Size = new System.Drawing.Size(256, 25);
            this.cmbTri.TabIndex = 4;
            // 
            // btnInverserTri
            // 
            this.btnInverserTri.BackColor = System.Drawing.Color.White;
            this.btnInverserTri.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInverserTri.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnInverserTri.FlatAppearance.BorderSize = 1;
            this.btnInverserTri.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnInverserTri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInverserTri.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInverserTri.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnInverserTri.Location = new System.Drawing.Point(264, 47);
            this.btnInverserTri.Margin = new System.Windows.Forms.Padding(2, 3, 8, 8);
            this.btnInverserTri.Name = "btnInverserTri";
            this.btnInverserTri.Size = new System.Drawing.Size(120, 33);
            this.btnInverserTri.TabIndex = 5;
            this.btnInverserTri.Text = "Croissant";
            this.btnInverserTri.UseVisualStyleBackColor = false;
            // 
            // lblResultat
            // 
            this.lblResultat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResultat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblResultat.Location = new System.Drawing.Point(397, 47);
            this.lblResultat.Margin = new System.Windows.Forms.Padding(5, 3, 8, 8);
            this.lblResultat.Name = "lblResultat";
            this.lblResultat.Size = new System.Drawing.Size(248, 33);
            this.lblResultat.TabIndex = 6;
            this.lblResultat.Text = "0 patient affiché";
            this.lblResultat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnStatistiques
            // 
            this.btnStatistiques.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnStatistiques.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStatistiques.FlatAppearance.BorderSize = 0;
            this.btnStatistiques.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnStatistiques.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatistiques.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStatistiques.ForeColor = System.Drawing.Color.White;
            this.btnStatistiques.Location = new System.Drawing.Point(655, 47);
            this.btnStatistiques.Margin = new System.Windows.Forms.Padding(2, 3, 0, 8);
            this.btnStatistiques.Name = "btnStatistiques";
            this.btnStatistiques.Size = new System.Drawing.Size(131, 33);
            this.btnStatistiques.TabIndex = 7;
            this.btnStatistiques.Text = "Statistiques";
            this.btnStatistiques.UseVisualStyleBackColor = false;
            // 
            // dgvPatients
            // 
            this.dgvPatients.AllowUserToAddRows = false;
            this.dgvPatients.AllowUserToDeleteRows = false;
            this.dgvPatients.AllowUserToResizeRows = false;
            this.dgvPatients.BackgroundColor = System.Drawing.Color.White;
            this.dgvPatients.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPatients.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPatients.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvPatients.ColumnHeadersHeight = 44;
            this.dgvPatients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPatients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPatients.EnableHeadersVisualStyles = false;
            this.dgvPatients.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.dgvPatients.Location = new System.Drawing.Point(0, 96);
            this.dgvPatients.Margin = new System.Windows.Forms.Padding(0);
            this.dgvPatients.MultiSelect = false;
            this.dgvPatients.Name = "dgvPatients";
            this.dgvPatients.ReadOnly = true;
            this.dgvPatients.RowHeadersVisible = false;
            this.dgvPatients.RowHeadersWidth = 51;
            this.dgvPatients.RowTemplate.Height = 40;
            this.dgvPatients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPatients.Size = new System.Drawing.Size(786, 480);
            this.dgvPatients.TabIndex = 1;
            // 
            // lblVide
            // 
            this.lblVide.BackColor = System.Drawing.Color.White;
            this.lblVide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVide.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVide.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblVide.Location = new System.Drawing.Point(0, 96);
            this.lblVide.Margin = new System.Windows.Forms.Padding(0);
            this.lblVide.Name = "lblVide";
            this.lblVide.Size = new System.Drawing.Size(786, 480);
            this.lblVide.TabIndex = 2;
            this.lblVide.Text = "Aucun patient enregistré.\r\nRenseignez la fiche à gauche puis cliquez sur « Ajouter ».";
            this.lblVide.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVide.Visible = false;
            // 
            // statusBar
            // 
            this.statusBar.BackColor = System.Drawing.Color.White;
            this.statusBar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTotal,
            this.lblMessage,
            this.lblChemin});
            this.statusBar.Location = new System.Drawing.Point(0, 734);
            this.statusBar.Name = "statusBar";
            this.statusBar.Padding = new System.Windows.Forms.Padding(16, 3, 16, 3);
            this.statusBar.Size = new System.Drawing.Size(1280, 26);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 2;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTotal.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Text = "0 patient";
            // 
            // lblMessage
            // 
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Spring = true;
            this.lblMessage.Text = "Prêt.";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblChemin
            // 
            this.lblChemin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblChemin.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChemin.Name = "lblChemin";
            this.lblChemin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1280, 760);
            this.Controls.Add(this.tableCorps);
            this.Controls.Add(this.panelEntete);
            this.Controls.Add(this.statusBar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(1024, 640);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion Dentaire — Cabinet";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.btnAjouter.Click += new System.EventHandler(this.btnAjouter_Click);
            this.btnModifier.Click += new System.EventHandler(this.btnModifier_Click);
            this.btnSupprimer.Click += new System.EventHandler(this.btnSupprimer_Click);
            this.btnNouveau.Click += new System.EventHandler(this.btnNouveau_Click);
            this.btnEnregistrer.Click += new System.EventHandler(this.btnEnregistrer_Click);
            this.btnRecharger.Click += new System.EventHandler(this.btnRecharger_Click);
            this.btnRechercher.Click += new System.EventHandler(this.btnRechercher_Click);
            this.btnReinitialiser.Click += new System.EventHandler(this.btnReinitialiser_Click);
            this.btnInverserTri.Click += new System.EventHandler(this.btnInverserTri_Click);
            this.btnStatistiques.Click += new System.EventHandler(this.btnStatistiques_Click);
            this.txtRecherche.TextChanged += new System.EventHandler(this.txtRecherche_TextChanged);
            this.cmbChampRecherche.SelectedIndexChanged += new System.EventHandler(this.filtre_SelectedIndexChanged);
            this.cmbTri.SelectedIndexChanged += new System.EventHandler(this.filtre_SelectedIndexChanged);
            this.dgvPatients.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPatients_CellClick);
            this.dgvPatients.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPatients_CellFormatting);
            this.panelEntete.ResumeLayout(false);
            this.panelEntete.PerformLayout();
            this.tableCorps.ResumeLayout(false);
            this.panneauFiche.ResumeLayout(false);
            this.tableFiche.ResumeLayout(false);
            this.tableFiche.PerformLayout();
            this.flowActions.ResumeLayout(false);
            this.panneauListe.ResumeLayout(false);
            this.tableListe.ResumeLayout(false);
            this.tableOutils.ResumeLayout(false);
            this.tableOutils.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatients)).EndInit();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panelEntete;
        private System.Windows.Forms.Panel panelAccent;
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.Label lblSousTitre;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.TableLayoutPanel tableCorps;
        private System.Windows.Forms.Panel panneauFiche;
        private System.Windows.Forms.TableLayoutPanel tableFiche;
        private System.Windows.Forms.Label lblSectionFiche;
        private System.Windows.Forms.Label lblAide;
        private System.Windows.Forms.Label lblReference;
        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.Label lblPrenom;
        private System.Windows.Forms.TextBox txtPrenom;
        private System.Windows.Forms.Label lblTelephone;
        private System.Windows.Forms.TextBox txtTelephone;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label lblStatut;
        private System.Windows.Forms.ComboBox cmbStatut;
        private System.Windows.Forms.Label lblDateRdv;
        private System.Windows.Forms.DateTimePicker dtpDateRdv;
        private System.Windows.Forms.FlowLayoutPanel flowActions;
        private System.Windows.Forms.Button btnAjouter;
        private System.Windows.Forms.Button btnModifier;
        private System.Windows.Forms.Button btnSupprimer;
        private System.Windows.Forms.Button btnNouveau;
        private System.Windows.Forms.Button btnEnregistrer;
        private System.Windows.Forms.Button btnRecharger;
        private System.Windows.Forms.Panel panneauListe;
        private System.Windows.Forms.TableLayoutPanel tableListe;
        private System.Windows.Forms.TableLayoutPanel tableOutils;
        private System.Windows.Forms.TextBox txtRecherche;
        private System.Windows.Forms.ComboBox cmbChampRecherche;
        private System.Windows.Forms.Button btnRechercher;
        private System.Windows.Forms.Button btnReinitialiser;
        private System.Windows.Forms.ComboBox cmbTri;
        private System.Windows.Forms.Button btnInverserTri;
        private System.Windows.Forms.Label lblResultat;
        private System.Windows.Forms.Button btnStatistiques;
        private System.Windows.Forms.DataGridView dgvPatients;
        private System.Windows.Forms.Label lblVide;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel lblTotal;
        private System.Windows.Forms.ToolStripStatusLabel lblMessage;
        private System.Windows.Forms.ToolStripStatusLabel lblChemin;
    }
}
