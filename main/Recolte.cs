using System.Security.Cryptography.X509Certificates;

public abstract class Recolte : ObjetJeu
{
    // objet du jeu dont le prix de vente est null
    public Recolte(string nom, string emoji, int prixVente) : base(nom, emoji, 1, 0, prixVente) { }
}
public class RecoltePommier : Recolte
{
    public RecoltePommier() : base("Pomme", "🍎", 2) { }
}
public class RecolteBle : Recolte
{
    public RecolteBle() : base("Epi de blé", "🌾", 2) { }
}
public class RecolteCarotte : Recolte
{
    public RecolteCarotte() : base("Carotte", "🥕", 2) { }
}
public class RecoltePecher : Recolte
{
    public RecoltePecher() : base("Pêche", "🍑", 5) { }
}
public class RecolteVigne : Recolte
{
    public RecolteVigne() : base("Grappe de raisin", "🍇", 4) { }
}
public class RecolteCitronnier : Recolte
{
    public RecolteCitronnier() : base("Citron", "🍋", 5) { }
}
public class RecoltePomme : Recolte
{
    public RecoltePomme() : base("Pomme", "🌻", 2) { }
}
public class RecolteMais : Recolte
{
    public RecolteMais() : base("Epi de mais", "🌽", 3) { }
}
public class RecolteHaricot : Recolte
{
    public RecolteHaricot() : base("Haricot", "🫘", 2) { }
}
public class RecolteTomate : Recolte
{
    public RecolteTomate() : base("Tomate", "🍅", 3) { }
}
public class RecolteAvocat : Recolte
{
    public RecolteAvocat() : base("Avocat", "🥑", 10) { }
}
public class RecolteCafe : Recolte
{
    public RecolteCafe() : base("Grain de café", "☕️", 8) { }
}
public class RecolteCacaoyer : Recolte
{
    public RecolteCacaoyer() : base("Fève de cacao", "🍫", 10) { }
}
public class RecolteTabac : Recolte
{
    public RecolteTabac() : base("Feuille de tabac", "🚬", 5) { }
}
public class RecoltePiment : Recolte
{
    public RecoltePiment() : base("Pomme", "🌶️", 4) { }
}
public class RecolteRiz : Recolte
{
    public RecolteRiz() : base("grain de riz", "🍚", 1) { }
}
public class RecoltePatateDouce : Recolte
{
    public RecoltePatateDouce() : base("Patate douce", "🍠", 2) { }
}
public class RecolteThe : Recolte
{
    public RecolteThe() : base("Feuille de thé", "🍵", 3) { }
}
public class RecolteConcombre : Recolte
{
    public RecolteConcombre() : base("Concombre", "🥒", 3) { }
}
public class RecolteBrocoli : Recolte
{
    public RecolteBrocoli() : base("Brocoli", "🥦", 3) { }
}



