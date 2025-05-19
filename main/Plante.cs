public abstract class ObjetJeu
{
    public string Nom { private set; get; }
    public string Emoji { set; get; }
    public int PrixAchat { private set; get; }
    public int PrixVente { private set; get; }

    public ObjetJeu(string nom, string emoji,int decallageAffichage, int prixAchat = 0, int prixVente = 0)
    {
        Nom = nom;
        Emoji = emoji;
        PrixAchat = prixAchat;
        PrixVente = prixVente;
    }
}
public abstract class Plante : ObjetJeu
{
    // Caractéristiques : ne bouge pas
    public static List<Plante> ListePlantes = new List<Plante>
    {
        new Pommier(),
        new Ble(),
        new Carotte(),
        new Pecher(),
        new VignesArtaban(),
        new Citronnier(),
        new Tournesol()
    };
    public string Type { set; get; }
    public int SaisonSemis { set; get; }
    public string TerrainDePreference { set; get; }
    public bool CraintFroid { set; get; }
    public bool CraintSecheresse { set; get; }
    public int BesoinEau { set; get; }
    public int SaisonRecolte { set; get; }
    public int TemperaturePreferee { set; get; }
    public string EsperanceDeVie { set; get; }

    // EtatActuel, varie selon les saisons, les années, les nuisibles, la météo et les années
    string[] Etats { set; get; }
    public int VitesseCroissance { set; get; }
    public int Sante { set; get; } // sur 100 détermine la santé de la plante, si < 50 elle meurt
    public int Age { set; get; } // ajoute +1 à chaque semaine, si annuelle et atteint 52 alors elle meurt
    public string Etat { set; get; } // indique l'état de la plante, défini à chaque nouvelle semaine
    public int EspeDeVie { set; get; } // change avec les outils et nuisibles
    public int QuantiteEau { set; get; } //change en fonction de la météo et de l'arosoir , // sur 100 détermine les besoins en eau, si < 20 ou > 80 =>santé -10
    public int BesoinSoleil { set; get; } //change en fonction de la météo
    public string Nuisible { set; get; } //change en fonction de la classe nuisible 
    public string Defense { set; get; } //change en fonction d'outils
    public int Espace { set; get; } //change quand on taille la plante
    public int Rendement { set; get; } //change en fonction de la saison, de la météo et de l'outil panier

    //public int QuantiteEau { set; get; }  
    //public int BesoinSoleil { set; get; } 


    public Plante(string nom, string emoji, string[] etats, string type, int saisonSemi, string terrainPref,
     int vitesseCroissance, int besoinEau, int besoinSoleil, int quantiteEau, bool craintFroid, bool crainSecheresse,
    string nuisible, string defence, int espace, int rendement, int saisonRecolte, int temperaturePref, string espeDeVie, int prixAchat, int prixVente)
    : base(nom, emoji, prixAchat, prixVente)
    {
        Emoji = emoji;
        Etats = etats;
        Type = type;
        SaisonSemis = saisonSemi;
        TerrainDePreference = terrainPref;
        VitesseCroissance = vitesseCroissance;
        EsperanceDeVie = espeDeVie;
        BesoinEau = besoinEau;
        QuantiteEau = quantiteEau;
        BesoinSoleil = besoinSoleil;
        CraintFroid = craintFroid;
        CraintSecheresse = crainSecheresse;
        Nuisible = nuisible;
        Defense = defence;
        Espace = espace;
        Rendement = rendement;
        SaisonRecolte = saisonRecolte;
        TemperaturePreferee = temperaturePref;
        EsperanceDeVie = espeDeVie;

        Age = 0;
        Etat = etats[0];
    }
    public override string ToString()
    {
        string reponse;
        if (Type == "plante vide")
            reponse = "Emplacement vide !";
        else
            reponse =
            $"{Emoji} {Nom} ({Type})\n" +
            $"Etat : {Etat}\n" +
            $"Quantité d'eau : {QuantiteEau}\n" +
            $"Taux d'exposition au soleil : {BesoinSoleil}";
        return reponse;
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
    public abstract Plante Dupliquer();
}
public class PlanteVide : Plante
{
    public PlanteVide() :
    base(nom: "plante vide",
    emoji: "  ",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "plante vide",
    saisonSemi: 0,
    terrainPref: "",
    vitesseCroissance: 0,
    besoinEau: 0,
    besoinSoleil: 0,
    quantiteEau: 0,
    craintFroid: false,
    crainSecheresse: false,
    nuisible: "",
    defence: "",
    espace: 0,
    rendement: 0,
    saisonRecolte: 0,
    temperaturePref: 0,
    espeDeVie: "",
    prixAchat: 0,
    prixVente: 0)
    {
        //corps du constructeur
    }
    public override Plante Dupliquer()
    {
        return new PlanteVide();
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
    besoinEau: 50,
    besoinSoleil: 30,
    quantiteEau: 0,
    craintFroid: false,
    crainSecheresse: true,
    nuisible: "chenilles ou oiseaux",
    defence: "fermier en colère, tailler",
    espace: 8,
    rendement: 80,
    saisonRecolte: 3,
    temperaturePref: 18,
    espeDeVie: "vivace",
    prixAchat: 150,
    prixVente: 120
    )

