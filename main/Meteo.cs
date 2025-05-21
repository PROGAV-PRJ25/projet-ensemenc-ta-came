public class Meteo{
    
    private static int[][] Temperature {get; set;}
    public int TemperatureActuelle {get; set;}
    public Temps TempsActuel {get; set;}


}

public abstract class Temps 
{
    public string NOM {set;get;}
    public string EMOJI {set;get;}
    public abstract void Action(Parcelle parcelle);
    protected Temps(string nom, string emoji)
    {
        NOM = nom;
        EMOJI = emoji;
    }
}
public class Pluie : Temps {
    public Pluie(string nom, string emoji) : base(nom, emoji) {
        NOM = "Pluie";
        EMOJI = "🌧️";
    }
    public override void Action(Parcelle parcelle) 
    {
        //dépend du besoin eau de la plante mais rajoute +10 à quantitéeau
        //si dépasse besoin eau de 30, santé -20
        //sinon si besoin eau atteint (à +30% pret), santé +10
        parcelle.Contenu.QuantiteEau += 10;
        if (parcelle.Contenu.QuantiteEau > parcelle.Contenu.BesoinEau + 30)
        {
            parcelle.Contenu.Sante -= 20;
        }
        else if (Math.Abs(parcelle.Contenu.QuantiteEau - parcelle.Contenu.BesoinEau) <= parcelle.Contenu.BesoinEau * 0.3)
        {
            parcelle.Contenu.Sante += 10;
        }
    }
}

public class Soleil : Temps {
    public Soleil(string nom, string emoji) : base(nom, emoji) {
        NOM = "Soleil";
        EMOJI = "🌞";
    }
    public override void Action(Parcelle parcelle) 
    {
        //dépend du besoinsoleil de la plante, rajoute +15 à quantité soleil
        //si dépasse besoin soleil de 5 et quantitéeau qui est en dessous de besoin eau,  ce mois ci, santé-25
        //sinon, si besoinsoleil atteint (à +_ 5%), santé +5
        parcelle.Contenu.BesoinSoleil += 15;
        if (parcelle.Contenu.BesoinSoleil > parcelle.Contenu.BesoinEau + 5 &&
            parcelle.Contenu.QuantiteEau < parcelle.Contenu.BesoinEau)
        {
            parcelle.Contenu.Sante -= 25;
        }
        else if (Math.Abs(parcelle.Contenu.BesoinSoleil - parcelle.Contenu.BesoinEau) <= 5)
        {
            parcelle.Contenu.Sante += 5;
        }
    }
}

public class VentAutan : Temps {
    public VentAutan(string nom, string emoji) : base(nom, emoji) {
        NOM = "Vent d'Autan";
        EMOJI = "🌬️";
    }
    public override void Action(Parcelle parcelle) 
    {
        //vent sec et chaud : réduit la quantité d'eau de -15%
        //si réduit trop quantitéeau en dessous de besoineau, santé-25
        //accelere la vitessecroissance de +2 (en mois) donc récolte plus tot
        parcelle.Contenu.QuantiteEau = (int)(parcelle.Contenu.QuantiteEau * 0.85);
        if (parcelle.Contenu.QuantiteEau < parcelle.Contenu.BesoinEau)
        {
            parcelle.Contenu.Sante -= 25;
        }
        parcelle.Contenu.VitesseCroissance += 2;
    }
}

public class Gel : Temps {
    public Gel(string nom, string emoji) : base(nom, emoji) {
        NOM = "Gel";
        EMOJI = "🥶";
    }
    public override void Action(Parcelle parcelle) 
    {
        //si gel et crainfroid true, santé =-20
        //si fumier ou tente, protège du gel et ne fait rien
        bool protegeParFumierOuSerre = parcelle.Defense != null &&
            (parcelle.Defense.Contains("Fumier") || parcelle.Defense.Contains("Serre"));

        if (parcelle.Contenu.CraintFroid && !protegeParFumierOuSerre)
        {
            parcelle.Contenu.Sante -= 20;
            parcelle.Contenu.Etat = "gelé";
        }
    }
}

