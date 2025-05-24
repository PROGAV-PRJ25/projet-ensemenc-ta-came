// =======================================================================
// Classe Parcelle
// -----------------------------------------------------------------------
// Cette classe représente une parcelle de terrain dans le jeu.
// Elle gère :
//   - L'état de la parcelle (libre ou occupée)
//   - La plante présente sur la parcelle
//   - Le type de sol associé
//   - Les défenses appliquées à la parcelle
//   - Les actions de plantation, récolte, et entretien hebdomadaire
// =======================================================================
public class Parcelle
{
    public bool Libre { get; set; }
    public Plante Plant { get; set; }
    public Terrain Sol { get; set; }
    public List<string> Defense;
    protected static Random rng = new Random();
    public Parcelle(string ville)
    {
        Libre = true;
        Plant = new PlanteVide();
        if (ville == "Soconusco")
            Sol = new TerrainArgileux();
        else if (ville == "Hokkaido")
            Sol = new TerrainArgileux(); // possiblité  dans le futur de changer le type de sol selon la ville sélectionnée
        else
            Sol = new TerrainArgileux();
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
    public bool PlacerRecoltesDansPanier()
    {
        return Plant.Recolter();
    }
    public void AppliquerConditionsHebdomadaires()
    {
        if (Plant.Type != "plante vide" || Plant.Type != "plante morte")
        {
            Plant.Sante = 100;
            AppliquerEnsoleillement();
            AppliquerHumidite();
            AppliquerOptions();
            AppliquerNuisibles();
            Plant.Age += 1;
            Plant.Sante += Sol.Fertilite;
            if (Plant.Sante < 50)
            {
                Plant = new PlanteMorte();
            }
            // on fait grandir la plante si elle n'est pas mature
            else if (!Plant.Mature)
            {
                Plant.Croissance += Plant.VitesseCroissance + Sol.Fertilite;
                if (Plant.Croissance >= 100)
                    Plant.Mature = true;
            }
            else
            {
                Plant.RendementActuel = Plant.Rendement[(Plant.Sante - 50) / 10];
                //choisit le rendement[4] si santé >90, [3] si >80 etc...
            }
        }
        Sol.Fertilite -= 2;
        Sol.TauxHumidite -= 10;
        Sol.TauxExposition -= 5;
    }
    private void AppliquerEnsoleillement()
    {
        // on checke l'Ensoleillement 
        //  si on dépasse besoin soleil de 5 et quantitéeau qui est en dessous de besoin eau,  ce mois ci, santé-25
        //  sinon, si besoinsoleil atteint (à +- 5%), santé +5
        if (Sol.TauxExposition < Plant.BesoinSoleil)
            Plant.Sante -= 15;
        else if (Sol.TauxExposition == 100)
        {
            Plant.Sante -= 5;
        }
        else if (Math.Abs(Plant.BesoinSoleil - Plant.BesoinEau) <= 5)
        {
            Plant.Sante += 5;
        }
    }
    private void AppliquerHumidite()
    {
        if (Sol.TauxHumidite > 90)
        {
            Plant.Sante -= 15;
        }
        else if (Sol.TauxHumidite < Plant.BesoinEau)
        {
            Plant.Sante -= 20;
        }
    }
    public void AppliquerOptions()
    {
        for (int i = 0; i < Plant.Options.Count; i++)
        {
            Plant.Options[i].Actionner(this);
        }
    }
    public void AppliquerNuisibles()
    {
        for (int i = 0; i < Plant.NuisiblesActuels.Count; i++)
        {
            Plant.NuisiblesActuels[i].Actionner(this);
        }
    }
    public override string ToString()
    {
        string reponse = "PLANTE\n" + Plant.ToString() + "\n\nSOL\n" + Sol.ToString();
        return reponse;
    }
    public bool Contient(string nom)
    { // permet de savoir si la plante possède une option ou un nuisible
        for (int i = 0; i < Plant.NuisiblesActuels.Count(); i++)
        {
            if (Plant.NuisiblesActuels[i].Nom == nom)
                return true;
        }
        for (int i = 0; i < Plant.Options.Count(); i++)
        {
            if (Plant.Options[i].Nom == nom)
                return true;
        }
        return false;
    }
    public void Retirer(string nom)
    {
        for (int i = 0; i < Plant.NuisiblesActuels.Count(); i++)
        {
            if (Plant.NuisiblesActuels[i].Nom == nom)
            {
                Plant.NuisiblesActuels.RemoveAt(i);
                return;
            }
        }
        for (int i = 0; i < Plant.Options.Count(); i++)
        {
            if (Plant.Options[i].Nom == nom)
            {
                Plant.Options.RemoveAt(i);
                return;
            }

        }
    }

}