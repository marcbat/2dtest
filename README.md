# Space Shooter 2D - Jeu de Shoot'em Up Progressif

## 🎮 Aperçu du jeu

Space Shooter 2D est un shoot'em up vertical où vous contrôlez un vaisseau spatial et devez survivre face à des vagues d'ennemis de plus en plus dangereuses. Le jeu propose une **progression par collectibles** et un système d'**ennemis variés** avec différents patterns de mouvement.

### 🎯 Concept du jeu

Le joueur incarne un pilote de vaisseau spatial qui doit:
- **Survivre** aux attaques continues de 6 types d'ennemis différents
- **Collecter des power-ups** lâchés par les ennemis pour s'améliorer
- **Détruire des ennemis** avec différents niveaux de résistance (1 à 5 HP)
- **Atteindre le meilleur score** possible avant de perdre ses 3 vies

Le jeu utilise un **système de 7 phases progressives** basées sur le score pour introduire graduellement de nouveaux types d'ennemis et augmenter la difficulté.

## 🚀 Gameplay

### 🎮 Contrôles

| Action | Touche |
|--------|--------|
| **Déplacer à gauche** | ⬅️ Flèche gauche |
| **Déplacer à droite** | ➡️ Flèche droite |
| **Déplacer vers le haut** | ⬆️ Flèche haut |
| **Déplacer vers le bas** | ⬇️ Flèche bas |
| **Tirer** | ⌨️ Espace (maintenir pour tir continu) |
| **Recommencer** | R (en cas de Game Over) |

**Note** : Le joueur est limité à la moitié inférieure de l'écran.

### 🛸 Le vaisseau du joueur

- **Tir manuel** : Vous devez appuyer sur Espace pour tirer
- **6 niveaux d'armes** progressifs (via collectibles ⚡)
- **3 vies** au départ (récupérables via collectibles ❤️)
- **Buffs temporaires** : Shield (🛡️ 5s) et Rapid Fire (🔥 10s)
- 📊 **Points gagnés** : 10 points
- 💥 **Difficulté** : Très facile

### 👾 Les 6 types d'ennemis

Le jeu propose **6 types d'ennemis** avec des caractéristiques progressives :

#### 🟦 Scout (Niveau 1)
- **Points de vie** : 1 HP
- **Pattern de mouvement** : Vertical (descente simple)
- **Projectiles** : 1 projectile
- **Vitesse** : Normale
- **Points gagnés** : 10 pts

#### 🟨 Fighter (Niveau 2)
- **Points de vie** : 2 HP
- **Pattern de mouvement** : DiagonalSoft (descente en diagonale douce)
- **Projectiles** : 2 projectiles
- **Vitesse** : Normale
- **Points gagnés** : 20 pts

#### 🟧 Bomber (Niveau 3)
- **Points de vie** : 3 HP
- **Pattern de mouvement** : VerticalZigzag (descente en zigzag)
- **Projectiles** : 3 projectiles
- **Vitesse** : Normale
- **Points gagnés** : 30 pts

#### 🟪 Interceptor (Niveau 4)
- **Points de vie** : 2 HP
- **Pattern de mouvement** : DiagonalFast (descente diagonale rapide)
- **Projectiles** : 2 projectiles
- **Vitesse** : Très rapide (×1.5)
- **Points gagnés** : 25 pts

#### 🟥 Assault (Niveau 5)
- **Points de vie** : 4 HP
- **Pattern de mouvement** : Sinusoidal (descente en vague)
- **Projectiles** : 5 projectiles en éventail
- **Vitesse** : Normale
- **Points gagnés** : 50 pts

#### ⬛ Dreadnought (Niveau 6)
- **Points de vie** : 5 HP
- **Pattern de mouvement** : Circular (descente en spirale)
- **Projectiles** : 6 projectiles en cercle
- **Vitesse** : Lente (×0.8)
- **Points gagnés** : 100 pts

### 💚 Système de vies

