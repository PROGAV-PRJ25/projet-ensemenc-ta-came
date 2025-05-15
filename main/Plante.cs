public abstract class Plante
{
    // Caractéristiques : ne bouge pas
    public string NOM { get; private set; }
    public string EMOJI { set; get; }
    public string TYPE { set; get; }
    public int SAISONSEMI { set; get; }
    public string TERRAINPREF { set; get; }
    public int VITESSECROISSANCE { set; get; }
    public bool CRAINFROID { set; get; }
    public bool CRAINSECHERESSE { set; get; }
    public int BESOIN_EAU { set; get; } 
    public int SAISONRECOLTE { set; get; }
    public int TEMPERATUREPREF { set; get; }

    // EtatActuel, varie selon les saisons, les années, les nuisibles, la météo et les années
    string[] Etats { set; get; }
    public int Sante { set; get; } // sur 100 détermine la santé de la plante, si < 50 elle meurt
    public int Age { set; get; } // ajoute +1 à chaque semaine, si annuelle et atteint 52 alors elle meurt
    public string Etat { set; get; } // indique l'état de la plante, défini à chaque nouvelle semaine
    public int EspeDeVie { set; get; } //change avec les outils et nuisibles
    public int QuantiteEau { set; get; } //change en fonction de la météo et de l'arosoir , // sur 100 détermine les besoins en eau, si < 20 ou > 80 =>santé -10
    public int BesoinSoleil { set; get; } //change en fonction de la météo
    public string Nuisible { set; get; } //change en fonction de la classe nuisible 
    public string Defense { set; get; } //change en fonction d'outils
    public int Espace { set; get; } //change quand on taille la plante
    public int Rendement { set; get; } //change en fonction de la saison, de la météo et de l'outil panier
    
    //public int QuantiteEau { set; get; }  
    //public int BesoinSoleil { set; get; } 


    public Plante(string nom, string emoji, string[] etats, string type, int saisonSemi, string terrainPref,
    int vitesseCroissance, int espeDeVie , int besoinEau, int besoinSoleil, int quantiteEau, bool crainFroid, bool crainSecheresse,
    string nuisible, string defence, int espace, int rendement, int saisonRecolte, int temperaturePref)
    {
        NOM = nom;
        EMOJI = emoji;
        Etats = etats;
        TYPE = type;
        SAISONSEMI = saisonSemi;
        TERRAINPREF = terrainPref;
        VITESSECROISSANCE = vitesseCroissance;
        EspeDeVie = espeDeVie;
        BESOIN_EAU = besoinEau;
        QuantiteEau = quantiteEau;
        BesoinSoleil = besoinSoleil;
        CRAINFROID = crainFroid;
        CRAINSECHERESSE = crainSecheresse;
        Nuisible = nuisible;
        Defense = defence;
        Espace = espace;
        Rendement = rendement;
        SAISONRECOLTE = saisonRecolte;
        TEMPERATUREPREF = temperaturePref;

        Age = 0;
        Etat = etats[0];
    }
    public void Vieillir()
    {
        Age += 1;
    }
    public void Mourir()
    {
        Etat = "mort";
        EMOJI = "💀";
    }


}
public class PlanteVide : Plante
{
    public PlanteVide() :
    base(nom: "",
    emoji: " ",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "",
    saisonSemi: 0,
    terrainPref: "",
    vitesseCroissance: 0,
    espeDeVie:0,
    besoinEau: 0,
    besoinSoleil: 0,
    quantiteEau: 0,
    crainFroid: false,
    crainSecheresse: false,
    nuisible: "",
    defence: "",
    espace: 0,
    rendement: 0,
    saisonRecolte: 0,
    temperaturePref: 0)
    {
        //corps du constructeur
    }


}

public class Pommier : Plante
{
    public Pommier() :
    base(nom: "Pommier",
    emoji: "🍎",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "arbre fruitier",
    saisonSemi: 2,
    terrainPref: "argileux, drainé",
    vitesseCroissance: 10,
    espeDeVie:0,
    besoinEau: 50,
    besoinSoleil: 30,
    quantiteEau:0,
    crainFroid: false,
    crainSecheresse: true,
    nuisible: "chenilles ou oiseaux",
    defence: "fermier en colère, tailler",
    espace: 8,
    rendement: 80,
    saisonRecolte: 3,
    temperaturePref: 18)
    {
        //corps du constructeur
    }


}


