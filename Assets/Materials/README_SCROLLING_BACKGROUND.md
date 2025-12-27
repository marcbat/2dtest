# Configuration du Fond Défilant

Ce document explique comment configurer le fond défilant dans Unity Editor.

## 📋 Fichiers créés

- **Script** : `Assets/ScrollingBackground.cs` - Script de gestion du défilement
- **Material** : `Assets/Materials/BackgroundMaterial.mat` - Material avec texture répétée
- **Texture** : Utilise `Assets/Sprites/Backgrounds/black.png` (ou autre fond disponible)

## 🎮 Configuration dans Unity Editor

### Étape 1 : Créer l'objet Background

1. Dans la **Hierarchy**, clic droit → **2D Object** → **Sprite**
2. Renommer l'objet en **"Background"**
3. Sélectionner l'objet Background

### Étape 2 : Configurer la position et l'échelle

Dans l'**Inspector**, configurer le **Transform** :
- **Position** : `X: 0, Y: 0, Z: 10` (Z=10 pour être derrière les autres objets)
- **Rotation** : `X: 0, Y: 0, Z: 0`
- **Scale** : Ajuster pour couvrir tout l'écran (par exemple `X: 20, Y: 20, Z: 1`)

### Étape 3 : Assigner le Sprite

Dans l'**Inspector**, dans le composant **Sprite Renderer** :
1. **Sprite** : Sélectionner `black_0` (ou un autre sprite de Backgrounds)
2. **Material** : Assigner `Assets/Materials/BackgroundMaterial`
3. **Sorting Layer** : Créer ou sélectionner "Background"
4. **Order in Layer** : `-10` (pour assurer qu'il reste derrière tout)

### Étape 4 : Ajouter le script de défilement

1. Avec l'objet **Background** sélectionné, cliquer sur **Add Component**
2. Chercher et ajouter le script **ScrollingBackground**
3. Dans l'Inspector, configurer les paramètres :
   - **Scroll Speed** : `2` (valeur recommandée : 2-5)
   - **Background Material** : Glisser-déposer `Assets/Materials/BackgroundMaterial`

### Étape 5 : Configurer le Sorting Layer (si pas encore fait)

1. Dans Unity, aller dans **Edit** → **Project Settings** → **Tags and Layers**
2. Dans la section **Sorting Layers**, ajouter un layer "Background"
3. S'assurer qu'il est en premier (ordre le plus bas)

## ⚙️ Paramètres ajustables

### Vitesse de défilement (Scroll Speed)
- **2-3** : Défilement subtil, recommandé pour ne pas distraire
- **4-5** : Défilement plus rapide, plus dynamique
- **1** : Très lent, ambiance calme

### Choix de la texture
Vous pouvez utiliser d'autres textures de fond disponibles :
- `black.png` - Fond noir avec étoiles (par défaut)
- `blue.png` - Fond bleu spatial
- `darkPurple.png` - Fond violet foncé
- `purple.png` - Fond violet

Pour changer :
1. Modifier le **Material** `BackgroundMaterial.mat`
2. Dans **Main Tex**, sélectionner un autre sprite du dossier Backgrounds

## ✅ Vérification

Une fois configuré, vous devriez voir :
- ✅ Le fond couvre tout l'écran
- ✅ Le fond défile de haut en bas
- ✅ Le défilement est fluide sans saccades
- ✅ Aucune couture visible (répétition transparente)
- ✅ Le fond reste derrière tous les autres éléments
- ✅ Pas de baisse de performance

## 🐛 Dépannage

### Le fond ne défile pas
- Vérifier que le script `ScrollingBackground` est bien attaché
- Vérifier que le `Background Material` est assigné dans le script
- Vérifier que le jeu est en mode Play

### Le fond a des coutures visibles
- Vérifier que la texture a le **Wrap Mode** en **Repeat** (configuré automatiquement)
- Essayer une autre texture (certaines sont plus adaptées au tiling)

### Le fond est devant les autres objets
- Ajuster le **Sorting Layer** à "Background" avec **Order in Layer** = -10
- Ou augmenter la valeur Z de la position (ex: Z=10 ou Z=15)

## 🎨 Personnalisation avancée

### Ajouter plusieurs couches de parallaxe
Créer plusieurs objets Background avec différentes vitesses :
- Background 1 : Speed = 1.5, Z = 10 (le plus loin)
- Background 2 : Speed = 2.5, Z = 8 (intermédiaire)
- Background 3 : Speed = 4.0, Z = 6 (le plus proche)

### Défilement horizontal
Modifier `ScrollingBackground.cs` ligne 28 :
```csharp
// Défilement horizontal au lieu de vertical
backgroundMaterial.mainTextureOffset = new Vector2(offset, 0);
```

### Défilement diagonal
Modifier `ScrollingBackground.cs` ligne 28 :
```csharp
// Défilement diagonal
backgroundMaterial.mainTextureOffset = new Vector2(offset * 0.5f, offset);
```
