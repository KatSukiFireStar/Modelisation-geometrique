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