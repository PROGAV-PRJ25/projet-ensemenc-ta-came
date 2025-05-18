public class Repertoire
{
    public List<ItemInventaireOutil> Outils { set; get; }
    public List<ItemInventaire> Recoltes { set; get; }
    public List<ItemInventaireSemis> Semis { set; get; }

    public Repertoire()
    {
        Outils = [];
        Recoltes = [];
        Semis = [];

    }

    public int RecupererIndice(Plante plante)
    {

        int reponse = -1;
        for (int i = 0; i < Semis.Count(); i++)
        {
            if (Semis[i].Contenu.NOM == plante.NOM)
                reponse = i;
        }
        return reponse;
    }
    public int RecupererIndice(Outil outil)
    {

        int reponse = -1;
        for (int i = 0; i < Outils.Count(); i++)
        {
            if (Outils[i].Contenu.NOM == outil.NOM)
                reponse = i;
        }
        return reponse;
    }
    public int RecupererQuantite(Plante plante)
    {

        int indice = RecupererIndice(plante);
        return indice == -1 ? 0 : Semis[indice].Quantite;
    }
    public int RecupererQuantite(Outil outil)
    {
        int indice = RecupererIndice(outil);
        return indice == -1 ? 0 : Outils[indice].Quantite;
    }

    public void Ajouter(Plante plante)
    {
        int indice = RecupererIndice(plante);
        if (indice == -1)
        {
            Semis.Add(new ItemInventaireSemis(plante));
        }
        else
        {
            Semis[indice].Quantite += 1;
        }
    }
    public void Ajouter(Outil outil)
    {
        int indice = RecupererIndice(outil);
        if (indice == -1)
        {
            Outils.Add(new ItemInventaireOutil(outil));
        }
        else
        {
            Outils[indice].Quantite += 1;
        }
    }
    public void Retirer(Plante plante)
    {
        int indice = RecupererIndice(plante);
        if (indice != -1)
        {
            Semis[indice].Quantite -= 1;
            if (RecupererQuantite(plante) == 0)
            {
                Semis.RemoveAt(indice);
            }
        }
    }

}
public class ItemInventaire
{
    private int _quantite { set; get; }
    public int Quantite
    {
        set
        {
            _quantite = value >= 0 ? value : 0;
        }
        get
        {
            return _quantite;
        }
    }
    public string Nom { set; get; }
    public ItemInventaire(string nom = "", int quantite = 1)
    {
        Quantite = quantite;
        Nom = nom;
    }
}
public class ItemInventaireSemis : ItemInventaire
{
    public Plante Contenu { set; get; }
    public ItemInventaireSemis(Plante plante)
    {
        Contenu = plante;
        Nom = plante.NOM;
        Quantite = 1;

    }
}
public class ItemInventaireOutil : ItemInventaire
{
    public Outil Contenu { set; get; }
    public ItemInventaireOutil(Outil outil)
    {
        Contenu = outil;
        Nom = outil.NOM;
        Quantite = 1;

    }
}