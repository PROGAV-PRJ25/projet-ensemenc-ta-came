using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

public class SessionJeu
{
    public Joueur JoueurActuel { set; get; }
    public InterfaceAccueil ECRAN_ACCUEIL { set; get; }
    public ZoneEcranJeu ECRAN_JEU { set; get; }
    public ConsoleKeyInfo Touche { set; get; }
    public List<ZoneMenu> MenusPrincipaux = [];
    public List<ZoneMenu> MenusUrgence = [];
    public SessionJeu()
    {
        Touche = new ConsoleKeyInfo();
        ECRAN_JEU = new ZoneEcranJeu();
        ECRAN_ACCUEIL = new InterfaceAccueil(0, 0, Console.WindowWidth, Console.WindowHeight - 1);
        ConstruireMenus();
        ECRAN_JEU.ZoneActive = ECRAN_ACCUEIL.ACCUEIL;
        JoueurActuel = new Joueur();

    }
    // Initialisation ===========================================================
    // Gestion des menus ======================================================
    public void ConstruireMenus()
    {
        ECRAN_ACCUEIL.ACCUEIL.Racine.Description = "Bienvenue dans Ensemence! Faites votre choix parmi la sélection ci-dessous";
        ElementMenu NouvellePartie = new ElementMenu(ECRAN_ACCUEIL.ACCUEIL, "Commencer une nouvelle partie", "Sélectionnez le pays dans lequel vous voulez jouer !");
        NouvellePartie.AjouterItem(new ElementMenuNouvellePartie(ECRAN_ACCUEIL.ACCUEIL, "Carcassonne (France)", this));
        NouvellePartie.AjouterItem(new ElementMenuNouvellePartie(ECRAN_ACCUEIL.ACCUEIL, "Oaxaca de Juárez (Mexique)", this));
        NouvellePartie.AjouterItem(new ElementMenuNouvellePartie(ECRAN_ACCUEIL.ACCUEIL, "Kanazawa (Japon)", this));

        ElementMenu ApprendreJeu = new ElementMenu(ECRAN_ACCUEIL.ACCUEIL, "Apprendre les commandes de base");
        ECRAN_ACCUEIL.ACCUEIL.Racine.AjouterItem(NouvellePartie);
        ECRAN_ACCUEIL.ACCUEIL.Racine.AjouterItem(ApprendreJeu);

        ECRAN_JEU.Inventaire.Racine.Description = "Inventaire - Choisissez parmis les catégories de votre inventaire";
        ElementMenu MenuOutils = new ElementMenu(ECRAN_JEU.Inventaire, "Outils", "Choisissez un de vos outils à utiliser !");
        ElementMenu MenuSemis = new ElementMenu(ECRAN_JEU.Inventaire, "Semis", "Choisissez un de vos semis à planter !");
        ECRAN_JEU.Inventaire.Racine.AjouterItem(MenuOutils);
        ECRAN_JEU.Inventaire.Racine.AjouterItem(MenuSemis);

        ECRAN_JEU.Magasin.Racine.Description = "Magasin - Acheter ou vendre, c'est vous qui choisissez ! ";
        ElementMenu Acheter = new ElementMenu(ECRAN_JEU.Magasin, "Acheter", "Choisissez parmis les catégories d'articles à acheter !");
        ElementMenu Vendre = new ElementMenu(ECRAN_JEU.Magasin, "Vendre", "Choisissez une de vos récoltes à vendre !");
        ECRAN_JEU.Magasin.Racine.AjouterItem(Acheter);
        ECRAN_JEU.Magasin.Racine.AjouterItem(Vendre);

        foreach (Plante plante in Plante.ListePlantes)
        {
            Acheter.AjouterItem(new ElementMenuMagasinSemis(ECRAN_JEU.Magasin, this, plante));
        }
        foreach (Outil outil in Outil.ListeOutils)
        {
            Acheter.AjouterItem(new ElementMenuMagasinOutil(ECRAN_JEU.Magasin, this, outil));
        }

        ECRAN_JEU.Journal.Racine.Description = "Renseignez vous sur le fonctionnement du jeu !";
        ElementMenu Plantes = new ElementMenu(ECRAN_JEU.Journal, "Plantes", "Trouvez tout ce dont vous avez besoin de savoir à propos des plantes !");
        ElementMenu Meteo = new ElementMenu(ECRAN_JEU.Journal, "Meteo", "Devenez incollables sur la météo !");
        ECRAN_JEU.Journal.Racine.AjouterItem(Plantes);
        ECRAN_JEU.Journal.Racine.AjouterItem(Meteo);

        ECRAN_JEU.Suivant.Racine.Description = "Voulez vous vraiment passer à la semaine suivante ? Vous ne pourrez pas revenir en Arrière";
        ElementMenu Oui = new ElementMenuSuivant(ECRAN_JEU.Suivant, "Oui je le veux", this);
        ECRAN_JEU.Suivant.Racine.AjouterItem(Oui);
    }
    // ------------------------------------------------------------------------
    public void ActualiserMenuInventaire()
    {
        // Mise à jour des semis
        List<ElementMenu> itemsSemis = new List<ElementMenu>();
        if (JoueurActuel.Inventaire.Semis.Count == 0)
        {
            ECRAN_JEU.Inventaire.Racine.Items[1].Description = "Vous n'avez plus aucun semis ! Allez au magasin en acheter !";
        }
        else
        {
            foreach (ItemInventaireSemis item in JoueurActuel.Inventaire.Semis)
            {
                itemsSemis.Add(new ElementMenuInventaireSemis(ECRAN_JEU.Inventaire, $"{item.Contenu.Emoji} {item.Contenu.Nom} ({item.Quantite} en stock)", this, item.Contenu));
            }
            ECRAN_JEU.Inventaire.Racine.Items[1].Description = "Choisissez un de vos semis à planter !";
        }
        ECRAN_JEU.Inventaire.Racine.Items[1].Items = itemsSemis;

        // Mise à jour des outils
        List<ElementMenu> itemsOutils = new List<ElementMenu>();
        if (JoueurActuel.Inventaire.Outils.Count == 0)
        {
            ECRAN_JEU.Inventaire.Racine.Items[0].Description = "Vous n'avez plus aucun outil ! Allez au magasin en acheter !";
        }
        else
        {
            foreach (ItemInventaireOutil item in JoueurActuel.Inventaire.Outils)
            {
                itemsOutils.Add(new ElementMenuInventaireOutil(ECRAN_JEU.Inventaire, $"{item.Contenu.Emoji} {item.Contenu.Nom} ({item.Quantite} en stock)", this, item.Contenu));
            }
            ECRAN_JEU.Inventaire.Racine.Items[0].Description = "Choisissez un de vos outils à utiliser !";
        }
        ECRAN_JEU.Inventaire.Racine.Items[0].Items = itemsOutils;
        ECRAN_JEU.Inventaire.Curseur = 0;

    }
    // Affichage ==============================================================
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
    // -------------------------------------------------------------------------
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
                ECRAN_JEU.ZoneActive.DeplacerCurseur("gauche");
            }
            else if (Touche.Key == ConsoleKey.RightArrow)
            {
                ECRAN_JEU.ZoneActive.DeplacerCurseur("droite");
            }
            else if (Touche.Key == ConsoleKey.UpArrow)
            {
                ECRAN_JEU.ZoneActive.DeplacerCurseur("haut");
            }
            else if (Touche.Key == ConsoleKey.DownArrow)
            {
                ECRAN_JEU.ZoneActive.DeplacerCurseur("bas");
            }
            else if (Touche.Key == ConsoleKey.Enter)
            {
                ECRAN_JEU.ZoneActive.ValiderSelection();
            }
            else if (Touche.Key == ConsoleKey.Escape || Touche.Key == ConsoleKey.Backspace)
            {
                ECRAN_JEU.ZoneActive.RetournerEnArriere();
            }
            else if (ECRAN_JEU.ZoneActive != ECRAN_ACCUEIL.ACCUEIL)
            {

                if (Touche.Key == ConsoleKey.C)
                {
                    //Comment jouer?
                    ECRAN_JEU.BasculerSurZone(6); // 0 = fenêtre d'aide aux commandes de base

                }
                else if (Touche.Key == ConsoleKey.P)
                {
                    ECRAN_JEU.BasculerSurZone(0); // 0 = Potager
                }
                else if (Touche.Key == ConsoleKey.I)
                {
                    ECRAN_JEU.BasculerSurZone(1); // 1 = Inventaire
                }
                else if (Touche.Key == ConsoleKey.J)
                {
                    ECRAN_JEU.BasculerSurZone(2); // 2 = Journal
                }
                else if (Touche.Key == ConsoleKey.M)
                {
                    ECRAN_JEU.BasculerSurZone(3); // 3 = Magasin
                }
                else if (Touche.Key == ConsoleKey.S)
                {
                    ECRAN_JEU.BasculerSurZone(4); // 4 Suivant
                }
                else if (Touche.Key == ConsoleKey.W)
                {
                    // Champs.ChangerVue()
                    JoueurActuel.Argent *= 200;
                    ActualiserAffichageArgent();
                }
                else if (Touche.Key == ConsoleKey.X)
                {
                    choixFait = true;
                }
                else if (Touche.Key == ConsoleKey.U)
                {
                    DeclencherModeUrgence();
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
        ECRAN_JEU.BasculerSurZone(0);
        bool choixFait = false;
        while (!choixFait)
        {
            touche = Console.ReadKey(intercept: true);
            if (touche.Key == ConsoleKey.LeftArrow)
            {
                ECRAN_JEU.ZoneActive.DeplacerCurseur("gauche");
            }
            else if (touche.Key == ConsoleKey.RightArrow)
            {
                ECRAN_JEU.ZoneActive.DeplacerCurseur("droite");
            }
            else if (touche.Key == ConsoleKey.UpArrow)
            {
                ECRAN_JEU.ZoneActive.DeplacerCurseur("haut");
            }
            else if (touche.Key == ConsoleKey.DownArrow)
            {
                ECRAN_JEU.ZoneActive.DeplacerCurseur("bas");
            }
            else if (touche.Key == ConsoleKey.Enter)
            {
                choixFait = true;
            }
        }
        return ECRAN_JEU.Champs.Curseur;
    }
    // Lancement d'une partie =================================================
    public void Demarrer()
    {
        Console.Clear();
        ECRAN_ACCUEIL.Afficher();
        ECRAN_ACCUEIL.ACCUEIL.Afficher();
        Naviguer();

    }
    // ------------------------------------------------------------------------
    public void DemarrerNouvellePartie(string pays)
    {
        JoueurActuel = new Joueur(pays);
        // on met à jour les zones d'affichage liées au joueur
        ECRAN_JEU.Lieu.Contenu = JoueurActuel.Lieu;
        ECRAN_JEU.Date.Contenu = "2003 - Semaine 1 (printemps)";
        ECRAN_JEU.Champs.Synchroniser(JoueurActuel.Potager);
        ECRAN_JEU.BasculerSurZone(0);
        ECRAN_JEU.ChampsEtDetails.Champs = ECRAN_JEU.Champs;
        ECRAN_JEU.ZoneActive = ECRAN_JEU.ChampsEtDetails;

        ECRAN_JEU.Afficher();
        ActualiserMenuInventaire();
    }
    // ACTIONS DURANT LA PARTIE
    // Actions Inventaire =======================================================
    public void PlanterSemis(Plante plante)
    {
        ECRAN_JEU.Dialogue.Contenu = "Choisissez un emplacement où planter votre semis!"; // plus tard {JoueurActuel.Inventaire.Semis[INVENTAIRE.Curseur].NOM}
        ECRAN_JEU.Dialogue.Afficher();
        int indiceParcelle = DemanderPositionPotager();
        int colonne = indiceParcelle % JoueurActuel.Potager.GetLength(0);
        int ligne = indiceParcelle / JoueurActuel.Potager.GetLength(0);
        if (JoueurActuel.Potager[colonne, ligne].Libre)
        {
            JoueurActuel.Potager[colonne, ligne].Planter(plante);//on ajoute la plante au potager
            JoueurActuel.Inventaire.Retirer(plante); //on actualise l'inventaire
            ECRAN_JEU.Champs.Synchroniser(JoueurActuel.Potager);  //on actualise le menu 
            //on actualise l'affichage
            ECRAN_JEU.Dialogue.Contenu = $"{plante.Nom} Ajouté ! en stock : {JoueurActuel.Inventaire.RecupererQuantite(plante)}. Retour à l'inventaire";
            ECRAN_JEU.ChampsEtDetails.Afficher();
        }
        else
        {
            ECRAN_JEU.Dialogue.Contenu = $"{JoueurActuel.Potager[colonne, ligne].Contenu.Nom} OPERATION ANNULEE : cet emplacement n'est pas libre ! Utilisez la pelle pour libérer cet emplacement";

        }
        ActualiserMenuInventaire();

        ECRAN_JEU.Dialogue.Afficher();
        ECRAN_JEU.BasculerSurZone(1); // retour à l'inventaire
    }
    public void UtiliserOutil(Outil outil)
    {
        ECRAN_JEU.Dialogue.Contenu = $"Choisissez un emplacement où {outil.Verbe}!"; // plus tard {JoueurActuel.Inventaire.Semis[INVENTAIRE.Curseur].NOM}
        ECRAN_JEU.Dialogue.Afficher();
        int indiceParcelle = DemanderPositionPotager();
        int colonne = indiceParcelle % JoueurActuel.Potager.GetLength(0);
        int ligne = indiceParcelle / JoueurActuel.Potager.GetLength(0);
        outil.Actionner(JoueurActuel.Potager[colonne, ligne]);
    }
    // Actions Journal ==========================================================
    public void AfficherArticle() { }
    // Actions Magasin ==========================================================
    public void Acheter(Outil outil)
    {
        if (outil.PrixAchat <= JoueurActuel.Argent)
        {
            JoueurActuel.Argent -= outil.PrixAchat;
            JoueurActuel.Inventaire.Ajouter(outil);
            ECRAN_JEU.Dialogue.Contenu = $"{outil.Nom} acheté  ! {JoueurActuel.Inventaire.RecupererQuantite(outil)} en stock ! ";
            ECRAN_JEU.Dialogue.Afficher();
            ActualiserAffichageArgent();
            ActualiserMenuInventaire();
        }
    }
    public void Acheter(Plante semis)
    {
        if (semis.PrixAchat <= JoueurActuel.Argent)
        {
            JoueurActuel.Argent -= semis.PrixAchat;
            JoueurActuel.Inventaire.Ajouter(semis);
            ECRAN_JEU.Dialogue.Contenu = $"Semis de {semis.Nom} acheté  ! {JoueurActuel.Inventaire.RecupererQuantite(semis)} en stock ! ";
            ECRAN_JEU.Dialogue.Afficher();
            ActualiserAffichageArgent();
            ActualiserMenuInventaire();
        }
        else
        {
            ECRAN_JEU.Dialogue.Contenu = $"ERREUR : pas assez d'argent ! Vendez un de vos biens !";
            ECRAN_JEU.Dialogue.Afficher();
        }
    }
    public void Vendre() { }
    // Actions Semaine Suivante =================================================
    public void PasserSemaineSuivante()
    {
        JoueurActuel.Semaine += 1;
        ActualiserDate();
        ECRAN_JEU.Dialogue.AfficherMessageSemaine(JoueurActuel.Semaine);
        // { JoueurActuel.Semaine % 52} semaines se sont écoulées depuis votre arrivée !";
        // ECRAN_JEU.DIALOGUE.Contenu += 
        // ECRAN_JEU.DIALOGUE.Afficher();
        // ActualiserMeteo();
        // ActualiserEtatPlantes();
    }
    // Actualisation des données
    public void ActualiserDate()
    {
        ECRAN_JEU.Date.Contenu = $"{2003 + JoueurActuel.Semaine / 52} - Semaine {1 + JoueurActuel.Semaine % 52} ";
        if (JoueurActuel.Semaine % 52 < 13)
            ECRAN_JEU.Date.Contenu += "(printemps)";
        else if (JoueurActuel.Semaine % 52 < 26)
            ECRAN_JEU.Date.Contenu += "(été)";
        else if (JoueurActuel.Semaine % 52 < 39)
            ECRAN_JEU.Date.Contenu += "(automne)";
        else
            ECRAN_JEU.Date.Contenu += "(hiver)";
        ECRAN_JEU.Date.Afficher();
    }
    public void ActualiserAffichageArgent()
    {
        int argent = JoueurActuel.Argent;
        ECRAN_JEU.Argent.Contenu = $"{argent} 💰";
        ECRAN_JEU.Argent.Afficher();
    }
    // Otuils =====================================================================
    public void Arroser()
    {
        int indice = DemanderPositionPotager();
        int colonne = indice % JoueurActuel.Potager.GetLength(0);
        int ligne = indice / JoueurActuel.Potager.GetLength(0);
        // JoueurActuel.Potager[colonne,ligne].Contenu.Arroser(indice);
        // //si dessous besoineau , augmente l'hydratation de +15%
        // //si au dessus besoineau , attention plante surhydraté et santé -20
        // if (parcelle.Contenu.QuantiteEau < parcelle.Contenu.BESOIN_EAU)
        // {
        //     parcelle.Contenu.QuantiteEau += (int)(parcelle.Contenu.BESOIN_EAU * 0.15);
        // }
        // else
        // {
        //     parcelle.Contenu.Sante -= 20;
        // }
    }
    public void DeclencherModeUrgence(int scenario = 0)
    {
        ECRAN_JEU.Urgence.Racine.Description = "URGENCE ! {}";
        Console.ForegroundColor = ConsoleColor.Red;
        ECRAN_JEU.AfficherLignesDirectrices();
        //JoueurAnimation();
        

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