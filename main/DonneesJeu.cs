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