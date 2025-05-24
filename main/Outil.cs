// CLASSE OUTIL
/* ============================================================================
    Les classes Outil et ses dérivées représentent les outils utilisables dans le jeu pour interagir avec les parcelles.

    La classe abstraite Outil définit :
    - les propriétés communes à tous les outils (nom, emoji, prix, messages, etc.)
    - la méthode abstraite Actionner(Parcelle) à implémenter pour chaque outil
    - une liste statique ListeOutils contenant tous les outils disponibles

    Chaque classe dérivée de Outil (Arrosoir, Panier, Pioche, Secateur, CD, Fumier, Traitement, Coccinelle, Megaphone, Serre, IrrigationUrgence, Paillage) représente un outil spécifique avec :
    - ses propres messages et effets sur la parcelle ou la plante
    - une implémentation personnalisée de la méthode Actionner(Parcelle) qui applique l'effet de l'outil

    Exemples d'effets :
    - Arrosoir : augmente l'hydratation du sol
    - Panier : permet de récolter les plantes
    - Pioche : améliore le sol ou retire une plante
    - Secateur : taille la plante ou retire des nuisibles
    - CD, Coccinelle, Megaphone : repoussent ou éliminent certains nuisibles
    - Fumier : fertilise le sol
    - Traitement : élimine maladies ou champignons
    - Serre, Paillage : protègent la plante
    - IrrigationUrgence : outil spécial utilisable en cas d'urgence

    Ces classes permettent d'encapsuler la logique de chaque outil et de faciliter leur utilisation dans le jeu.
*/
public abstract class Outil : ObjetJeuAchatVente
{
    public static List<Outil> ListeOutils = new List<Outil>
    {
        new Arrosoir(),
        new Panier(),
        new Secateur(),
        new CD(),
        new Fumier(),
        new Traitement(),
        new Coccinelle(),
        new Megaphone(),
        new Serre(),
        new IrrigationUrgence(),
        new Paillage(),
        new Panier(),
        new Pioche(),
    };
    public string MessageInitial { get; set; }
    public string MessageSucces { get; protected set; }
    public string MessageEchec { get; protected set; }
    public string Verbe { get; set; }
    public bool UsageUrgence { get; set; }
    public abstract bool Actionner(Parcelle parcelle);


    protected Outil(string nom, string emoji, int decallageAffichage, int prixAchat, string verbe, string messageInitial, string messageSucces, string messageEchec, bool usageUrgence = false) : base(nom, emoji, decallageAffichage, prixAchat)
    {

        Verbe = verbe;
        UsageUrgence = usageUrgence;
        MessageInitial = messageInitial;
        MessageSucces = messageSucces;
        MessageEchec = messageEchec;
    }
}
public class Arrosoir : Outil
{
    public Arrosoir() : base("Arrosoir", "💦", 0, 50, "arroser", "Choisissez une parcelle à arroser !", "Plante arrosée !", "Vous ne pouvez pas arroser ici !") { }
    public override bool Actionner(Parcelle parcelle)
    {
        //si dessous besoineau , augmente l'hydratation de +15%
        //si au dessus besoineau , attention plante surhydraté et santé -20
        parcelle.Sol.Arroser(15);
        return true;
    }
}

public class Panier : Outil
{
    public Panier() : base("Panier", "🧺", 0, 40, "ramasser vos récoltes", "Choisissez une parcelle à récolter !", "Récolte(s) ajoutée(s) à votre inventaire !"
, "Aucune récolte disponible sur cette parcelle !")
    { }
    public override bool Actionner(Parcelle parcelle)
    {

        return parcelle.PlacerRecoltesDansPanier();
    }
}
public class Pioche : Outil
{
    public Pioche() : base(
        "Pioche", "⛏️", 0, 90, "creuser", "Choisissez une parcelle à travailler avec la pioche !", "Sol travaillé, drainage amélioré ou cailloux retirés !", "Impossible d'utiliser la pioche ici !"
    )
    { }

    public override bool Actionner(Parcelle parcelle)
    {
        // Exemple d'effet : améliore le drainage ou enlève les cailloux
        // À adapter selon ta logique de jeu
        if (parcelle.Plant.Type != "plante vide")
        {
            parcelle.DeterrerPlante();
        }
        return true;
    }
}
public class Secateur : Outil
{
    public Secateur() : base("Secateur", "🪓", 1, 60, "tailler", "Choisissez une parcelle à tailler !", "Plante taillée avec succès !", "Impossible de tailler ici !"
)
    { }
    public override bool Actionner(Parcelle parcelle)
    {
        //si prend trop de place , tailler et cases -2
        //si chenille , tailler et chenille -1
        //sinon, abime la plante et santé -15
        if (parcelle.Plant.Espace > 6)
        {
            parcelle.Plant.Espace -= 2;
        }
        else if (parcelle.Contient("Chenille"))
        {
            parcelle.Retirer("Chenille");
        }
        else
        {
            parcelle.Plant.Sante -= 15;
        }
        return true;
    }
}

