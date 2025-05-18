public class Parcelle
{
    public bool Libre { set; get; }
    public Plante Contenu { set; get; }
    public Terrain Sol { set; get; }
    public List<string> NuisiblesActuels;
    public List<string> Defense;
    public Random Rng = new Random() ;
    public Parcelle()
    {
        Libre = true;
        Contenu = new PlanteVide();
        Sol = new TerrainArgileux();
        NuisiblesActuels = [];
        Defense = [];
    }
    public void Planter(Plante plante)
    {
        Contenu = plante.Dupliquer();
        Libre = false; 
    }
    public void Recolter()
    {

    }
    public void Arroser()
    {
        
    }
    public bool NuisibleSemainePro(string nom)
    {
        return false;
    }
}