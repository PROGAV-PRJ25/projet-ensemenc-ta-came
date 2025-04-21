// TESTTTT
// coucou thimeo
// lol
// coucou camille :)


// TODO
// Lancer la partie 
//  - soit continuer celle en cours
//  - soit commencer une nouvelle de 0
// Initialiser une partie 

// on crée l'herbier en récupérant les données du csv
// string[] LIGNES_CSV = File.ReadAllLines("Plantes.csv");
// string[] PARAMETRES = LIGNES_CSV[0].Split(',');
// string[][] HERBIER = new string[LIGNES_CSV.Length][];
// for (int i = 1; i < LIGNES_CSV.Length; i++)
//         {
//             HERBIER[i] = LIGNES_CSV[i].Split(',');
//         }
// Console.SetWindowSize(40, 40);

int[] curseur = new int[] {0,0};

Console.Clear();
Interface Ecran = new Interface();
Ecran.AfficherGrille();
void LancerPartie()
{
    while(Console.ReadKey().Key != ConsoleKey.A){
        string contenu = File.ReadAllText("AffichageAccueil.txt");
        

    }
    
    
}


// LancerPartie();