    {

    }
    public override Plante Dupliquer()
    {
        return new Pommier();
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
    besoinEau: 70,
    besoinSoleil: 80,
    quantiteEau: 0,
    craintFroid: true,
    crainSecheresse: false,
    nuisible: "pucerons , Maladie",
    defence: "Traitement , coccinnelle",
    espace: 4,
    rendement: 20,
    saisonRecolte: 3,
    temperaturePref: 16,
    espeDeVie: "annuel",
    prixAchat: 20,
    prixVente: 20
    )
    {
        //corps du constructeur
    }
    public override Plante Dupliquer()
    {
        return new Ble();
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
    besoinEau: 40,
    besoinSoleil: 90,
    quantiteEau: 0,
    craintFroid: false,
    crainSecheresse: false,
    nuisible: "lapins",
    defence: "fermier en colère",
    espace: 4,
    rendement: 6,
    saisonRecolte: 3,
    temperaturePref: 18,
    espeDeVie: "annuel",
    prixAchat: 15,
    prixVente: 12
    )
    
    {
        //corps du constructeur
    }
    public override Plante Dupliquer()
    {
        return new Carotte();
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
    besoinEau: 50,
    besoinSoleil: 80,
    quantiteEau: 0,
    craintFroid: false,
    crainSecheresse: true,
    nuisible: "pucerons , champignons, maladies, gelées",
    defence: "Traitement , coccinnelle",
    espace: 8,
    rendement: 30,
    saisonRecolte: 3,
    temperaturePref: 18,
    espeDeVie: "vivace",
    prixAchat: 120,
    prixVente: 100
    )
    {
        //corps du constructeur
    }
    public override Plante Dupliquer()
    {
        return new Pecher();
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
    besoinEau: 50,
    besoinSoleil: 80,
    quantiteEau: 0,
    craintFroid: true,
    crainSecheresse: false,
    nuisible: "Maladies, gelée",
    defence: "Traitement , taille",
    espace: 4,
    rendement: 20,
    saisonRecolte: 0,
    temperaturePref: 25,
    espeDeVie: "vivace",
    prixAchat: 100,
    prixVente: 80
    )
    {
        //corps du constructeur
    }
    public override Plante Dupliquer()
    {
        return new VignesArtaban();
    }

}
public class Citronnier : Plante
{
    public Citronnier() :
    base(nom: "Citronnier",
    emoji: "🍋",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "arbre fruitier",
    saisonSemi: 2,
    terrainPref: "drainé",
    vitesseCroissance: 7,
    besoinEau: 30,
    besoinSoleil: 80,
    quantiteEau: 0,
    craintFroid: true,
    crainSecheresse: false,
    nuisible: "chenilles, gelées, pucerons",
    defence: "Taille, traitement, coccinnelle",
    espace: 8,
    rendement: 5,
    saisonRecolte: 4,
    temperaturePref: 20,
    espeDeVie: "vivace",
    prixAchat: 180,
    prixVente: 150
    )

    {
        //corps du constructeur
    }
    public override Plante Dupliquer()
    {
        return new Citronnier();
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
    besoinEau: 70,
    besoinSoleil: 70,
    quantiteEau: 0,
    craintFroid: true,
    crainSecheresse: false,
    nuisible: "pucerons , oiseaux",
    defence: "fermier en colère , coccinnelle",
    espace: 4,
    rendement: 1,
    saisonRecolte: 3,
    temperaturePref: 22,
    espeDeVie: "annuel",
    prixAchat: 25,
    prixVente: 20
    )
    {
        //corps du constructeur
    }
    public override Plante Dupliquer()
    {
        return new Tournesol();
    }

}
