using System.Security.Principal;

public class Interface
{
    private string[,] TexteComplet = new string[Console.WindowHeight, Console.WindowWidth]; //tableau rassemblant tout ce qu'il y a à afficher
    

    public string RécupererASCII(string nomFichierTxt)
    {
        //TODO 
        //récupérer le nom de fichier ascii et le transformer en tableau à double entrée.
        string contenu = File.ReadAllText($"{nomFichierTxt}.txt");
        
        return contenu;
    }
}

