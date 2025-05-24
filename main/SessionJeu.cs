// =======================================================================
// Classe SessionJeu
// -----------------------------------------------------------------------
// Cette classe centralise la logique principale du jeu
// Elle gère :
//   - Les menus et l'affichage
//   - La navigation utilisateur
//   - Les actions sur l'inventaire, le magasin et le potager
//   - La progression du temps et les événements spéciaux
// =======================================================================
public class SessionJeu
{
    public Joueur JoueurActuel { get; private set; }
    public Date DateActuelle { get; private set; }
    public GestionnaireMeteo Meteo { set; private get; }
    public InterfaceAccueil EcranAccueil { get; private set; }
    public ZoneEcranJeu EcranJeu { get; private set; }
    public ConsoleKeyInfo Touche { get; private set; }
    private bool ModeUregenceActif { get; set; }
    private Random rng = new Random();
    public SessionJeu()
    {
        Touche = new ConsoleKeyInfo();
        EcranJeu = new ZoneEcranJeu();
        EcranAccueil = new InterfaceAccueil(0, 0, Console.WindowWidth, Console.WindowHeight - 1);
        ConstruireMenus();
        EcranJeu.ZoneActive = EcranAccueil.Accueil;
        JoueurActuel = new Joueur();
        DateActuelle = JoueurActuel.DateActuelle;
        Meteo = new GestionnaireMeteo("Carcassonne", DateActuelle);
        ModeUregenceActif = false;

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
        ApprendreJeu.Description = (
            "Bienvenue dans Ensemence !\n" +
            "Dans ce jeu de gestion de potager, vous incarnez un cultivateur qui doit semer, entretenir et récolter ses cultures au fil des saisons, que vous soyez à Carcassonne, Soconusco ou Hokkaido. Gérez votre inventaire, faites face à la météo et aux imprévus, et développez votre exploitation !\n\n" +
            "Navigation dans le jeu :\n" +
            "- Utilisez les flèches directionnelles pour vous déplacer dans les menus ou à travers le champ.\n" +
            "- Appuyez sur Entrée pour valider une sélection.\n" +
            "- Utilisez la touche Retour arrière ou Echap pour revenir en arrière.\n" +
            "- Raccourcis clavier :\n" +
            "    P : Potager\n" +
            "    I : Inventaire\n" +
            "    J : Journal\n" +
            "    M : Magasin\n" +
            "    S : Semaine suivante\n" +
            "    C : Commandes de base (aide)\n" +
            "    U : Tester l'affichage en mode urgence \n" +
            "    X : Quitter instantanément la partie\n" +
            "\nAppuyez sur la touche ECHAP ou RETOUR pour continuer"
        );
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
        EcranJeu.Meteo.Contenu = Meteo.ToString();
        EcranJeu.Date.Afficher();
        EcranJeu.ActualiserAffichageMeteo(Meteo, DateActuelle);
        EcranJeu.ActualiserAffichageArgent(JoueurActuel.Argent);
        EcranJeu.ChampsEtDetails.Synchroniser();
        EcranJeu.ChampsEtDetails.Afficher();
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
                    PresenterAide(); // 5 = fenêtre d'aide aux commandes de base
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
                    if (!ModeUregenceActif) DeclencherModeUrgence(); else RevenirModeNormal();
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
        Console.ResetColor();
        Console.Clear();
        EcranAccueil.Afficher();
        EcranAccueil.Accueil.Afficher();
        Naviguer();

    }
    // ------------------------------------------------------------------------
    public void DemarrerNouvellePartie(string ville)
    {
        // on fait le choix de déterminer la taille du champs en fonction de la taille de fenêtre
        JoueurActuel = new Joueur(ville, EcranJeu.Champs.Largeur, EcranJeu.Hauteur);
        Meteo = new GestionnaireMeteo(ville, DateActuelle);
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
    private void PresenterAide()
    {
        Console.Clear();
        EcranAccueil.Accueil.Racine.Items[1].Actionner();
        do
            Touche = Console.ReadKey(intercept: true);
        while (Touche.Key != ConsoleKey.Escape && Touche.Key != ConsoleKey.Backspace);
        EcranJeu.Afficher();

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
        if (plante.SaisonSemis == DateActuelle.Saison)
        {
            EcranJeu.Dialogue.Contenu = $"OPERATION ANNULEE : cette plante ne se sème pas durant cette saison !";
        }
        else if (JoueurActuel.Potager[colonne, ligne].Libre)
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
            EcranJeu.Dialogue.Contenu = $"{JoueurActuel.Potager[colonne, ligne].Plant.Nom} OPERATION ANNULEE : cet emplacement n'est pas libre ! Utilisez la pelle";

        }
        ActualiserMenuInventaire();

        EcranJeu.Dialogue.Afficher();
        EcranJeu.BasculerSurZone(1); // retour à l'inventaire
    }
    // Actions Inventaire > Outils ==============================================
    public void UtiliserOutil(Outil outil)
    {
        EcranJeu.Dialogue.Contenu = $"Choisissez un emplacement où {outil.Verbe}!"; // plus tard {JoueurActuel.Inventaire.Semis[INVENTAIRE.Curseur].NOM}
        EcranJeu.Dialogue.Afficher();
        int indiceParcelle = DemanderPositionPotager();
        int colonne = indiceParcelle % JoueurActuel.Potager.GetLength(0);
        int ligne = indiceParcelle / JoueurActuel.Potager.GetLength(0);
        bool succes = outil.Actionner(JoueurActuel.Potager[colonne, ligne]);
        EcranJeu.Dialogue.Contenu = succes ? outil.MessageSucces : outil.MessageEchec;
        EcranJeu.Dialogue.Contenu += " Retour à l'inventaire.";
        EcranJeu.Dialogue.Afficher();
        if (outil.Nom == "Panier")
        {
            ViderPanier();
        }
        EcranJeu.BasculerSurZone(1);//retour à l'inventaire
        ActualiserAffichage();
    }
    public void ViderPanier()
    {   // on vide le PanierRecoltes de chaque parcelle
        foreach (Parcelle parcelle in JoueurActuel.Potager)
        {
            for (int i = 0; i < parcelle.Plant.PanierRecoltes; i++)
            {
                JoueurActuel.Inventaire.Ajouter(parcelle.Plant.TypeRecolte);
            }
            parcelle.Plant.PanierRecoltes = 0;
        }

    }
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
        EcranJeu.ActualiserAffichageArgent(JoueurActuel.Argent);
        EcranJeu.Magasin.Afficher();

    }
    // Actions Semaine Suivante =================================================
    public void PasserSemaineSuivante()
    {
        DeterminerDeclenchementModeUrgence();
        DateActuelle.Avancer();
        // if(Date.Annee()==)
        Meteo.AppliquerMeteo(JoueurActuel.Potager, DateActuelle);
        FaireVieillirPotager();
        EcranJeu.Dialogue.AfficherMessageSemaine(DateActuelle);
        EcranJeu.ChampsEtDetails.Afficher();
        ActualiserAffichage();
        EcranJeu.Meteo.Afficher();
    }
    // Actualisation des données==================================================
    public void FaireVieillirPotager()
    {
        List<Plante> PlantesPossédées = [];
        for (int colonne = 0; colonne < JoueurActuel.Potager.GetLength(0); colonne++)
        {
            for (int ligne = 0; ligne < JoueurActuel.Potager.GetLength(1); ligne++)
            {
                JoueurActuel.Potager[colonne, ligne].AppliquerConditionsHebdomadaires();
                if (JoueurActuel.Potager[colonne, ligne].Plant.Type != "plante vide")
                {
                    PlantesPossédées.Add(JoueurActuel.Potager[colonne, ligne].Plant);
                }
            }
        }
        PlantesPossédées[rng.Next(PlantesPossédées.Count())].AjouterNuisible();
    }
    // Outils =====================================================================
    public void DeterminerDeclenchementModeUrgence()
    {
        if (rng.Next(53) < 4) // proba d'apparition 4 fois dans une année
            return;
        if (DateActuelle.Saison == 0)
        {
            ElementMenu typeUrgence = new ElementMenu(EcranJeu.Urgence, "URGENCE EN COURS", "Du gel est à prévoir l'année prochaine ! Que voulez vous faire ?");
            ElementMenuInventaireOutil solution = new ElementMenuInventaireOutil(EcranJeu.Urgence, "Protéger ses parcelles avec une serre", this, new IrrigationUrgence());

        }
        ElementMenu alternative = new ElementMenu(EcranJeu.Urgence, "Ne rien faire, tant pis");
        { }
    }
    public void DeclencherModeUrgence(int scenario = 0)
    {
        ModeUregenceActif = true;
        EcranJeu.Urgence.Racine.Description = "URGENCE ! Faites quelque chose ! ";
        EcranJeu.Mode.Contenu = "MODE URGENCE";
        EcranJeu.Mode.CouleurTexte = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.Red;
        EcranJeu.AfficherLignesDirectrices();
        Console.ResetColor();
        EcranJeu.AfficherContenu();
    }
    public void RevenirModeNormal()
    {
        ModeUregenceActif = false;
        Console.ResetColor();
        EcranJeu.Mode.Contenu = "Mode Normal";
        EcranJeu.Mode.CouleurTexte = ConsoleColor.White;
        EcranJeu.Afficher();
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
    //     }
    //   }


    //   }

    // };
}