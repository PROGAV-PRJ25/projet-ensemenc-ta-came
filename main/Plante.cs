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
    public int SaisonRecolte { set; get; }
    public string TerrainDePreference { set; get; }
    // si elle n'est pas plantée sur son terrain de préférence -> santé -10
    public bool CraintFroid { set; get; }
    public bool CraintSecheresse { set; get; }
    public int BesoinEau { set; get; }
    // indique la quantité nécessaire chaque semaine
    public int TemperaturePreferee { set; get; }
    // temperature preferee, si > ou
    public string EsperanceDeVie { set; get; }

    // EtatActuel, varie selon les saisons, les années, les nuisibles, la météo et les années
    string[] Etats { set; get; }
    public int VitesseCroissance { set; get; }
    public int Sante { set; get; }
    // sur 100 détermine la santé de la plante, si < 50 elle meurt
    public int Age { set; get; }
    // ajoute +1 à chaque semaine, si annuelle et atteint 52 alors elle meurt
    public int EspeDeVie { set; get; }
    // change avec les outils et nuisibles
    public string Etat { set; get; }
    // indique l'état de la plante, défini à chaque nouvelle semaine
    
    public int QuantiteEau { set; get; }
    //change en fonction de la météo et de l'arosoir , // sur 100 détermine les besoins en eau, si < 20 ou > 80 =>santé -10
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
    saisonSemi: 2,
    terrainPref: "riche, profond",
    vitesseCroissance: 4,
    besoinEau: 45,
    besoinSoleil: 90,
    quantiteEau: 0,
    craintFroid: false,
    crainSecheresse: false,
    nuisible: "pyrales, pucerons",
    defence: "traitements bio",
    espace: 4,
    rendement: 3,
    saisonRecolte: 3,
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
    saisonSemi: 3,
    terrainPref: "léger, bien drainé",
    vitesseCroissance: 3,
    besoinEau: 40,
    besoinSoleil: 85,
    quantiteEau: 0,
    craintFroid: false,
    crainSecheresse: false,
    nuisible: "bruches, pucerons",
    defence: "filets anti-insectes",
    espace: 4,
    rendement: 15,
    saisonRecolte: 3,
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
    saisonSemi: 2,
    terrainPref: "riche, chaud",
    vitesseCroissance: 4,
    besoinEau: 70,
    besoinSoleil: 75,
    quantiteEau: 0,
    craintFroid: false,
    crainSecheresse: false,
    nuisible: "mildiou, aleurodes",
    defence: "paillage, traitements naturels",
    espace: 4,
    rendement: 5,
    saisonRecolte: 4,
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
    type: "fruit",
    saisonSemi: 2,
    terrainPref: "profond, drainé",
    vitesseCroissance: 48,
    besoinEau: 50,
    besoinSoleil: 70,
    quantiteEau: 0,
    craintFroid: true,
    crainSecheresse: true,
    nuisible: "cochenilles, acariens",
    defence: "traitements bio, paillage",
    espace: 6,
    rendement: 150,
    saisonRecolte: 4,
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
    type: "graines",
    saisonSemi: 2,
    terrainPref: "léger, acide, bien drainé",
    vitesseCroissance: 48,
    besoinEau: 60,
    besoinSoleil: 65,
    quantiteEau: 0,
    craintFroid: true,
    crainSecheresse: true,
    nuisible: "nématodes, rouille",
    defence: "ombres, traitements naturels",
    espace: 6,
    rendement: 2,
    saisonRecolte: 4,
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
    type: "graines",
    saisonSemi: 2,
    terrainPref: "humide, riche, ombragé",
    vitesseCroissance: 48,
    besoinEau: 70,
    besoinSoleil: 60,
    quantiteEau: 0,
    craintFroid: true,
    crainSecheresse: true,
    nuisible: "charançons, pourriture",
    defence: "ombres, paillage, traitement naturel",
    espace: 6,
    rendement: 35,
    saisonRecolte: 4,
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
    type: "plante",
    saisonSemi: 3,
    terrainPref: "léger, bien drainé",
    vitesseCroissance: 3,
    besoinEau: 35,
    besoinSoleil: 85,
    quantiteEau: 0,
    craintFroid: true,
    crainSecheresse: false,
    nuisible: "pucerons, chenilles",
    defence: "traitements bio, rotation cultures",
    espace: 3,
    rendement: 25,
    saisonRecolte: 4,
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
    type: "plante",
    saisonSemi: 2,
    terrainPref: "léger, riche",
    vitesseCroissance: 5,
    besoinEau: 55,
    besoinSoleil: 90,
    quantiteEau: 0,
    craintFroid: true,
    crainSecheresse: true,
    nuisible: "pucerons, thrips",
    defence: "traitements naturels, paillage",
    espace: 3,
    rendement: 35,
    saisonRecolte: 3,
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
    saisonSemi: 2,
    terrainPref: "submergé, argileux",
    vitesseCroissance: 5,
    besoinEau: 90,
    besoinSoleil: 85,
    quantiteEau: 0,
    craintFroid: true,
    crainSecheresse: true,
    nuisible: "pyriculariose, insectes aquatiques",
    defence: "traitement, fermier en colère",
    espace: 4,
    rendement: 100,
    saisonRecolte: 4,
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
    saisonSemi: 2,
    terrainPref: "sableux, meuble",
    vitesseCroissance: 4,
    besoinEau: 20,
    besoinSoleil: 90,
    quantiteEau: 0,
    craintFroid: false,
    crainSecheresse: false,
    nuisible: "charançons, nématodes",
    defence: "traitements naturels",
    espace: 4,
    rendement: 3,
    saisonRecolte: 3,
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
    type: "plante",
    saisonSemi: 2,
    terrainPref: "acide, drainé",
    vitesseCroissance: 2,
    besoinEau: 50,
    besoinSoleil: 40,
    quantiteEau: 0,
    craintFroid: false,
    crainSecheresse: false,
    nuisible: "acariens, champignons",
    defence: "taille, traitements bio",
    espace: 4,
    rendement: 100,
    saisonRecolte: 2,
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
    saisonSemi: 3,
    terrainPref: "riche, humifère",
    vitesseCroissance: 3,
    besoinEau: 85,
    besoinSoleil: 75,
    quantiteEau: 0,
    craintFroid: true,
    crainSecheresse: true,
    nuisible: "oïdium, pucerons",
    defence: "paillage, traitements naturels",
    espace: 4,
    rendement: 10,
    saisonRecolte: 3,
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
    saisonSemi: 3,
    terrainPref: "frais, riche",
    vitesseCroissance: 3,
    besoinEau: 60,
    besoinSoleil: 80,
    quantiteEau: 0,
    craintFroid: true,
    crainSecheresse: true,
    nuisible: "altises, chenilles",
    defence: "filets, fermier en colère",
    espace: 4,
    rendement: 2,
    saisonRecolte: 3,
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


