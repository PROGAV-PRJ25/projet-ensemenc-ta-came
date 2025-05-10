using System.Linq.Expressions;

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
    public ZoneMenu? Journal { set; get;}
    public ZoneMenu? Suivant {set;get;}
    public ZoneMenu? Details { set; get; }
    public ZoneTexte? Dialogue { set; get; }
    public ZoneTexte? Date { set; get; }
    public ZoneTexte? Lieu { set; get; }
    public ZoneTexte? Mode { set; get; }
    public ZoneTexte? Argent { set; get; }
    public ZoneTexte? Meteo { set; get; }
    public ZoneTexte? Webcam { set; get; }
    public int indiceZoneActive { set; get; }
    public EnsembleZoneTexte BarreNavigation { set; get; }

    public ZoneEcranJeu(int positionColonne, int positionLigne, int largeur, int hauteur) 
    : base(positionColonne, positionLigne, largeur, hauteur)
    {
        //initialisation de l'affichage
        ConstruireLignesDirectrices();
        
        //Création des éléments composant le volet supérieur
        Date = new ZoneTexte(1, 1, 18, 1, "2025 - Semaine 01");
        Lieu = new ZoneTexte(Largeur / 2, 1, 20, 1, "Carcassonne");
        Mode = new ZoneTexte(Largeur - 13, 1, 13, 1, "Mode Normal");
        Dialogue = new ZoneTexte(1, Hauteur - 2,Largeur*2/3,1,"Bienvenue dans cette nouvelle partie ! Par quoi veux-tu commencer ?");

        //Création des éléments composant la barre de navigation
        int hauteurNavBar = Hauteur - (Hauteur / 3);
        BarreNavigation = new EnsembleZoneTexte();
        BarreNavigation.Ajouter("Inventaire", new ZoneTexte(2, hauteurNavBar, 14, 1, "Inventaire (I)"));
        BarreNavigation.Ajouter("Journal", new ZoneTexte(19, hauteurNavBar, 11, 1, "Journal (J)"));
        BarreNavigation.Ajouter("Magasin", new ZoneTexte(33, hauteurNavBar, 11, 1, "Magasin (M)"));
        BarreNavigation.Ajouter("Suivant", new ZoneTexte(47, hauteurNavBar, 20, 1, "Semaine Suivante (S)"));

        //Création des différents menus de l'affichage
        
        Inventaire = new ZoneMenu("Inventaire",1,hauteurNavBar+2,);
        Journal = new ZoneMenu("Journal");
        Magasin = new ZoneMenu("Magasin");
        Suivant = new ZoneMenu("Suivant");
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
    public void ConstruireCadre()
    {
        InsererLigne(0);
        InsererLigne(Hauteur - 1);
        InsererColonne(0, '┌', '└');
        InsererColonne(Largeur - 1, '┐', '┘');
    }
    // Affichage =======================================================================
    public override void Afficher()
    {
        Console.Clear();
        AfficherLignesDirectrices();
        foreach (ZoneTexte element in BarreNavigation.Valeurs)
        {
            element.Afficher();
        }
        Date.Afficher();
        Lieu.Afficher();
        Mode.Afficher();
        Dialogue.Afficher();
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
        BarreNavigation.Valeurs[indiceZoneActive].Afficher();
        indiceZoneActive = indice;
        Console.ForegroundColor = ConsoleColor.DarkRed;
        BarreNavigation.Valeurs[indiceZoneActive].Afficher();
        Console.ResetColor();

    }

}
class InterfaceAccueil : Zone
{
    public InterfaceAccueil(int colonne, int ligne, int largeur, int hauteur) : base(colonne, ligne, largeur, hauteur) { }
    public override void Afficher() { }

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