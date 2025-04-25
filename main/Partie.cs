

public class Partie
{
  public ZoneAffichage EcranDeJeu = new ZoneAffichage();
  public Partie() { }


  public void Demarrer()
  {
    Console.ResetColor();
    //Console.CursorVisible = false;

    EcranDeJeu.Afficher();
    ConsoleKeyInfo AppuiTouche = new ConsoleKeyInfo();
    do
    {
      AppuiTouche = Console.ReadKey();

    } while (AppuiTouche.Key != ConsoleKey.A);

  }
  public void DemanderDebutPartie() { }
  public void Naviguer() { }
  public void DemanderFinPartie() { }

};