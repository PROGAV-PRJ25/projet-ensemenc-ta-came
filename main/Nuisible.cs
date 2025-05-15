public abstract class Nuisible 
{
    public string NOM {set;get;}
    public string EMOJI {set;get;}
    public abstract void Action(Parcelle parcelle);
    protected Nuisible(string nom, string emoji)
    {
        NOM = nom;
        EMOJI = emoji;

    }
}
public class Maladie : Nuisible {
    public Maladie (string nom, string emoji) : base(nom, emoji) {
        NOM = "Maladie";
        EMOJI = "🦠";
    }
    public override void Action(Parcelle parcelle) 
    {
        //si maladie, santé -10
        //si maladie non traité au mois suivant, santé -40
    }
}

public class Champignon : Nuisible {
    public Champignon(string nom, string emoji) : base(nom, emoji) {
        NOM = "Champignon";
        EMOJI = "🍄";
    }
    public override void Action(Parcelle parcelle) 
    {
        //si champi , santé -20
        //si non traité au mois suivant, santé -20
    }
}

public class Chenille : Nuisible {
    public Chenille(string nom, string emoji) : base(nom, emoji) {
        NOM = "Chenille";
        EMOJI = "🐛";
    }
    public override void Action(Parcelle parcelle) 
    {
        //chenille santé=-15
        //si non traité au mois suivant, santé -40

    }
}

public class Pucerons : Nuisible {
    public Pucerons(string nom, string emoji) : base(nom, emoji) {
        NOM = "Pucerons";
        EMOJI = "🐜"; //lol cest le plus ressemblant
    }
    public override void Action(Parcelle parcelle) 
    {
        //santé =-10
        //si non traité au mois suivant santé=-20
    }
}

public class Lapin : Nuisible {
    public Lapin(string nom, string emoji) : base(nom, emoji) {
        NOM = "Lapin";
        EMOJI = "🐇";
    }
    public override void Action(Parcelle parcelle) 
    {
        //santé =-10
        //si non traité au mois suivant santé=-30
    }
}

public class Oiseau : Nuisible {
    public Oiseau(string nom, string emoji) : base(nom, emoji) {
        NOM = "Oiseau";
        EMOJI = "🐦‍⬛";
    }
    public override void Action(Parcelle parcelle) 
    {
        //santé -20
        //si non traité au mois suivant santé=-35
    }
}

