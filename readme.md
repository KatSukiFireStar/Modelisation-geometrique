# Modelisation Géométrique 

## Environnement de développement

Pour ce cours, l'environement de développement utiliser est le moteur de jeu Unity3D ainsi que l'IDE Rider faisant partie de la suite JetBrains.

## TD 1

Ce TD 1 comprend 5 scripts différents permettant chacun de créer différents types d'objets. Voici les 5 objets pouvant être créer:

- Plan: Ce script prend en paramètre un nombre de lignes et aussi un nombre de colonnes.
- Cylindre: Ce script prend en paramètre le rayon et la hauteur du cylindre mais aussi un nombre de méridiens.
- Sphere: Ce script prend en paramètre un rayon mais aussi un nombre de méridiens et un nombre de paralleles
- Cone: Ce script prend en paramètre un rayon, une hauteur, un nombre de méridiens et une hauteur de tronquage. Si la hauteur de tronquage est égale à la hauteur, le cone ne sera pas tronqué.
- Pac-Man (sphere tronquer): Ce script prend en paramètre un rayon, un nombre de meridiens, un nombre de paralleles et un angle de tronquage.

Aussi chaque script prend en compte un material qui sera appliquer au mesh renderer.

Pour utiliser les scripts, ils faut les attacher à des objets vides et renseigner les diverses variables cités pour chacun.

Pour voir les objets, il faut lancer le jeu et ensuite retourner dans la scene afin de pouvoir se balader.

Pour le TD1 le nom de la scene utilisé comprenant tous mes objets avec mes scripts est la scene: Scene TD1

## TD 2

Ce TD 2 comprend un seul script permettant l'affiche d'un objet de type off.
Pour charger ce fichier, il faut ajouter le script CreateMaillage à un objet. Ensuite, il faut renseigner le nom du fichier qui se trouvera dans le repertoire Assets/Maillage du projet Unity. Il ne faut pas renseigner l'extension du fichier dans le champ fileName. On peut aussi ajouter un material qui sera appliquer au mesh.

A la fin de chaque lecture pour l'objet, il le réecrira dans le format obj au même emplacement.

Pour voir les objets, il faut lancer le jeu et ensuite retourner dans la scene afin de pouvoir se balader.

Pour ce TD, la scène utilisé porte le nom: Scene TD2

## TD3

Ce TD 3 comprend plusieurs script. Plusieurs d'entres eux sont des classes permettant le stockage de mes informations et les autres, la création de mes objets.
Les 3 classes sont les suivantes:
- Sphere: Contient toutes les informations relatives à la sphere.
- Voxel: Chaque voxel contient plusieurs coordonnées. Le point max, le point min et le centre. Les voxels contiennent aussi un potentiel.
- Octree: Contient ma structure permettant la création d'octree. Ce script comprent 2 listes. La premiere est une liste d'octree fils et la seconde est une liste de voxels fils. Ce script comprend aussi deux fonctions permettant l'ajout d'octree et de voxels au liste. Cependant, un octree ne peut etre ajouté à la liste que ssi la liste de voxel est vide et inversement pour les ajouts de voxels.

Les 2 scripts de création sont les suivants:
- VolumicSphere: Permet la création d'une (ou plusieurs) sphere volumique. Chaque sphere possède ses informations de position et leurs radius. Il y a aussi plusieurs options d'octree comme créer l'octree en adaptatif ou non et aussi sa précision. Il y a aussi l'implémentation de deux operateur l'intersection et l'union.
- PotentialTools: Permet la création d'une sphere volumique et ensuite de pouvoir changer le potentiel des voxels. Il y a aussi un tool pouvant se deplacer grâce au touches ZQSDAE. 

Ce TD contient deux scènes permettant l'utilisation de différents outils.
La premiere scène s'appele Scene TD3 Voxel et montre une création de voxel.
La deuxième scène s'appele Scene TD3 Tools et montre l'utilisation du potential tool.

## TD4

Ce TD4 comprend un script permettant la simplification d'un maillage. Pour ce faire, il faut accrocher le script à un objet vide et lui specifiez les informations suivantes:
- Precision de l'octree : Precision de la découpe du noyveau maillage. Plus ce chiffre est élévé et plus le maillage sera précis
- Material : Le matériau à attacher au mesh
- Filename : Le nom du fichier dans le dossier "Assets/Maillage". Attention ce fichier doit être au format off

Ce TD contient une seul et unique scène nommer Scene TD4.

## TD5

Ce TD5 comprend un script permettant la subdivion de ligne. Pour ce faire, il faut accrocher le script à un objet vide et lui specifiez les informations suivantes:
- StartsPoints : Point de base de la forme 
- BaseColor : Color reliant les points de la forme de base
- Precision : Precision de subdivision de la ligne
- CurveColor : Couleur reliant les points de la forme créer

Ce TD contient une seul et unique scène nommer Scene TD5.

## TD6

Ce TD6 comprend plusieurs script permettant la création de différentes courbes. Pour ce faire voici les différents script et les informations à specifier :
- HermiteCurve : Permet de creer une courbe d'Hermit
    - CurveColor : Couleur de base de la ligne
    - Delta : Interstice de calcul entre 0 et 1
    - Points :
        - P0 : Coordonnées du point 0
        - P1 : Coordonnées du point 1
    - Tangents :
        - V0 : Coordonnées de la tangentes au point 0
        - V1 : Coordonnées de la tangentes au pount 1
- BezierCurve : Permet de creer une courbe de Bezier
    - StartsPoints : Point de base de la forme 
    - BaseColor : Couleur des points de base
    - CurveColor : Couleur reliant les points de la forme créer
    - Delta : Interstice de calcul entre 0 et 1
- BezierCurveCasteljau : Permet de creer une courbe de Bezier avec l'algorithme de De Casteljau
    - StartsPoints : Point de base de la forme 
    - BaseColor : Couleur des points de base
    - CurveColor : Couleur reliant les points de la forme créer
    - Delta : Interstice de calcul entre 0 et 1

Ce TD contient une seul et unique scène nommer Scene TD6.