public class Ble : Plante
{
    public Ble() :
    base(nom: "Blé",
    emoji: "🌾",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "céréale",
    saisonSemi: 4,
    terrainPref: "riche et humifère",
    vitesseCroissance: 9,
    espeDeVie:0,
    besoinEau: 70,
    besoinSoleil: 80,
    quantiteEau:0,
    crainFroid: true,
    crainSecheresse: false,
    nuisible: "pucerons , Maladie",
    defence: "Traitement , coccinnelle",
    espace: 4,
    rendement: 20,
    saisonRecolte: 3,
    temperaturePref: 16)
    {
        //corps du constructeur
    }

}
public class Carotte : Plante
{
    public Carotte() :
    base(nom: "Carotte",
    emoji: "🥕",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "racine",
    saisonSemi: 2,
    terrainPref: "meuble , sablonneu",
    vitesseCroissance: 3,
    espeDeVie:0,
    besoinEau: 40,
    besoinSoleil: 90,
    quantiteEau:0,
    crainFroid: false,
    crainSecheresse: false,
    nuisible: "lapins",
    defence: "fermier en colère",
    espace: 4,
    rendement: 6,
    saisonRecolte: 3,
    temperaturePref: 18)
    {
        //corps du constructeur
    }
}
public class Pecher : Plante
{
    public Pecher() :
    base(nom: "Pêcher",
    emoji: "🍑",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "arbre fruitier",
    saisonSemi: 1,
    terrainPref: "caillouteu, à l'abbri du vent",
    vitesseCroissance: 9,
    espeDeVie:0,
    besoinEau: 50,
    besoinSoleil: 80,
    quantiteEau:0,
    crainFroid: false,
    crainSecheresse: true,
    nuisible: "pucerons , champignons, maladies, gelées",
    defence: "Traitement , coccinnelle",
    espace: 8,
    rendement: 30,
    saisonRecolte: 3,
    temperaturePref: 18)
    {
        //corps du constructeur
    }

}
public class VignesArtaban : Plante
{
    public VignesArtaban() :
    base(nom: "Vignes Artaban",
    emoji: "🍇",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "vignes",
    saisonSemi: 2,
    terrainPref: "calcaire, drainé",
    vitesseCroissance: 5,
    espeDeVie:0,
    besoinEau: 50,
    besoinSoleil: 80,
    quantiteEau:0,
    crainFroid: true,
    crainSecheresse: false,
    nuisible: "Maladies, gelée",
    defence: "Traitement , taille",
    espace: 4,
    rendement: 20,
    saisonRecolte: 0,
    temperaturePref: 25)
    {
        //corps du constructeur
    }

}
public class Olivier : Plante
{
    public Olivier() :
    base(nom: "Olivier",
    emoji: "🫒",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "arbre fruitier",
    saisonSemi: 2,
    terrainPref: "drainé",
    vitesseCroissance: 7,
    espeDeVie:0,
    besoinEau: 30,
    besoinSoleil: 80,
    quantiteEau:0,
    crainFroid: false,
    crainSecheresse: false,
    nuisible: "chenilles, gelées",
    defence: "Taille, traitement",
    espace: 8,
    rendement: 50,
    saisonRecolte: 4,
    temperaturePref: 30)
    {
        //corps du constructeur
    }

}
public class Tournesol : Plante
{
    public Tournesol() :
    base(nom: "Tournesol",
    emoji: "🌻",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "fleur",
    saisonSemi: 2,
    terrainPref: "drainé, pauvre",
    vitesseCroissance: 3,
    espeDeVie:0,
    besoinEau: 70,
    besoinSoleil: 70,
    quantiteEau:0,
    crainFroid: true,
    crainSecheresse: false,
    nuisible: "pucerons , oiseaux",
    defence: "fermier en colère , coccinnelle",
    espace: 4,
    rendement: 1,
    saisonRecolte: 3,
    temperaturePref: 22)
    {
        //corps du constructeur
    }

}
