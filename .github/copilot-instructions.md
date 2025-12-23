# Instructions Copilot - Projet Unity 2D

## Informations du projet

### Configuration technique
- **Moteur** : Unity (projet 2D)
- **Pipeline de rendu** : Universal Render Pipeline (URP)
- **Input System** : Nouveau Input System (Input System package) - **NE PAS utiliser** `UnityEngine.Input`
- **Version Control** : Git avec .gitignore Unity standard

### Structure du projet
- **Scène principale** : `Assets/Scenes/SampleScene.unity`
- **Scripts** : `Assets/`

### Objets de jeu
- **MonPremierObjet** : Premier objet créé (triangle/sprite)
  - Composant : `PlayerController`
  - Fonction : Rotation contrôlée par les flèches du clavier

### Scripts existants

#### PlayerController.cs
- **Emplacement** : `Assets/PlayerController.cs`
- **Fonction** : Contrôle la rotation d'un objet avec les flèches gauche/droite
- **Input System** : Utilise `UnityEngine.InputSystem` avec `Keyboard.current`
- **Paramètres** :
  - `rotationSpeed` : 100f (vitesse de rotation en degrés/seconde)

## Règles de développement

### Input System
⚠️ **IMPORTANT** : Ce projet utilise le **nouveau Input System**.
- Toujours ajouter `using UnityEngine.InputSystem;`
- Utiliser `Keyboard.current`, `Mouse.current`, etc.
- **NE JAMAIS utiliser** `Input.GetKey()`, `Input.GetAxis()`, etc. de l'ancien système

### Convention de code
- Langue des commentaires : Français
- Langue du code : Anglais (noms de variables, méthodes, classes)
- Utiliser `Debug.Log()` pour le débogage

### Git
- Les dossiers `Library/`, `Temp/`, `Logs/`, `UserSettings/` sont ignorés
- Commiter uniquement `Assets/`, `ProjectSettings/`, `Packages/`
- **Messages de commit** : TOUJOURS en français
  - Exemple : "Ajout du système de tir" ✅
  - Exemple : "Add shooting system" ❌

## Historique des modifications
- 22/12/2025 : Création du projet Unity 2D
- 22/12/2025 : Ajout de PlayerController avec rotation par Input System
- 22/12/2025 : Configuration Git avec .gitignore Unity
