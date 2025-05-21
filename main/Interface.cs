using System.Runtime.InteropServices;
public abstract class Interface : Zone
{
    public virtual CelluleAffichage[,] Grille { set; get; }
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
    public ZoneInteractive ZoneActive { set; get; }
    public List<Zone> ZonesInternes = new List<Zone> { };
    public ZoneMenu Magasin { set; get; }
    public ZoneMenu Inventaire { set; get; }
    public ZoneMenu Journal { set; get; }
    public ZoneMenu Suivant { set; get; }
    public ZoneMenu Urgence { set; get; }
    public ZoneTexte Details { set; get; }

    public ZoneDialogue Dialogue { set; get; }

    public ZoneTexte Date { set; get; }
    public ZoneTexte Lieu { set; get; }
    public ZoneTexte Mode { set; get; }
    public ZoneTexte Argent { set; get; }
    public ZoneTexte Meteo { set; get; }
    public ZoneTexte? Webcam { set; get; }
    public ZoneChamps Champs { set; get; }
    public int IndiceZoneActive { set; get; }
    public EnsembleZoneTexte TitresMenus { set; get; }
    public GroupeChampsDetails ChampsEtDetails { set; get; }

    public ZoneEcranJeu(int positionColonne, int positionLigne, int largeur, int hauteur)
    : base(positionColonne, positionLigne, largeur, hauteur)
    {
        //initialisation de l'affichage
        ConstruireLignesDirectrices();

        //Création des éléments composant le volet supérieur
        Date = new ZoneTexte(1, 1, 30, 1, "2003 - Semaine 1 (printemps)");
        Lieu = new ZoneTexte(Largeur / 2, 1, 20, 1, "Carcassonne");
        Mode = new ZoneTexte(Largeur - 18, 1, 12, 1, "Mode Urgence");
        Argent = new ZoneTexte(1, 2, 28, 1, "Argent : 2000 💰");
        Meteo = new ZoneTexte(Largeur - 18, 2, 2, 15, "Meteo : 🌧️ -20°C");
        // Créaction de la zone CHAMPS
        Champs = new ZoneChamps(1, 6, 10, 10);

        //Création des éléments composant la barre de navigation
        int hauteurNavBar = Hauteur - (Hauteur / 3);
        TitresMenus = new EnsembleZoneTexte();
        TitresMenus.Ajouter("Potager", new ZoneTexte(2, 4, 11, 1, "Potager (P)"));
        TitresMenus.Ajouter("Inventaire", new ZoneTexte(2, hauteurNavBar, 14, 1, "Inventaire (I)"));
        TitresMenus.Ajouter("Journal", new ZoneTexte(19, hauteurNavBar, 11, 1, "Journal (J)"));
        TitresMenus.Ajouter("Magasin", new ZoneTexte(33, hauteurNavBar, 11, 1, "Magasin (M)"));
        TitresMenus.Ajouter("Suivant", new ZoneTexte(47, hauteurNavBar, 20, 1, "Semaine Suivante (S)"));

        //Création des différents menus de l'affichage
        Inventaire = new ZoneMenu("Inventaire", 2, hauteurNavBar + 2, (Largeur * 3 / 4) - 3, Hauteur - 3 - (hauteurNavBar + 2));
        Journal = new ZoneMenu("Journal", 2, hauteurNavBar + 2, (Largeur * 3 / 4) - 3, Hauteur - 3 - (hauteurNavBar + 2));
        Magasin = new ZoneMenu("Magasin", 2, hauteurNavBar + 2, (Largeur * 3 / 4) - 3, Hauteur - 3 - (hauteurNavBar + 2));
        Suivant = new ZoneMenu("Suivant", 2, hauteurNavBar + 2, (Largeur * 3 / 4) - 3, Hauteur - 3 - (hauteurNavBar + 2));
        Urgence = new ZoneMenu("Suivant", 2, hauteurNavBar + 2, (Largeur * 3 / 4) - 3, Hauteur - 3 - (hauteurNavBar + 2));
        // Création de la zone dédiée au dialogue
        Dialogue = new ZoneDialogue(2, Hauteur - 2, Largeur - 4, 1, "Bienvenue dans cette nouvelle partie ! Par quoi veux-tu commencer ?");
        //Création de la zone dédiée aux détails
        Details = new ZoneTexte(Largeur * 3 / 4 + 2, 4, (Largeur * 1 / 4) - 7, Hauteur - 7);

        ChampsEtDetails = new GroupeChampsDetails(Champs, Details);
        //JournalEtArticles = new GroupeJournalEtArticles;
        ZoneActive = ChampsEtDetails;
        BasculerSurZone(0);
    }
    public ZoneEcranJeu() : this(0, 0, Console.WindowWidth, Console.WindowHeight - 1) { }

