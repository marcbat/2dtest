# ✅ TODO Unity - Refonte du Système de Jeu

## 📊 État du Projet

### ✅ CODE (100% Terminé)
- [x] EnemyTypeData.cs
- [x] Collectible.cs
- [x] CollectibleSpawner.cs
- [x] Enemy.cs (refactorisé)
- [x] PlayerController.cs (refactorisé)
- [x] EnemySpawner.cs (refactorisé)
- [x] GameManager.cs (nettoyé)

### 🎮 UNITY (À Faire)
- [ ] Créer les ScriptableObjects
- [ ] Créer les prefabs de collectibles
- [ ] Configurer la scène
- [ ] Assigner les références
- [ ] Tester le jeu

---

## 📦 Tâche 1: Créer les 6 EnemyTypeData ScriptableObjects

### Localisation
`Assets/ScriptableObjects/EnemyTypes/`

### Étapes
1. **Créer le dossier** (si nécessaire)
   - Clic droit dans Assets → Create → Folder → "ScriptableObjects"
   - Dans ScriptableObjects → Create → Folder → "EnemyTypes"

2. **Créer les 6 assets**
   - Dans le dossier EnemyTypes
   - Clic droit → Create → Game → EnemyType
   - Créer 6 fois et renommer:
     * `EnemyType_Scout`
     * `EnemyType_Fighter`
     * `EnemyType_Bomber`
     * `EnemyType_Interceptor`
     * `EnemyType_Assault`
     * `EnemyType_Dreadnought`

3. **Configurer chaque asset**
   
   📄 **Voir le fichier `Assets/EnemyTypes_Configuration.txt` pour toutes les valeurs**
   
   Exemple pour Scout:
   ```
   Enemy Name: Scout
   Max Health: 1
   Move Speed: 2.5
   Projectile Count: 1
   Fire Rate: 2.5
   Movement Pattern: Vertical
   Score Value: 10
   
   Drop Rates:
   - Weapon Upgrade: 0.15
   - Health: 0.10
   - Shield: 0.08
   - Rapid Fire: 0.12
   ```

---

## 🎁 Tâche 2: Créer les 4 Collectible Prefabs

### Localisation
`Assets/Prefabs/Collectibles/`

### Étapes pour CHAQUE collectible

1. **Créer le GameObject**
   - Hierarchy → Clic droit → Create Empty
   - Nom: `Collectible_[Type]`
   - Tag: "Collectible" (⚠️ CRÉER LE TAG si inexistant)

2. **Ajouter les composants**
   
   **SpriteRenderer:**
   - Sprite: (choisir un sprite coloré)
   - Sorting Order: 5
   
   **Rigidbody2D:**
   - Body Type: Kinematic
   - Gravity Scale: 0
   
   **CircleCollider2D:**
   - ⚠️ **Is Trigger: ✓ COCHÉ** (sinon ça ne marche pas!)
   
   **Script Collectible:**
   - Type: (sélectionner WeaponUpgrade / Health / Shield / RapidFire)
   - Move Speed: 2.0

3. **Créer le prefab**
   - Glisser le GameObject dans `Assets/Prefabs/Collectibles/`
   - Supprimer l'instance de la Hierarchy

4. **Répéter pour les 4 types:**
   - `Collectible_WeaponUpgrade` (⚡ jaune/orange)
   - `Collectible_Health` (❤️ rouge)
   - `Collectible_Shield` (🛡️ bleu/cyan)
   - `Collectible_RapidFire` (🔥 rouge vif)

📄 **Voir `Assets/Collectibles_Configuration.txt` pour plus de détails**

---

## 🎯 Tâche 3: Créer CollectibleSpawner dans la Scène

### Étapes

1. **Créer le GameObject**
   - Hierarchy → Clic droit → Create Empty
   - Nom: `CollectibleSpawner`
   - Position: (0, 0, 0)

2. **Ajouter le script**
   - Add Component → CollectibleSpawner

3. **Assigner les 4 prefabs**
   - Weapon Upgrade Prefab: `Collectible_WeaponUpgrade`
   - Health Prefab: `Collectible_Health`
   - Shield Prefab: `Collectible_Shield`
   - Rapid Fire Prefab: `Collectible_RapidFire`

---

## 🎮 Tâche 4: Mettre à Jour le Prefab Enemy

### Localisation
`Assets/Prefabs/Enemy.prefab` (ou dans Assets/ directement)

### Vérifications

1. **Ouvrir le prefab Enemy**
2. **Vérifier les composants:**
   - ✅ Script Enemy.cs attaché
   - ✅ enemyProjectilePrefab assigné
   - ✅ firePoint assigné
3. **Supprimer les anciennes références:**
   - Anciennes variables de sprite (enemySprite1, enemySprite3, etc.) peuvent être ignorées
   - Le sprite sera assigné par l'EnemyTypeData
4. **Sauvegarder le prefab**

---

## 🚀 Tâche 5: Assigner dans EnemySpawner

### Localisation
GameObject `EnemySpawner` dans la scène

### Étapes

1. **Sélectionner EnemySpawner** dans la Hierarchy
2. **Dans l'Inspector, assigner:**
   
   **Enemy Types:**
   - Scout Type: `EnemyType_Scout`
   - Fighter Type: `EnemyType_Fighter`
   - Bomber Type: `EnemyType_Bomber`
   - Interceptor Type: `EnemyType_Interceptor`
   - Assault Type: `EnemyType_Assault`
   - Dreadnought Type: `EnemyType_Dreadnought`
   
   **Enemy Prefab:**
   - Enemy Prefab: `Enemy` (le prefab)
   
   **Spawn Settings:**
   - Initial Spawn Rate: 2.5
   - Min Spawn Rate: 1.2
   - Screen Margin: 0.5

