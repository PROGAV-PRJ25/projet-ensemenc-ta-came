public abstract class Outil 
{
    public string NOM {set;get;}
    public string EMOJI {set;get;}
    public abstract void Actionner(Parcelle parcelle);
    protected Outil(string nom, string emoji)
    {
        NOM = nom;
        EMOJI = emoji;

    }
}
public class Arrosoir : Outil {
    public Arrosoir(string nom, string emoji) : base(nom, emoji) {
        NOM = "Arosoir";
        EMOJI = "💦";
    }
    public override void Actionner(Parcelle parcelle) 
    {
        //si dessous besoineau , augmente l'hydratation de +15%
        //si au dessus besoineau , attention plante surhydraté et santé -20
    }
}

public class Panier : Outil {
    public Panier(string nom, string emoji) : base(nom, emoji) {
        NOM = "Panier";
        EMOJI = "🧺";
    }
    public override void Actionner(Parcelle parcelle) 
    {
        //si fruit , le récolte et rendement -1
        //si pas fruit, ne sert à rien
    }
}

public class Secateur : Outil {
    public Secateur(string nom, string emoji) : base(nom, emoji) {
        NOM = "Secateur";
        EMOJI = "✂️";
    }
    public override void Actionner(Parcelle parcelle) 
    {
        //si prend trop de place , tailler et cases -2
        //si chenille , tailler et chenille -1
        //sinon, abime la plante et santé -15

    }
}

public class CD : Outil {
    public CD(string nom, string emoji) : base(nom, emoji) {
        NOM = "CD";
        EMOJI = "💿";
    }
    public override void Actionner(Parcelle parcelle) 
    {
        //si oiseau, cd et oiseau -1
        //si chenille, cd et chenille -1
        //sinon , sert de déco mais inutile
    }
}

public class Fumier : Outil {
    public Fumier(string nom, string emoji) : base(nom, emoji) {
        NOM = "Fumier";
        EMOJI = "💩";
    }
    public override void Actionner(Parcelle parcelle) 
    {
        //engrais naturel donc booste la croissance des plantes de +2
        //si trop, ça pue et santé -15
        //si gel, protège du gel
    }
}

public class Traitement : Outil {
    public Traitement(string nom, string emoji) : base(nom, emoji) {
        NOM = "Traitement";
        EMOJI = "🧪";
    }
    public override void Actionner(Parcelle parcelle) 
    {
        //si maladies , traitement => maladie-1
        //si champignon , traitement => champi-1
        //sinon , trop de chimie => santé -20
    }
}

public class Coccinnelle : Outil {
    public Coccinnelle(string nom, string emoji) : base(nom, emoji) {
        NOM = "Coccinnelle";
        EMOJI = "🐞";
    }
    public override void Actionner(Parcelle parcelle) 
    {
        //si pucerons, coccinelle => pucerons -1
        //sinon , fait joli
    }
}

public class FermierEnColere : Outil {
    public FermierEnColere(string nom, string emoji) : base(nom, emoji)
    {
        NOM = "Fermier en colère";
        EMOJI = "👨🏻‍🌾";
    }
    public override void Actionner(Parcelle parcelle) 
    {
        //si oiseaux, => oiseau-1
        //si lapin, => lapin-1
        //sinon , sert de prévention et santé =+10
    }
}

public class Serre : Outil {
    public Serre(string nom, string emoji) : base(nom, emoji) {
        NOM = "Serre";
        EMOJI = "⛺️";
    }
    public override void Actionner(Parcelle parcelle) 
    {
        //si gel,protège du gel
        //si oiseau, oiseau-1
        //si pluis, quantitéeau ne bouge pas
        //augmente la temperature de +5 degré
    }
}
