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
    // Ajout de lignes et de colonnes
    public void InsererLigne(int indiceLigne, int indiceGauche = -1, int indiceDroite = -1, char typeGauche = '─', char typeDroite = '─')
    {
        if (indiceGauche == -1) { indiceGauche = 0; }
        if (indiceDroite == -1) { indiceDroite = Largeur - 1; }

        for (int colonne = indiceGauche; colonne <= indiceDroite; colonne++)
        {
            Grille[colonne, indiceLigne].Contenu = '─';
        }
        Grille[indiceGauche, indiceLigne].Contenu = typeGauche;
        Grille[indiceDroite, indiceLigne].Contenu = typeDroite;
    }
    public void InsererLigne(int indiceLigne, char typeGauche, char typeDroite)
    {
        InsererLigne(indiceLigne, -1, -1, typeGauche, typeDroite);
    }
    public void InsererColonne(int indiceColonne, int sommet = 0, int pied = -1, char typeSommet = '│', char typePied = '│')
    {

        if (pied == -1)
        {
            pied = Hauteur - 1;
        }
        for (int ligne = sommet; ligne <= pied; ligne++)
        {
            Grille[indiceColonne, ligne].Contenu = '│';
        }
        Grille[indiceColonne, sommet].Contenu = typeSommet;
        Grille[indiceColonne, pied].Contenu = typePied;
    }
    public void InsererColonne(int indiceColonne, char typeSommet, char typePied)
    {
        InsererColonne(indiceColonne, 0, -1, typeSommet, typePied);
    }
    public void ConstruireCadre()
    {
        InsererLigne(0);
        InsererLigne(Hauteur - 1);
        InsererColonne(0, '┌', '└');
        InsererColonne(Largeur - 1, '┐', '┘');
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
    public void AfficherLignesDirectrices()
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
}
public class ZoneEcranJeu : Interface
{
    public List<Zone> ZonesInternes = new List<Zone> { };
    public ZoneMenu MAGASIN { set; get; }
    public ZoneMenu INVENTAIRE { set; get; }
    public ZoneMenu JOURNAL { set; get; }
    public ZoneMenu SUIVANT { set; get; }
    public ZoneTexte DETAILS { set; get; }
    public ZoneTexte DIALOGUE { set; get; }
    public ZoneTexte DATE { set; get; }
    public ZoneTexte LIEU { set; get; }
    public ZoneTexte MODE { set; get; }
    public ZoneTexte? ARGENT { set; get; }
    public ZoneTexte? METEO { set; get; }
    public ZoneTexte? WEBCAM { set; get; }
    public Champs POTAGER { set; get; }
    public int IndiceZoneActive { set; get; }
    public EnsembleZoneTexte  BARRE_NAVIGATION { set; get; }

    public ZoneEcranJeu(int positionColonne, int positionLigne, int largeur, int hauteur)
    : base(positionColonne, positionLigne, largeur, hauteur)
    {
        //initialisation de l'affichage
        ConstruireLignesDirectrices();

        //Création des éléments composant le volet supérieur
        DATE = new ZoneTexte(1, 1, 30, 1, "2003 - Semaine 1 (printemps)");
        LIEU = new ZoneTexte(Largeur / 2, 1, 20, 1, "Carcassonne");
        MODE = new ZoneTexte(Largeur - 13, 1, 13, 1, "Mode Normal");
        // Créaction de la zone potager
        POTAGER = new Champs(1, 4, 10, 10);

        //Création des éléments composant la barre de navigation
        int hauteurNavBar = Hauteur - (Hauteur / 3);
        BARRE_NAVIGATION = new EnsembleZoneTexte();
        BARRE_NAVIGATION.Ajouter("Inventaire", new ZoneTexte(2, hauteurNavBar, 14, 1, "Inventaire (I)"));
        BARRE_NAVIGATION.Ajouter("Journal", new ZoneTexte(19, hauteurNavBar, 11, 1, "Journal (J)"));
        BARRE_NAVIGATION.Ajouter("Magasin", new ZoneTexte(33, hauteurNavBar, 11, 1, "Magasin (M)"));
        BARRE_NAVIGATION.Ajouter("Suivant", new ZoneTexte(47, hauteurNavBar, 20, 1, "Semaine Suivante (S)"));

        //Création des différents menus de l'affichage
        INVENTAIRE = new ZoneMenu("Inventaire", 1, hauteurNavBar + 2, (Largeur * 3 / 4) - 2, Hauteur - 3 - (hauteurNavBar + 2));
        JOURNAL = new ZoneMenu("Journal", 1, hauteurNavBar + 2, (Largeur * 3 / 4) - 2, Hauteur - 3 - (hauteurNavBar + 2));
        MAGASIN = new ZoneMenu("Magasin", 1, hauteurNavBar + 2, (Largeur * 3 / 4) - 2, Hauteur - 3 - (hauteurNavBar + 2));
        SUIVANT = new ZoneMenu("Suivant", 1, hauteurNavBar + 2, (Largeur * 3 / 4) - 2, Hauteur - 3 - (hauteurNavBar + 2));
        // Création de la zone dédiée au dialogue
        DIALOGUE = new ZoneTexte(1, Hauteur - 2, Largeur * 2 / 3, 1, "Bienvenue dans cette nouvelle partie ! Par quoi veux-tu commencer ?");
        //Création de la zone dédiée aux détails
        DETAILS = new ZoneTexte(Largeur * 3 / 4, 4, (Largeur * 1 / 4) - 1, Hauteur - 5);

    }
    public ZoneEcranJeu() : this(0, 0, Console.WindowWidth, Console.WindowHeight - 1) { }

