public abstract class Terrain
{
    public int TauxHumidite { set; get; }
    // taux d'humidité sur 100, fournit la plante en eau
    // pour les fleurs arbres et arbustes, entre 21 et 40%
    // pour les légumes entre 40 et 80% (idéal 60)
    public int Drainage { set; get; }
    // indique la quantité d'eau perdue durant une semaine
    public int Fertilite
}
public class TerrainArgileux : Terrain
{
    public TerrainArgileux()
    {

        TauxHumidite = 0;
        Drainage = 0;
        Fertilite = 0;

    }
}



