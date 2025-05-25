public abstract class ObjetJeu
{
    protected static Random rng = new Random();
    public string Nom { get; protected set; }
    public string Emoji { get; protected set; }
    public int decallageAffichage { get; protected set; } //si l'emoji est 1 permet d'ajuster l'affichage (pas implémenté totalement)
    public ObjetJeu(string nom, string emoji, int decallageAffichage = 0)
    {
        Nom = nom;
        Emoji = emoji;
    }
    public override string ToString()
    {
        return Emoji + " " + Nom;
    }
}
public abstract class ObjetJeuAchatVente : ObjetJeu
{
    
    public int PrixAchat { get; protected set; }
    public int PrixVente { get; protected set; }

    public ObjetJeuAchatVente(string nom, string emoji, int decallageAffichage, int prixAchat = 0, int prixVente = 0) : base(nom,emoji,decallageAffichage)
    {
        //Decallage affichage est sensé prendre en compte la taille que prend un emoji (1 ou 2)
        PrixAchat = prixAchat;
        PrixVente = prixVente;
    }
}