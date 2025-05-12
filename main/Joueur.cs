public class Joueur
{
    public int Argent { set; get; }
    public int Semaine { set; get; }
    public string Lieu { set; get; }
    public List<Plante> Semis { set; get; }
    public List<Outil> Outils { set; get; }


    public Joueur(string lieu)
    {
        Argent = 2000;
        Semaine = 1;
        Lieu = lieu;
        Semis = [];
        Outils = [];
    }

}