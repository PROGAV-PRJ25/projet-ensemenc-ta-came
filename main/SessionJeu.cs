using System.Runtime.CompilerServices;
using System.Security.AccessControl;

public class SessionJeu
{
  /*
  Classe qui combine affichage, gestion des données et Navigation
  */
  public ZoneEcranJeu EcranDeJeu { set; get; }
  public ConsoleKeyInfo PressionTouche { set; get; }
  public ZoneInteractive? ZoneActive { set; get; }
  public ZoneMenu? Accueil { set; get; }
  

  public List<ZoneMenu> MenusPrincipaux = [];

  public SessionJeu()
  {
    EcranDeJeu = new ZoneEcranJeu();
    PressionTouche = new ConsoleKeyInfo();
    //Accueil = new ZoneMenu("Accueil", new MenuItem("Accueil"));
  }
  public void Demarrer()
  {
    Console.Clear();
    
    ZoneEcranJeu Ecran = new ZoneEcranJeu(0, 0, Console.WindowWidth, Console.WindowHeight - 1);

    Ecran.ConstruireCadre();
    Ecran.Afficher();

    Console.SetCursorPosition(1, 1);
    Accueil = new ZoneMenu("Accueil", Ecran.Position[0] + 1, Ecran.Position[1] + 1, Console.WindowWidth - 2, Console.WindowHeight - 3);

    Accueil.Racine.Description = "Bienvenue dans Ensemence! Faites votre choix parmi la sélection ci-dessous";
    MenuItem NouvellePartie = new MenuItem(Accueil,"Commencer une nouvelle partie","Sélectionnez le pays dans lequel vous voulez jouer !");
    NouvellePartie.AjouterItem(new MenuItemNouvellePartie(Accueil,"France",this));
    NouvellePartie.AjouterItem(new MenuItemNouvellePartie(Accueil,"Mexique",this));
    NouvellePartie.AjouterItem(new MenuItemNouvellePartie(Accueil,"Japon",this));

    MenuItem ApprendreJeu = new MenuItem(Accueil,"Apprendre les commandes de base");
    Accueil.Racine.AjouterItem(NouvellePartie);
    Accueil.Racine.AjouterItem(ApprendreJeu);


    
    ZoneActive=Accueil;
    Accueil.Afficher();
    Naviguer();
  }
  
  public void DemarrerNouvellePartie(string pays){
    EcranDeJeu.Afficher();
  }
  public void RafraichirAffichageJeu()
  {
    Console.Clear();
    EcranDeJeu.Afficher();
    //VoletSuperieur.Afficher();
    //VoletPrincipal.Afficher();
    //VoletInferieur.Afficher();
    //Dialogue.Afficher();
    //Details.Afficher();
  }
 
  public bool Naviguer()
  {
    bool choixFait = false;
    while (!choixFait)
    {
      PressionTouche = Console.ReadKey();
      if (PressionTouche.Key == ConsoleKey.C)
      {
        //Comment jouer?
        EcranDeJeu.BasculerSurFenetre(0); // 0 = fenêtre d'aide aux commandes de base
      }
      else if (PressionTouche.Key == ConsoleKey.I)
      {
        EcranDeJeu.BasculerSurFenetre(1); // 1 = Inventaire
      }
      else if (PressionTouche.Key == ConsoleKey.J)
      {
        EcranDeJeu.BasculerSurFenetre(2); // 2 = Journal
      }
      else if (PressionTouche.Key == ConsoleKey.M)
      {
        EcranDeJeu.BasculerSurFenetre(3); // 3 = Magasin
        
      }
      else if (PressionTouche.Key == ConsoleKey.S)
      {
        EcranDeJeu.BasculerSurFenetre(4); // 4 Suivant
      }
      else if (PressionTouche.Key == ConsoleKey.W)
      {
        // Champs.ChangerVue()
      }
      // else if (PressionTouche.Key == ConsoleKey.LeftArrow)
      //   DeplacerSelection("gauche");
      // else if (PressionTouche.Key == ConsoleKey.RightArrow)
      //   DeplacerSelection("droite");
      else if (PressionTouche.Key == ConsoleKey.UpArrow)
      {
        ZoneActive.DeplacerCurseur("haut");
      }
      else if (PressionTouche.Key == ConsoleKey.DownArrow)
      {
        ZoneActive.DeplacerCurseur("bas");
      }
      else if (PressionTouche.Key == ConsoleKey.Enter)
      {
        ZoneActive.ValiderSelection();
      }
      else if (PressionTouche.Key== ConsoleKey.Escape || PressionTouche.Key == ConsoleKey.Backspace )
      {
        ZoneActive.RetournerEnArriere();
      }
      else{
        Console.SetCursorPosition(0,0);
      }
    }

    return choixFait;
  }

