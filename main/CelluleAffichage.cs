public class CelluleAffichage
{
    public char Contenu { get; set; }
    public ConsoleColor CouleurTexte { get; set; }
    public ConsoleColor CouleurFond { get; set; }
    public CelluleAffichage(char contenu = ' ', ConsoleColor couleurTexte = ConsoleColor.White, ConsoleColor couleurFond = ConsoleColor.Black)
    {
        Contenu = contenu;
        CouleurTexte = couleurTexte;
        CouleurFond = couleurFond;
    }
    public void Actualiser(char contenu = ' ', ConsoleColor couleurTexte = ConsoleColor.White, ConsoleColor couleurFond = ConsoleColor.Black)
    {
        Contenu = contenu;
        CouleurTexte = couleurTexte;
        CouleurFond = couleurFond;
    }
    public void Actualiser(string contenu, ConsoleColor couleurTexte = ConsoleColor.White, ConsoleColor couleurFond = ConsoleColor.Black)
    {
        Actualiser(Convert.ToChar(contenu), couleurTexte, couleurFond);
    }




}