public abstract class Interface : Zone
{
    public CelluleAffichage[,] Grille { set; get; }
    public Interface(int positionColonne, int positionLigne, int largeur, int hauteur) : base(positionColonne, positionLigne, largeur, hauteur)
    {
        Grille = new CelluleAffichage[Largeur, Hauteur];
        InitialiserGrille();
    }
    public void InitialiserGrille()
    {
        for (int colonne = 0; colonne < Largeur; colonne++)
            for (int ligne = 0; ligne < Hauteur; ligne++)
                Grille[colonne, ligne] = new CelluleAffichage();
    }
    public void InsererLigne(int indiceLigne, int ancreGauche = -1, int ancreDroite = -1)
    {
        if (ancreGauche == -1) { ancreGauche = 0; }
        if (ancreDroite == -1) { ancreDroite = Largeur; }

        for (int colonne = ancreGauche; colonne < ancreDroite; colonne++)
        {
            Grille[colonne, indiceLigne].Contenu = '─';
        }
    }
    public void InsererColonne(int indiceColonne, int sommet = 0, int pied = -1, string type = "")
    {

        if (pied == -1)
        {
            pied = Hauteur;
        }
        for (int ligne = sommet; ligne < pied; ligne++)
        {
            Grille[indiceColonne, ligne].Contenu = '│';
        }

    }
    public string[] AssemblerGrille()
    {
        string[] lignesTexte = new string[Hauteur];
        for (int ligne = 0; ligne < Hauteur; ligne++)
        {
            lignesTexte[ligne] = "";
            for (int colonne = 0; colonne < Largeur; colonne++)
            {
                lignesTexte[ligne] += Grille[colonne, ligne].Contenu.ToString();
            }
        }
        return lignesTexte;
    }

}

public class ZoneEcranJeu : Interface
{
    public List<Zone> ZonesInternes = new List<Zone> { };
    public ZoneMenu? Magasin { set; get; }
    public ZoneMenu? Inventaire { set; get; }
    public ZoneMenu? Journal { set; get; }
    public ZoneMenu? Details { set; get; }
    public ZoneTexte? Dialogue { set; get; }
    public ZoneTexte? Date { set; get; }
    public ZoneTexte? Lieu { set; get; }
    public ZoneTexte? Mode { set; get; }
    public ZoneTexte? Argent { set; get; }
    public ZoneTexte? Meteo { set; get; }
    public ZoneTexte? Webcam { set; get; }
    public int indiceZoneActive {set;get;}
    public List<ZoneTexte>? BarreNavigation {set;get;}

    public ZoneEcranJeu(int positionColonne, int positionLigne, int largeur, int hauteur) : base(positionColonne, positionLigne, largeur, hauteur)
    {
        ConstruireLignesDirectrices();

        Date = new ZoneTexte(1, 1, 17, 1, "2025 - Semaine 01");
        Lieu = new ZoneTexte(1, Largeur / 2, 20, 1, "Carcassonne");
        Mode = new ZoneTexte(1, Largeur - 13, 13, 1, "Mode Normal");

// | Inventaire (I) | Journal (J) | Magasin (M) | Semaine Suivante (S) |;
        int hauteurNavBar = Hauteur - 3 - (Hauteur / 3);
         BarreNavigation = [new ZoneTexte(2,hauteurNavBar,14,1),new ZoneTexte(19,hauteurNavBar,11,1),new ZoneTexte(33,hauteurNavBar,11,1), new ZoneTexte(47,hauteurNavBar,20,1)];
        
    }
    public ZoneEcranJeu() : this(0, 0, Console.WindowWidth, Console.WindowHeight - 1) { }

    //Initialisation===================================================================

    public void ConstruireLignesDirectrices()
    {
        InsererColonne(0);
        InsererColonne(Largeur - 1);
        InsererLigne(0);
        InsererLigne(3);
        InsererLigne(Hauteur - 3 - (Hauteur / 3), 0, Largeur * 3 / 4);
        InsererLigne(Hauteur - 1 - (Hauteur / 3), 0, Largeur * 3 / 4);
        InsererLigne(Hauteur - 3, 0, Largeur * 3 / 4);
        InsererLigne(Hauteur - 1);
        InsererColonne(Largeur * 3 / 4, 3);

        InsererColonne(16,Hauteur - 3 - (Hauteur / 3),Hauteur  - (Hauteur / 3));
        InsererColonne(16,Hauteur - 3 - (Hauteur / 3),Hauteur  - (Hauteur / 3));

        // intersections de la première ligne
        Grille[0, 0].Contenu = '┌';
        Grille[Largeur - 1, 0].Contenu = '┐';
        // celles de la seconde
        Grille[0, 3].Contenu = '├';
        Grille[Largeur * 3 / 4, 3].Contenu = '┬';
        Grille[Largeur - 1, 3].Contenu = '┤';
        // et ainsi de suite
        Grille[0, Hauteur - 1 - (Hauteur / 3)].Contenu = '├';
        Grille[Largeur * 3 / 4, Hauteur - 1 - (Hauteur / 3)].Contenu = '┤';

        Grille[0, Hauteur - 3].Contenu = '├';
        Grille[Largeur * 3 / 4, Hauteur - 3].Contenu = '┤';

        Grille[0, Hauteur - 3 - (Hauteur / 3)].Contenu = '├';
        Grille[Largeur - 3, Hauteur - 3 - (Hauteur / 3)].Contenu = '┤';

        Grille[0, Hauteur - 1 - (Hauteur / 3)].Contenu = '├';
        Grille[Largeur - 1, Hauteur - 1 - (Hauteur / 3)].Contenu = '┤';

        Grille[0, Hauteur - 1].Contenu = '└';
        Grille[Largeur * 3 / 4, Hauteur - 1].Contenu = '┴';
        Grille[Largeur - 1, Hauteur - 1].Contenu = '┘';


    }
    public void ConstruireCadre()
    {
        InsererColonne(0);
        InsererColonne(Largeur - 1);
        InsererLigne(0);
        InsererLigne(Hauteur - 1);
        Grille[0,0].Contenu='┌';
        
        Grille[Largeur-1,0].Contenu='┐';
        Grille[0,Hauteur-1].Contenu='└';
        Grille[Largeur-1,Hauteur-1].Contenu='┘';

    }
    // Affichage =======================================================================
    public override void Afficher()
    {
        Console.Clear();
        AfficherLignesDirectrices();
    }

    private void AfficherLignesDirectrices()
    {
        string[] lignesTexte = AssemblerGrille();
        Console.SetCursorPosition(Position[0], Position[1]);
        for (int i = 0; i < lignesTexte.Length; i++)
        {
            Console.Write(lignesTexte[i]);
            if (i < lignesTexte.Length - 1)
            {
                RevenirALaLigne(Position[0]);
            }
        }
    }
    public void BasculerSurFenetre(int indice)
    {
        Console.ResetColor();
        BarreNavigation[indiceZoneActive].Afficher();
        indiceZoneActive = indice;
        Console.ForegroundColor = ConsoleColor.DarkRed;
        BarreNavigation[indiceZoneActive].Afficher();
        Console.ResetColor();

    }

}
class InterfaceAccueil : Zone
{
    public InterfaceAccueil(int colonne, int ligne, int largeur, int hauteur) : base(colonne, ligne, largeur, hauteur) { }
    public override void Afficher() { }

}