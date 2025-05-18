

public class ElementMenu
{
    public string Description { set; get; }
    public ElementMenu Parent { set; get; }
    public List<ElementMenu> Items { set; get; }
    public ZoneMenu MenuReference { set; get; }
    public string Titre { set; get; }


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
    public SessionJeu Session { set; get; }
    string Ville { set; get; }

    public ElementMenuNouvellePartie(ZoneMenu menuReference, string titre, SessionJeu session) : base(menuReference, titre)
    {
        Session = session;
        Ville = titre;
    }

    public override void Actionner()
    {
        Session.DemarrerNouvellePartie(Ville);
    }
}

public class ElementMenuFonctionnel : ElementMenu
{
    public SessionJeu Session { set; get; }
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
public class ElementMenuMagasinOutil : ElementMenuMagasin
{
    public Outil Contenu { set; get; }
    public ElementMenuMagasinOutil(ZoneMenu menuReference, SessionJeu session, Outil outil) : base(menuReference, $"{outil.EMOJI} {outil.NOM} - {outil.PRIX_ACHAT} ", session, outil)
    {
        Contenu = outil;
    }
    public override void Actionner()
    {
        Session.Acheter(Contenu);
    }
}
public class ElementMenuMagasinSemis : ElementMenuMagasin
{
    public Plante Contenu { set; get; }
    public ElementMenuMagasinSemis(ZoneMenu menuReference, SessionJeu session, Plante plante) : base(menuReference, $"{plante.EMOJI} {plante.NOM} - {plante.PRIX_ACHAT} ", session, plante)
    {
        Contenu = plante;
    }
    public override void Actionner()
    {
        Session.Acheter(Contenu);
    }
}
public class ElementMenuInventaireSemis : ElementMenuFonctionnel
{
    Plante Semis { set; get; }
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
    public Outil Outil { set; get; }
    public ElementMenuInventaireOutil(ZoneMenu menuReference, string titre, SessionJeu session, Outil outil) : base(menuReference, titre, session)
    {
        Outil = outil;
    }
    public override void Actionner()
    {
        Session.UtiliserOutil(Outil);
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