public class CD : Outil
{
    public CD() : base("CD", "💿", 0, 20, "installer votre CD", "Choisissez une parcelle à protéger avec un CD !", "CD installé, les nuisibles sont repoussés !", "Aucun effet, les nuisibles ne sont pas concernés !"
    )
    { }
    public override bool Actionner(Parcelle parcelle)
    {
        //si oiseau, cd et oiseau -1
        //si chenille, cd et chenille -1
        //sinon , sert de déco mais inutile
        if (parcelle.Contient("Oiseau"))
        {
            parcelle.Retirer("Oiseau");
        }
        else if (parcelle.Contient("Chenille"))
        {
            parcelle.Retirer("Chenille");
        }
        parcelle.Plant.Options.Add(this);
        // sinon, décoration uniquement
        return true;
    }
}

public class Fumier : Outil
{
    public Fumier() : base("Fumier", "💩", 0, 30, "mettre du fumier", "Choisissez une parcelle à fertiliser !", "Fumier ajouté, croissance boostée !", "Sol déjà fertilisé ici !"
)
    {
    }
    public override bool Actionner(Parcelle parcelle)
    {
        // la fertilité est remise à son max
        parcelle.Sol.Fertilite = 10;

        return true;
    }
}

public class Traitement : Outil
{
    public Traitement() : base("Traitement", "🧪", 0, 80, "traiter", "Choisissez une parcelle à traiter !", "Traitement appliqué, nuisible(s) éliminé(s) !", "Aucun nuisible à traiter sur cette parcelle !"
)
    {
    }
    public override bool Actionner(Parcelle parcelle)
    {
        //si maladies , traitement => maladie-1
        //si champignon , traitement => champi-1
        //sinon , trop de chimie => santé -20
        if (parcelle.Contient("Maladie"))
        {
            parcelle.Retirer("Maladie");
        }
        else if (parcelle.Contient("Champignon"))
        {
            parcelle.Retirer("Champignon");
        }
        else
        {
            parcelle.Plant.Sante -= 20;
        }
        return true;
    }
}

public class Coccinelle : Outil
{
    public Coccinelle() : base("Coccinnelle", "🐞", 0, 70, "déposer vos coccinelles", "Choisissez une parcelle à déposer des coccinelles !", "Coccinelles déposées, les pucerons sont éliminés !", "Aucun puceron à éliminer ici !"
)
    {
    }
    public override bool Actionner(Parcelle parcelle)
    {
        //si pucerons, coccinelle => pucerons -1
        //sinon , fait joli
        if (parcelle.Contient("Pucerons"))
        {
            parcelle.Retirer("Pucerons");
        }
        
        // sinon, jolie décoration
            return true;
    }
}

public class Megaphone : Outil
{
    public Megaphone() : base("Mégaphone", "📢", 100, 0, "faire peur", "Choisissez une parcelle à surveiller !", "Nuisible effrayé ou plante renforcée !", "Aucun effet, aucun nuisible concerné !"
)
    {

    }
    public override bool Actionner(Parcelle parcelle)
    {
        //si oiseaux, => oiseau-1
        //si lapin, => lapin-1
        //sinon , sert de prévention et santé =+10
        for (int i = 0; i < parcelle.Plant.NuisiblesActuels.Count; i++)
            if (parcelle.Contient("Oiseau"))
            {
                parcelle.Retirer("Oiseau");
            }
            else if (parcelle.Contient("Lapin"))
            {
                parcelle.Retirer("Lapin");
            }
            else
            {
                parcelle.Plant.Sante += 10;
            }
        return true;
    }
}
public class Serre : Outil
{ 
    public Serre() : base("Serre", "⛺️", 0, 200, "protéger vos récoltes", "Choisissez une parcelle à protéger avec une serre !", "Serre installée, la plante est protégée !", "Impossible d’installer une serre ici !", true
)
    { }
    public override bool Actionner(Parcelle parcelle)
    {
        if (!parcelle.Contient(this.Nom))
        {
            parcelle.Plant.Options.Add(this);
            return true;
        }
        return false;
    }
}

public class IrrigationUrgence : Outil
{
    public IrrigationUrgence() : base("Irrigation d'urgence", "🚿", 0, 120, "attention", "Choisissez une parcelle à irriguer en urgence !", "Irrigation d'urgence effectuée !", "Vous ne pouvez utiliser cet objet qu'en cas d'urgence !", true
)
    { }
    public override bool Actionner(Parcelle parcelle)
    {
        return false; // outil non implémenté 
    }
}

public class Paillage : Outil
{
    public Paillage() : base("Paillage", "🍂", 25, 0, "attention", "Choisissez une parcelle à pailler !", "Paillage appliqué, la plante est protégée !", "Impossible de pailler ici !"
)
    { }
    public override bool Actionner(Parcelle parcelle)
    {
        //permet de limiter les maladies : A CODER
        //si trop d'eau, paille absorbe : A CODER
        if (parcelle.Sol.TauxHumidite < parcelle.Plant.BesoinEau)
        {
            // sauve les plantes mais sante -5
            parcelle.Sol.TauxHumidite = parcelle.Plant.BesoinEau;
        }
        else if (parcelle.Sol.TauxHumidite > 80)
        {
            parcelle.Sol.TauxHumidite = 75;
        }
        // on ajoute le paillage aux options de la plante
        if (!parcelle.Contient(this.Nom))
        {
            parcelle.Plant.Options.Add(this);
            return true;
        }
        return false;
    }
}
