public abstract class Terrain : ZoneInteractive
{
    public Parcelle[][]? Parcelles { set; get; }
    public Terrain(int colonne, int ligne, int largeur, int hauteur) : base(colonne, ligne, largeur, hauteur)
    {
        Parcelles = new Parcelle[10][];
    }

    public void Ajouter(Plante semis, int[] position)
    {
        if (Parcelles[position[0]][position[1]].Libre)
        {
            Parcelles[position[0]][position[1]].Contenu = semis;
            AcutaliserEspaceLibre();
        }

    }
    public void Retirer(Plante plante)
    {
        //retirer une plante
    }
    public void AcutaliserEspaceLibre()
    {

        for (int x = 0; x < Parcelles.GetLength(0); x++)
            for (int y = 0; y < Parcelles.GetLength(1); y++)
                DiffuserEffets(x, y);
    }
    public void DiffuserEffets(int x, int y)
    {
        Parcelle parcelle = Parcelles[x][y];
        int portee = parcelle.Contenu.Espace;
        if (portee > 1)
        {
            for (int colonne = x - portee; colonne < x + 1 + portee; colonne++)
            {
                for (int ligne = y - portee; ligne < y + 1 + portee; ligne++)
                {
                    if (EstDansTerrain(colonne, ligne) && colonne != x && ligne != y)
                    {
                        parcelle.Libre = false;
                    }
                }
            }
        }

    }
    public bool EstDansTerrain(int colonne, int ligne)
    {
        return (colonne >= 0) && (ligne >= 0) && (colonne < Parcelles.GetLength(0)) && (ligne < Parcelles.GetLength(1));
    }


    public class Parcelle : CelluleAffichage
    {
        public Plante Contenu { set; get; }
        public bool Libre { set; get; }
        public Parcelle(int[] position) : base()
        {
            Libre = true ;
            Contenu = new PlanteVide();
        }

    }
}