    //Initialisation===================================================================

    public void ConstruireLignesDirectrices()
    {
        ConstruireCadre();

        InsererLigne(3, '├', '┤');
        InsererLigne(Hauteur - 3, '├', '┤');
        InsererColonne(Largeur * 3 / 4, 3, Hauteur - 3, '┬', '┴');

        InsererColonne(14, 3, 5, '┬', '┘');
        InsererLigne(5, 0, 14, '├', '┘');

        int ligneNavBar = Hauteur - 1 - (Hauteur / 3);
        InsererLigne(ligneNavBar, 0, Largeur * 3 / 4, '├', '┤');
        InsererLigne(ligneNavBar + 2, 0, Largeur * 3 / 4, '├', '┤');
        // //0 17 31 45 67
        InsererColonne(17, ligneNavBar, ligneNavBar + 2, '┬', '┴');
        InsererColonne(31, ligneNavBar, ligneNavBar + 2, '┬', '┴');
        InsererColonne(45, ligneNavBar, ligneNavBar + 2, '┬', '┴');
        InsererColonne(68, ligneNavBar, ligneNavBar + 2, '┬', '┴');
    }
    // Affichage =======================================================================
    public override void Afficher()
    {
        Console.Clear();
        AfficherLignesDirectrices();
        foreach (ZoneTexte element in TitresMenus.Valeurs)
        {
            element.Afficher();
        }
        Date.Afficher();
        Lieu.Afficher();
        Mode.Afficher();
        Argent.Afficher();
        Champs.Afficher();
        Dialogue.Afficher();
        Inventaire.Afficher();
        Details.Afficher();
        TitresMenus.Afficher();
    }
    public void BasculerSurZone(int indice)
    {
        // on actualise l'affichage dans les titres de menu
        TitresMenus.Valeurs[IndiceZoneActive].CouleurTexte = ConsoleColor.White;
        IndiceZoneActive = indice;
        TitresMenus.Valeurs[IndiceZoneActive].CouleurTexte = ConsoleColor.DarkRed;
        // puis on change la fenêtre active;
        if (indice == 0)
        {
            ZoneActive = ChampsEtDetails;
        }
        else if (indice == 1)
        {
            ZoneActive = Inventaire;
        }
        else if (indice == 2)
        {
            ZoneActive = Journal;
        }
        else if (indice == 3)
        {
            ZoneActive = Magasin;
        }
        else if (indice == 4)
        {
            ZoneActive = Suivant;
        }
        TitresMenus.Afficher();
        ZoneActive.Afficher();
    }
    public void ActualiserAffichageArgent(int argent)
    {
        Argent.Contenu = $"Argent : {argent} 💰";
        Argent.Afficher();
    }
    public void ActualiserAffichageMeteo(int temperature)
    {
        Meteo.Contenu = $"Meteo : {temperature}°C";
    }

}
public class InterfaceAccueil : Interface
{
    public ZoneMenu Accueil { set; get; }
    public InterfaceAccueil(int colonne, int ligne, int largeur, int hauteur) : base(colonne, ligne, largeur, hauteur)
    {
        Accueil = new ZoneMenu("Accueil", Position[0] + 1, Position[1] + 1, Largeur - 3, Hauteur - 3);
        ConstruireCadre();
    }
    public InterfaceAccueil() : this(0, 0, Console.WindowWidth, Console.WindowHeight) { }
    public override void Afficher()
    {
        AfficherLignesDirectrices();
    }
}

public class GroupeChampsDetails : ZoneInteractive
{
    public ZoneChamps Champs { set; get; }
    public ZoneTexte Details { set; get; }

    public GroupeChampsDetails(ZoneChamps champs, ZoneTexte details) : base(champs.Position[0], champs.Position[1], champs.Largeur, champs.Hauteur)
    {
        Champs = champs;
        Details = details;
        Champs = champs;

    }
    public override void ValiderSelection() { }
    public override void RetournerEnArriere() { }
    public override void Afficher()
    {
        Synchroniser();
        Champs.Afficher();
        Details.Afficher();
    }
    public override void DeplacerCurseur(string deplacement)
    {
        int curseurInitial = Champs.Curseur;
        Champs.DeplacerCurseur(deplacement);

        if (Champs.Curseur != curseurInitial)
        { // attention on n'utilise que le curseur du champs et non celui de la classe du groupe
            Synchroniser();
            Details.Afficher();
        }
    }
    public void Synchroniser()
    {
        Details.Contenu = Champs.Grille[Champs.Curseur % Champs.Largeur, Champs.Curseur / Champs.Largeur].ToString();
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
    public void Afficher()
    {
        foreach (ZoneTexte texte in Valeurs)
        {
            texte.Afficher();
        }
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