public class Nuage : Temps {
    public Nuage(string nom, string emoji) : base(nom, emoji) {
        NOM = "Nuage";
        EMOJI = "🌥️";
    }
    public override void Action(Parcelle parcelle) 
    {

        // nuage ; pas d'ensoleillement et 0 humidité
    }
    
}

public class Temperature {
    
    public string NOM {set;get;}
    public string EMOJI {set;get;}
    
    public Temperature(string nom, string emoji) 
    {
        NOM = "Temperature";
        EMOJI = "🌡️";
    }

    public void Action(Parcelle parcelle)
    {
        double[][] Temperatures = new double[][] 
        {
            /* 2009 */ new double[] { 3.00, -1.19, 3.67, 5.86, 5.10, 3.90, 3.05, 2.90, 6.43, 4.57, 8.71, 8.48, 7.29, 8.29, 9.33, 10.57, 11.52, 12.57, 16.90, 16.81, 20.38, 19.33, 20.24, 21.57, 22.29, 22.57, 27.29, 22.00, 23.10, 25.48, 23.86, 23.24, 23.29, 25.52, 20.95, 20.24, 18.52, 14.95, 16.05, 14.71, 18.14, 7.76, 12.10, 13.05, 9.33, 9.43, 12.48, 8.90, 6.90, 6.00, -3.14, 3.95 },
            /* 2010 */ new double[] { 0.00, 2.90, 4.86, 1.43, 3.43, -1.81, 3.52, 7.81, 4.57, -2.71, 8.76, 10.95, 9.10, 10.19, 9.43, 14.95, 15.52, 8.86, 12.19, 16.90, 18.76, 19.76, 19.95, 15.19, 21.76, 25.95, 26.33, 25.90, 22.57, 23.14, 21.05, 20.76, 21.90, 21.81, 17.95, 17.14, 15.14, 13.90, 12.71, 15.90, 9.38, 7.95, 8.67, 10.33, 9.76, 6.14, 2.33, 0.81, 5.76, -0.52, 2.90, 4.29 },
            /* 2011 */ new double[] { 4.90, 6.29, 1.05, 0.00, 1.71, 5.14, 5.19, 7.05, 4.48, 7.19, 8.62, 7.90, 11.95, 15.24, 12.33, 13.90, 14.48, 16.52, 18.10, 18.52, 19.86, 16.52, 17.05, 20.81, 21.95, 21.67, 22.90, 21.29, 17.57, 19.67, 22.81, 20.24, 23.81, 20.67, 20.29, 19.10, 18.76, 15.00, 16.90, 14.29, 14.00, 9.90, 12.24, 12.81, 11.38, 10.95, 7.57, 7.90, 7.62, 5.57, 5.57, 5.76 },
            /* 2012 */ new double[] { 6.71, 2.43, 5.52, 3.52, -2.57, -7.52, 0.76, 3.62, 7.33, 3.90, 9.14, 7.76, 12.00, 10.24, 9.24, 8.86, 13.10, 12.95, 17.62, 14.14, 17.38, 21.19, 19.57, 19.33, 22.43, 23.24, 21.24, 20.71, 21.48, 21.67, 24.00, 23.43, 23.29, 24.38, 17.52, 18.38, 16.38, 17.00, 12.67, 14.43, 14.33, 15.05, 11.33, 8.43, 8.29, 9.33, 9.43, 3.48, 3.43, 4.38, 7.48, 8.05 },
            /* 2013 */ new double[] { 5.00, 3.86, 2.10, 2.24, 6.38, 3.43, 3.67, 2.38, 0.62, 9.24, 3.71, 6.95, 9.10, 6.57, 11.10, 12.86, 10.76, 12.52, 15.62, 13.00, 11.90, 12.62, 17.19, 21.52, 19.10, 18.10, 23.00, 25.90, 24.62, 26.67, 24.43, 22.24, 22.33, 20.95, 17.86, 19.05, 14.76, 15.29, 17.81, 16.62, 10.48, 15.29, 16.81, 10.48, 12.38, 6.38, 3.14, -0.38, 3.19, 5.71, 6.43, 5.62 },
            /* 2014 */ new double[] { 6.90, 8.29, 5.95, 5.33, 3.71, 5.19, 6.71, 6.00, 5.33, 6.00, 8.76, 9.24, 8.67, 11.38, 13.67, 12.90, 12.62, 12.57, 16.76, 14.19, 16.95, 15.33, 19.38, 23.48, 21.90, 21.95, 21.67, 18.10, 23.57, 22.57, 21.00, 23.00, 19.00, 17.76, 20.71, 19.33, 18.76, 19.10, 14.43, 15.19, 16.52, 15.52, 13.71, 12.86, 8.67, 9.29, 11.14, 12.71, 3.81, 6.52, 7.67, 5.14 },
            /* 2015 */ new double[] { 2.00, 6.14, 4.52, 0.62, 3.43, -2.86, 2.90, 2.86, 5.62, 6.62, 8.10, 8.33, 7.86, 9.62, 8.86, 12.62, 13.95, 14.33, 16.48, 17.19, 14.38, 18.00, 23.67, 21.05, 19.05, 24.05, 26.05, 24.62, 25.62, 25.52, 20.81, 23.00, 20.52, 19.14, 20.33, 17.86, 16.38, 15.52, 12.33, 11.48, 12.52, 8.62, 11.38, 11.62, 12.33, 10.05, 9.10, 3.95, 6.10, 5.67, 6.52, 8.05 },
            /* 2016 */ new double[] { 7.24, 3.62, 4.52, 7.19, 6.95, 7.95, 2.24, 6.86, 4.86, 3.05, 5.19, 7.14, 9.62, 9.24, 11.62, 11.71, 9.76, 13.95, 14.86, 15.43, 14.38, 17.71, 20.05, 16.29, 22.00, 22.57, 25.81, 19.81, 21.90, 23.43, 21.90, 20.33, 21.86, 21.33, 21.10, 19.90, 16.52, 14.81, 14.90, 11.90, 10.62, 9.71, 12.29, 9.81, 6.62, 7.00, 9.48, 5.05, 6.33, 5.19, 4.71, 4.00 },
            /* 2017 */ new double[] { 0.14, 3.48, -0.86, 1.67, 6.43, 4.95, 7.48, 6.67, 7.33, 9.62, 9.52, 8.33, 9.81, 10.29, 12.76, 10.62, 10.29, 12.90, 16.05, 16.67, 20.76, 20.52, 20.86, 26.05, 26.24, 18.90, 23.86, 23.29, 23.62, 22.62, 25.76, 18.67, 21.71, 22.62, 19.71, 16.71, 13.38, 13.62, 14.52, 14.00, 14.86, 14.76, 12.14, 10.19, 7.10, 4.33, 8.19, 1.81, 2.86, 4.76, 4.24, 5.62 },
            /* 2018 */ new double[] { 11.00, 4.52, 9.05, 6.86, 3.95, 1.52, 5.76, 2.57, 3.95, 7.52, 7.05, 3.76, 8.05, 11.29, 10.33, 14.29, 15.71, 12.38, 15.10, 14.67, 19.19, 19.33, 20.24, 17.67, 22.33, 24.00, 24.29, 25.71, 22.67, 26.38, 27.90, 24.38, 21.57, 21.67, 19.57, 19.33, 19.10, 19.81, 16.24, 13.14, 16.38, 13.48, 9.71, 7.86, 10.24, 11.48, 5.67, 7.14, 10.71, 5.52, 7.24, 5.14 },
            /* 2019 */ new double[] { 1.33, 2.48, 4.14, 3.52, 4.76, 6.24, 5.05, 8.05, 10.00, 9.24, 8.00, 7.86, 7.67, 7.71, 9.05, 11.76, 12.52, 11.76, 13.52, 13.29, 15.86, 17.67, 18.57, 16.29, 21.19, 26.52, 25.57, 23.67, 23.95, 24.67, 23.19, 25.19, 20.95, 20.00, 22.62, 15.71, 16.05, 19.19, 17.90, 15.33, 14.95, 14.00, 12.19, 13.29, 7.76, 5.81, 6.95, 9.00, 6.62, 7.33, 10.10, 7.52 },
            /* 2020 */ new double[] { 6.43, 6.90, 6.05, 7.43, 10.05, 9.00, 10.24, 9.86, 9.52, 7.67, 10.43, 12.00, 8.71, 9.14, 13.76, 13.95, 15.43, 15.95, 18.76, 14.86, 19.19, 19.76, 18.14, 17.24, 18.86, 23.81, 21.95, 23.81, 23.33, 25.81, 26.19, 25.90, 26.00, 25.10, 22.14, 20.71, 21.90, 24.24, 16.43, 13.29, 14.67, 10.48, 15.19, 13.90, 12.62, 13.43, 9.62, 9.81, 5.81, 6.33, 8.90, 6.52 },
            /* 2021 */ new double[] { 1.14, 4.95, 6.05, 8.38, 10.71, 6.86, 10.24, 10.52, 9.95, 9.19, 6.86, 10.95, 12.67, 9.43, 8.76, 12.62, 12.95, 15.62, 12.90, 14.10, 17.90, 18.90, 23.19, 25.43, 21.38, 22.00, 23.57, 20.90, 27.81, 23.43, 20.57, 27.71, 23.43, 23.95, 24.43, 23.00, 21.29, 18.71, 17.52, 13.71, 13.05, 14.67, 14.33, 9.29, 10.33, 8.90, 6.43, 5.19, 6.29, 7.38, 7.00, 12.33 },
            /* 2022 */ new double[] { 6.71, 4.90, 4.14, 5.81, 7.67, 7.71, 9.67, 9.62, 8.76, 9.24, 11.19, 10.90, 8.00, 10.10, 14.71, 11.48, 16.00, 15.29, 20.86, 24.52, 17.81, 23.14, 23.05, 29.86, 25.29, 22.48, 27.38, 32.05, 28.48, 25.76, 29.81, 29.62, 24.19, 26.43, 24.71, 23.29, 21.43, 18.29, 15.14, 18.33, 17.90, 18.90, 17.95, 14.14, 13.86, 11.67, 8.81, 6.43, 6.38, 6.00, 12.05, 10.19 },
            /* 2023 */ new double[] { 9.48, 8.19, 2.43, 0.67, 3.24, 3.67, 8.71, 6.43, 1.14, 9.76, 10.48, 11.48, 10.33, 9.14, 10.14, 12.81, 15.57, 16.33, 11.86, 11.57, 17.10, 19.48, 20.67, 20.29, 21.14, 20.00, 21.67, 23.52, 23.43, 20.86, 19.57, 24.00, 27.10, 25.95, 18.81, 24.19, 20.90, 16.95, 20.95, 19.24, 18.71, 14.81, 13.19, 9.76, 8.71, 11.10, 4.95, 4.57, 5.62, 6.67, 5.48, 5.95 },
            /* 2024 */ new double[] { 5.19, -0.19, 4.81, 8.19, 8.38, 7.81, 8.10, 6.76, 5.62, 6.19, 10.24, 12.76, 9.29, 13.38, 13.48, 9.19, 8.57, 11.10, 14.38, 13.71, 13.76, 13.19, 19.19, 16.14, 19.05, 21.81, 16.76, 20.95, 22.14, 22.29, 25.00, 25.33, 20.62, 21.33, 21.57, 18.24, 13.48, 16.10, 15.00, 13.62, 15.52, 14.67, 12.05, 14.86, 13.71, 8.24, 7.14, 9.33, 6.05, 3.48, 5.71, 4.10 },
            /* 2025 */ new double[] { 5.67, 7.05, 0.76, 7.33, 4.81, 3.81, 7.43, 8.33, 7.29, 11.24, 8.48, 10.71, 10.38, 13.90, 15.00, 12.10, 13.86, 16.78, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00 },
        };
        
        if (parcelle.Date == null) return;

        int annee = parcelle.Date.Annee;
        int semaine = parcelle.Date.Semaine;

        double temperature = Temperatures[annee - 2009][semaine - 1];

        int tempref = parcelle.Contenu.TemperaturePreferee;

        if (Math.Abs(temperature - tempref) < 0.5)
        {
            parcelle.Contenu.VitesseCroissance += 2;
        }
        else if (temperature < tempref - 15 || temperature > tempref + 15)
        {
            parcelle.Contenu.Sante -= 15;
        }
    }


}

