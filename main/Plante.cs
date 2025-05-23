public abstract class Plante : ObjetJeuAchatVente
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
    public string Type { get; set; }
    public int SaisonSemis { get; set; }
    public int SaisonRecolte { get; set; }
    public string TerrainDePreference { get; set; }
    // si elle n'est pas plantée sur son terrain de préférence -> santé -10
    public bool CraintFroid { get; set; }
    public bool CraintSecheresse { get; set; }
    public int BesoinEau { get; set; }
    // indique la quantité nécessaire chaque semaine
    public int TemperaturePreferee { get; set; }
    // temperature preferee, si > ou
    public string EsperanceDeVie { get; set; }

    // EtatActuel, varie selon les saisons, les années, les nuisibles, la météo et les années
    string[] Etats { get; set; }
    public int Croissance { get; set; }
    public int VitesseCroissance { get; set; }
    public bool Mature { get; set; }
    public int Age { get; set; }
    // ajoute +1 à chaque semaine, si annuelle et atteint 52 alors elle meurt
    public int EspeDeVie { get; set; }
    // change avec les outils et nuisibles
    public int Sante { get; set; }
    // sur 100 détermine la santé de la plante, si < 50 elle meurt

    public string Etat { get; set; }
    // indique l'état de la plante, défini à chaque nouvelle semaine

    public int QuantiteEau { get; set; }
    //change en fonction de la météo et de l'arosoir , // sur 100 détermine les besoins en eau, si < 20 ou > 80 =>santé -10
    public int BesoinSoleil { get; set; } //change en fonction de la météo
    public int QuantiteSoleil { get; set; }

    public List<string> Nuisibles { get; set; } //change en fonction de la classe nuisible 
    public string Defense { get; set; } //change en fonction d'outils
    public int Espace { get; set; } //change quand on taille la plante
    public int[] Rendement { get; set; } //change en fonction de la saison, de la météo et de l'outil panier
    public Recolte TypeRecolte { get; set; } //
    public int RendementActuel { get; set; }

    //public int QuantiteEau { get; set; }  
    //public int BesoinSoleil { get; set; } 


    public Plante(string nom, string emoji, string[] etats, string type, int saisonSemi, string terrainPref,
     int vitesseCroissance, int besoinEau, int besoinSoleil, int quantiteEau, bool craintFroid, bool craintSecheresse,
    List<string> nuisibles, string defence, int espace, int[] rendement, int saisonRecolte, Recolte typeRecolte, int temperaturePref, string espeDeVie, int prixAchat, int prixVente)
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
        CraintSecheresse = craintSecheresse;
        Nuisibles = nuisibles;
        Defense = defence;
        Espace = espace;
        Rendement = rendement;
        SaisonRecolte = saisonRecolte;
        TypeRecolte = typeRecolte;
        TemperaturePreferee = temperaturePref;
        EsperanceDeVie = espeDeVie;
        Age = 0;
        Etat = "semis";
        RendementActuel = 0;
        Mature = false;
        Croissance = 0;
    }
    public override string ToString()
    {
        string reponse;
        if (Type == "plante vide")
            reponse = "Emplacement vide !";
        else
        {
            reponse =
            $"{Emoji} {Nom} ({Type})\n" +
            $"- Santé : {Sante}%\n"+
            $"- Etat : {Etat}\n" +
            $"- Nuisibles :";
            if (Nuisibles.Count() == 0) reponse += " aucun\n";
            else
            {
                for (int i = 0; i < Nuisibles.Count(); i++)
                {
                    reponse += " " + Nuisibles[i];
                }
                reponse += "\n";
            }
            reponse += $"- Options :";
            if (Nuisibles.Count() == 0) reponse += " aucun\n";
            else
            {
                for (int i = 0; i < Nuisibles.Count(); i++)
                {
                    reponse += " " + Nuisibles[i];
                }
                reponse += "\n";
            }
        }
        return reponse;
    }
    public void Vieillir()
    {
        Age += 1;
        if (!Mature)
        {
            Croissance += VitesseCroissance;
            if (Croissance > 100)
            {
                Mature = true;
            }
        }

    }
    public void Mourir()
    {
        Etat = "mort";
        Emoji = "💀";

    }
    // public int AjouterNuisible()
    // {
    //     int indice = rng.Next(Nuisibles.Count);

    // }
    
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
    craintSecheresse: false,
    nuisibles: [],
    defence: "",
    espace: 0,
    rendement: new int[] { 0, 0, 0, 0, 0 },
    saisonRecolte: 0,
    typeRecolte: new RecolteVide(),
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


