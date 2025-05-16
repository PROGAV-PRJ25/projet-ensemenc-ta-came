

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
public class ElementMenuNoeud : ElementMenu
{
    public ElementMenuNoeud(ZoneMenu menuReference, string titre = "(vide)", string description = "(vide)") : base(menuReference, titre, description) { }
    public override void Actionner()
    {
        MenuReference.NoeudActif = this;
        MenuReference.Afficher();
    }
}

public class ElementMenuMagasin : ElementMenu
{
    ElementMenuMagasin(ZoneMenu menuReference, string description) : base(menuReference, description)
    {
    }
    public override void Actionner()
    {
        //Article.Acheter()
    }
}
public class ElementMenuInventaire : ElementMenu
{
    Outil Item { set; get; }
    ElementMenuInventaire(ZoneMenu menuReference, string titre, Outil outil) : base(menuReference, titre)
    {
        Item = outil;
    }
    public override void Actionner()
    {

    }
}
public class ElementMenuJournal : ElementMenu
{
    ElementMenuJournal(ZoneMenu menuReference, string description) : base(menuReference, description)
    {
        //Presenter information
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
public class ElementMenuSuivant : ElementMenuFonctionnel
{
    public ElementMenuSuivant(ZoneMenu menuReference, string description, SessionJeu session) : base(menuReference, description, session) { }
    public override void Actionner()
    {
        Session.PasserSemaineSuivante();
    }
}

public class ElementMenuAjoutSemis : ElementMenuFonctionnel
{
    public ElementMenuAjoutSemis(ZoneMenu menuReference, string description, SessionJeu session, Plante semis) : base(menuReference, description, session) { }
    public override void Actionner()
    {
        Session.PlanterSemis();
    }
}