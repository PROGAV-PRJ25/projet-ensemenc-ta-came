using System.Security.Principal;

public class Interface
{
    private string[,] TexteComplet = new string[Console.WindowHeight, Console.WindowWidth]; //tableau rassemblant tout ce qu'il y a à afficher

    private void AjouterInterface(string[,] texte, int positionLigne, int positionColonne, string direction)
    {
        string ligne = "";
        if (direction == "haut-droite")
        {

            for (int indiceLigne = 0; indiceLigne < texte.GetLength(0); indiceLigne++)
            {
                for (int indiceColonne = 0; indiceColonne < texte.GetLength(0); indiceColonne++)
                {
                    ligne += texte[indiceLigne, indiceColonne];
                }
            }
        }

        else if (direction == "haut-gauche")
        {

            for (int indiceLigne = 0; indiceLigne < texte.GetLength(0); indiceLigne++)
            {
                for (int indiceColonne = 0; indiceColonne < texte.GetLength(0); indiceColonne++)
                {
                    ligne += texte[indiceLigne, indiceColonne];
                }
            }
        }

        else if (direction == "bas-droite")
        {
            for (int indiceLigne = 0; indiceLigne < texte.GetLength(0); indiceLigne++)
            {
                for (int indiceColonne = 0; indiceColonne < texte.GetLength(0); indiceColonne++)
                {
                    ligne += texte[indiceLigne, indiceColonne];
                }
            }
        }
        else if (direction == "bas-gauche")
        {

            for (int indiceLigne = 0; indiceLigne < texte.GetLength(0); indiceLigne++)
            {
                for (int indiceColonne = 0; indiceColonne < texte.GetLength(0); indiceColonne++)
                {
                    ligne += texte[indiceLigne, indiceColonne];
                }
            }
        }
        else if (direction == "droite-haut")
        {

            for (int indiceLigne = 0; indiceLigne < texte.GetLength(0); indiceLigne++)
            {
                for (int indiceColonne = 0; indiceColonne < texte.GetLength(0); indiceColonne++)
                {
                    ligne += texte[indiceLigne, indiceColonne];
                }
            }
        }
        else if (direction == "droite-bas")
        {

            for (int indiceLigne = 0; indiceLigne < texte.GetLength(0); indiceLigne++)
            {
                for (int indiceColonne = 0; indiceColonne < texte.GetLength(0); indiceColonne++)
                {
                    ligne += texte[indiceLigne, indiceColonne];
                }
            }
        }

        else if (direction == "gauche-haut")
        {
            for (int indiceLigne = 0; indiceLigne < texte.GetLength(0); indiceLigne++)
            {
                for (int indiceColonne = 0; indiceColonne < texte.GetLength(0); indiceColonne++)
                {
                    ligne += texte[indiceLigne, indiceColonne];
                }
            }
        }
        else if (direction == "gauche-bas")
        {
            for (int indiceColonne = 0; indiceColonne < texte.GetLength(0); indiceColonne++)
            {
                for (int indiceLigne = 0; indiceLigne < texte.GetLength(0); indiceLigne++)
                {
                    ligne += texte[indiceLigne, indiceColonne];
                }
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