  // }
  // void DeplacerSelection(MenuItem? nouveauMenuActif)
  // {

  //   if (nouveauMenuActif != null)
  //   {
  //     if (MenuActif != null)
  //     {
  //       MenuActif.Actif = false;
  //     }
  //     nouveauMenuActif.Actif = true;
  //     MenuActif = nouveauMenuActif;
  //   }
  // }
  // void DeplacerSelection(string direction)
  // {
  //   if (MenuActif != null)
  //   {
  //     if (direction == "gauche" && MenuActif.VoisinGauche != null)
  //     {
  //       DeplacerSelection(MenuActif.VoisinGauche);
  //     }
  //     else if (direction == "droite" && MenuActif.VoisinDroite != null)
  //     {
  //       DeplacerSelection(MenuActif.VoisinDroite);
  //     }
  //     else if (direction == "haut" && MenuActif.VoisinHaut != null)
  //     {
  //       DeplacerSelection(MenuActif.VoisinHaut);
  //     }
  //     else if (direction == "bas" && MenuActif.VoisinBas != null)
  //     {
  //       DeplacerSelection(MenuActif.VoisinBas);
  //     }
  //   }


  // }
  public void ReinitialiserAffichage()
  {
    Console.ResetColor();
    Console.Clear();
  }
}
//   public string RecupererASCII(string nomFichierTxt)
//   {
//     //TODO
//     //récupérer le nom de fichier ascii et le transformer en tableau à double entrée.
//     string Grille = File.ReadAllText($"{nomFichierTxt}.txt");
//     return Grille;
//   }
//   public bool VerificationFinPartie()
//   {
//     return true;
//   }
//   public void ActualiserAffichageMenus(List<MenuItem> menus)
//   {
//     foreach (MenuItem item in menus)
//     {
//       Console.ForegroundColor = item.Actif ? ConsoleColor.Red : ConsoleColor.White;
//       Console.SetCursorPosition(item.Position[0], item.Position[1]);
//       Console.Write(item.Nom);
//       Console.ResetColor();
//     }
//   }

//   public void PresenterTutoriel()
//   {
//     ZoneEcran EcranTutoriel = new ZoneEcran();
//     EcranTutoriel.ConstruireCadre();
//     string introduction =  "Bonjour et bienvenue dans ENSEMENCE!\n" +
//                       "Dans ce jeu vous pourrez cultiver votre propre potager chez vous! "+
//                       "Achetez vos premiers semis et prévoyez vos moyens de défense contre les différents dangers qui peuvent survenir !\n" +
//                       "Voici les principales commandes :\n" +
//                       " - Touche I => accéder à l'Inventaire pour utiliser les éléments que vous possédez\n" +
//                       " - Touche J => accéder au Journal pour tout connaître sur les plantes\n" +
//                       " - Touche M => accéder au Magasin pour acheter vos semis, outils et autres moyens de défense contre les différents dangers ! \n" +
//                       " - Touche P => accéder à la semaine suivante !";
//     EcranTutoriel.InsererTexte(introduction);


//   }

// };