using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

public class SessionJeu
{
    public Joueur JoueurActuel { get; set; }
    public Date DateActuelle { get; set; }
    public GestionnaireMeteo Meteo { set; get; }
    public InterfaceAccueil EcranAccueil { get; set; }
    public ZoneEcranJeu EcranJeu { get; set; }
    public ConsoleKeyInfo Touche { get; set; }
    public List<ZoneMenu> MenusPrincipaux = [];
    public List<ZoneMenu> MenusUrgence = [];
    public SessionJeu()
    {
        Touche = new ConsoleKeyInfo();
        EcranJeu = new ZoneEcranJeu();
        EcranAccueil = new InterfaceAccueil(0, 0, Console.WindowWidth, Console.WindowHeight - 1);
        ConstruireMenus();
        EcranJeu.ZoneActive = EcranAccueil.Accueil;
        JoueurActuel = new Joueur();
        DateActuelle = JoueurActuel.DateActuelle;
        Meteo = new GestionnaireMeteo("Carcassonne",DateActuelle);

    }
    // Initialisation ===========================================================
    // Gestion des menus ======================================================
    public void ConstruireMenus()
    {
        EcranAccueil.Accueil.Racine.Description = "Bienvenue dans Ensemence! Faites votre choix parmi la sélection ci-dessous";
        ElementMenu NouvellePartie = new ElementMenu(EcranAccueil.Accueil, "Commencer une nouvelle partie", "Sélectionnez le pays dans lequel vous voulez jouer !");
        NouvellePartie.AjouterItem(new ElementMenuNouvellePartie(EcranAccueil.Accueil, "Carcassonne (France)", this));
        NouvellePartie.AjouterItem(new ElementMenuNouvellePartie(EcranAccueil.Accueil, "Soconusco (Mexique)", this));
        NouvellePartie.AjouterItem(new ElementMenuNouvellePartie(EcranAccueil.Accueil, "Hokkaido (Japon)", this));

        ElementMenu ApprendreJeu = new ElementMenu(EcranAccueil.Accueil, "Apprendre les commandes de base");
        EcranAccueil.Accueil.Racine.AjouterItem(NouvellePartie);
        EcranAccueil.Accueil.Racine.AjouterItem(ApprendreJeu);

        EcranJeu.Inventaire.Racine.Description = "Inventaire - Choisissez parmis les catégories de votre inventaire";
        ElementMenu MenuOutils = new ElementMenu(EcranJeu.Inventaire, "Outils", "Choisissez un de vos outils à utiliser !");
        ElementMenu MenuSemis = new ElementMenu(EcranJeu.Inventaire, "Semis", "Choisissez un de vos semis à planter !");
        ElementMenu MenuRecoltes = new ElementMenu(EcranJeu.Inventaire, "Récoltes", "Voici la liste de vos récoltes !");
        EcranJeu.Inventaire.Racine.AjouterItem(MenuOutils);
        EcranJeu.Inventaire.Racine.AjouterItem(MenuSemis);
        EcranJeu.Inventaire.Racine.AjouterItem(MenuRecoltes);

        EcranJeu.Magasin.Racine.Description = "Magasin - Acheter ou vendre, c'est vous qui choisissez ! ";
        ElementMenu Acheter = new ElementMenu(EcranJeu.Magasin, "Acheter", "Choisissez parmis les catégories d'articles à acheter !");
        ElementMenu Vendre = new ElementMenu(EcranJeu.Magasin, "Vendre", "Choisissez une de vos récoltes à vendre !");
        EcranJeu.Magasin.Racine.AjouterItem(Acheter);
        EcranJeu.Magasin.Racine.AjouterItem(Vendre);

        foreach (Plante plante in Plante.ListePlantes)
        {
            Acheter.AjouterItem(new ElementMenuMagasinAchatSemis(EcranJeu.Magasin, this, plante));
        }
        foreach (Outil outil in Outil.ListeOutils)
        {
            Acheter.AjouterItem(new ElementMenuMagasinAchatOutil(EcranJeu.Magasin, this, outil));
        }

        EcranJeu.Journal.Racine.Description = "Renseignez vous sur le fonctionnement du jeu !";
        ElementMenu Plantes = new ElementMenu(EcranJeu.Journal, "Plantes", "Trouvez tout ce dont vous avez besoin de savoir à propos des plantes !");
        ElementMenu Meteo = new ElementMenu(EcranJeu.Journal, "Meteo", "Devenez incollables sur la météo !");
        EcranJeu.Journal.Racine.AjouterItem(Plantes);
        EcranJeu.Journal.Racine.AjouterItem(Meteo);
        
        EcranJeu.Suivant.Racine.Description = "Voulez vous vraiment passer à la semaine suivante ? Vous ne pourrez pas revenir en Arrière";
        ElementMenu Oui = new ElementMenuSuivant(EcranJeu.Suivant, "Oui je le veux", this);
        EcranJeu.Suivant.Racine.AjouterItem(Oui);
    }
    // ------------------------------------------------------------------------
    public void ActualiserMenuInventaire()
    {
        // Mise à jour des semis
        List<ElementMenu> itemsSemis = new List<ElementMenu>();
        if (JoueurActuel.Inventaire.Semis.Count == 0)
        {
            EcranJeu.Inventaire.Racine.Items[1].Description = "Vous n'avez plus aucun semis ! Allez au magasin en acheter !";
        }
        else
        {
            foreach (ItemInventaireSemis item in JoueurActuel.Inventaire.Semis)
            {
                itemsSemis.Add(new ElementMenuInventaireSemis(EcranJeu.Inventaire, $"{item.Contenu.Emoji} {item.Contenu.Nom} ({item.Quantite} en stock)", this, item.Contenu));
            }
            EcranJeu.Inventaire.Racine.Items[1].Description = "Choisissez un de vos semis à planter !";
        }
        EcranJeu.Inventaire.Racine.Items[1].Items = itemsSemis;

        // Mise à jour des outils
        List<ElementMenu> itemsOutils = new List<ElementMenu>();
        if (JoueurActuel.Inventaire.Outils.Count == 0)
        {
            EcranJeu.Inventaire.Racine.Items[0].Description = "Vous n'avez plus aucun outil ! Allez au magasin en acheter !";
        }
        else
        {
            foreach (ItemInventaireOutil item in JoueurActuel.Inventaire.Outils)
            {
                itemsOutils.Add(new ElementMenuInventaireOutil(EcranJeu.Inventaire, $"{item.Contenu.Emoji} {item.Contenu.Nom} ({item.Quantite} en stock)", this, item.Contenu));
            }
            EcranJeu.Inventaire.Racine.Items[0].Description = "Choisissez un de vos outils à utiliser !";
        }
        EcranJeu.Inventaire.Racine.Items[0].Items = itemsOutils;
        EcranJeu.Inventaire.Curseur = 0;

        List<ElementMenu> itemsRecoltes = new List<ElementMenu>();
        if (JoueurActuel.Inventaire.Recoltes.Count == 0)
        {
            EcranJeu.Inventaire.Racine.Items[2].Description = "Aucune récolte dans l'inventaire ! Allez planter des semis ou récolter ce qu'ils ont produit !";
        }
        else
        {
            foreach (ItemInventaireRecolte item in JoueurActuel.Inventaire.Recoltes)
            {
                itemsRecoltes.Add(new ElementMenuInventaireRecolte(EcranJeu.Inventaire, $"{item.Contenu.Emoji} {item.Contenu.Nom} ({item.Quantite} en stock)", this, item.Contenu));
            }
            EcranJeu.Inventaire.Racine.Items[2].Description = "Voici la liste de vos récoltes !";
        }
        EcranJeu.Inventaire.Racine.Items[2].Items = itemsRecoltes;
        EcranJeu.Inventaire.Curseur = 0;

    }
    public void ActualiserMenuVente()
    {
        // Mise à jour des semis
        List<ElementMenu> itemsRecoltes = new List<ElementMenu>();
        if (JoueurActuel.Inventaire.Recoltes.Count == 0)
        {
            EcranJeu.Magasin.Racine.Items[1].Description = "Vous n'avez aucune récolte ! Allez planter des semis ou récolter ce qu'ils ont produit !";
        }
        else
        {
            foreach (ItemInventaireRecolte item in JoueurActuel.Inventaire.Recoltes)
            {
                itemsRecoltes.Add(new ElementMenuMagasinVenteRecolte(EcranJeu.Inventaire, $"{item.Contenu.Emoji} {item.Contenu.Nom} Prix : {item.Contenu.PrixVente} 🪙 ({item.Quantite} en stock)", this, item.Contenu));
            }
            EcranJeu.Magasin.Racine.Items[1].Description = "Voici la liste de vos récoltes !";
        }
        EcranJeu.Magasin.Racine.Items[1].Items = itemsRecoltes;
        EcranJeu.Magasin.Curseur = 0;
    }
    // Affichage ==============================================================
    public void ActualiserAffichage()
    {

        EcranJeu.Date.Contenu = DateActuelle.ToString();
        EcranJeu.Date.Afficher();
        EcranJeu.Meteo.Contenu = Meteo.ToString();
        EcranJeu.Meteo.Afficher();
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
                EcranJeu.ZoneActive.DeplacerCurseur("gauche");
            }
            else if (Touche.Key == ConsoleKey.RightArrow)
            {
                EcranJeu.ZoneActive.DeplacerCurseur("droite");
            }
            else if (Touche.Key == ConsoleKey.UpArrow)
            {
                EcranJeu.ZoneActive.DeplacerCurseur("haut");
            }
            else if (Touche.Key == ConsoleKey.DownArrow)
            {
                EcranJeu.ZoneActive.DeplacerCurseur("bas");
            }
            else if (Touche.Key == ConsoleKey.Enter)
            {
                EcranJeu.ZoneActive.ValiderSelection();
            }
            else if (Touche.Key == ConsoleKey.Escape || Touche.Key == ConsoleKey.Backspace)
            {
                EcranJeu.ZoneActive.RetournerEnArriere();
            }
            else if (EcranJeu.ZoneActive != EcranAccueil.Accueil)
            {

                if (Touche.Key == ConsoleKey.C)
                {
                    //Comment jouer?
                    EcranJeu.BasculerSurZone(6); // 0 = fenêtre d'aide aux commandes de base

                }
                else if (Touche.Key == ConsoleKey.P)
                {
                    EcranJeu.BasculerSurZone(0); // 0 = Potager
                }
                else if (Touche.Key == ConsoleKey.I)
                {
                    EcranJeu.BasculerSurZone(1); // 1 = Inventaire
                }
                else if (Touche.Key == ConsoleKey.J)
                {
                    EcranJeu.BasculerSurZone(2); // 2 = Journal
                }
                else if (Touche.Key == ConsoleKey.M)
                {
                    EcranJeu.BasculerSurZone(3); // 3 = Magasin
                }
                else if (Touche.Key == ConsoleKey.S)
                {
                    EcranJeu.BasculerSurZone(4); // 4 Suivant
                }
                else if (Touche.Key == ConsoleKey.W)
                {
                    // Champs.ChangerVue()
                    JoueurActuel.Argent *= 200;
                    EcranJeu.ActualiserAffichageArgent(JoueurActuel.Argent);
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
        EcranJeu.BasculerSurZone(0);
        bool choixFait = false;
        while (!choixFait)
        {
            touche = Console.ReadKey(intercept: true);
            if (touche.Key == ConsoleKey.LeftArrow)
            {
                EcranJeu.ZoneActive.DeplacerCurseur("gauche");
            }
            else if (touche.Key == ConsoleKey.RightArrow)
            {
                EcranJeu.ZoneActive.DeplacerCurseur("droite");
            }
            else if (touche.Key == ConsoleKey.UpArrow)
            {
                EcranJeu.ZoneActive.DeplacerCurseur("haut");
            }
            else if (touche.Key == ConsoleKey.DownArrow)
            {
                EcranJeu.ZoneActive.DeplacerCurseur("bas");
            }
            else if (touche.Key == ConsoleKey.Enter)
            {
                choixFait = true;
            }
        }
        return EcranJeu.Champs.Curseur;
    }
    // Lancement d'une partie =================================================
    public void Demarrer()
    {
        Console.Clear();
        EcranAccueil.Afficher();
        EcranAccueil.Accueil.Afficher();
        Naviguer();

    }
    // ------------------------------------------------------------------------
    public void DemarrerNouvellePartie(string ville)
    {
        JoueurActuel = new Joueur(ville);
        Meteo = new GestionnaireMeteo(ville,DateActuelle);
        // on met à jour les zones d'affichage liées au joueur
        EcranJeu.Lieu.Contenu = JoueurActuel.Lieu;
        EcranJeu.Date.Contenu = DateActuelle.ToString();
        EcranJeu.Meteo.Contenu = Meteo.ToString();
        EcranJeu.Champs.Synchroniser(JoueurActuel.Potager);
        EcranJeu.BasculerSurZone(0);
        EcranJeu.ChampsEtDetails.Champs = EcranJeu.Champs;
        EcranJeu.ZoneActive = EcranJeu.ChampsEtDetails;

        EcranJeu.Afficher();
        ActualiserMenuInventaire();
        ActualiserMenuVente();
    }
    // ACTIONS DURANT LA PARTIE
    // Actions Inventaire =======================================================
    public void PlanterSemis(Plante plante)
    {
        EcranJeu.Dialogue.Contenu = "Choisissez un emplacement où planter votre semis!"; // plus tard {JoueurActuel.Inventaire.Semis[INVENTAIRE.Curseur].NOM}
        EcranJeu.Dialogue.Afficher();
        int indiceParcelle = DemanderPositionPotager();
        int colonne = indiceParcelle % JoueurActuel.Potager.GetLength(0);
        int ligne = indiceParcelle / JoueurActuel.Potager.GetLength(0);
        if (JoueurActuel.Potager[colonne, ligne].Libre)
        {
            JoueurActuel.Potager[colonne, ligne].Planter(plante);//on ajoute la plante au potager
            JoueurActuel.Inventaire.Retirer(plante); //on actualise l'inventaire
            EcranJeu.Champs.Synchroniser(JoueurActuel.Potager);  //on actualise le menu 
            //on actualise l'affichage
            EcranJeu.Dialogue.Contenu = $"{plante.Nom} Ajouté ! en stock : {JoueurActuel.Inventaire.RecupererQuantite(plante)}. Retour à l'inventaire";
            EcranJeu.ChampsEtDetails.Afficher();
        }
        else
        {
            EcranJeu.Dialogue.Contenu = $"{JoueurActuel.Potager[colonne, ligne].Plant.Nom} OPERATION ANNULEE : cet emplacement n'est pas libre ! Utilisez la pelle pour libérer cet emplacement";

        }
        ActualiserMenuInventaire();

        EcranJeu.Dialogue.Afficher();
        EcranJeu.BasculerSurZone(1); // retour à l'inventaire
    }
    public void UtiliserOutil(Outil outil)
    {
        EcranJeu.Dialogue.Contenu = $"Choisissez un emplacement où {outil.Verbe}!"; // plus tard {JoueurActuel.Inventaire.Semis[INVENTAIRE.Curseur].NOM}
        EcranJeu.Dialogue.Afficher();
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
            EcranJeu.Dialogue.Contenu = $"{outil.Nom} acheté  ! {JoueurActuel.Inventaire.RecupererQuantite(outil)} en stock ! ";
            EcranJeu.Dialogue.Afficher();
            EcranJeu.ActualiserAffichageArgent(JoueurActuel.Argent);
            ActualiserMenuInventaire();
        }
        else
        {
            EcranJeu.Dialogue.Contenu = $"ERREUR : pas assez d'argent ! Vendez un de vos biens !";
            EcranJeu.Dialogue.Afficher();
        }
    }
    public void Acheter(Plante semis)
    {
        if (semis.PrixAchat <= JoueurActuel.Argent)
        {
            JoueurActuel.Argent -= semis.PrixAchat;
            JoueurActuel.Inventaire.Ajouter(semis);
            EcranJeu.Dialogue.Contenu = $"Semis de {semis.Nom} acheté  ! {JoueurActuel.Inventaire.RecupererQuantite(semis)} en stock ! ";
            EcranJeu.Dialogue.Afficher();
            EcranJeu.ActualiserAffichageArgent(JoueurActuel.Argent);
            ActualiserMenuInventaire();
        }
        else
        {
            EcranJeu.Dialogue.Contenu = $"ERREUR : pas assez d'argent ! Vendez un de vos biens !";
            EcranJeu.Dialogue.Afficher();
        }
    }

