public abstract class Terrain
{
    public int TauxHumidite { set; get; }
}
public class TerrainArgileux : Terrain
{
    public TerrainArgileux()
    {
        TauxHumidite = 0;
    }
}



