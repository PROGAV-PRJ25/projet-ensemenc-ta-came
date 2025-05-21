public class Parcelle
{
    public bool Libre { set; get; }
    public Plante Plant { set; get; }
    public Terrain Sol { set; get; }
    public List<string> NuisiblesActuels;
    public List<string> Defense;
    public Random Rng = new Random();
    public Date Date { get; set; }


    public Parcelle()
    {
        Libre = true;
        Plant = new PlanteVide();
        Sol = new TerrainArgileux();
        NuisiblesActuels = [];
        Defense = [];
        Date = new Date(2009, 1);
    }
    public void Planter(Plante plante)
    {
        Plant = plante.Dupliquer();
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
        string reponse = Plant.ToString()+ "\n" + Sol.ToString();
        return reponse;
    }
}