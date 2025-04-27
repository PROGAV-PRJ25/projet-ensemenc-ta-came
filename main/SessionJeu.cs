

using System.Reflection.Metadata.Ecma335;

public class SessionJeu
{
  public ZoneAffichage EcranDeJeu = new ZoneAffichage();
  public ConsoleKeyInfo PressionTouche = new ConsoleKeyInfo();
  public SessionJeu() {

  }
  


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
  public void Naviguer() {
    PressionTouche = Console.ReadKey();

    if(PressionTouche.Key == ConsoleKey.C){
      //TODO : Presenter le tutoriel
    }
    else if(PressionTouche.Key == ConsoleKey.I){
      //TODO : Accéder à l'inventaire (possiblement avec la dernière position du curseur)
    }
    else if(PressionTouche.Key == ConsoleKey.J){
      //TODO : Accéder au Journal (possiblement avec la dernière position du curseur)
    }
    else if(PressionTouche.Key == ConsoleKey.M){
      //TODO : 
    }
    else if(PressionTouche.Key == ConsoleKey.S){
      //TODO demander à passer à la semaine suivante
    }
  }
  public bool VerificationFinPartie()
  {
    return true;
  }
  
};