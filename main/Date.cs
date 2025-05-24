// =======================================================================
// Classe Date
// -----------------------------------------------------------------------
// Elle gère :
//   - L'année et la semaine en cours
//   - Le calcul de la saison en fonction de la semaine
//   - L'avancement du temps (semaine suivante, changement d'année)
// =======================================================================
public class Date
{
    public int Annee { get; private set; }
    public int Semaine { get; private set; }
    public int Saison { get { return Semaine / 13; } }

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
        if (Saison == 0)
            reponse += " (hiver)";
        else if (Saison == 1)
            reponse += " (printemps)";
        else if (Saison == 2)
            reponse += " (été)";
        else
            reponse += "(automne)";
        return reponse;
    }
}