using System.ComponentModel;
using System.Linq.Expressions;

public class Date
{
    public int Annee { get; private set; }
    public int Semaine { get; private set; }

    public Date(int annee = 2009, int semaine = 1)
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
        string reponse = $"{Annee} - Semaine {Semaine}";
        if (Semaine < 13)
            reponse += " (hiver)";
        else if (Semaine < 26)
            reponse += " (printemps)";
        else if (Semaine < 39)
            reponse += " (été)";
        else
            reponse += "(automne)";
        return reponse;
    }
}
public class InformationsJeu{}