- **Vies initiales** : 3 vies
- **Perte d'une vie** : Toucher un projectile ennemi OU entrer en collision avec un ennemi
- **Gain d'une vie** : Collecter un collectible ❤️ Health
- **Game Over** : Quand vous perdez toutes vos vies
- **Feedback** : Le vaisseau devient invulnérable 1 seconde après avoir été touché

### 🎁 Système de collectibles

Les collectibles sont **lâchés par les ennemis** quand ils sont détruits. Il existe **4 types de collectibles** :

#### ⚡ Weapon Upgrade
- **Effet** : Améliore **définitivement** l'arme (6 niveaux)
- **Taux de drop** : 15% (réduit à 6% en phase 7)
- **Niveaux d'arme** :
  1. Single (0.35s) : 1 projectile droit
  2. Double (0.3s) : 2 projectiles parallèles
  3. Triple (0.28s) : 3 projectiles en éventail
  4. DoubleAngled (0.25s) : 4 projectiles (2 avant + 2 diagonaux)
  5. QuintupleFan (0.22s) : 5 projectiles en large éventail
  6. DoubleDiagonal (0.2s) : 6 projectiles (2 avant + 4 diagonaux)

#### ❤️ Health
- **Effet** : +1 vie
- **Taux de drop** : 10% (réduit à 4% en phase 7)

#### 🛡️ Shield
- **Effet** : Invincibilité pendant **5 secondes**
- **Taux de drop** : 8% (réduit à 3.2% en phase 7)
- **Feedback visuel** : Aura bleue autour du vaisseau

#### 🔥 Rapid Fire
- **Effet** : Cadence de tir ×2 pendant **10 secondes** (temporaire)
- **Taux de drop** : 12% (réduit à 4.8% en phase 7)
- **Note** : Ne remplace PAS le Weapon Upgrade permanent

### 📊 Système de score et progression

Le score détermine la difficulté du jeu en introduisant de nouveaux types d'ennemis progressivement.

#### Calcul des points

| Action | Points |
|--------|--------|
| **Détruire un Scout (1 HP)** | 10 pts |
| **Détruire un Fighter (2 HP)** | 20 pts |
| **Détruire un Bomber (3 HP)** | 30 pts |
| **Détruire un Interceptor (2 HP)** | 25 pts |
| **Détruire un Assault (4 HP)** | 50 pts |
| **Détruire un Dreadnought (5 HP)** | 100 pts |

#### Le meilleur score (high score) est **sauvegardé automatiquement** dans `Application.persistentDataPath/savegame.json`.

## 📊 Barème de progression (7 phases)

Le jeu devient progressivement plus difficile à mesure que votre score augmente :

### Phase 1 - Initiation (0-300 points)
- **Ennemis par vague** : 1 ennemi
- **Fréquence de spawn** : 2.5 secondes
- **Types d'ennemis** : Scout uniquement (100%)
- **Objectif** : Apprendre les contrôles

### Phase 2 - Découverte (300-800 points)
- **Ennemis par vague** : 1-2 ennemis (aléatoire)
- **Fréquence de spawn** : 2.2 secondes
- **Types d'ennemis** :
  - Scout : 70%
  - Fighter : 30%
- **Objectif** : Introduction du premier ennemi avec 2 HP

### Phase 3 - Variété (800-1500 points)
- **Ennemis par vague** : 2 ennemis
- **Fréquence de spawn** : 2.0 secondes
- **Types d'ennemis** :
  - Scout : 45%
  - Fighter : 35%
  - Bomber : 20%
- **Objectif** : Apparition du Bomber (3 HP, zigzag)

### Phase 4 - Vitesse (1500-2200 points)
- **Ennemis par vague** : 2-3 ennemis (aléatoire)
- **Fréquence de spawn** : 1.8 secondes
- **Types d'ennemis** :
  - Scout : 30%
  - Fighter : 30%
  - Bomber : 20%
  - Interceptor : 20%
- **Objectif** : Introduction de l'Interceptor (rapide)

