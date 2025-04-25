

public class Partie
{
  public ZoneAffichage EcranDeJeu = new ZoneAffichage();
  public Partie() { }


  public void Demarrer()
  {
    Console.ResetColor();
    EcranDeJeu.AfficherGrille();
    Console.SetCursorPosition(1, 1);
    while (Console.ReadLine() != "hello")
    {
    }


  }
  public void DemanderDebutPartie() { }
  public void Naviguer() { }
  public void DemanderFinPartie() { }




};