//////////////////
////Carcassonne /
/////////////////

public class Pommier : Plante
{
    public Pommier() :
    base(nom: "Pommier",
    emoji: "🍎",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "arbre fruitier",
    saisonSemi: 1,
    terrainPref: "argileux, drainé",
    vitesseCroissance: 10,
    besoinEau: 50,
    besoinSoleil: 30,
    quantiteEau: 0,
    craintFroid: false,
    craintSecheresse: true,
    nuisibles: ["chenilles", "oiseaux"],
    defence: "fermier en colère, tailler",
    espace: 8,
    rendement: new int[] { 0, 4, 8, 12, 20 },
    saisonRecolte: 2,
    typeRecolte: new RecoltePommier(),
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
    saisonSemi: 3,
    terrainPref: "riche et humifère",
    vitesseCroissance: 9,
    besoinEau: 70,
    besoinSoleil: 80,
    quantiteEau: 0,
    craintFroid: true,
    craintSecheresse: false,
    nuisibles: ["pucerons", "maladie"],
    defence: "Traitement , coccinnelle",
    espace: 4,
    rendement: new int[] { 0, 1, 3, 5, 8 },
    saisonRecolte: 2,
    typeRecolte: new RecolteBle(),
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
    saisonSemi: 1,
    terrainPref: "meuble , sablonneu",
    vitesseCroissance: 3,
    besoinEau: 40,
    besoinSoleil: 90,
    quantiteEau: 0,
    craintFroid: false,
    craintSecheresse: false,
    nuisibles: ["lapins"],
    defence: "fermier en colère",
    espace: 4,
    rendement: new int[] { 0, 1, 2, 3, 6 },
    saisonRecolte: 2,
    typeRecolte: new RecolteCarotte(),
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
    saisonSemi: 0,
    terrainPref: "caillouteu, à l'abbri du vent",
    vitesseCroissance: 9,
    besoinEau: 50,
    besoinSoleil: 80,
    quantiteEau: 0,
    craintFroid: false,
    craintSecheresse: true,
    nuisibles: ["pucerons", "champignons", "maladies", "gelées"],
    defence: "Traitement , coccinnelle",
    espace: 8,
    rendement: new int[] { 0, 2, 5, 8, 15 },
    saisonRecolte: 2,
    typeRecolte: new RecoltePecher(),
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
    craintSecheresse: false,
    nuisibles: ["maladies, gelée"],
    defence: "Traitement , taille",
    espace: 4,
    rendement: new int[] { 0, 2, 4, 7, 10 },
    saisonRecolte: 0,
    typeRecolte: new RecolteVigne(),
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
    saisonSemi: 1,
    terrainPref: "drainé",
    vitesseCroissance: 7,
    besoinEau: 30,
    besoinSoleil: 80,
    quantiteEau: 0,
    craintFroid: true,
    craintSecheresse: false,
    nuisibles: ["chenilles, gelées, pucerons"],
    defence: "Taille, traitement, coccinnelle",
    espace: 8,
    rendement: new int[] { 0, 1, 2, 3, 5 },
    saisonRecolte: 3,
    typeRecolte: new RecolteCitronnier(),
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
    saisonSemi: 1,
    terrainPref: "drainé, pauvre",
    vitesseCroissance: 3,
    besoinEau: 70,
    besoinSoleil: 70,
    quantiteEau: 0,
    craintFroid: true,
    craintSecheresse: false,
    nuisibles: ["pucerons , oiseaux"],
    defence: "fermier en colère , coccinnelle",
    espace: 4,
    rendement: new int[] { 0, 0, 1, 1, 1 },
    saisonRecolte: 2,
    typeRecolte: new RecolteTournesol(),
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


////////////////////////////
////Mexique : Soconusco ////
///////////////////////////

public class Mais : Plante
{
    public Mais() :
    base(nom: "Maïs",
    emoji: "🌽",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "céréale",
    saisonSemi: 1,
    terrainPref: "riche, profond",
    vitesseCroissance: 4,
    besoinEau: 45,
    besoinSoleil: 90,
    quantiteEau: 0,
    craintFroid: false,
    craintSecheresse: false,
    nuisibles: ["maladie", "pucerons"],
    defence: "traitements bio",
    espace: 4,
    rendement: new int[] { 0, 0, 1, 2, 3 },
    saisonRecolte: 2,
    typeRecolte: new RecolteMais(),
    temperaturePref: 25,
    espeDeVie: "annuelle",
    prixAchat: 20,
    prixVente: 40
    )
    { }

    public override Plante Dupliquer()
    {
        return new Mais();
    }
}

public class Haricot : Plante
{
    public Haricot() :
    base(nom: "Haricot",
    emoji: "🫘",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "légume",
    saisonSemi: 2,
    terrainPref: "léger, bien drainé",
    vitesseCroissance: 3,
    besoinEau: 40,
    besoinSoleil: 85,
    quantiteEau: 0,
    craintFroid: false,
    craintSecheresse: false,
    nuisibles: ["maladie", "pucerons"],
    defence: "filets anti-insectes",
    espace: 4,
    rendement: new int[] { 0, 2, 5, 8, 15 },
    saisonRecolte: 3,
    typeRecolte: new RecolteHaricot(),
    temperaturePref: 22,
    espeDeVie: "annuelle",
    prixAchat: 15,
    prixVente: 35
    )
    { }

    public override Plante Dupliquer()
    {
        return new Haricot();
    }
}

public class Tomate : Plante
{
    public Tomate() :
    base(nom: "Tomate",
    emoji: "🍅",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "légume",
    saisonSemi: 1,
    terrainPref: "riche, chaud",
    vitesseCroissance: 4,
    besoinEau: 70,
    besoinSoleil: 75,
    quantiteEau: 0,
    craintFroid: false,
    craintSecheresse: false,
    nuisibles: ["maldie"],
    defence: "paillage, traitements naturels",
    espace: 4,
    rendement: new int[] { 0, 1, 2, 3, 5 },
    saisonRecolte: 3,
    typeRecolte: new RecolteTomate(),
    temperaturePref: 22,
    espeDeVie: "annuelle",
    prixAchat: 25,
    prixVente: 50
    )
    { }

    public override Plante Dupliquer()
    {
        return new Tomate();
    }
}

public class Avocat : Plante
{
    public Avocat() :
    base(nom: "Avocat",
    emoji: "🥑",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "légume",
    saisonSemi: 1,
    terrainPref: "profond, drainé",
    vitesseCroissance: 48,
    besoinEau: 50,
    besoinSoleil: 70,
    quantiteEau: 0,
    craintFroid: true,
    craintSecheresse: true,
    nuisibles: ["chenilles", "pucerons"],
    defence: "traitements bio, paillage",
    espace: 6,
    rendement: new int[] { 0, 5, 15, 30, 50 },
    saisonRecolte: 3,
    typeRecolte: new RecolteAvocat(),
    temperaturePref: 25,
    espeDeVie: "pérenne",
    prixAchat: 80,
    prixVente: 300
    )
    { }

    public override Plante Dupliquer()
    {
        return new Avocat();
    }
}

public class Cafe : Plante
{
    public Cafe() :
    base(nom: "Café",
    emoji: "☕️",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "arbre fruitié",
    saisonSemi: 1,
    terrainPref: "léger, acide, bien drainé",
    vitesseCroissance: 48,
    besoinEau: 60,
    besoinSoleil: 65,
    quantiteEau: 0,
    craintFroid: true,
    craintSecheresse: true,
    nuisibles: [ "maladie"],
    defence: "ombres, traitements naturels",
    espace: 6,
    rendement: new int[] { 0, 0, 1, 1, 2 },
    saisonRecolte: 3,
    typeRecolte: new RecolteCafe(),
    temperaturePref: 21,
    espeDeVie: "pérenne",
    prixAchat: 100,
    prixVente: 350
    )
    { }

    public override Plante Dupliquer()
    {
        return new Cafe();
    }
}

public class Cacaoyer : Plante
{
    public Cacaoyer() :
    base(nom: "Cacaoyer",
    emoji: "🍫",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "arbre fruitier",
    saisonSemi: 1,
    terrainPref: "humide, riche, ombragé",
    vitesseCroissance: 48,
    besoinEau: 70,
    besoinSoleil: 60,
    quantiteEau: 0,
    craintFroid: true,
    craintSecheresse: true,
    nuisibles: ["maladie"],
    defence: "ombres, paillage, traitement naturel",
    espace: 6,
    rendement: new int[] { 0, 2, 5, 10, 18 },
    saisonRecolte: 3,
    typeRecolte: new RecolteCacaoyer(),
    temperaturePref: 26,
    espeDeVie: "pérenne",
    prixAchat: 120,
    prixVente: 400
    )
    { }

    public override Plante Dupliquer()
    {
        return new Cacaoyer();
    }
}

public class Tabac : Plante
{
    public Tabac() :
    base(nom: "Tabac",
    emoji: "🚬",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "fleur",
    saisonSemi: 3,
    terrainPref: "léger, bien drainé",
    vitesseCroissance: 3,
    besoinEau: 35,
    besoinSoleil: 85,
    quantiteEau: 0,
    craintFroid: true,
    craintSecheresse: false,
    nuisibles: ["pucerons, chenilles"],
    defence: "traitements bio, rotation cultures",
    espace: 3,
    rendement: new int[] { 0, 2, 5, 10, 20 },
    saisonRecolte: 3,
    typeRecolte: new RecolteTabac(),
    temperaturePref: 24,
    espeDeVie: "annuelle",
    prixAchat: 18,
    prixVente: 60
    )
    { }

    public override Plante Dupliquer()
    {
        return new Tabac();
    }
}

public class Piment : Plante
{
    public Piment() :
    base(nom: "Piment",
    emoji: "🌶️",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "légume",
    saisonSemi: 1,
    terrainPref: "léger, riche",
    vitesseCroissance: 5,
    besoinEau: 55,
    besoinSoleil: 90,
    quantiteEau: 0,
    craintFroid: true,
    craintSecheresse: true,
    nuisibles: ["pucerons"],
    defence: "traitements naturels, paillage",
    espace: 3,
    rendement: new int[] { 0, 3, 8, 15, 25 },
    saisonRecolte: 2,
    typeRecolte: new RecoltePiment(),
    temperaturePref: 30,
    espeDeVie: "annuelle",
    prixAchat: 22,
    prixVente: 70
    )
    { }

    public override Plante Dupliquer()
    {
        return new Piment();
    }
}

/////////////////////////
////Japon : Hokkaido ////
/////////////////////////

public class Riz : Plante
{
    public Riz() :
    base(nom: "Riz",
    emoji: "🍚",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "céréales",
    saisonSemi: 1,
    terrainPref: "submergé, argileux",
    vitesseCroissance: 5,
    besoinEau: 90,
    besoinSoleil: 85,
    quantiteEau: 0,
    craintFroid: true,
    craintSecheresse: true,
    nuisibles: ["pucerons"],
    defence: "traitement, fermier en colère",
    espace: 4,
    rendement: new int[] { 0, 5, 20, 40, 80 },
    saisonRecolte: 3,
    typeRecolte: new RecolteRiz(),
    temperaturePref: 28,
    espeDeVie: "annuelle",
    prixAchat: 15,
    prixVente: 45
    )
    { }

    public override Plante Dupliquer()
    {
        return new Riz();
    }
}

public class PatateDouce : Plante
{
    public PatateDouce() :
    base(nom: "Patate douce",
    emoji: "🍠",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "légume",
    saisonSemi: 1,
    terrainPref: "sableux, meuble",
    vitesseCroissance: 4,
    besoinEau: 20,
    besoinSoleil: 90,
    quantiteEau: 0,
    craintFroid: false,
    craintSecheresse: false,
    nuisibles: ["maladie"],
    defence: "traitements naturels",
    espace: 4,
    rendement: new int[] { 0, 0, 1, 2, 3 },
    saisonRecolte: 2,
    typeRecolte: new RecoltePatateDouce(),
    temperaturePref: 24,
    espeDeVie: "annuelle",
    prixAchat: 20,
    prixVente: 50
    )
    { }

    public override Plante Dupliquer()
    {
        return new PatateDouce();
    }
}

public class TheVert : Plante
{
    public TheVert() :
    base(nom: "Thé vert",
    emoji: "🍵",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "fleur",
    saisonSemi: 1,
    terrainPref: "acide, drainé",
    vitesseCroissance: 2,
    besoinEau: 50,
    besoinSoleil: 40,
    quantiteEau: 0,
    craintFroid: false,
    craintSecheresse: false,
    nuisibles: ["pucerons"],
    defence: "taille, traitements bio",
    espace: 4,
    rendement: new int[] { 0, 5, 20, 40, 80 },
    saisonRecolte: 1,
    typeRecolte: new RecolteTheVert(),
    temperaturePref: 21,
    espeDeVie: "pérenne",
    prixAchat: 50,
    prixVente: 120
    )
    { }

    public override Plante Dupliquer()
    {
        return new TheVert();
    }
}



public class ConcombreJaponais : Plante
{
    public ConcombreJaponais() :
    base(nom: "Concombre jap.",
    emoji: "🥒",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "légume",
    saisonSemi: 2,
    terrainPref: "riche, humifère",
    vitesseCroissance: 3,
    besoinEau: 85,
    besoinSoleil: 75,
    quantiteEau: 0,
    craintFroid: true,
    craintSecheresse: true,
    nuisibles: ["maladie", "pucerons"],
    defence: "paillage, traitements naturels",
    espace: 4,
    rendement: new int[] { 0, 1, 3, 6, 10 },
    saisonRecolte: 2,
    typeRecolte: new RecolteConcombreJaponais(),
    temperaturePref: 24,
    espeDeVie: "annuelle",
    prixAchat: 22,
    prixVente: 55
    )
    { }

    public override Plante Dupliquer()
    {
        return new ConcombreJaponais();
    }
}





public class Brocoli : Plante
{
    public Brocoli() :
    base(nom: "Brocoli",
    emoji: "🥦",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "légume",
    saisonSemi: 2,
    terrainPref: "frais, riche",
    vitesseCroissance: 3,
    besoinEau: 60,
    besoinSoleil: 80,
    quantiteEau: 0,
    craintFroid: true,
    craintSecheresse: true,
    nuisibles: ["maladie", "chenilles"],
    defence: "filets, fermier en colère",
    espace: 4,
    rendement: new int[] { 0, 0, 1, 1, 2 },
    saisonRecolte: 2,
    typeRecolte: new RecolteBrocoli(),
    temperaturePref: 18,
    espeDeVie: "annuelle",
    prixAchat: 30,
    prixVente: 65
    )
    { }

    public override Plante Dupliquer()
    {
        return new Brocoli();
    }
}

public class Tulipe : Plante
{
    public Tulipe() :
    base(nom: "Tulipe",
    emoji: "🌷",
    etats: ["semis", "mature", "déshydraté", "gelé", "malade", "mort"],
    type: "fleur",
    saisonSemi: 2,
    terrainPref: "Léger, bien drainé",
    vitesseCroissance: 3,
    besoinEau: 30,
    besoinSoleil: 70,
    quantiteEau: 0,
    craintFroid: true,
    craintSecheresse: true,
    nuisibles: ["pucerons", "chenilles"],
    defence: "paillage, coccinnelle",
    espace: 4,
    saisonRecolte: 1,
    typeRecolte: new RecolteTulipe(),
    rendement: new int[] { 0, 0, 1, 1, 2 },
    temperaturePref: 18,
    espeDeVie: "annuelle",
    prixAchat: 25,
    prixVente: 60
    )
    { }

    public override Plante Dupliquer()
    {
        return new Brocoli();
    }
}


