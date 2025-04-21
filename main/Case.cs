public class Case{
    public char Contenu {get;set;}
    public ConsoleColor CouleurTexte {get;set;}
    public ConsoleColor CouleurFond {get;set;}
    public Case(char contenu = ' ', ConsoleColor couleurTexte = ConsoleColor.White, ConsoleColor couleurFond = ConsoleColor.Black){
        Contenu = contenu;
        CouleurTexte = couleurTexte;
        CouleurFond = couleurFond;
    }

    
    
}