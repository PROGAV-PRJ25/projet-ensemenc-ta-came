using System.Security.Principal;

public class Interface
{
    private char[,] TexteComplet = new char[Console.WindowHeight, Console.WindowWidth]; //tableau rassemblant tout ce qu'il y a à afficher

    private void AjouterInterface(char[,] texte, int positionLigne, int positionColonne, string direction)
    {
        if (direction == "haut-droite") 
        {
            for (int indiceLigne = 0; indiceLigne < texte.GetLength(0); indiceLigne++)
            {
                for (int indiceColonne = 0; indiceColonne < texte.GetLength(0); indiceColonne++) {}
            }
        }

        else if (direction == "haut-gauche")
        {
            for (int indiceLigne = 0; indiceLigne < texte.GetLength(0); indiceLigne++)
            {
                for (int indiceColonne = 0; indiceColonne < texte.GetLength(0); indiceColonne++) {}
            }
        }

        else if (direction == "bas-droite")
        {
            for (int indiceLigne = 0; indiceLigne < texte.GetLength(0); indiceLigne++)
            {
                for (int indiceColonne = 0; indiceColonne < texte.GetLength(0); indiceColonne++) {}
            }
        }
        else if (direction == "bas-gauche")
        {
            for (int indiceLigne = 0; indiceLigne < texte.GetLength(0); indiceLigne++)
            {
                for (int indiceColonne = 0; indiceColonne < texte.GetLength(0); indiceColonne++){}
            }
        }
        else if (direction == "droite-haut")
        {
            for (int indiceLigne = 0; indiceLigne < texte.GetLength(0); indiceLigne++)
            {
                for (int indiceColonne = 0; indiceColonne < texte.GetLength(0); indiceColonne++){}
            }
        }
        else if (direction == "droite-bas")
        {
            for (int indiceLigne = 0; indiceLigne < texte.GetLength(0); indiceLigne++)
            {
                for (int indiceColonne = 0; indiceColonne < texte.GetLength(0); indiceColonne++) {}
            }
        }

        else if (direction == "gauche-haut")
        {
            for (int indiceLigne = 0; indiceLigne < texte.GetLength(0); indiceLigne++)
            {
                for (int indiceColonne = 0; indiceColonne < texte.GetLength(0); indiceColonne++) {}
            }
        }
        else if (direction == "gauche-bas")
        {
            for (int indiceColonne = 0; indiceColonne < texte.GetLength(0); indiceColonne++)
            {
                for (int indiceLigne = 0; indiceLigne < texte.GetLength(0); indiceLigne++) {}
            }
        }

    }

    public string RécupererASCII(string nomFichierTxt)
    {
        //TODO 
        //récupérer le nom de fichier ascii et le transformer en tableau à double entrée.
        return "";
    }
}

