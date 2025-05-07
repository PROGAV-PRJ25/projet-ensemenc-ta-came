public class Date
{
    private int Annee;
    private int Semaine;

    public Date(int annee= 2010,int semaine = 1){
        Annee = annee;
        Semaine = semaine;
    }
    public override string ToString()
    {
        return $"{Annee} - Semaine {Semaine}";
    }
}
public class Potager{

}
public class Sauvegarde
{
    private Date DateActuelle{set;get;}
    private string Lieu{set;get;}
    private int Argent{set;get;}
    private Potager PotagerActuel {set;get;}
    public Sauvegarde(Date date, string lieu, int argent, Potager potagerActuel){
        DateActuelle=date;
        Lieu = lieu;
        PotagerActuel = potagerActuel;

    }
}