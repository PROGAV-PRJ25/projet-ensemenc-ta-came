public class Parcelle
{
    public bool Libre { get; set; }
    public Plante Plant { get; set; }
    public Terrain Sol { get; set; }
    public List<string> NuisiblesActuels;
    public List<string> Defense;
    public Random Rng = new Random();
    public Parcelle(string ville)
    {
        Libre = true;
        Plant = new PlanteVide();
        if (ville == "Soconusco")
            Sol = new TerrainArgileux();
        else if (ville == "Hokkaido")
            Sol = new TerrainArgileux();
        else
            Sol = new TerrainArgileux();
        NuisiblesActuels = [];
        Defense = [];
    }
    public void Planter(Plante plante)
    {
        Plant = plante.Dupliquer();
        Libre = false;
    }
    public void Recolter()
    {

    }
    public void DeterrerPlante()
    {
        Plant = new PlanteVide();
    }
    
    public bool AvancerAge()
    {
        if (Plant.Type == "plante vide")
        {
            return true;
        }
        return true;
    }
    public override string ToString()
    {
        string reponse ="PLANTE\n"+Plant.ToString()+ "\n\nSOL\n" + Sol.ToString();
        return reponse;
    }
}