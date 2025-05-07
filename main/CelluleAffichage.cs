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
    public void Appliquer(char contenu = ' ', ConsoleColor couleurTexte = ConsoleColor.White, ConsoleColor couleurFond = ConsoleColor.Black)
    {
        Contenu = contenu;
        CouleurTexte = couleurTexte;
        CouleurFond = couleurFond;
    }
    public void Appliquer(string contenu, ConsoleColor couleurTexte = ConsoleColor.White, ConsoleColor couleurFond = ConsoleColor.Black)
    {
        Appliquer(Convert.ToChar(contenu), couleurTexte, couleurFond);
    }
    public void Appliquer(CelluleAffichage cellule) 
    {
        Appliquer(cellule.Contenu,cellule.CouleurTexte,cellule.CouleurFond);
    }




}