    //Initialisation===================================================================

    public void ConstruireLignesDirectrices()
    {
        ConstruireCadre();

        InsererLigne(3, '├', '┤');
        InsererColonne(Largeur * 3 / 4, 3, Hauteur - 1, '┬', '┴');

        int ligneNavBar = Hauteur - 1 - (Hauteur / 3);
        InsererLigne(ligneNavBar, 0, Largeur * 3 / 4, '├', '┤');
        InsererLigne(ligneNavBar + 2, 0, Largeur * 3 / 4, '├', '┤');
        // //0 17 31 45 67
        InsererColonne(17, ligneNavBar, ligneNavBar + 2, '┬', '┴');
        InsererColonne(31, ligneNavBar, ligneNavBar + 2, '┬', '┴');
        InsererColonne(45, ligneNavBar, ligneNavBar + 2, '┬', '┴');
        InsererColonne(68, ligneNavBar, ligneNavBar + 2, '┬', '┴');

        InsererLigne(Hauteur - 3, 0, Largeur * 3 / 4, '├', '┤');


    }
    // Affichage =======================================================================
    public override void Afficher()
    {
        Console.Clear();
        AfficherLignesDirectrices();
        foreach (ZoneTexte element in BARRE_NAVIGATION.Valeurs)
        {
            element.Afficher();
        }
        DATE.Afficher();
        LIEU.Afficher();
        MODE.Afficher();
        POTAGER.Afficher();
        DIALOGUE.Afficher();
        INVENTAIRE.Afficher();
    }


    public void BasculerSurFenetre(int indice)
    {
        Console.ResetColor();
        BARRE_NAVIGATION.Valeurs[IndiceZoneActive].Afficher();
        IndiceZoneActive = indice;
        Console.ForegroundColor = ConsoleColor.DarkRed;
        BARRE_NAVIGATION.Valeurs[IndiceZoneActive].Afficher();
        Console.ResetColor();

    }

}
public class InterfaceAccueil : Interface
{
    public ZoneMenu ACCUEIL { set; get; }
    public InterfaceAccueil(int colonne, int ligne, int largeur, int hauteur) : base(colonne, ligne, largeur, hauteur)
    {
        ACCUEIL = new ZoneMenu("Accueil", Position[0] + 1, Position[1] + 1, Largeur - 2, Hauteur - 2);
        ConstruireCadre();
    }
    public override void Afficher()
    {
        AfficherLignesDirectrices();
    }

}

public class EnsembleZoneTexte
{
    public List<ZoneTexte> Valeurs { set; get; }
    public List<string> Cles { set; get; }

    public EnsembleZoneTexte()
    {
        Cles = [];
        Valeurs = [];
    }
    public ZoneTexte Trouver(string titre)
    {
        ZoneTexte reponse = Valeurs[0];
        for (int i = 0; i < Valeurs.Count(); i++)
        {
            if (Cles[i] == titre)
                reponse = Valeurs[i];
        }
        return reponse;
    }
    public void Ajouter(string cle, ZoneTexte valeur)
    {
        Cles.Add(cle);
        Valeurs.Add(valeur);
    }
}