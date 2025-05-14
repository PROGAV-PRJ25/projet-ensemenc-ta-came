public abstract class Outil 
{
    public string NOM {set;get;}
    public string EMOJI {set;get;}
    public abstract void Actionner(Parcelle parcelle);
    protected Outil(string nom)
    {
        Nom = nom;
    }
}
public class Arrosoir : Outil {
    public Arrosoir(string nom, string emoji) : base(nom, emoji) {
        NOM = "Arosoir"
        EMOJI = "💦";
    }
    public override void Actionner(Parcelle parcelle) 
    {
        parcelle.Contenu.QuantiteEau-= parcelle.Contenu.QuantiteEau<20?0:-1;
    }
}

public class Panier : Outil {
    public Panier(string nom, string emoji) : base(nom, emoji) {
        NOM = "Panier";
        EMOJI = "🧺";
    }
    public override void Actionner(Parcelle parcelle) 
    {
        parcelle.Contenu.QuantiteEau-= parcelle.Contenu.QuantiteEau<20?0:-1;
    }
}

public class Secateur : Outil {
    public Secateur(string nom, string emoji) : base(nom, emoji) {
        NOM = "Secateur";
        EMOJI = "✂️";
    }
    public override void Actionner(Parcelle parcelle) 
    {
        parcelle.Contenu.QuantiteEau-= parcelle.Contenu.QuantiteEau<20?0:-1;
    }
}

public class CD : Outil {
    public CD(string nom, string emoji) : base(nom, emoji) {
        NOM = "CD";
        EMOJI = "💿";
    }
    public override void Actionner(Parcelle parcelle) 
    {
        parcelle.Contenu.QuantiteEau-= parcelle.Contenu.QuantiteEau<20?0:-1;
    }
}

public class Fumier : Outil {
    public Fumier(string nom, string emoji) : base(nom, emoji) {
        NOM = "Fumier";
        EMOJI = "💩";
    }
    public override void Actionner(Parcelle parcelle) 
    {
        parcelle.Contenu.QuantiteEau-= parcelle.Contenu.QuantiteEau<20?0:-1;
    }
}

public class Traitement : Outil {
    public Traitement(string nom, string emoji) : base(nom, emoji) {
        NOM = "Traitement";
        EMOJI = "🧪";
    }
    public override void Actionner(Parcelle parcelle) 
    {
        parcelle.Contenu.QuantiteEau-= parcelle.Contenu.QuantiteEau<20?0:-1;
    }
}

public class Coccinnelle : Outil {
    public Coccinnelle(string nom, string emoji) : base(nom, emoji) {
        NOM = "Coccinnelle";
        EMOJI = "🐞";
    }
    public override void Actionner(Parcelle parcelle) 
    {
        parcelle.Contenu.QuantiteEau-= parcelle.Contenu.QuantiteEau<20?0:-1;
    }
}