### Phase 5 - Puissance (2200-3200 points)
- **Ennemis par vague** : 3 ennemis
- **Fréquence de spawn** : 1.6 secondes
- **Types d'ennemis** :
  - Scout : 15%
  - Fighter : 20%
  - Bomber : 15%
  - Interceptor : 20%
  - Assault : 20%
  - Dreadnought : 10%
- **Objectif** : Tous les types d'ennemis disponibles

### Phase 6 - Intensité (3200-4500 points)
- **Ennemis par vague** : 3-4 ennemis (aléatoire)
- **Fréquence de spawn** : 1.4 secondes
- **Types d'ennemis** :
  - Scout : 10%
  - Fighter : 15%
  - Bomber : 15%
  - Interceptor : 15%
  - Assault : 25%
  - Dreadnought : 20%
- **Objectif** : Augmentation des ennemis lourds

### Phase 7 - Expert (4500+ points)
- **Ennemis par vague** : 4 ennemis
- **Fréquence de spawn** : 1.2 secondes (minimum)
- **Types d'ennemis** :
  - Scout : 5%
  - Fighter : 10%
  - Bomber : 10%
  - Interceptor : 15%
  - Assault : 30%
  - Dreadnought : 30%
- **Objectif** : Survie maximale - ennemis lourds dominants

### 🎵 Audio

- **Musique de fond** : Lecture en boucle (configurable via AudioSource)
- **Son de tir** : Effet sonore à chaque tir du joueur
- **Son d'explosion** : Quand un ennemi est détruit
- **Son de collision** : Quand le joueur est touché

### 💾 Persistance des données

- Le **high score** est sauvegardé automatiquement dans : `Application.persistentDataPath/savegame.json`
- Format : JSON simple avec la valeur du high score
- Chargé au démarrage du jeu
- **Fréquence minimale** : 0.8 seconde (atteinte à 4000+ points)
- **Réduction par palier** : -0.1s tous les 100 points

## 🎮 Progression du joueur

## 🎮 Architecture technique

### Fichiers principaux

- **PlayerController.cs** : Contrôle du vaisseau, tir manuel, gestion des armes et des vies
- **GameManager.cs** : Gestion du score, progression, Game Over, sauvegarde
- **EnemySpawner.cs** : Spawn des ennemis, 7 phases de difficulté progressive
- **Enemy.cs** : Comportement des ennemis (mouvement patterns, HP, tir)
- **EnemyTypeData.cs** : ScriptableObjects pour la configuration des 6 types d'ennemis
- **Collectible.cs** : Gestion des collectibles (spawn au death des ennemis)
- **CollectibleType.cs** : ScriptableObjects pour les 4 types de collectibles

### Configuration Unity

- **Moteur** : Unity 2D avec Universal Render Pipeline (URP)
- **Input System** : Nouveau Input System (`UnityEngine.InputSystem`)
- **Scène principale** : `Assets/Scenes/SampleScene.unity`

### Points clés du gameplay

✅ **Tir manuel** : Le joueur doit maintenir Espace pour tirer
✅ **6 niveaux d'armes** : Amélioration progressive par collectibles
✅ **Collectibles par drops** : Les ennemis lâchent des objets à leur mort
✅ **7 phases progressives** : Nouvelle difficulté tous les 300-800 points
✅ **6 types d'ennemis** : Diversité avec HP et patterns différents
✅ **Taux de drop dégressifs** : Les collectibles deviennent plus rares en phase tardive

## 📝 Notes de conception

- **Progression accélérée** : Les nouveaux ennemis apparaissent 25-40% plus vite qu'avant
- **Phase 1 à 300 points** : Apprentissage rapide avec Scout uniquement
- **Tous les types à 2200 points** : Diversité maximale atteinte plus tôt
- **Collectibles stratégiques** : Health et Shield deviennent rares en fin de partie
- **Fire rate progressive** : Base 0.35s, max 0.2s avec DoubleDiagonal
- **HP System** : Les ennemis nécessitent plusieurs tirs (1-5 HP selon le type)