    public void Vendre(Recolte recolte)
    {

        JoueurActuel.Argent += recolte.PrixAchat;
        JoueurActuel.Inventaire.Retirer(recolte);
        EcranJeu.Dialogue.Contenu = $"Vente de {recolte.Nom} effectuée ! {JoueurActuel.Inventaire.RecupererQuantite(recolte)} en stock ! ";
        EcranJeu.Dialogue.Afficher();
        EcranJeu.ActualiserAffichageArgent(JoueurActuel.Argent);
        ActualiserMenuInventaire();
        ActualiserMenuVente();
        EcranJeu.Magasin.Afficher();
        
    }
    // Actions Semaine Suivante =================================================
    public void PasserSemaineSuivante()
    {
        DeterminerDeclenchementModeUrgence();
        DateActuelle.Avancer();
        FaireVieillirPlantes();
        Meteo.AppliquerMeteo(JoueurActuel.Potager, DateActuelle);
        EcranJeu.Dialogue.AfficherMessageSemaine(DateActuelle);
        EcranJeu.ChampsEtDetails.Afficher();
        ActualiserAffichage();
        EcranJeu.Meteo.Afficher();
    }
    // Actualisation des données
    public void ActualiserAffichageDate()
    {
        
    }
    //
    public void FaireVieillirPlantes()
    {
        for (int colonne = 0; colonne < JoueurActuel.Potager.GetLength(0); colonne++)
        {
            for (int ligne = 0; ligne < JoueurActuel.Potager.GetLength(1); ligne++)
            {
                Plante plante = JoueurActuel.Potager[colonne, ligne].Plant;
                Terrain sol = JoueurActuel.Potager[colonne, ligne].Sol;

                if (!plante.Mature)
                {
                    plante.Croissance += plante.VitesseCroissance + sol.Fertilite;

                    if (plante.Croissance >= 100)
                        plante.Mature = true;
                }
                sol.TauxHumidite -= 10;
                sol.TauxExposition -= 5;

            }
         }       
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
        EcranJeu.Urgence.Racine.Description = "URGENCE ! {}";
        Console.ForegroundColor = ConsoleColor.Red;
        EcranJeu.AfficherLignesDirectrices();
        //JoueurAnimation();
    }
    public void DeterminerDeclenchementModeUrgence() { }


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