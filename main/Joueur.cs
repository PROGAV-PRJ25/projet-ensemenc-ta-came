using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;

public class Joueur
{
    public int Argent { get; set; }
    public Date DateActuelle { get; set; }
    public Repertoire Inventaire { get; set; }
    public string Lieu { set; get; }
    public Parcelle[,] Potager { get; set; }



    public Joueur(string lieu = "")
    {
        Argent = 2000;
        DateActuelle = new Date();
        Lieu = lieu;
        Inventaire = new Repertoire();
        Potager = new Parcelle[10,10];
        CreerPotager();
        Inventaire.Ajouter(new Tournesol());
        Inventaire.Ajouter(new Pecher());
        Inventaire.Ajouter(new RecolteAvocat());
        Inventaire.Ajouter(new RecolteBle());
    }

    private void CreerPotager()
    {
        for (int colonne = 0; colonne < Potager.GetLength(0); colonne++)
        {
            for (int ligne = 0; ligne < Potager.GetLength(1); ligne++)
            {
                Potager[colonne, ligne] = new Parcelle(Lieu);
            }
        }
        Potager[2, 1].Planter(new Citronnier());
    }
}
// public class Culture
// {
//     public int Largeur { set; get; }
//     public int Hauteur { set; get; }
//     Parcelle[,] Parcelles { set; get; }
//     public Culture(int largeur, int hauteur)
//     {
//         Largeur = largeur;
//         Hauteur = hauteur;
//         Parcelles = new Parcelle[largeur, hauteur];
//         for (int colonne = 0; colonne < Largeur; colonne++)
//         {
//             for (int ligne = 0; ligne < Hauteur; ligne++)
//             {
//                 Parcelles[ligne, colonne] = new Parcelle();
//             }
//         }
//     }
//     public void AvancerSemaine()
//     {
        
//     }


// }
