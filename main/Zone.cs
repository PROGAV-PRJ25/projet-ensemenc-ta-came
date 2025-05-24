/*
    Les classes Zone permettent de gérer des zones d'affichage.
    Chaque Zone peut : 
    - afficher son contenu
    - écrire du texte à la position donnée
    - revenir à la ligne
    - écrire une ligne vide
    - effacer l'affichage complet de la zone 

    Il y a différents types de zones : 
    - ZoneTexte : simple zone contenant du texte
    - ZoneInteractive : zone permettant de naviguer à travers son contenu à l'aide d'un curseur
    - ZoneMenu : zone dédiée à l'affichage d'un menu, permettant de naviguer dans son arborescence
    - ZoneInformations générale : zone dédiée à l'affichage des informations générales (volet principal)
    - ZoneMenuDialogue qui possède une méthode pour l'affichage de la semaine prochaine, mais qui pourrait avoir d'autres méthodes dédiés qui sont éparpillées dans le reste du code.
*/
public abstract class Zone
{

    public int Largeur { get; set; }
    public int Hauteur { get; set; }
    public int[] Position { get; set; }


    public Zone(int colonne, int ligne, int largeur, int hauteur)
    {
        Position = new int[] { colonne, ligne };
        Hauteur = hauteur;
        Largeur = largeur;
    }

    public abstract void Afficher();


    public int EcrireTexte(string texte, int positionColonne, int positionLigne)
    { // affiche le texte en partant de la position de départ donnée
      // retourne automatiquement à la ligne si le texte dépasse de la zone
      // s'arrête dès que le texte a atteint le bas de la zone, 

        int pointeurLigne = positionLigne;
        int limiteColonne;
        int indiceColonne = positionColonne;
        int hauteurTexte = 0;

        string[] lignesTexte = texte.Split("\n");
        string[][] MotsEtLignes = new string[lignesTexte.Length][];

        for (int indiceMot = 0; indiceMot < lignesTexte.Length; indiceMot++)
        {
            MotsEtLignes[indiceMot] = lignesTexte[indiceMot].Split(" ");
        }

        string texteEnAttente;
        Console.SetCursorPosition(positionColonne, positionLigne);

        for (int indiceLigne = 0; indiceLigne < MotsEtLignes.Length; indiceLigne++)
        {
            string[] ligne = MotsEtLignes[indiceLigne];
            texteEnAttente = "";
            indiceColonne = positionColonne;

            for (int indiceMot = 0; indiceMot < ligne.Length; indiceMot++)
            {// pour chaque mot
                if (pointeurLigne < Hauteur + Position[1])
                { // si on ne dépasse pas de la zone verticalement
                    limiteColonne = indiceColonne + ligne[indiceMot].Length + 1;
                    if (indiceMot == ligne.Length - 1) // si c'est le dernier mot de la ligne, pas besoin d'anticiper le prochain espace
                    {
                        limiteColonne--;
                    }
                    if (limiteColonne <= Largeur + Position[0])
                    {//si le mot suivant ne dépasse pas de la zone horizontalement (+1 pour indiceLigne'espace, +1 pour la ligne du cadre)
                        texteEnAttente += ligne[indiceMot] + " ";
                        indiceColonne += ligne[indiceMot].Length + 1;
                    }
                    else
                    {//la ligne a fini d'être construite
                        Console.Write(texteEnAttente);
                        texteEnAttente = ligne[indiceMot] + " ";
                        RevenirALaLigne(positionColonne);
                        indiceColonne = positionColonne + texteEnAttente.Length;
                        hauteurTexte++;
                    }
                }
            }
            Console.Write(texteEnAttente);
            hauteurTexte++;
            if (indiceLigne < MotsEtLignes.Length - 1)
            {
                RevenirALaLigne(positionColonne);
            }
        }
        return hauteurTexte;
    }
    public void EcrireTexte(string texte, int positionColonne, int positionLigne,
    ConsoleColor couleurTexte = ConsoleColor.White, ConsoleColor couleurFond = ConsoleColor.Black)
    {
        Console.BackgroundColor = couleurFond;
        Console.ForegroundColor = couleurTexte;
        EcrireTexte(texte, positionColonne, positionLigne);
        Console.ResetColor();
    }
    public void EcrireLigneVide(int largeur)
    {
        string ligneVide = "";
        for (int i = 0; i < largeur; i++)
        {
            ligneVide += " ";
        }
        Console.Write(ligneVide);
    }
    public void RevenirALaLigne(int indiceColonne)
    {
        if (Console.GetCursorPosition().Top < Console.WindowHeight - 1)
        {
            int indiceLigne = Console.GetCursorPosition().Top + 1;
            Console.SetCursorPosition(indiceColonne, indiceLigne);
        }
    }
    public void Effacer()
    {
        Console.SetCursorPosition(Position[0], Position[1]);
        for (int i = 0; i < Hauteur; i++)
        {
            EcrireLigneVide(Largeur);
            RevenirALaLigne(Position[0]);
        }
    }
}
public class ZoneTexte : Zone
{
    public string Contenu { get; set; }
    public ConsoleColor CouleurTexte { get; set; }
    public ConsoleColor CouleurFond { get; set; }
    public ZoneTexte(int colonne, int ligne, int largeur, int hauteur, string contenu = "(vide)", ConsoleColor couleurTexte = ConsoleColor.White, ConsoleColor couleurFond = ConsoleColor.Black) : base(colonne, ligne, largeur, hauteur)
    {
        Contenu = contenu;
        CouleurTexte = couleurTexte;
        CouleurFond = couleurFond;
    }
    public override void Afficher()
    {
        Effacer();
        Console.ForegroundColor = CouleurTexte;
        Console.BackgroundColor = CouleurFond;
        EcrireTexte(Contenu, Position[0], Position[1]);
        Console.ResetColor();
    }
}
public class ZoneDialogue : ZoneTexte
{
    public ZoneDialogue(int colonne, int ligne, int largeur, int hauteur, string titre, ConsoleColor couleurTexte = ConsoleColor.Blue, ConsoleColor couleurFond = ConsoleColor.Black)
        : base(colonne, ligne, largeur, hauteur, titre, couleurTexte, couleurFond)
    { }
    public void AfficherMessageSemaine(Date date)
    {
        int semaine = date.Semaine;
        int annee = date.Annee - 2009;
        Contenu = $"Semaine passée ! ";
        if (semaine == 2)
        {
            Contenu += $"1 semaine s'est écoulée depuis le début de votre aventure !";
        }
        else if (semaine == 1 && annee == 1)
        {
            Contenu += $"1 an s'est écoulé depuis le début de votre aventure !";
        }
        else
        {
            if (semaine == 1)
            {
                Contenu += $"{annee} ans se sont écoulés depuis le début de l'aventure !";
            }
            else
            {
                if (annee > 1)
                {
                    Contenu += $"{annee} ans et ";
                }
                else if (annee > 0)
                {
                    Contenu += $"{annee} an et ";
                }

                Contenu += $"{semaine - 1} semaines se sont écoulées depuis le début de votre aventure !";
            }
        }
        Afficher();
    }
}
public abstract class ZoneInteractive : Zone
{
    public int Curseur { get; set; }
    public abstract void DeplacerCurseur(string direction);
    public abstract void ValiderSelection();
    public abstract void RetournerEnArriere();
    public ZoneInteractive(int colonne, int ligne, int largeur, int hauteur) : base(colonne, ligne, largeur, hauteur) { }
}
public class ZoneMenu : ZoneInteractive
{
    //element d'affichage permettant de naviguer dans une arborescence et de valider un choix 
    public string Nom { get; set; }
    public ElementMenu NoeudActif { get; set; }
    public ElementMenu Racine { get; set; }
    public int AncreAffichageItems { get; set; }

