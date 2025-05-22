

public class ElementMenu
{
    public string Description { get; set; }
    public ElementMenu Parent { get; set; }
    public List<ElementMenu> Items { get; set; }
    public ZoneMenu MenuReference { get; set; }
    public string Titre { get; set; }


    public ElementMenu(ZoneMenu menuReference, string titre = "(vide)", string description = "(vide)")
    {
        MenuReference = menuReference;
        Titre = titre;
        Description = description;
        Items = [];
        Parent = this;
    }
    public ElementMenu(ZoneMenu menuReference, string titre, string description, ElementMenu parent) : this(menuReference, titre, description)
    {
        Parent = parent;
    }

    public override string ToString()
    {
        return " - " + Titre;
    }

    public virtual void Actionner()
    {
        MenuReference.NoeudActif = this;
        MenuReference.Curseur = 0;
        MenuReference.Afficher();
    }
    public virtual void RevenirAuParent()
    {
        MenuReference.NoeudActif = Parent;
        MenuReference.Afficher();


    }
    public void AjouterItem(ElementMenu item)
    {
        Items.Add(item);
        item.MenuReference = MenuReference;
        item.Parent = this;
    }
}

public class ElementMenuNouvellePartie : ElementMenu
{
    public SessionJeu Session { get; set; }
    string Ville { get; set; }

    public ElementMenuNouvellePartie(ZoneMenu menuReference, string titre, SessionJeu session) : base(menuReference, titre)
    {
        Session = session;
        Ville = titre.Split(" ")[0];
    }

    public override void Actionner()
    {
        Session.DemarrerNouvellePartie(Ville);
    }
}

public class ElementMenuFonctionnel : ElementMenu
{
    public SessionJeu Session { get; set; }
    public ElementMenuFonctionnel(ZoneMenu menuReference, string description, SessionJeu session) : base(menuReference, description)
    {
        Session = session;
    }
}
public abstract class ElementMenuMagasin : ElementMenuFonctionnel
{
    public ElementMenuMagasin(ZoneMenu menuReference, string titre, SessionJeu session, ObjetJeu contenu) : base(menuReference, titre, session)
    { }
}
public class ElementMenuMagasinAchatOutil : ElementMenuMagasin
{
    public Outil Contenu { get; set; }
    public ElementMenuMagasinAchatOutil(ZoneMenu menuReference, SessionJeu session, Outil outil) : base(menuReference, $"{outil.Emoji} {outil.Nom} - {outil.PrixAchat} ", session, outil)
    {
        Contenu = outil;
    }
    public override void Actionner()
    {
        Session.Acheter(Contenu);
    }
}
public class ElementMenuMagasinAchatSemis : ElementMenuMagasin
{
    public Plante Contenu { get; set; }
    public ElementMenuMagasinAchatSemis(ZoneMenu menuReference, SessionJeu session, Plante plante) : base(menuReference, $"{plante.Emoji} {plante.Nom} - {plante.PrixAchat} ", session, plante)
    {
        Contenu = plante;
    }
    public override void Actionner()
    {
        Session.Acheter(Contenu);
    }
}
public class ElementMenuMagasinVenteRecolte : ElementMenuMagasin
{
    public Recolte Contenu { get; set; }
    public ElementMenuMagasinVenteRecolte(ZoneMenu menuReference,string titre, SessionJeu session, Recolte recolte) : base(menuReference,titre, session, recolte)
    {
        Contenu = recolte;
    }
    public override void Actionner()
    {
        Session.Vendre(Contenu);
    }
}
public class ElementMenuInventaireSemis : ElementMenuFonctionnel
{
    Plante Semis { get; set; }
    public ElementMenuInventaireSemis(ZoneMenu menuReference, string description, SessionJeu session, Plante semis) : base(menuReference, description, session)
    {
        Semis = semis;
    }
    public override void Actionner()
    {
        Session.PlanterSemis(Semis);
    }
}
public class ElementMenuInventaireOutil : ElementMenuFonctionnel
{
    public Outil Outil { get; set; }
    public ElementMenuInventaireOutil(ZoneMenu menuReference, string titre, SessionJeu session, Outil outil) : base(menuReference, titre, session)
    {
        Outil = outil;
    }
    public override void Actionner()
    {
        Session.UtiliserOutil(Outil);
    }
}
public class ElementMenuInventaireRecolte : ElementMenuFonctionnel
{
    public Recolte Recolte { get; set; }
    public ElementMenuInventaireRecolte(ZoneMenu menuReference, string titre, SessionJeu session, Recolte recolte) : base(menuReference, titre, session)
    {
        Recolte = recolte;
    }
    public override void Actionner()
    {
        // ne fait rien
        //Session.ProposerVenteRecolte();
    }
}
public class ElementMenuJournal : ElementMenu
{
    ElementMenuJournal(ZoneMenu menuReference, string titre) : base(menuReference, titre)
    {
        //Presenter information
    }
}
public class ElementMenuSuivant : ElementMenuFonctionnel
{
    public ElementMenuSuivant(ZoneMenu menuReference, string description, SessionJeu session) : base(menuReference, description, session) { }
    public override void Actionner()
    {
        Session.PasserSemaineSuivante();
    }
}