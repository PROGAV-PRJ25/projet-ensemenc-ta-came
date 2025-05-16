using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;

public class Joueur
{
    public int Argent { set; get; }
    public int Semaine { set; get; }
    public string Lieu { set; get; }
    public List<Plante> Semis { set; get; }
    public List<Outil> Outils { set; get; }
    public Plante[,] Potager { set; get; }


    public Joueur(string lieu="")
    {
        Argent = 2000;
        Semaine = 0;
        Lieu = lieu;
        Semis = [];
        Outils = [];
        Potager = new Plante[10, 10];
        CreerPotager();
    }
    private void CreerPotager()
    {
        for (int colonne = 0; colonne < Potager.GetLength(0); colonne++)
        {
            for (int ligne = 0; ligne < Potager.GetLength(1); ligne++)
            {
                Potager[colonne, ligne] = new PlanteVide();
            }
        }
        Potager[2, 1] = new Tournesol();
    }
}
