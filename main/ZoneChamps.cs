using System.Reflection.Metadata.Ecma335;

public class ZoneChamps : ZoneInteractive
{

    public CelluleChamps[,] Grille { set; get; }
    public ZoneChamps(int colonne, int ligne, int largeur, int hauteur) : base(colonne, ligne, largeur, hauteur)
    {
        Grille = new CelluleChamps[largeur,hauteur];
        for (int indiceColonne = 0; indiceColonne < largeur; indiceColonne++)
        {
            for (int indiceLigne = 0; indiceLigne < hauteur; indiceLigne++)
            {
                Grille[indiceColonne, indiceLigne] = new CelluleChamps(new Parcelle("Carcassonne")); //besoin d'assigner une ville, mais recalculé à chaque début de partie lorsque le joueur fait son choix
            }
        }
        Curseur = 22;
    }
    public ZoneChamps(int colonne, int ligne, int largeur, int hauteur, Parcelle[,] grille) : this(colonne, ligne, largeur, hauteur)
    {
        Grille = new CelluleChamps[grille.GetLength(0), grille.GetLength(1)];
        Synchroniser(grille);
    }
    public void Synchroniser(Parcelle[,] grille)
    {
        for (int ligne = 0; ligne < Hauteur; ligne++)
            for (int colonne = 0; colonne < Largeur; colonne++)
                Grille[colonne, ligne] = new CelluleChamps(grille[colonne, ligne]);
    }
    public void Synchroniser(Parcelle parcelle, int colonne, int ligne)
    {
        Grille[colonne, ligne].Contenu = parcelle;
    }
    public override void RetournerEnArriere() { }
    public override void ValiderSelection() { }
    public override void DeplacerCurseur(string direction)
    {
        int nouveauCurseur = Curseur;

        if (direction == "haut" && Curseur / Largeur > 0) // le curseur n'est pas sur la première ligne
        {
            nouveauCurseur = Curseur - Largeur;
        }
        else if (direction == "bas" && Curseur / Largeur < Hauteur - 1)
        {
            nouveauCurseur = Curseur + Largeur;
        }
        else if (direction == "gauche" && Curseur > 0)
        {
            nouveauCurseur--;
        }
        else if (direction == "droite" && Curseur < (Largeur * Hauteur) - 1)
        {
            nouveauCurseur++;
        }

        if (nouveauCurseur != Curseur)
        {
            AfficherCelluleChamps(Curseur % Largeur, Curseur / Largeur);
            Curseur = nouveauCurseur;
            Afficher();
        }
    }
    public override void Afficher()
    {
        Console.BackgroundColor = ConsoleColor.DarkGreen;

        for (int ligne = 0; ligne < Hauteur; ligne++)
        {
            Console.SetCursorPosition(Position[0], Position[1] + ligne);

            for (int colonne = 0; colonne < Largeur; colonne++)
            {
                Console.Write(Grille[colonne, ligne].Contenu.Plant.Emoji);
            }
        }
        Console.ResetColor();
        AfficherCurseur();
    }
    public void AfficherCurseur()
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.SetCursorPosition(Position[0] + (Curseur % Largeur) * 2 , Position[1] + (Curseur / Largeur));
        Console.Write(Grille[Curseur % Largeur, Curseur / Largeur].Contenu.Plant.Emoji);
        Console.ResetColor();
    }
    public void AfficherCelluleChamps(int colonne, int ligne)
    {
        Console.BackgroundColor = Grille[colonne, ligne].CouleurFond;
        Console.SetCursorPosition(Position[0] + colonne*2 , Position[1] + ligne);
        Console.Write(Grille[colonne, ligne].Contenu.Plant.Emoji);
        Console.ResetColor();
    }
    
    public bool EstDansTerrain(int colonne, int ligne)
    {
        return (colonne >= 0) && (ligne >= 0) && (colonne < Grille.GetLength(0)) && (ligne < Grille.GetLength(1));
    }
}


public class CelluleChamps : CelluleAffichage
{
    public new Parcelle Contenu { set; get; } // on utilise new car le contenu devient une plante cette fois
    public bool Libre { set; get; }
    public List<string> NuisiblesActuels { get; set; }
    public CelluleChamps(Parcelle parcelle) : base()
    {
        Libre = true;
        Contenu = parcelle;
        CouleurFond = ConsoleColor.DarkGreen;
        NuisiblesActuels = new List<string>();
    }
    public bool NuisibleSemainePro(string nom)
    {
        return NuisiblesActuels.Contains(nom);
    }
}