public abstract class Meteo 
{
    public string NOM {set;get;}
    public string EMOJI {set;get;}
    public abstract void Action(Parcelle parcelle);
    protected Meteo(string nom, string emoji)
    {
        NOM = nom;
        EMOJI = emoji;

    }
}
public class Pluie : Meteo {
    public Pluie(string nom, string emoji) : base(nom, emoji) {
        NOM = "Pluie"
        EMOJI = "🌧️";
    }
    public override void Action(Parcelle parcelle) 
    {
        //dépend du besoin eau de la plante mais rajoute +10 à quantitéeau
        //si dépasse besoin eau de 30, santé -20
        //sinon si besoin eau atteint (à +30% pret), santé +10
    }
}

public class Soleil : Meteo {
    public Soleil(string nom, string emoji) : base(nom, emoji) {
        NOM = "Soleil";
        EMOJI = "🌞";
    }
    public override void Action(Parcelle parcelle) 
    {
        //dépend du besoinsoleil de la plante, rajoute +15 à quantité soleil
        //si dépasse besoin soleil de 5 et quantitéeau qui est en dessous de besoin eau,  ce mois ci, santé-25
        //sinon, si besoinsoleil atteint (à +_ 5%), santé +5
    }
}

public class VentAutan : Meteo {
    public VentAutan(string nom, string emoji) : base(nom, emoji) {
        NOM = "Vent d'Autan";
        EMOJI = "🌬️";
    }
    public override void Action(Parcelle parcelle) 
    {
        //vent sec et chaud : réduit la quantité d'eau de -15%
        //si réduit trop quantitéeau en dessous de besoineau, santé-25
        //accelere la vitessecroissance de +2 (en mois) donc récolte plus tot

    }
}

public class Gel : Meteo {
    public Gel(string nom, string emoji) : base(nom, emoji) {
        NOM = "Gel";
        EMOJI = "🥶";
    }
    public override void Action(Parcelle parcelle) 
    {
        //si gel et crainfroid true, santé =-20
        //si fumier ou tente, protège du gel et ne fait rien
    }
}

public class Temperature : Meteo {
    public Temperature(string nom, string emoji) : base(nom, emoji) {
        NOM = "Temperature";
        EMOJI = "🌡️";
    }
    public override void Action(Parcelle parcelle) 
    {
        //si temperature =temppref , vitessecroissance =+2 en mois
        //si temperature< ou > à temp pref ne fait rien
        //si temperature <15° ou >30° à temppref , santé =-15
    }
}

