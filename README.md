# ENSemenCe - Carcassonne
> *Salutations jardinier.e ! Atteri.e depuis peu dans la forteresse de Carcassonne, tu as été placé.e à la gestion du potager de la forteresse. Cultive, entretiens et aggrandis ton potager tout en prenant garde aux différents s'obstacles qui pourront s'opposer à toi !*
Ce travail concerne le projet de programmation orientée objet en c#, unité d'enseignement de première année à l'ENSC.
Le projet consiste en un jeu de gestion de potager intégrant différentes temporalités.
Réalisé par **TONON Thiméo** et **VIGNES Camille**
## Affichage
Le jeu se présente sous la forme d'une application console.
Ce dernier est programmé de manière à permettre une taille de fenêtre flexible. 
L'affichage s'ajuste donc dynamiquement selon la taille de la fenêtre en début de partie.
La fenêtre comporte une taille d'affichage minimum, si cette dernière est trop réduite, l'écran indique à l'utilisateur de la réajuster.
Lors du réajustemment de la taille de l'affichage, l'utilisateur reçoit un retour lui disant que l'interface se réajuste, de façon à ne pas entrainer de saccade dans le réajustement des éléments, et ainsi de ne pas brouiller le joueur.
### Page d'Accueil
La page d'accueil comporte un titre principal et une image formés en caractères ASCII.
À côté de ces deux éléments d'interfaces, un menu interractif permet de choisir entre créer une partie et continuer la précédente.
### Interface de jeu
L'interface comporte 3 parties principales :
- le volet supérieur regroupant les informations générales et renseignant sur l'avancement actuel du jeu
- le volet intermédiaire permettant une visualisation des récoltes et permettant d'interagir avec
- le volet inférieur comprenant les menus de jeu et des commandes d'actions (sélectionner un outil, prendre une décision, etc...)
- un volet latéral permettant de présenter les différentes caractéristiques selon la plante sélectionnée dans le champs
#### Volet supérieur
Le volet supérieur comporte des informations générales sur l'état du jeu comme la date, la météo, le lieu et le mode de jeu dans lequel le joueur se trouve. 
#### Volet intermédiaire
Le volet intermédiaire comporte une visualisation des différentes informations.
#### Volet inférieur
Contient tous les éléments de menu permettant au joueur d'effectuer des actions 
## Navigation
La navigation du jeu se fait à l'aide des touches du clavier.
Pour vous déplacer, utilisez les touches directionnelles.
Pour accéder aux différents menus :
- Inventaire > touche "I"
- Magasin > touche "M"
- Journal > touche "J"
- Champs > touche "C"
## Jeu
### Joueur (sauvegarde)
### Plantes
Les plantes sont la base du jeu.
Chaque plante peut être achetée ou vendue au magasin et possède un prix d'achat et de vente.
Lorsqu'une plante est plantée sur un terrain, elle possède un temps de croissance qui augmente chaque semaine jusqu'à ce qu'elle soit mature (si croissance==100 : devient mature).
La plante possède des besoins en soleil et en eau, une température et un terrain de préférence. Tous ces facteurs influencent la santé d'une plante.
La santé d'une plante affecte son rendement et est déterminée sur 100:
- > 80 rendement maximum
- > 70 rendement important
- > 60 rendement modéré
- > 50 rendement faible
Chaque plante possède son propre rendement.
Eau : 
- santé -20 si le besoin en eau n'est pas suffisant
- santé -20 si le besoin en eau est trop important
Soleil : 
- santé -15 si la quantité de soleil n'est pas suffisante
- santé -5 si la quantité de soleil est à 100% (trop d'exposition tue l'exposition)
Température
- santé -15 si la différence de température avec la température de préférence est > à 15°C
### Nuisible
Chaque semaine, un nuisible vient s'installer sur une des plantes
On choisit pour une plante dans le champs un des nuisibles qu'il possède et on l'ajoute à la liste.
Chaque catégorie de plante possède ses propres types de nuisibles.
Chaque nuisible peut être retiré selon son outil.

### Terrains
Il existe 3 types de terrains selon la localisation où vous vous trouvez.
Le terrain à Carcassonne est argileux;
Un terrain possède un taux d'humidité et d'exposition au soleil. Le terrain peut être fertile (nombre qui peut ajouter jusqu'à 10 à la santé, diminue de 2 à chaque semaine)
Le terrain possède un drainage qui est fixe et déterminé par le type de terrain.

### Météo
La météo renferme les températures et temps que le joueur va rencontrer tout du long de sa partie. Les températures ont été récupérées de données réelles et 3 types de temps on été modélisés : pluie soleil et nuageux.
Pluie : donne de +10 à +25% d"humidité à l'ensemble des terrains
Soleil : donne de +10 à +25% d'exposition à l'ensemble des terrains
Nuageux : pas d'évolution de la pluie et du taux de soleil


### Objets

#### 

Peuvent être taillés, 
## Choix Réalisés pour la stucture du code
Nous avons choisi de construire tous nos tableaux à double entrée ou ordres de paramètres en mettant toujours en premier les abcisses puis les ordonnées.
Ce choix s'est imposé pour que la logique des méthodes déjà implémentées par la classe Console.
*Console.GetCursorPosition* et *Console.SetCursorPosition* prennent en premier indice la largeur/colonne, et en second la hauteur/ligne. 
