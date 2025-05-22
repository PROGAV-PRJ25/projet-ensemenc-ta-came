public class Repertoire
{
    public List<ItemInventaireOutil> Outils { get; set; }
    public List<ItemInventaireRecolte> Recoltes { get; set; }
    public List<ItemInventaireSemis> Semis { get; set; }

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
            if (Semis[i].Contenu.Nom == plante.Nom)
                reponse = i;
        }
        return reponse;
    }
    public int RecupererIndice(Outil outil)
    {

        int reponse = -1;
        for (int i = 0; i < Outils.Count(); i++)
        {
            if (Outils[i].Contenu.Nom == outil.Nom)
                reponse = i;
        }
        return reponse;
    }
    public int RecupererIndice(Recolte recolte)
    {

        int reponse = -1;
        for (int i = 0; i < Recoltes.Count(); i++)
        {
            if (Recoltes[i].Contenu.Nom == recolte.Nom)
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
    public int RecupererQuantite(Recolte recolte)
    {
        int indice = RecupererIndice(recolte);
        return indice == -1 ? 0 : Recoltes[indice].Quantite;
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
    public void Ajouter(Recolte recolte)
    {
        int indice = RecupererIndice(recolte);
        if (indice == -1)
        {
            Recoltes.Add(new ItemInventaireRecolte(recolte));
        }
        else
        {
            Recoltes[indice].Quantite += 1;
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
    public void Retirer(Recolte recolte)
    {
        int indice = RecupererIndice(recolte);
        if (indice != -1)
        {
            Recoltes[indice].Quantite -= 1;
            if (RecupererQuantite(recolte) == 0)
            {
                Recoltes.RemoveAt(indice);
            }
        }
    }

}
public class ItemInventaire
{
    private int _quantite { get; set; }
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
    public string Nom { get; set; }
    public ItemInventaire(string nom = "", int quantite = 1)
    {
        Quantite = quantite;
        Nom = nom;
    }
}
public class ItemInventaireSemis : ItemInventaire
{
    public Plante Contenu { get; set; }
    public ItemInventaireSemis(Plante plante)
    {
        Contenu = plante;
        Nom = plante.Nom;
        Quantite = 1;

    }
}
public class ItemInventaireOutil : ItemInventaire
{
    public Outil Contenu { get; set; }
    public ItemInventaireOutil(Outil outil)
    {
        Contenu = outil;
        Nom = outil.Nom;
        Quantite = 1;

    }
}
public class ItemInventaireRecolte : ItemInventaire
{
    public Recolte Contenu { get; set; }
    public ItemInventaireRecolte(Recolte recolte)
    {
        Contenu = recolte;
        Nom = recolte.Nom;
        Quantite = 1;
    }
}