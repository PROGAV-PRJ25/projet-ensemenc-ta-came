using System.ComponentModel;
using System.Linq.Expressions;

public class Date
{
    public int Annee { get; private set; }
    public int Semaine { get; private set; }

    public Date(int annee = 2010, int semaine = 1)
    {
        Annee = annee;
        Semaine = semaine;
    }
    
    public void Avancer()
    {
        Semaine++;
        if (Semaine > 52)
        {
            Semaine = 1;
            Annee++;
        }
    }
    
    public override string ToString()
    {
        return $"{Annee} - Semaine {Semaine}";
    }
}
public class InformationsJeu{}