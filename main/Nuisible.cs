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
        parcelle.Plant.Sante -= 10;
        if(parcelle.NuisibleSemainePro(NOM)) //si la semaine d'après, le nuisible est toujours là, (NuisibleSemainePro dans la classe parcelle)
        {
            parcelle.Plant.Sante -= 40;
        }
    }
}

public class Champignon : Nuisible {
    public Champignon(string nom, string emoji) : base(nom, emoji) {
        NOM = "Champignon";
        EMOJI = "🍄";
    }
    public override void Action(Parcelle parcelle) 
    {
        parcelle.Plant.Sante -= 20;
        if(parcelle.NuisibleSemainePro(NOM))
        {
            parcelle.Plant.Sante -= 20;
        }
    }
}

public class Chenille : Nuisible {
    public Chenille(string nom, string emoji) : base(nom, emoji) {
        NOM = "Chenille";
        EMOJI = "🐛";
    }
    public override void Action(Parcelle parcelle) 
    {
        parcelle.Plant.Sante -= 15;
        if(parcelle.NuisibleSemainePro(NOM))
        {
            parcelle.Plant.Sante -= 40;
        }

    }
}

public class Pucerons : Nuisible {
    public Pucerons(string nom, string emoji) : base(nom, emoji) {
        NOM = "Pucerons";
        EMOJI = "🐜"; //lol cest le plus ressemblant
    }
    public override void Action(Parcelle parcelle) 
    {
        parcelle.Plant.Sante -= 10;
        if(parcelle.NuisibleSemainePro(NOM))
        {
            parcelle.Plant.Sante -= 20;
        }
    }
}

public class Lapin : Nuisible {
    public Lapin(string nom, string emoji) : base(nom, emoji) {
        NOM = "Lapin";
        EMOJI = "🐇";
    }
    public override void Action(Parcelle parcelle) 
    {
        parcelle.Plant.Sante -= 10;
        if(parcelle.NuisibleSemainePro(NOM))
        {
            parcelle.Plant.Sante -= 30;
        }
    }
}

public class Oiseau : Nuisible {
    public Oiseau(string nom, string emoji) : base(nom, emoji) {
        NOM = "Oiseau";
        EMOJI = "🐦‍⬛";
    }
    public override void Action(Parcelle parcelle) 
    {
        parcelle.Plant.Sante -= 20;
        if(parcelle.NuisibleSemainePro(NOM))
        {
            parcelle.Plant.Sante -= 35;
        }
    }
}

