public abstract class Terrain(){
    public Plante[][]? parcelles;
    public void Ajouter(Plante semis,int[] coordonnéees)
    {
        //TODO
        //ajouter au parcelles la plante
    }
    public void Retirer(Plante plante){
        //retirer une plante
    }
}
public class Parcelle : CelluleAffichage
{
    public Parcelle() : base(){

    }
}
