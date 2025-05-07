public abstract class Plante{

    string[] etats = ["semis","mature","déshydraté","gelé", "malade", "mort"];
    public string Nom {get;private set;}
    public string nature {set;get;}
    public int saisonSemi {set;get;}
    public string terrainPrefere {set;get;}
    public int vitesseCroissance {set;get;}
    public int besoinEau {set;get;}
    public Plante(string nom){
        Nom=nom;
    }
    
}