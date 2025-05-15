using System.Runtime.CompilerServices;
using System.Security.AccessControl;

public class SessionJeu
{
  public Joueur? JoueurActuel { set; get; }
  public InterfaceAccueil ECRAN_ACCUEIL { set; get; }
  public ZoneEcranJeu ECRAN_JEU { set; get; }
  public ConsoleKeyInfo Touche { set; get; }
  public ZoneInteractive ZoneActive { set; get; }
  public List<ZoneMenu> MenusPrincipaux = [];
  public SessionJeu()
  {
    Touche = new ConsoleKeyInfo();
    ECRAN_JEU = new ZoneEcranJeu();
    ECRAN_ACCUEIL = new InterfaceAccueil(0, 0, Console.WindowWidth, Console.WindowHeight - 1);
    ConstruireMenus();
    ZoneActive = ECRAN_ACCUEIL.ACCUEIL;
  }
  // Initialisation ===========================================================
  public void DemarrerChamps()
  {
    Champs MonChamps = new Champs(0, 0, 10, 10);
    Console.Clear();
    MonChamps.Afficher();
    ZoneActive = MonChamps;
    Naviguer();
  }
  public void ConstruireMenus()
  {
    ECRAN_ACCUEIL.ACCUEIL.Racine.Description = "Bienvenue dans Ensemence! Faites votre choix parmi la sélection ci-dessous";
    ElementMenu NouvellePartie = new ElementMenu(ECRAN_ACCUEIL.ACCUEIL, "Commencer une nouvelle partie", "Sélectionnez le pays dans lequel vous voulez jouer !");
    NouvellePartie.AjouterItem(new ElementMenuNouvellePartie(ECRAN_ACCUEIL.ACCUEIL, "France", this));
    NouvellePartie.AjouterItem(new ElementMenuNouvellePartie(ECRAN_ACCUEIL.ACCUEIL, "Mexique", this));
    NouvellePartie.AjouterItem(new ElementMenuNouvellePartie(ECRAN_ACCUEIL.ACCUEIL, "Japon", this));

    ElementMenu ApprendreJeu = new ElementMenu(ECRAN_ACCUEIL.ACCUEIL, "Apprendre les commandes de base");
    ECRAN_ACCUEIL.ACCUEIL.Racine.AjouterItem(NouvellePartie);
    ECRAN_ACCUEIL.ACCUEIL.Racine.AjouterItem(ApprendreJeu);

    ECRAN_JEU.INVENTAIRE.Racine.Description = "Inventaire - Choisissez parmis les catégories de votre inventaire";
    ElementMenu Outils = new ElementMenu(ECRAN_JEU.INVENTAIRE, "Outils", "Choisissez un de vos outils à utiliser !");
    ElementMenu Semis = new ElementMenu(ECRAN_JEU.INVENTAIRE, "Semis", "Choisissez un de vos semis à planter !");
    ECRAN_JEU.INVENTAIRE.Racine.AjouterItem(Outils);
    ECRAN_JEU.INVENTAIRE.Racine.AjouterItem(Semis);

    ECRAN_JEU.MAGASIN.Racine.Description = "Magasin - Acheter ou vendre, c'est vous qui choisissez ! ";
    ElementMenu Acheter = new ElementMenu(ECRAN_JEU.MAGASIN, "Acheter", "Choisissez parmis les catégories d'articles à acheter !");
    ElementMenu Vendre = new ElementMenu(ECRAN_JEU.MAGASIN, "Vendre", "Choisissez une de vos récoltes à vendre !");
    ECRAN_JEU.MAGASIN.Racine.AjouterItem(Acheter);
    ECRAN_JEU.MAGASIN.Racine.AjouterItem(Vendre);

    ECRAN_JEU.JOURNAL.Racine.Description = "Renseignez vous sur le fonctionnement du jeu !";
    ElementMenu Plantes = new ElementMenu(ECRAN_JEU.JOURNAL, "Plantes", "Trouvez tout ce dont vous avez besoin de savoir à propos des plantes !");
    ElementMenu Meteo = new ElementMenu(ECRAN_JEU.JOURNAL, "Meteo", "Devenez incollables sur la météo !");
    ECRAN_JEU.JOURNAL.Racine.AjouterItem(Plantes);
    ECRAN_JEU.JOURNAL.Racine.AjouterItem(Meteo);

    ECRAN_JEU.SUIVANT.Racine.Description = "Voulez vous vraiment passer à la semaine suivante ? Vous ne pourrez pas revenir en Arrière";
    ElementMenu Oui = new ElementMenuSuivant(ECRAN_JEU.SUIVANT, "Oui je le veux",this);
    ECRAN_JEU.SUIVANT.Racine.AjouterItem(Oui);

  }
  public void Demarrer()
  {
    Console.Clear();
    ECRAN_ACCUEIL.Afficher();
    ECRAN_ACCUEIL.ACCUEIL.Afficher();
    Naviguer();
    
  }
  public void DemarrerNouvellePartie(string pays)
  {
    ECRAN_JEU.Afficher();
    ZoneActive = ECRAN_JEU.POTAGER;
    JoueurActuel = new Joueur(pays);
  }

