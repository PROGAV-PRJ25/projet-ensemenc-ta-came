public abstract class Plante{

    string[] Etats {set;get;}
    public string Nom {get;private set;}
    public string Type {set;get;}
    public int SaisonSemi {set;get;}
    public string TerrainPref {set;get;}
    public int VitesseCroissance {set;get;}
    public int BesoinEau {set;get;}
    public int BesoinSoleil {set;get;}
    public bool CrainFroid {set;get;}
    public bool CrainSecheresse {set;get;}
    public string Nuisible {set;get;}
    public string Defense {set;get;}
    public int Espace {set;get;}
    public int Rendement  {set;get;}
    public int Recolte {set;get;}
    public int TemperaturePref {set;get;}

    public Plante(string nom , string[] etats , string type , int saisonSemi , string terrainPref , 
    int vitesseCroissance , int besoinEau , int besoinSoleil , bool crainFroid , bool crainSecheresse , 
    string nuisible , string defence , int espace , int rendement , int recolte , int temperaturePref)
    {
        Nom = nom;
        Etats = etats;
        Type=type;
        SaisonSemi=saisonSemi;
        TerrainPref=terrainPref;
        VitesseCroissance=vitesseCroissance;
        BesoinEau=besoinEau;
        BesoinSoleil=besoinSoleil;
        CrainFroid=crainFroid;
        CrainSecheresse=crainSecheresse;
        Nuisible=nuisible;
        Defense= defence;
        Espace=espace;
        Rendement=rendement;
        Recolte=recolte;
        TemperaturePref=temperaturePref;
    }
}
    public class PlanteVide : Plante {
        public PlanteVide() : base(
                nom: "Vide",
                etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"] ,
                type: "",
                saisonSemi: 0,
                terrainPref: "",
                vitesseCroissance: 0,
                besoinEau: 0,
                besoinSoleil: 0,
                crainFroid: false,
                crainSecheresse: false,
                nuisible: "", 
                defence: "",
                espace: 0,
                rendement: 0,
                recolte: 0,
                temperaturePref: 18)
                {
                    //corps du constructeur
                }
    }

    public class Pommier : Plante
    {
        public Pommier() : 
        base(nom: "Pommier",
        etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"] ,
        type: "arbre fruitier",
        saisonSemi: 2,
        terrainPref: "argileux, drainé",
        vitesseCroissance: 10,
        besoinEau: 5,
        besoinSoleil: 3,
        crainFroid: false,
        crainSecheresse: true,
        nuisible: "chenilles et oiseaux", 
        defence: "fermier en colère",
        espace: 8,
        rendement: 100,
        recolte: 3,
        temperaturePref: 18)
        {
            //corps du constructeur
        }


    }
    
    
    public class Ble : Plante
    {
        public Ble() : 
        base(nom: "Blé",
        etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"] ,
        type: "céréale",
        saisonSemi: 4,
        terrainPref: "riche et humifère",
        vitesseCroissance: 16,
        besoinEau: 5,
        besoinSoleil: 8,
        crainFroid: true,
        crainSecheresse: false,
        nuisible: "pucerons , Oïdium", 
        defence: "Traitement , coccinnelle",
        espace: 4,
        rendement: 10,
        recolte: 3,
        temperaturePref: 18)
        {
            //corps du constructeur
        }

    }
    public class Carotte : Plante
    {
        public Carotte() : 
        base(nom: "Blé",
        etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"] ,
        type: "céréale",
        saisonSemi: 4,
        terrainPref: "riche et humifère",
        vitesseCroissance: 16,
        besoinEau: 5,
        besoinSoleil: 8,
        crainFroid: true,
        crainSecheresse: false,
        nuisible: "pucerons , Oïdium", 
        defence: "Traitement , coccinnelle",
        espace: 4,
        rendement: 10,
        recolte: 3,
        temperaturePref: 18)
        {
            //corps du constructeur
        }
    }
    public class Pecher : Plante
    {
        public Pecher() : 
        base(nom: "Blé",
        etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"] ,
        type: "céréale",
        saisonSemi: 4,
        terrainPref: "riche et humifère",
        vitesseCroissance: 16,
        besoinEau: 5,
        besoinSoleil: 8,
        crainFroid: true,
        crainSecheresse: false,
        nuisible: "pucerons , Oïdium", 
        defence: "Traitement , coccinnelle",
        espace: 4,
        rendement: 10,
        recolte: 3,
        temperaturePref: 18)
        {
            //corps du constructeur
        }

    }
    public class VignesArtaban : Plante
    {
        public VignesArtaban() : 
        base(nom: "Blé",
        etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"] ,
        type: "céréale",
        saisonSemi: 4,
        terrainPref: "riche et humifère",
        vitesseCroissance: 16,
        besoinEau: 5,
        besoinSoleil: 8,
        crainFroid: true,
        crainSecheresse: false,
        nuisible: "pucerons , Oïdium", 
        defence: "Traitement , coccinnelle",
        espace: 4,
        rendement: 10,
        recolte: 3,
        temperaturePref: 18)
        {
            //corps du constructeur
        }

    }
    public class Olivier : Plante
    {
        public Olivier() : 
        base(nom: "Blé",
        etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"] ,
        type: "céréale",
        saisonSemi: 4,
        terrainPref: "riche et humifère",
        vitesseCroissance: 16,
        besoinEau: 5,
        besoinSoleil: 8,
        crainFroid: true,
        crainSecheresse: false,
        nuisible: "pucerons , Oïdium", 
        defence: "Traitement , coccinnelle",
        espace: 4,
        rendement: 10,
        recolte: 3,
        temperaturePref: 18)
        {
            //corps du constructeur
        }

    }
    public class Tournesol : Plante
    {
        public Tournesol() : 
        base(nom: "Blé",
        etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"] ,
        type: "céréale",
        saisonSemi: 4,
        terrainPref: "riche et humifère",
        vitesseCroissance: 16,
        besoinEau: 5,
        besoinSoleil: 8,
        crainFroid: true,
        crainSecheresse: false,
        nuisible: "pucerons , Oïdium", 
        defence: "Traitement , coccinnelle",
        espace: 4,
        rendement: 10,
        recolte: 3,
        temperaturePref: 18)
        {
            //corps du constructeur
        }

    }
