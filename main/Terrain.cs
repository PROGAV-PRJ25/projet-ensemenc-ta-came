using System.Runtime.InteropServices;

public class Champs : ZoneInteractive
{

    public Parcelle[,] Parcelles { set; get; }
    public Champs(int colonne, int ligne, int largeur, int hauteur) : base(colonne, ligne, largeur, hauteur)
    {
        Parcelles = new Parcelle[largeur, hauteur];
        InitialiserParcelles();
        Curseur = 22;
    }
    private void InitialiserParcelles()
    {
        for (int ligne = 0; ligne < Hauteur; ligne++)
            for (int colonne = 0; colonne < Largeur; colonne++)
                Parcelles[ligne, colonne] = new Parcelle();
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
            AfficherParcelle(Curseur % Largeur, Curseur / Largeur);
            Curseur = nouveauCurseur;
            AfficherCurseur();
        }
    }
    public override void Afficher()
    {
        Console.BackgroundColor = ConsoleColor.DarkGreen;

        for (int ligne = 0; ligne < Hauteur; ligne++)
        {
            Console.SetCursorPosition(Position[0], Position[1] + ligne);

            for (int colonne = 0; colonne < Largeur; colonne++)
                Console.Write(Parcelles[ligne, colonne].Contenu.EMOJI);
        }
        Console.ResetColor();
        AfficherCurseur();
    }
    public void AfficherCurseur()
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.SetCursorPosition(Position[0]+ (Curseur % Largeur), Position[1] +(Curseur / Largeur));
        Console.Write(Parcelles[Curseur % Largeur, Curseur / Largeur].Contenu.EMOJI);
        Console.ResetColor();
    }
    public void AfficherParcelle(int colonne, int ligne)
    {
        Console.BackgroundColor = Parcelles[colonne, ligne].CouleurFond;
        Console.SetCursorPosition(Position[0] + colonne, Position[1] + ligne);
        Console.Write(Parcelles[colonne, ligne].Contenu.EMOJI);
        Console.ResetColor();
    }
    public void Ajouter(Plante semis, int[] position)
    {
        if (Parcelles[position[0], position[1]].Libre)
        {
            Parcelles[position[0], position[1]].Contenu = semis;
            AcutaliserEspaceLibre();
        }

    }
    public void Retirer(Plante plante)
    {
        //retirer une plante
    }
    public void AcutaliserEspaceLibre()
    {
        for (int x = 0; x < Parcelles.GetLength(0); x++)
            for (int y = 0; y < Parcelles.GetLength(1); y++)
                DiffuserEffets(x, y);
    }
    public void DiffuserEffets(int x, int y)
    {
        Parcelle parcelle = Parcelles[x, y];
        int portee = parcelle.Contenu.Espace;
        if (portee > 1)
        {
            for (int colonne = x - portee; colonne < x + 1 + portee; colonne++)
            {
                for (int ligne = y - portee; ligne < y + 1 + portee; ligne++)
                {
                    if (EstDansTerrain(colonne, ligne) && colonne != x && ligne != y)
                    {
                        parcelle.Libre = false;
                    }
                }
            }
        }

    }
    public bool EstDansTerrain(int colonne, int ligne)
    {
        return (colonne >= 0) && (ligne >= 0) && (colonne < Parcelles.GetLength(0)) && (ligne < Parcelles.GetLength(1));
    }
}


public class Parcelle : CelluleAffichage
{
    public new Plante Contenu { set; get; } // on utilise new car le contenu devient une plante cette fois
    public bool Libre { set; get; }
    public Parcelle() : base()
    {
        Libre = true;
        Contenu = new PlanteVide();
        CouleurFond = ConsoleColor.DarkGreen;
    }
}

