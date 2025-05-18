

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
        if (Items.Count() != 0)
        {
            MenuReference.NoeudActif = this;
            MenuReference.Curseur = 0;
            MenuReference.Afficher();
        }
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
    public int Prix { set; get; }
    public ElementMenuMagasin(ZoneMenu menuReference, string titre, SessionJeu session, int prix) : base(menuReference, titre, session)
    {
        Prix = prix;
    }
}
public class ElementMenuMagasinOutil : ElementMenuMagasin
{
    public Outil Contenu { set; get; }
    public ElementMenuMagasinOutil(ZoneMenu menuReference, string titre, SessionJeu session, Outil outil, int prix) : base(menuReference, titre, session, prix)
    {
        Contenu = outil;
    }
    public override void Actionner()
    {
        Session.Acheter(Contenu, Prix);
    }
}
public class ElementMenuMagasinSemis : ElementMenuMagasin
{
    public Plante Contenu { set; get; }
    public ElementMenuMagasinSemis(ZoneMenu menuReference, string titre, SessionJeu session, Plante plante, int prix) : base(menuReference, titre, session, prix)
    {
        Contenu = plante;
    }
    public override void Actionner()
    {
        Session.Acheter(Contenu, Prix);
    }
}

public class ElementMenuAchatSemis : ElementMenuMagasin
{
    Plante Semis { set; get; }
    public ElementMenuAchatSemis(ZoneMenu menuReference, string titre, SessionJeu session, int prix, Plante semis) : base(menuReference, titre, session, prix)
    {
        Prix = prix;
        Semis = semis;
    }


}
public class ElementMenuInventaireOutil : ElementMenuFonctionnel
{
    Outil Outil { set; get; }
    ElementMenuInventaireOutil(ZoneMenu menuReference, string titre, SessionJeu session, Outil outil) : base(menuReference, titre, session)
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

public class ElementMenuAjoutSemis : ElementMenuFonctionnel
{
    Plante Semis { set; get; }
    public ElementMenuAjoutSemis(ZoneMenu menuReference, string description, SessionJeu session, Plante semis) : base(menuReference, description, session)
    {
        Semis = semis;
    }
    public override void Actionner()
    {
        Session.PlanterSemis(Semis);
    }
}