  // Affichage ================================================================
  public void RafraichirAffichageJeu()
  {
    Console.Clear();
    ECRAN_JEU.Afficher();
    //VoletSuperieur.Afficher();
    //VoletPrincipal.Afficher();
    //VoletInferieur.Afficher();
    //Dialogue.Afficher();
    //Details.Afficher();
  }
  public void ReinitialiserAffichage()
  {
    Console.ResetColor();
    Console.Clear();
  }


  // Navigation ===============================================================
  public bool Naviguer()
  {
    bool choixFait = false;
    while (!choixFait)
    {
      Touche = Console.ReadKey(intercept: true);

      if (Touche.Key == ConsoleKey.LeftArrow)
      {
        ZoneActive.DeplacerCurseur("gauche");
      }
      else if (Touche.Key == ConsoleKey.RightArrow)
      {
        ZoneActive.DeplacerCurseur("droite");
      }
      else if (Touche.Key == ConsoleKey.UpArrow)
      {
        ZoneActive.DeplacerCurseur("haut");
      }
      else if (Touche.Key == ConsoleKey.DownArrow)
      {
        ZoneActive.DeplacerCurseur("bas");
      }
      else if (Touche.Key == ConsoleKey.Enter)
      {
        ZoneActive.ValiderSelection();
      }
      else if (Touche.Key == ConsoleKey.Escape || Touche.Key == ConsoleKey.Backspace)
      {
        ZoneActive.RetournerEnArriere();
      }
      else if (ZoneActive != ECRAN_ACCUEIL.ACCUEIL)
      {

        if (Touche.Key == ConsoleKey.C)
        {
          //Comment jouer?
          ECRAN_JEU.BasculerSurFenetre(4); // 0 = fenêtre d'aide aux commandes de base
          ZoneActive.Afficher();
        }
        else if (Touche.Key == ConsoleKey.I)
        {
          ZoneActive = ECRAN_JEU.INVENTAIRE;
          ECRAN_JEU.BasculerSurFenetre(0); // 1 = Inventaire
          ZoneActive.Afficher();
        }
        else if (Touche.Key == ConsoleKey.J)
        {
          ZoneActive = ECRAN_JEU.JOURNAL;
          ECRAN_JEU.BasculerSurFenetre(1); // 2 = Journal
          ZoneActive.Afficher();
        }
        else if (Touche.Key == ConsoleKey.M)
        {
          ZoneActive = ECRAN_JEU.MAGASIN;
          ECRAN_JEU.BasculerSurFenetre(2); // 3 = Magasin
          ZoneActive.Afficher();
        }
        else if (Touche.Key == ConsoleKey.S)
        {
          ZoneActive = ECRAN_JEU.SUIVANT;
          ECRAN_JEU.BasculerSurFenetre(3); // 4 Suivant
          ZoneActive.Afficher();
        }
        else if (Touche.Key == ConsoleKey.W)
        {
          // Champs.ChangerVue()
        }

      }
      else
      {
        Console.SetCursorPosition(0, 0);
      }
    }
    return choixFait;
  }
  public int DemanderPositionPotager()
  {
    ConsoleKeyInfo touche;
    ZoneActive = ECRAN_JEU.POTAGER;
    bool choixFait = false;
    while (!choixFait)
    {
      touche = Console.ReadKey(intercept: true);
      if (Touche.Key == ConsoleKey.LeftArrow)
      {
        ZoneActive.DeplacerCurseur("gauche");
      }
      else if (touche.Key == ConsoleKey.RightArrow)
      {
        ZoneActive.DeplacerCurseur("droite");
      }
      else if (touche.Key == ConsoleKey.UpArrow)
      {
        ZoneActive.DeplacerCurseur("haut");
      }
      else if (touche.Key == ConsoleKey.DownArrow)
      {
        ZoneActive.DeplacerCurseur("bas");
      }
      else if (touche.Key == ConsoleKey.Enter)
      {
        choixFait = true;
      }
    }
    return ECRAN_JEU.POTAGER.Curseur;
  }

  // Actions Inventaire =======================================================
  public void UtiliserOuil() { }
  public void PlanterSemis() { }
  // Actions Journal ==========================================================
  public void Consulter() { }
  // Actions Magasin ==========================================================
  public void Acheter() { }
  // Actions Semaine Suivante =================================================
  public void PasserSemaineSuivante()
  {
    JoueurActuel.Semaine += 1;
    ActualiserDate();
    // ActualiserMeteo();
    // ActualiserEtatPlantes();
  }
  public void ActualiserDate(){
    ECRAN_JEU.DATE.Contenu = $"{2003+JoueurActuel.Semaine/52} - Semaine {JoueurActuel.Semaine%52} ";
    if(JoueurActuel.Semaine%52<13)
      ECRAN_JEU.DATE.Contenu+= "(printemps)";
    else if(JoueurActuel.Semaine%52<26)
      ECRAN_JEU.DATE.Contenu+= "(été)";
    else if(JoueurActuel.Semaine%52<39)
      ECRAN_JEU.DATE.Contenu+= "(automne)";
    else
      ECRAN_JEU.DATE.Contenu+= "(hiver)";
    ECRAN_JEU.DATE.Afficher();
  }
  // public void ActualiserArgent(){
  //   ECRAN_JEU.ARGENT.Contenu = 
  // }
  // Actions Webcam =========================================================== 



  // }
  // void DeplacerSelection(ElementMenu? nouveauMenuActif)
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
  //   public void ActualiserAffichageMenus(List<ElementMenu> menus)
  //   {
  //     foreach (ElementMenu item in menus)
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
}