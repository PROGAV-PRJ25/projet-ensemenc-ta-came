# ENSemenCe - Carcassonne

> *Salutations jardinier.e ! Atteri.e depuis peu dans la forteresse de Carcassonne, tu as été placé.e à la gestion du potager de la forteresse. Cultive, entretiens et aggrandis ton potager tout en prenant garde aux différents s'obstacles qui pourront s'opposer à toi !*


Ce travail concerne le projet de programmation orientée objet en c#, unité d'enseignement de première année à l'ENSC.
Le projet consiste en un jeu de gestion de potager intégrant différentes temporalités.
Réalisé par **TONON Thiméo** et **VIGNES Camille**

## Affichage

Le jeu se présente sous la forme d'une application console.
Ce dernier est programmé de manière à permettre une taille de fenêtre flexible. 
L'affichage s'ajuste donc dynamiquement selon la taille de la fenêtre.
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
### Terrains

Il existe 3 types de terrains selon la localisation où vous vous trouvez.
Le terrain à Carcassonne est argileux;

### Plantes
Les plantes sont la partie principale du jeu

### Arbres
Peuvent être taillés, 