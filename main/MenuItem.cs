using System.Runtime.InteropServices;

public class MenuItem
{
    public string Description { set; get; }
    public MenuItem Parent { set; get; }
    public List<MenuItem> Items { set; get; }
    public ZoneMenu MenuReference { set; get; }
    public string Titre { set; get; }

    
    public MenuItem(ZoneMenu menuReference,string titre = "(vide)", string description = "(vide)")
    {
        MenuReference = menuReference;
        Titre = titre;
        Description = description;
        Items = [];
        Parent=this;        
    }
    public MenuItem(ZoneMenu menuReference,string titre, string description,MenuItem parent) : this(menuReference,titre, description)
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
    public void AjouterItem(MenuItem item)
    {
        Items.Add(item);
        item.MenuReference = MenuReference;
        item.Parent = this;
    }
}

public class MenuItemNouvellePartie: MenuItem{
    public SessionJeu Session {set;get;}
    string Pays {set;get;}

    public MenuItemNouvellePartie(ZoneMenu menuReference,string titre,SessionJeu session): base(menuReference,titre)
    {
        Session = session;
        Pays = titre.ToLower();
    }
    
    public override void Actionner()
    {
        Session.DemarrerNouvellePartie(Pays);
    }
}
public class MenuItemNoeud : MenuItem
{
    public MenuItemNoeud(ZoneMenu menuReference,string titre = "(vide)", string description = "(vide)") : base(menuReference,titre,description){}
    public override void Actionner()
    {
        MenuReference.NoeudActif = this;
        MenuReference.Afficher();
    }
}

public class MenuItemMagasin : MenuItem
{
    MenuItemMagasin(ZoneMenu menuReference,string description) : base(menuReference,description)
    {
    }
    public override void Actionner()
    {
        //Article.Acheter()
    }
}
public class MenuItemInventaire : MenuItem
{
    MenuItemInventaire(ZoneMenu menuReference,string description) : base(menuReference,description)
    {
        //Objet.Ajouter
    }
}
public class MenuItemJournal : MenuItem
{
    MenuItemJournal(ZoneMenu menuReference,string description) : base(menuReference,description)
    {
        //Presenter information
    }
}