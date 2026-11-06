namespace GestionDentaire;

/// <summary>Point d'entrée de l'application WinForms.</summary>
internal static class Program
{
    /// <summary>Lance la fenêtre principale.</summary>
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
