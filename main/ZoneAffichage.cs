using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

public class ZoneAffichage
{
    public List<ZoneAffichage> ZonesInternes = new List<ZoneAffichage> {};
    public int WinHeight = Console.WindowHeight - 1;
    public int WinWidth = Console.WindowWidth;
    public CelluleAffichage[,] Grille { set; get; }
    public ZoneAffichage()
    {
        Grille = new CelluleAffichage[WinHeight, WinWidth];
        InitialiserGrille();
        ConstruireEcranDeJeu();
    }

    public void InitialiserGrille()
    {
        for (int ligne = 0; ligne < WinHeight; ligne++)
        {
            for (int colonne = 0; colonne < WinWidth; colonne++)
            {
                Grille[ligne, colonne] = new CelluleAffichage();
            }
        }
    }

    public string CreerVoletSuperieur()
    {
        return "";
    }
    public void Afficher()
    {
        for (int ligne = 0; ligne < Grille.GetLength(0); ligne++)
        {
            for (int colonne = 0; colonne < Grille.GetLength(1); colonne++)
            {
                if (Console.ForegroundColor != Grille[ligne, colonne].CouleurTexte)
                    Console.ForegroundColor = Grille[ligne, colonne].CouleurTexte;
                if (Console.BackgroundColor != Grille[ligne, colonne].CouleurFond)
                    Console.BackgroundColor = Grille[ligne, colonne].CouleurFond;

                Console.Write(Grille[ligne, colonne].Contenu);
            }
        }
        Console.ResetColor();
    }
    public void InsererLigne(int indiceLigne, string typeDeLigne = "")
    {
        for (int colonne = 0; colonne < WinWidth - 1; colonne++)
        {
            Grille[indiceLigne, colonne].Contenu = '─';
        }
        if (typeDeLigne == "debut")
        {
            Grille[indiceLigne, 0].Contenu = '┌';
            Grille[indiceLigne, WinWidth - 1].Contenu = '┐';
        }
        else if (typeDeLigne == "fin")
        {
            Grille[indiceLigne, 0].Contenu = '└';
            Grille[indiceLigne, WinWidth - 1].Contenu = '┘';
        }
        else
        {
            Grille[indiceLigne, 0].Contenu = '├';
            Grille[indiceLigne, WinWidth - 1].Contenu = '┤';
        }
    }
    public void InsererColonne(int indiceColonne, int sommet = 0, int pied = -1)
    {
        if (pied == -1)
        {
            pied = WinHeight;
        }
        for (int ligne = sommet; ligne < pied; ligne++)
        {
            Grille[ligne, indiceColonne].Contenu = '│';
        }
    }

    public void TracerZoneAffichage()
    {

    }
    void ConstruireEcranDeJeu()
    {
        InsererColonne(0);
        InsererColonne(WinWidth - 1);
        InsererLigne(0, "debut");
        InsererLigne(3);
        InsererLigne(WinHeight - 1 - (WinHeight / 3));
        InsererLigne(WinHeight - 3);
        InsererLigne(WinHeight - 1, "fin");
    }

    public string RécupererASCII(string nomFichierTxt)
    {
        //TODO
        //récupérer le nom de fichier ascii et le transformer en tableau à double entrée.
        string Grille = File.ReadAllText($"{nomFichierTxt}.txt");

        return Grille;
    }
    void InsererTexte(string texte, int positionLigne, int positionColonne, ConsoleColor couleurTexte = ConsoleColor.White, ConsoleColor couleurFond = ConsoleColor.Black)
    { // consiste à ajouter du texte dans la grille

        string[] mots = texte.Split(' ');
        int ligne = positionLigne;

        for (int colonne = positionColonne; colonne < positionColonne + texte.Length; colonne++)
        {
            if (ligne < WinHeight || colonne < WinWidth)
            {
                Grille[ligne, colonne].Appliquer(texte[colonne], couleurTexte, couleurFond);
            }
        }
    }
}