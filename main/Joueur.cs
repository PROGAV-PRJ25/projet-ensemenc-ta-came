using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;

public class Joueur
{
    public int Argent { set; get; }
    public int Semaine { set; get; }
    public string Lieu { set; get; }
    public Repertoire Inventaire { set; get; }
    public Parcelle[,] Potager { set; get; }


    public Joueur(string lieu = "")
    {
        Argent = 2000;
        Semaine = 0;
        Lieu = lieu;
        Inventaire = new Repertoire();
        Potager = new Parcelle[10, 10];
        CreerPotager();
        Inventaire.Ajouter(new Tournesol());
        Inventaire.Ajouter(new Pecher());

    }
    private void CreerPotager()
    {
        for (int colonne = 0; colonne < Potager.GetLength(0); colonne++)
        {
            for (int ligne = 0; ligne < Potager.GetLength(1); ligne++)
            {
                Potager[colonne, ligne] = new Parcelle();
            }
        }
        Potager[2, 1].Planter(new Citronnier());
    }
}
