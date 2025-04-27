using System.Runtime.CompilerServices;

public class Menu
{
    public int PositionLigne { set; get; }
    public Menu? VoisinDroite {set;get;}
    public Menu? VoisinGauche {set;get;}
    public Menu? VoisinHaut {set;get;}
    public Menu? VoisinBas {set; get;}
    public string? Nom { set; get; }
    public int Description { set; get; }
    private bool Actif { get; set; }
    public Menu? Parent { set; get; }

    // public Menu(string nom, int[] positionAffichage, int[] positionTableauMenus)
    // {
    //     Nom = nom;
    //     Actif = false;
    //     PositionAffichage = positionAffichage;
    // }
    // public Menu(string nom, int[] positionAffichage, int[] positionTableauMenus, Menu voisinGauche) : this(nom,positionAffichage, positionTableauMenus){
    //     VoisinGauche = voisinGauche;
    // }
    // public Menu(string nom, int[] positionAffichage, int[] positionTableauMenus, Menu voisinGauche, Menu voisinDroite) : this(nom,positionAffichage, positionTableauMenus, voisinGauche){
    //     VoisinDroite = voisinGauche;
    
    // }

}

