// début de partie : détermination de la taille de la fenêtre avec une petite animation
bool choixFait = false;
int x = -1; int y = -1;
while (!choixFait)
{
    if (x != Console.WindowWidth || y != Console.WindowHeight)
    {
        x = Console.WindowWidth; y = Console.WindowHeight;
        Console.Clear();

        if (Console.WindowWidth < 30)
            Console.Write("Elargissez la fenêtre avant de commencer");

        else if (Console.WindowHeight < 30)
            Console.Write("Aggrandissez la hauteur de la fenêtre avant de commencer");

        else
        {
            int compteur = 5;
            while (x != Console.WindowWidth || y != Console.WindowHeight || !choixFait)
            {
                x = Console.WindowWidth; y = Console.WindowHeight;
                Console.Clear();
                Console.WriteLine("Taille suffisante, ne rétrécissez plus !\n🚨 ne bougez pas la taille de la fenêtre non plus lorsque le jeu sera lancé  !🚨");
                Console.WriteLine($"Début dans {compteur}");
                System.Threading.Thread.Sleep(1000);
                compteur -= 1;
                if (compteur == 0)
                {
                    choixFait = true;
                }
            }
        }
    }
} while (Console.WindowHeight < 70 && Console.WindowWidth < 80) ;
Console.CursorVisible = false;


SessionJeu Partie = new SessionJeu();
Partie.Demarrer();
Console.Clear();









