public abstract class Plante{

    string[] Etats = ["semis","mature","déshydraté","gelé", "malade", "mort"];
    public string Nom {get;private set;}
    public string Type {set;get;}
    public int SaisonSemi {set;get;}
    public string TerrainPrefere {set;get;}
    public int VitesseCroissance {set;get;}
    public int BesoinEau {set;get;}
    public int BesoinSoleil {set;get;}
    public bool CrainFroid {set;get;}
    public bool CrainSecheresse {set;get;}
    public string Nuisible {set;get;}
    public string Défense {set;get;}
    public int Espace {set;get;}
    public int Rendement  {set;get;}
    public string Recolte {set;get;}
    public int TemperaturePref {set;get;}


        public Plante(string nom){
        Nom=nom;
    }
    


    public class Olivier : Plante
    {

    }
}