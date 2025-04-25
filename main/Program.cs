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

using System.Net.Security;

int[] curseur = new int[] { 0, 0 };

Console.Clear();
ZoneAffichage Ecran = new ZoneAffichage();

Partie Jeu = new Partie();
Jeu.Demarrer();

Console.BackgroundColor = ConsoleColor.DarkYellow;
Console.WriteLine(" ");
Console.WriteLine(" ");
Console.WriteLine("🥬");
Console.WriteLine(" ");
Console.WriteLine(" ");


