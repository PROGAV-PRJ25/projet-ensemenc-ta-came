public abstract class Nuisible : ObjetJeu
{
    public abstract void Action(Parcelle parcelle);
    protected Nuisible(string nom, string emoji) : base(nom, emoji)
    {
        Nom = nom;
        Emoji = emoji;
    }
    public override string ToString()
    {
        return "{Emoji} {Nom}";
    }
}
public class Maladie : Nuisible {
    public Maladie (string nom, string emoji) : base(nom, emoji) {
        Nom = "Maladie";
        Emoji = "🦠";
    }
    public override void Action(Parcelle parcelle) 
    {
        parcelle.Plant.Sante -= 10;
    }
}
public class Champignon : Nuisible {
    public Champignon(string nom, string emoji) : base(nom, emoji) {
        Nom = "Champignon";
        Emoji = "🍄";
    }
    public override void Action(Parcelle parcelle) 
    {
        parcelle.Plant.Sante -= 20;
    }
}
public class Chenille : Nuisible {
    public Chenille(string nom, string emoji) : base(nom, emoji) {
        Nom = "Chenille";
        Emoji = "🐛";
    }
    public override void Action(Parcelle parcelle) 
    {
        parcelle.Plant.Sante -= 15;
    }
}

public class Pucerons : Nuisible {
    public Pucerons(string nom, string emoji) : base(nom, emoji) {
        Nom = "Pucerons";
        Emoji = "🐜"; //lol cest le plus ressemblant
    }
    public override void Action(Parcelle parcelle) 
    {
        parcelle.Plant.Sante -= 10;
    }
}
public class Lapin : Nuisible {
    public Lapin(string nom, string emoji) : base(nom, emoji) {
        Nom = "Lapin";
        Emoji = "🐇";
    }
    public override void Action(Parcelle parcelle) 
    {
        parcelle.Plant.Sante -= 10;
    }
}
public class Oiseau : Nuisible {
    public Oiseau(string nom, string emoji) : base(nom, emoji) {
        Nom = "Oiseau";
        Emoji = "🐦‍⬛";
    }
    public override void Action(Parcelle parcelle) 
    {
        parcelle.Plant.Sante -= 20;
    }
}