    public ZoneMenu(string nom, int positionColonne = 0, int positionLigne = 0, int largeur = 5, int hauteur = 5) : base(positionColonne, positionLigne, largeur, hauteur)
    {
        Curseur = 0;
        Nom = nom;
        Racine = new ElementMenu(this);
        NoeudActif = Racine;
    }
    public override void DeplacerCurseur(string direction)
    {
        if (direction == "haut" && Curseur > 0)
        {
            Curseur -= 1;
        }
        if (direction == "bas" && Curseur < NoeudActif.Items.Count - 1)
        {
            Curseur += 1;
        }
        ActualiserAffichageListeItems();
    }
    public void ActualiserAffichageListeItems()
    {
        int nombreItems = NoeudActif.Items.Count;
        int itemsParPage = Hauteur - (AncreAffichageItems - Position[1]) - 1;// -1 pour laisser une ligne pour afficher le nombre de pages
        int nombrePages = (nombreItems - 1) / itemsParPage; // le nombre commence à 0
        //on met -1 à items pour évider de créer des pages vides
        //par exemple si on a nombreItems=3, itemsPagPage=3, on n'a besoin que d'une page pour afficher les 3, et non deux, donc on met -1
        int pageActive = Curseur / itemsParPage; // commence aussi à 0
        Console.SetCursorPosition(Position[0], AncreAffichageItems);
        for (int i = itemsParPage * pageActive; i < (itemsParPage * (pageActive + 1)); i++) // on parcours les n-premiers de la page
        {
            if (i >= nombreItems)
            {
                EcrireLigneVide(Largeur);
            }
            else
            {
                EcrireLigneVide(Largeur);
                Console.SetCursorPosition(Position[0], Console.GetCursorPosition().Top);
                if (i == Curseur)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(NoeudActif.Items[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(NoeudActif.Items[i]);
                }
            }
            RevenirALaLigne(Position[0]);
        }
        Console.Write($"(page {pageActive + 1}/{nombrePages + 1})");
    }
    public override void Afficher()
    {
        Effacer();
        EcrireTexte(NoeudActif.Description, Position[0], Position[1]);
        RevenirALaLigne(Position[0]);
        // prend en compte la taille de la description ainsi que de la taille d'affichage de la zone
        AncreAffichageItems = Console.GetCursorPosition().Top;
        ActualiserAffichageListeItems();
    }
    public override void ValiderSelection()
    {
        if (NoeudActif.Items.Count() != 0)
            NoeudActif.Items[Curseur].Actionner();
    }
    public override void RetournerEnArriere()
    {
        NoeudActif.RevenirAuParent();
        Effacer();
        Afficher();
    }
}
// nous avions essayé d'intégrer des dessins mais au final nous ne l'avons pas fait
public class ZoneDessin : Zone
{
    public string[] Dessin;
    public ZoneDessin(int colonne, int ligne, string nomFichierTxt) : base(colonne, ligne, 0, 0)
    {
        Dessin = File.ReadAllLines(nomFichierTxt);
        Position = new int[2] { colonne, ligne };
        Hauteur = Dessin.Length;
        Largeur = 0;

        foreach (string ligneDessin in Dessin)
        {
            if (ligneDessin.Length > Largeur)
                Largeur = ligneDessin.Length;
        }

    }
    public override void Afficher()
    {
        for (int ligne = 0; ligne < Dessin.Length; ligne++)
            EcrireTexte(Dessin[ligne], Position[0], Position[1] + ligne);
    }
}