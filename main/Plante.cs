public abstract class Plante
{
    // Caractéristiques
    string[] Etats { set; get; }
    public string Nom { get; private set; }
    public string Emoji { set; get; }
    public string Type { set; get; }
    public int SaisonSemi { set; get; }
    public string TerrainPref { set; get; }
    public int VitesseCroissance { set; get; }
    public int BesoinEau { set; get; }
    public bool CrainFroid { set; get; }
    public bool CrainSecheresse { set; get; }
    public string Nuisible { set; get; }
    public string Defense { set; get; }
    public int Espace { set; get; }
    public int Rendement { set; get; }
    public int Recolte { set; get; }
    public int TemperaturePref { set; get; }

    // EtatActuel
    public int Sante { set; get; } // sur 100 détermine la santé de la plante, si < 50 elle meurt
    public int Age { set; get; } // ajoute +1 à chaque semaine, si annuelle et atteint 52 alors elle meurt
    public string Etat { set; get; } // indique l'état de la plante, défini à chaque nouvelle semaine
    public int QuantiteEau { set; get; } // sur 100 détermine les besoins en eau, si < 20 ou > 80 =>santé -10 
    public int BesoinSoleil { set; get; }


    public Plante(string nom, string emoji, string[] etats, string type, int saisonSemi, string terrainPref,
    int vitesseCroissance, int besoinEau, int besoinSoleil, bool crainFroid, bool crainSecheresse,
    string nuisible, string defence, int espace, int rendement, int recolte, int temperaturePref)
    {
        Nom = nom;
        Emoji = emoji;
        Etats = etats;
        Type = type;
        SaisonSemi = saisonSemi;
        TerrainPref = terrainPref;
        VitesseCroissance = vitesseCroissance;
        BesoinEau = besoinEau;
        BesoinSoleil = besoinSoleil;
        CrainFroid = crainFroid;
        CrainSecheresse = crainSecheresse;
        Nuisible = nuisible;
        Defense = defence;
        Espace = espace;
        Rendement = rendement;
        Recolte = recolte;
        TemperaturePref = temperaturePref;

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
        Emoji = "💀";
    }


}
public class PlanteVide : Plante
{
    public PlanteVide() :
    base(nom: "Pommier",
    emoji: " ",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "",
    saisonSemi: 2,
    terrainPref: "argileux, drainé",
    vitesseCroissance: 10,
    besoinEau: 5,
    besoinSoleil: 3,
    crainFroid: false,
    crainSecheresse: true,
    nuisible: "chenilles et oiseaux",
    defence: "fermier en colère, filets, tailler",
    espace: 8,
    rendement: 80,
    recolte: 3,
    temperaturePref: 18)
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
    besoinEau: 5,
    besoinSoleil: 3,
    crainFroid: false,
    crainSecheresse: true,
    nuisible: "chenilles et oiseaux",
    defence: "fermier en colère, filets, tailler",
    espace: 8,
    rendement: 80,
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
    emoji: "🌾",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "céréale",
    saisonSemi: 4,
    terrainPref: "riche et humifère",
    vitesseCroissance: 9,
    besoinEau: 7,
    besoinSoleil: 8,
    crainFroid: true,
    crainSecheresse: false,
    nuisible: "pucerons , Oïdium",
    defence: "Traitement , coccinnelle",
    espace: 4,
    rendement: 20,
    recolte: 3,
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
    besoinEau: 4,
    besoinSoleil: 9,
    crainFroid: false,
    crainSecheresse: false,
    nuisible: "lapins, mouches",
    defence: "filets, fermier en colère",
    espace: 4,
    rendement: 6,
    recolte: 3,
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
    besoinEau: 5,
    besoinSoleil: 8,
    crainFroid: false,
    crainSecheresse: true,
    nuisible: "pucerons , champignons, oïdium, gelées",
    defence: "Traitement , coccinnelle",
    espace: 8,
    rendement: 30,
    recolte: 3,
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
    besoinEau: 5,
    besoinSoleil: 8,
    crainFroid: true,
    crainSecheresse: false,
    nuisible: "mildiou , Oïdium, gelée",
    defence: "Traitement , taille",
    espace: 4,
    rendement: 20,
    recolte: 0,
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
    besoinEau: 3,
    besoinSoleil: 9,
    crainFroid: false,
    crainSecheresse: false,
    nuisible: "cochenilles, gelées",
    defence: "Taille, tratement",
    espace: 8,
    rendement: 50,
    recolte: 4,
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
    besoinEau: 7,
    besoinSoleil: 8,
    crainFroid: true,
    crainSecheresse: false,
    nuisible: "pucerons , oiseaux",
    defence: "filets , coccinnelle",
    espace: 4,
    rendement: 1,
    recolte: 3,
    temperaturePref: 22)
    {
        //corps du constructeur
    }

}
