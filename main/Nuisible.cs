// =======================================================================
// Classes Nuisible et dérivées
// -----------------------------------------------------------------------
// Ces classes représentent les nuisibles pouvant affecter les parcelles du jeu.
// Chaque classe dérivée (Maladie, Champignon, Chenille, Pucerons, Lapin, Oiseau)
// représente un type spécifique de nuisible avec son effet propre sur la santé des plantes.
// =======================================================================
public abstract class Nuisible : ObjetJeu
{
    public abstract void Actionner(Parcelle parcelle);
    // on applique l'action à la parcelle
    // en pensant qu'on pourrait ajouter des
    // effets à appliquer sur le sol
    protected Nuisible(string nom, string emoji) : base(nom, emoji)
    {
    }
    public abstract Nuisible Dupliquer();
}
public class Maladie : Nuisible
{
    public Maladie() : base("Maladie", "🦠")
    {
    }
    public override void Actionner(Parcelle parcelle)
    {
        parcelle.Plant.Sante -= 10;
    }
    public override Nuisible Dupliquer()
    {
        return new Maladie();
    }
}
public class Champignon : Nuisible
{
    public Champignon() : base("Champignon", "🍄")
    {
    }
    public override void Actionner(Parcelle parcelle)
    {
        parcelle.Plant.Sante -= 20;
    }
    public override Nuisible Dupliquer()
    {
        return new Champignon();
    }
}
public class Chenille : Nuisible
{
    public Chenille() : base("Chenille", "🐛")
    {
    }
    public override void Actionner(Parcelle parcelle)
    {
        parcelle.Plant.Sante -= 15;
    }
    public override Nuisible Dupliquer()
    {
        return new Chenille();
    }

    
}

public class Pucerons : Nuisible
{
    public Pucerons() : base("Pucerons", "🐜") { }
    public override void Actionner(Parcelle parcelle)
    {
        parcelle.Plant.Sante -= 10;
    }
    public override Nuisible Dupliquer()
    {
        return new Pucerons();
    }
}
public class Lapin : Nuisible
{
    public Lapin() : base("Lapin", "🐇")
    {
    }
    public override void Actionner(Parcelle parcelle)
    {
        parcelle.Plant.Sante -= 10;
    }
    public override Nuisible Dupliquer()
    {
        return new Lapin();
    }
}
public class Oiseau : Nuisible
{
    public Oiseau() : base("Oiseau", "🐦‍⬛") { }
    public override void Actionner(Parcelle parcelle)
    {
        parcelle.Plant.Sante -= 20;
    }
    public override Nuisible Dupliquer()
    {
        return new Oiseau();
    }
}

