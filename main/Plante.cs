public abstract class Plante{

    string[] etats = ["semis","mature","déshydraté","gelé", "malade", "mort"];
    public string Nom {get;private set;}
    public Plante(string nom){
        Nom=nom;
    }
    
}