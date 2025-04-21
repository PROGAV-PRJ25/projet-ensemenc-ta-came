using System.Diagnostics;
using System.Runtime.InteropServices;

public class Interface
{
    ConsoleColor[] COULEURS = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
    public int WinHeight = Console.WindowHeight - 1;
    public int WinWidth = Console.WindowWidth;
    public Case[,] Grille { set; get; }
    public Interface()
    {
        Grille = new Case[WinHeight, WinWidth];
        InitialiserGrille();
        TracerInterface();
    }

    public void InitialiserGrille()
    {
        for (int ligne = 0; ligne < WinHeight; ligne++)
        {
            for (int colonne = 0; colonne < WinWidth; colonne++)
            {
                Grille[ligne, colonne] = new Case();
            }
        }
        
    }

    public string CreerVoletSuperieur()
    {
        return "";
    }
    public void AfficherGrille()
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
    public void TracerLigne(int indiceLigne, string typeDeLigne = "")
    {
        for (int colonne = 0; colonne < WinWidth - 1; colonne++)
        {
            Grille[indiceLigne, colonne].Contenu = '═';
        }
        if (typeDeLigne == "debut")
        {
            Grille[indiceLigne, 0].Contenu = '╔';
            Grille[indiceLigne, WinWidth - 1].Contenu = '╗';
        }
        else if (typeDeLigne == "fin")
        {
            Grille[indiceLigne, 0].Contenu = '╚';
            Grille[indiceLigne, WinWidth - 1].Contenu = '╝';
        }
        else
        {
            Grille[indiceLigne, 0].Contenu = '╠';
            Grille[indiceLigne, WinWidth - 1].Contenu = '╣';
        }
    }
    public void TracerColonne(int indiceColonne)
    {
        for (int ligne = 0; ligne < WinHeight; ligne++)
        {
            Grille[ligne, indiceColonne].Contenu = '║';
        }
    }

    public void TracerInterface()
    {
        TracerColonne(0);
        TracerColonne(WinWidth - 1);
        TracerLigne(0, "debut");
        TracerLigne(4);
        TracerLigne(WinHeight - 1, "fin");
        TracerLigne(WinHeight - 1 - (WinHeight / 3));

        // for (int ligne = 0; ligne < WinHeight; ligne++)
        // {
        //     for (int colonne = 0; colonne < WinWidth; colonne++)
        //     {
        //         Grille[ligne, colonne]=""
        //     }
        // }
    }

    public string RécupererASCII(string nomFichierTxt)
    {
        //TODO
        //récupérer le nom de fichier ascii et le transformer en tableau à double entrée.
        string Grille = File.ReadAllText($"{nomFichierTxt}.txt");

        return Grille;
    }
    void InsererTexte(int positionLigne, int positionColonne, string texte, ConsoleColor couleurTexte = ConsoleColor.White, ConsoleColor couleurFond = ConsoleColor.Black)
    {
        for (int colonne = positionColonne; colonne < positionColonne + texte.Length; colonne++)
        {
            if (positionLigne<WinHeight || colonne < WinWidth )
            {
                Grille[positionLigne, colonne].Contenu = Convert.ToChar(texte);
                Grille[positionLigne, colonne].CouleurTexte =  couleurTexte;
                Grille[positionLigne, colonne].CouleurFond =  couleurFond;
            }
        }
    }
}