---

## 🧪 Tâche 6: Tests

### Tests Basiques

1. **Lancer le jeu** (Play Mode)
2. **Vérifier:**
   - [ ] Le joueur peut se déplacer (flèches)
   - [ ] Le joueur peut tirer manuellement (Espace)
   - [ ] Les ennemis apparaissent
   - [ ] Les ennemis ont des patterns de mouvement différents
   - [ ] Les ennemis tirent
   - [ ] Les projectiles du joueur touchent les ennemis
   - [ ] Les ennemis flashent en blanc quand touchés
   - [ ] Les ennemis meurent après X hits (selon leur HP)
   - [ ] Des collectibles apparaissent parfois
   - [ ] Les collectibles descendent
   - [ ] Le joueur peut ramasser les collectibles
   - [ ] Les effets des collectibles fonctionnent

### Tests de Progression

Dans la Console Unity (pendant Play Mode):

```csharp
// Passer rapidement en Phase 2
GameManager.Instance.AddScore(500);

// Passer en Phase 3
GameManager.Instance.AddScore(700);  // +700 pour total 1200

// Passer en Phase 5
GameManager.Instance.AddScore(1800); // +1800 pour total 3000
```

**Vérifier:**
- [ ] Plus d'ennemis par vague aux phases supérieures
- [ ] Types d'ennemis différents selon la phase
- [ ] Spawn rate augmente (ennemis apparaissent plus vite)
- [ ] Drop rates diminuent aux phases élevées

### Tests des Collectibles

```csharp
// Tester upgrade d'arme
FindFirstObjectByType<PlayerController>().UpgradeWeapon();
// Tirer pour voir le pattern (max 6 niveaux)

// Tester Shield
FindFirstObjectByType<PlayerController>().ActivateShield(5f);
// Se faire toucher par un ennemi → ne perd pas de vie

// Tester Rapid Fire
FindFirstObjectByType<PlayerController>().ActivateRapidFire(10f);
// Tirer → cadence x2

// Tester Health
GameManager.Instance.AddLife(1);
// Vérifier le compteur de vies
```

---

## ⚙️ Tâche 7: Équilibrage (Optionnel)

Si le jeu semble trop facile/difficile:

### Drop Rates
Modifier les valeurs dans les **EnemyTypeData assets**:
- Trop de power-ups → Réduire les drop rates (ex: 0.15 → 0.10)
- Pas assez → Augmenter (ex: 0.10 → 0.15)

### HP des Ennemis
- Trop facile → Augmenter le HP (ex: Scout 1 → 2)
- Trop difficile → Réduire le HP

### Spawn Rate
Dans EnemySpawner:
- Trop lent → Réduire Initial Spawn Rate (2.5 → 2.0)
- Trop rapide → Augmenter (2.5 → 3.0)

### Cadence de Tir
Dans les **WeaponLevel** du PlayerController.cs (code):
- Modifier les valeurs de GetFireRate()

---

## 📝 Notes Importantes

### Tags Requis
⚠️ Vérifier que ces tags existent dans Unity:
- `Player` (normalement déjà créé)
- `Projectile` (normalement déjà créé)
- `Enemy` (normalement déjà créé)
- **`Collectible`** ⚠️ **À CRÉER!**

**Créer un tag:**
1. Tags & Layers (haut de l'Inspector)
2. Tags → + → "Collectible"

### Collisions
⚠️ Les Collectibles **DOIVENT** avoir:
- Collider2D avec **Is Trigger ✓ COCHÉ**
- Tag "Collectible"

Sinon le joueur ne pourra pas les ramasser!

### Singleton
⚠️ Ne créer qu'**UN SEUL** GameObject CollectibleSpawner dans la scène!

### Sprites
Les sprites pour les collectibles et ennemis peuvent être temporaires.
Vous pouvez:
- Utiliser des sprites Unity par défaut
- Créer des sprites simples avec des couleurs
- Télécharger des assets gratuits

---

## 📚 Fichiers de Référence

Tous les détails sont dans:
1. **EnemyTypes_Configuration.txt** - Valeurs exactes pour les 6 types d'ennemis
2. **Collectibles_Configuration.txt** - Instructions pour les 4 collectibles
3. **REFONTE_SYSTEME.txt** - Vue d'ensemble complète

---

## 🎯 Checklist Finale

Avant de tester:
- [ ] 6 EnemyTypeData créés et configurés
- [ ] 4 Collectible prefabs créés avec tag "Collectible"
- [ ] Tag "Collectible" créé dans Unity
- [ ] CollectibleSpawner dans la scène avec prefabs assignés
- [ ] EnemySpawner avec 6 EnemyTypeData assignés
- [ ] Enemy prefab vérifié
- [ ] Compilation sans erreurs

**Puis Play Mode et tester!**

---

## 🎮 Contrôles du Jeu

- **Flèches** : Déplacer le vaisseau
- **Espace** : Tirer (maintenir appuyé pour tir continu)
- **R** : Redémarrer après Game Over

---

## 🚀 Bon jeu !

Le système est maintenant complet au niveau code. Il ne reste plus qu'à créer les assets Unity et assigner les références. Bonne chance! 🎯
