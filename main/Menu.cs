using System.Runtime.CompilerServices;

public class Menu
{
    public int[] PositionAffichage { set; get; }
    public int[] PositionTableauMenus { set; get; }
    public string Nom { set; get; }
    public int Description { set; get; }
    private bool Actif { get; set; }
    public Menu? Parent { set; get; }

    public Menu(string nom, int[] positionAffichage, int[] positionTableauMenus, Func<string, string> Action)
    {
        Nom = nom;
        Actif = false;
        PositionAffichage = positionAffichage;
        PositionTableauMenus = positionTableauMenus;

    }


}