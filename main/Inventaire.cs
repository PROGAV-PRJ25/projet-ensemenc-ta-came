public class Inventaire
{
    public List<Outil> Outils { set; get; }
    public List<Plante> Recoltes { set; get; }
    public List<Plante> Semis { set; get; }
    public Inventaire()
    {
        Outils = [];
        Recoltes = [];
        Semis = [];
        
    }
    public void MettreAJourInventaire() { }
    
}