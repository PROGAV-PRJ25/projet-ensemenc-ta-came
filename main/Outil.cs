using System.Security.AccessControl;

public abstract class Outil : ObjetJeu
{
    public static List<Outil> ListeOutils = new List<Outil>
    {
        new Arrosoir(),
        new Panier(),
        new Secateur(),
        new CD(),
        new Fumier(),
        new Traitement(),
        new Coccinnelle(),
        new FermierEnColere(),
        new Serre(),
        new IrrigationUrgente(),
        new Paillage()
    };
    public string Verbe { get; set; }
    public bool UsageUrgence { get; set; }
    public abstract void Actionner(Parcelle parcelle);


    protected Outil(string nom, string emoji, int decallageAffichage, string verbe, bool usageUrgence=false) : base(nom, emoji, decallageAffichage)
    {
        Verbe = verbe;
        UsageUrgence = usageUrgence;
    }
}
public class Arrosoir : Outil
{
    public Arrosoir() : base("Arosoir", "💦",0, "arroser") { }
    public override void Actionner(Parcelle parcelle)
    {
        //si dessous besoineau , augmente l'hydratation de +15%
        //si au dessus besoineau , attention plante surhydraté et santé -20
        parcelle.Sol.Arroser(15);
    }
}

public class Panier : Outil
{
    public Panier() : base("Panier", "🧺", 0, "ramasser vos récoltes") { }
    public override void Actionner(Parcelle parceller)
    {
        //permet de récolter des fruits et de les ajouter dans l"inventaire d'un joueur
        //
        //si pas fruit, ne sert à rien
        //List<Recolte>Parcelle.Plant.RamasserRecoltes();
        // sinon, ne fait rien
    }
}

public class Secateur : Outil
{
    public Secateur() : base("Secateur", "🪓", 1,"tailler")
    {
    }
    public override void Actionner(Parcelle parcelle)
    {
        //si prend trop de place , tailler et cases -2
        //si chenille , tailler et chenille -1
        //sinon, abime la plante et santé -15
        if (parcelle.Plant.Espace > 6)
        {
            parcelle.Plant.Espace -= 2;
        }
        else if (parcelle.NuisiblesActuels.Contains("Chenille"))
        {
            parcelle.NuisiblesActuels.Remove("Chenille");
        }
        else
        {
            parcelle.Plant.Sante -= 15;
        }

    }
}

public class CD : Outil
{
    public CD() : base("CD", "💿", 0,"installer votre CD")
    {

    }
    public override void Actionner(Parcelle parcelle)
    {
        //si oiseau, cd et oiseau -1
        //si chenille, cd et chenille -1
        //sinon , sert de déco mais inutile
        if (parcelle.NuisiblesActuels.Contains("Oiseau"))
        {
            parcelle.NuisiblesActuels.Remove("Oiseau");
        }
        else if (parcelle.NuisiblesActuels.Contains("Chenille"))
        {
            parcelle.NuisiblesActuels.Remove("Chenille");
        }
        // sinon, décoration uniquement
    }
}

public class Fumier : Outil
{
    public Fumier() : base("Fumier", "💩", 0,"mettre du fumier")
    {
    }
    public override void Actionner(Parcelle parcelle)
    {
        //engrais naturel donc booste la croissance des plantes de +2
        //si trop, ça pue et santé -15
        //si gel, protège du gel
        parcelle.Plant.VitesseCroissance += 2;

        if (parcelle.Plant.VitesseCroissance > 10) // valeur seuil à adapter
        {
            parcelle.Plant.Sante -= 15;
        }

        if (parcelle.Plant.Etat == "gelé")
        {
            // Protège du gel : rien à faire ici car c’est une immunité 
        }
    }
}

public class Traitement : Outil
{
    public Traitement() : base("Traitement", "🧪",0,"traiter")
    {
    }
    public override void Actionner(Parcelle parcelle)
    {
        //si maladies , traitement => maladie-1
        //si champignon , traitement => champi-1
        //sinon , trop de chimie => santé -20
        if (parcelle.NuisiblesActuels.Contains("Maladie"))
        {
            parcelle.NuisiblesActuels.Remove("Maladie");
        }
        else if (parcelle.NuisiblesActuels.Contains("Champignon"))
        {
            parcelle.NuisiblesActuels.Remove("Champignon");
        }
        else
        {
            parcelle.Plant.Sante -= 20;
        }
    }
}

public class Coccinelle : Outil
{
    public Coccinelle() : base("Coccinnelle", "🐞",0, "déposer vos coccinelles")
    {
    }
    public override void Actionner(Parcelle parcelle)
    {
        //si pucerons, coccinelle => pucerons -1
        //sinon , fait joli
        if (parcelle.NuisiblesActuels.Contains("Pucerons"))
        {
            parcelle.NuisiblesActuels.Remove("Pucerons");
        }
        // sinon, jolie décoration
    }
}

public class FermierEnColere : Outil
{
    public FermierEnColere() : base("Fermier en colère", "🤬",0, "faire peur")
    {

    }
    public override void Actionner(Parcelle parcelle)
    {
        //si oiseaux, => oiseau-1
        //si lapin, => lapin-1
        //sinon , sert de prévention et santé =+10
        if (parcelle.NuisiblesActuels.Contains("Oiseau"))
        {
            parcelle.NuisiblesActuels.Remove("Oiseau");
        }
        else if (parcelle.NuisiblesActuels.Contains("Lapin"))
        {
            parcelle.NuisiblesActuels.Remove("Lapin");
        }
        else
        {
            parcelle.Plant.Sante += 10;
        }
    }
}

public class Serre : Outil
{
    public Serre() : base("Serre", "⛺️",0, "protéger vos récoltes")
    {
    }
    public override void Actionner(Parcelle parcelle)
    {
        //si gel,protège du gel
        //si oiseau, oiseau-1
        //si pluis, quantitéeau ne bouge pas
        //augmente la temperature de +5 degré
        if (parcelle.Plant.Etat == "gelé")
        {
            // Protège : rien à faire
        }
        if (parcelle.NuisiblesActuels.Contains("Oiseau"))
        {
            parcelle.NuisiblesActuels.Remove("Oiseau");
        }
        // Empêche la pluie d'agir
        // --> À gérer dans la météo, via une vérification de présence de serre
        // Augmente la température
        //meteo.Temperature += 5;
    }
}

public class IrrigationUrgente : Outil
{
    public IrrigationUrgente() : base("Irrigation d'urgence", "🚿",0, "attention")
    {
    }
    public override void Actionner(Parcelle parcelle)
    {
        if (parcelle.Plant.Etat == "sècheresse")
        {
            // sauve les plantes mais sante -10
            parcelle.Plant.Sante -= 10;
        }
        else 
        {
            //inonde les plantations 
            parcelle.Plant.Sante -= 25;
        }
    
    }
}

public class Paillage : Outil
{
    public Paillage() : base("Paillage", "🍂",0, "attention")
    {
    }
    public override void Actionner(Parcelle parcelle)
    {
        //permet de limiter les maladies : A CODER
        //si trop d'eau, paille absorbe : A CODER
        if (parcelle.Plant.Etat == "sècheresse")
        {
            // sauve les plantes mais sante -5
            parcelle.Plant.Sante -= 5;
        }
        else
        {
            //ne fait rien 
        }
    
    }
}
