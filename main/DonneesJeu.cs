using System.ComponentModel;
using System.Linq.Expressions;

public class Date
{
    private int Annee;
    private int Semaine;

    public Date(int annee = 2010, int semaine = 1)
    {
        Annee = annee;
        Semaine = semaine;
    }
    public override string ToString()
    {
        return $"{Annee} - Semaine {Semaine}";
    }
}
public class Potager
{

}
public abstract class Outil 
{
    public string Nom {set;get;}
    public abstract void Actionner(Parcelle parcelle);
    protected Outil(string nom)
    {
        Nom = nom;
    }
}
public class Arrosoir : Outil {
    public Arrosoir(string nom) : base(nom) {}
    public override void Actionner(Parcelle parcelle) 
    {
        parcelle.Contenu.QuantiteEau-= parcelle.Contenu.QuantiteEau<20?0:-1;
    }
}