# Space Shooter 2D - Barème de difficulté progressive

## 🎮 Aperçu du jeu

Space Shooter 2D est un jeu d'arcade rétro classique où vous contrôlez un vaisseau spatial et devez survivre aux vagues d'ennemis qui vous attaquent. Le jeu propose une progression de difficulté intelligente et progressive pour offrir un défi constant sans être accablant.

### 🎯 Concept du jeu

Le joueur incarne un pilote de vaisseau spatial qui doit:
- **Survivre** aux attaques continues d'ennemis de plus en plus dangereux
- **Progresser** en acquérant de nouvelles puissances et améliorations
- **Gagner des points** en détruisant les ennemis
- **Atteindre le meilleur score** possible avant de perdre toutes ses vies

Le jeu est conçu avec une **difficulté progressive et équilibrée** pour éviter une montée exponentielle trop brutale. Vous commencez facile et le jeu devient graduellement plus difficile à mesure que vous progresse.

## 🚀 Gameplay

### Contrôles

| Action | Touche |
|--------|--------|
| **Déplacer à gauche** | ⬅️ Flèche gauche |
| **Déplacer à droite** | ➡️ Flèche droite |
| **Déplacer vers le haut** | ⬆️ Flèche haut |
| **Déplacer vers le bas** | ⬇️ Flèche bas |
| **Tirer** | Automatique (pas besoin d'appuyer) |
| **Recommencer** | R (en cas de Game Over) |

**Note** : Le joueur est limité à la moitié inférieure de l'écran pour rester visible et accessible.

### 🛸 Le vaisseau du joueur

Votre vaisseau est un petit vaisseau de combat spatial qui:
- **Tire automatiquement** en continu (plus besoin d'appuyer sur Espace)
- **Se déplace** horizontalement sur toute la largeur de l'écran
- **Se déplace** verticalement dans la moitié inférieure de l'écran
- **Gagne des améliorations** à mesure que vous accumulez des points:
  - 🔥 **Cadence de tir** : Tire plus rapidement
  - 🎯 **Nombre de projectiles** : Crée des salves de 2, 3, 4 ou 5 projectiles
  - ⚡ **Vitesse** : Se déplace plus rapidement

### 👾 Les ennemis

Les ennemis sont des vaisseaux hostiles qui apparaissent en vagues et descendent l'écran en tirando des projectiles. Il existe **4 niveaux d'ennemis** avec croissance de dangerosité:

#### Level 1 - Ennemi basique
- 🟤 **Apparence** : Petit vaisseau carré simple
- 🎯 **Projectiles** : 1 seul projectile par tir
- ⏱️ **Cadence de tir** : 2.5 secondes entre chaque tir
- 📊 **Points gagnés** : 10 points
- 💥 **Difficulté** : Très facile

#### Level 2 - Ennemi moyen
- 🟡 **Apparence** : Vaisseau carré de taille moyenne
- 🎯 **Projectiles** : 3 projectiles en salve
- ⏱️ **Cadence de tir** : 2.0 secondes entre chaque tir
- 📊 **Points gagnés** : 25 points
- 💥 **Difficulté** : Facile

#### Level 3 - Ennemi dangereux
- 🟠 **Apparence** : Vaisseau carré plus grand
- 🎯 **Projectiles** : 5 projectiles en salve large
- ⏱️ **Cadence de tir** : 1.5 secondes entre chaque tir
- 📊 **Points gagnés** : 50 points
- 💥 **Difficulté** : Moyen

#### Level 4 - Ennemi très dangereux
- 🔴 **Apparence** : Grand vaisseau carré impressionnant
- 🎯 **Projectiles** : 6 projectiles en salve très large
- ⏱️ **Cadence de tir** : 1.0 seconde entre chaque tir
- 📊 **Points gagnés** : 100 points
- 💥 **Difficulté** : Très difficile

### 🎯 Les projectiles

#### Projectiles du joueur
- **Vitesse** : Très rapide (environ 10 unités/seconde)
- **Trajectoire** : En éventail symétrique (jusqu'à 5 projectiles)
  - 1 projectile : Droit devant
  - 3 projectiles : Centre + 15° à gauche et droite
  - 5 projectiles : Centre + 15°/30° à gauche et droite
- **Portée** : Illimitée (jusqu'au bord de l'écran)
- **Hitbox** : Très précise

#### Projectiles des ennemis
- **Vitesse** : Modérée (4 unités/seconde) pour laisser du temps de réaction
- **Trajectoire** : Directe vers le bas
  - Level 1 : 1 projectile vertical
  - Level 2 : 3 projectiles (vertical + 30° à gauche/droite)
  - Level 3 : 5 projectiles (angles plus larges)
  - Level 4 : 6 projectiles (angles très larges, jusqu'à 90° horizontal)
- **Portée** : Jusqu'au bas de l'écran
- **Dégât** : Toucher le joueur = perte d'une vie

### 💚 Système de vies

- **Vies initiales** : 3 vies
- **Perte d'une vie** : Toucher un projectile ennemi
- **Gain d'une vie** : Collecter un bonus de vie (rare)
- **Game Over** : Quand vous perdez toutes vos vies
- **Feedback** : Le vaisseau clignote en jaune pendant 0.5s quand il est touché

### 🎁 Système de bonus

Les bonus apparaissent aléatoirement à l'écran et descendent lentement:

#### Bonus Vie Extra
- 🟢 **Icône** : Coeur ou symbole de vie
- **Effet** : Ajoute +1 vie
- **Apparition** : Aléatoire (environ tous les 15 secondes ±5s)
- **Collecte** : Toucher le bonus avec votre vaisseau

### 📊 Système de score

| Action | Points |
|--------|--------|
| **Détruire ennemi Level 1** | 10 pts |
| **Détruire ennemi Level 2** | 25 pts |
| **Détruire ennemi Level 3** | 50 pts |
| **Détruire ennemi Level 4** | 100 pts |
| **Collecter bonus vie** | Aucun point (mais +1 vie) |

Le meilleur score (high score) est **sauvegardé automatiquement** dans un fichier JSON pour persister entre les parties.

### 🎵 Audio

- **Son de tir** : Effet sonore chaque fois que le joueur tire une salve
- **Son d'explosion** : Quand un ennemi est détruit
- **Saturation audio évitée** : Un seul son par salve (pas 5 sons pour 5 projectiles)

### 💾 Persistance des données

- Le **high score** est sauvegardé automatiquement dans : `Application.persistentDataPath/savegame.json`
- Format : JSON simple avec la valeur du high score
- Chargé au démarrage du jeu

## 📊 Vue d'ensemble

### Barème par phases

| Phase | Score | Ennemis/Vague | Fréquence spawn | Distribution des Levels |
|-------|-------|---------------|-----------------|------------------------|
| **Phase 1 - Initiation** | 0-300 | **1** | 2.0s → 1.9s | 100% Level 1 |
| **Phase 2 - Variété** | 300-800 | **2** | 1.8s → 1.7s | 80% Lv1, 20% Lv2 |
| **Phase 3 - Cadence** | 800-1300 | **2-3** (aléatoire) | 1.6s → 1.5s | 50% Lv1, 40% Lv2, 10% Lv3 |
| **Phase 4 - Volume** | 1300-2000 | **3** | 1.4s → 1.3s | 30% Lv1, 50% Lv2, 20% Lv3 |
| **Phase 5 - Montée** | 2000-3000 | **3-4** (aléatoire) | 1.2s → 1.0s | 10% Lv1, 40% Lv2, 40% Lv3, 10% Lv4 |
| **Phase 6 - Expert** | 3000-4000 | **4** | 0.9s → 0.8s | 5% Lv1, 25% Lv2, 50% Lv3, 20% Lv4 |
| **Phase 7 - Maître** | 4000+ | **5** (maximum) | 0.8s (minimum) | 20% Lv2, 50% Lv3, 30% Lv4 |

### Caractéristiques des niveaux d'ennemis

| Level | Projectiles | Cadence de tir | Score gagné | Sprite |
|-------|------------|----------------|-------------|--------|
| **Level 1** | 1 | 2.5s | 10 pts | enemySprite1 |
| **Level 2** | 3 | 2.0s | 25 pts | enemySprite3 |
| **Level 3** | 5 | 1.5s | 50 pts | enemySprite5 |
| **Level 4** | 6 | 1.0s | 100 pts | enemySprite6 |

### Paramètres du spawner

- **Fréquence initiale** : 2.0 secondes entre chaque vague
- **Fréquence minimale** : 0.8 seconde (atteinte à 4000+ points)
- **Réduction par palier** : -0.1s tous les 100 points

## 🎮 Progression du joueur

### Tir automatique

Le joueur tire **automatiquement en continu** (plus besoin d'appuyer sur Espace).

### Amélioration par paliers

| Amélioration | Fréquence | Détails |
|--------------|-----------|---------|
| 🔥 **Cadence de tir** | Tous les **150 points** | Démarre à 0.6s → diminue de 0.05s → Min 0.12s |
| 🎯 **Nombre de projectiles** | Tous les **400 points** | 1 → 2 → 3 → 4 → 5 (max à 1600 pts) |
| ⚡ **Vitesse de déplacement** | Tous les **200 points** | 5.0 → +0.5 par palier → Max 12.0 |

### Courbe de puissance

```
Score    Projectiles    Cadence     Vitesse
  0 pts:     1          0.6s        5.0
400 pts:     2          ~0.4s       6.0
800 pts:     3          ~0.25s      7.0
1200 pts:    4          ~0.15s      8.0
1600 pts:    5 (MAX)    ~0.1s       9.0
2000 pts:    5          ~0.07s      10.0
2400 pts:    5          0.12s (MAX) 11.0
```

## ⚖️ Philosophie d'équilibrage

### Courbe de difficulté

```
Ratio Puissance Joueur / Difficulté Ennemis

Phase 1-2 : Joueur >> Ennemis  (Apprentissage facile)
Phase 3-4 : Joueur ≈ Ennemis   (Équilibre, challenge)
Phase 5-6 : Joueur < Ennemis   (Défi intense)
Phase 7   : Joueur << Ennemis  (Survie, plateau)
```

### Avantages du système

✅ **Démarrage doux** : Un seul ennemi faible toutes les 2.5s
✅ **Progression naturelle** : Chaque amélioration est perceptible
✅ **Pas de pic brutal** : Les paliers sont bien espacés (150/200/400 pts)
✅ **Plateaux** : La difficulté se stabilise à ~4000 pts pour rester jouable
✅ **Variété** : Même distribution aléatoire des niveaux d'ennemis
✅ **Feedback** : Le joueur sent sa progression en puissance

## 🎯 Stratégie de conception

### Priorité de progression

1. **D'abord la cadence de tir** (150 pts) : Le joueur tire plus vite
2. **Puis la vitesse** (200 pts) : Le joueur esquive mieux
3. **Enfin les projectiles** (400 pts) : Le joueur couvre plus de surface

Cette ordre évite de rendre le joueur trop puissant trop vite, tout en lui donnant rapidement les outils pour survivre.

### Paramètres ennemis

Le nombre d'ennemis par vague augmente lentement (1 ennemi jusqu'à 500 pts), ce qui laisse au joueur le temps d'apprendre les mécaniques avant d'être submergé.

La distribution des niveaux introduit progressivement les ennemis dangereux sans jamais les rendre dominants, gardant toujours quelques ennemis faciles pour donner des "pauses" au joueur.

## 🔧 Configuration technique

### Fichiers impactés

- **PlayerController.cs** : Tir automatique, progression cadence/projectiles/vitesse
- **GameManager.cs** : Gestion des paliers de progression (150/200/400 pts)
- **EnemySpawner.cs** : Calcul du nombre d'ennemis et distribution des levels
- **Enemy.cs** : Configuration par level (projectiles/cadence/score/sprite)

### Paramètres ajustables

Pour modifier l'équilibre, ajuster dans les scripts :

**GameManager.cs** :
```csharp
// Fréquence des améliorations du joueur
CheckPlayerProgression() : 150, 400, 200  // cadence, projectiles, vitesse
```

**EnemySpawner.cs** :
```csharp
initialSpawnRate = 2.0f;    // Fréquence de spawn initiale (au lieu de 2.5f)
minSpawnRate = 0.8f;        // Fréquence minimale (au lieu de 1.0f)
spawnRateDecrease = 0.1f;   // Réduction par palier

CalculateEnemyCount()       // Barème nombre d'ennemis par score
CalculateEnemyLevel()       // Distribution des levels par score
```

**PlayerController.cs** :
```csharp
baseFireRate = 0.6f;              // Cadence initiale (au lieu de 1.2f)
minFireRate = 0.12f;              // Cadence maximale (au lieu de 0.15f)
fireRateDecreasePerLevel = 0.05f; // Amélioration par palier

maxProjectileCount = 5;           // Nombre max de projectiles
baseMoveSpeed = 5f;               // Vitesse initiale
maxMoveSpeed = 12f;               // Vitesse maximale
```

## 📝 Notes de conception

- **Démarrage accéléré** : Le joueur commence avec un tir 2x plus rapide (0.6s au lieu de 1.2s)
- **Plus d'ennemis plus tôt** : 2 ennemis apparaissent dès 300 pts (au lieu de 500)
- Les ennemis Level 1 ne disparaissent jamais complètement (sauf Phase 7) pour éviter une frustration excessive
- Le joueur atteint sa puissance maximale vers 2000-2400 points, après c'est de la survie pure
- La Phase 7 (4000+ pts) est un plateau intentionnel pour éviter une difficulté infinie impossible
- Les projectiles ennemis ont été ralentis (4f au lieu de 8f) pour donner plus de temps de réaction
