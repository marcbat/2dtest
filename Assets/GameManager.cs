using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    // Singleton pour accès global
    public static GameManager Instance { get; private set; }
    
    // Score du joueur
    private int score = 0;
    
    // Nombre d'ennemis tués
    private int enemiesKilled = 0;
    
    // Meilleur score (high score)
    private int highScore = 0;
    
    // Chemin du fichier de sauvegarde
    private string savePath;
    
    // Points nécessaires pour augmenter la difficulté
    public int pointsPerDifficultyLevel = 100;
    
    // Dernier seuil de difficulté atteint
    private int lastDifficultyThreshold = 0;
    
    // Paliers de progression du joueur
    private int lastFireRateMilestone = 0;
    private int lastProjectileMilestone = 0;
    private int lastSpeedMilestone = 0;
    
    // Nombre de vies
    public int lives = 3;
    
    // Game Over
    private bool isGameOver = false;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Définir le chemin de sauvegarde dans le profil utilisateur
        savePath = Path.Combine(Application.persistentDataPath, "savegame.json");
        
        // Charger le high score
        LoadGame();
        
        Debug.Log("Jeu démarré - Vies: " + lives + " | Score: " + score + " | High Score: " + highScore);
    }

    void Update()
    {
        // Redémarrer le jeu avec R en cas de Game Over
        if (isGameOver && Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }

    // Ajouter des points au score
    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
        
        // Vérifier les paliers de progression
        CheckPlayerProgression();
        
        // Vérifier si on doit augmenter la difficulté
        CheckDifficultyIncrease();
    }
    
    // Ajouter des vies
    public void AddLife(int amount)
    {
        lives += amount;
        Debug.Log($"Vie gagnée ! Vies restantes: {lives}");
    }
    
    // Incrémenter le compteur d'ennemis tués
    public void AddEnemyKilled()
    {
        enemiesKilled++;
    }
    
    // Vérifier et appliquer la progression du joueur selon le barème
    void CheckPlayerProgression()
    {
        // CADENCE DE TIR : Amélioration tous les 150 points (démarre plus rapide : 0.6s → 0.12s)
        int fireRateMilestone = (score / 150) * 150;
        if (fireRateMilestone > lastFireRateMilestone && fireRateMilestone > 0)
        {
            lastFireRateMilestone = fireRateMilestone;
            IncreasePlayerFireRate();
            Debug.Log($"[{score} pts] Cadence de tir améliorée !");
        }
        
        // NOMBRE DE PROJECTILES : Progression lente tous les 400 points (1 → 2 → 3 → 4 → 5)
        // 0: 1 proj, 400: 2 proj, 800: 3 proj, 1200: 4 proj, 1600: 5 proj (max)
        int projectileMilestone = (score / 400) * 400;
        if (projectileMilestone > lastProjectileMilestone && projectileMilestone > 0)
        {
            lastProjectileMilestone = projectileMilestone;
            IncreasePlayerProjectiles();
            Debug.Log($"[{score} pts] Nombre de projectiles augmenté !");
        }
        
        // VITESSE : Amélioration tous les 200 points
        int speedMilestone = (score / 200) * 200;
        if (speedMilestone > lastSpeedMilestone && speedMilestone > 0)
        {
            lastSpeedMilestone = speedMilestone;
            IncreasePlayerSpeed();
            Debug.Log($"[{score} pts] Vitesse augmentée !");
        }
    }
    
    // Augmenter la cadence de tir du joueur
    void IncreasePlayerFireRate()
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null)
        {
            player.IncreaseFireRate();
        }
    }
    
    // Augmenter le nombre de projectiles du joueur
    void IncreasePlayerProjectiles()
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null)
        {
            player.IncreaseProjectileCount();
        }
        else
        {
            Debug.LogWarning("PlayerController non trouvé pour augmenter les projectiles !");
        }
    }
    
    // Augmenter la vitesse du joueur
    void IncreasePlayerSpeed()
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null)
        {
            player.IncreaseSpeed();
        }
    }
    
    // Vérifier et augmenter la difficulté selon le score
    void CheckDifficultyIncrease()
    {
        int currentThreshold = (score / pointsPerDifficultyLevel) * pointsPerDifficultyLevel;
        
        // Si on a franchi un nouveau palier de 100 points
        if (currentThreshold > lastDifficultyThreshold)
        {
            lastDifficultyThreshold = currentThreshold;
            IncreaseDifficulty();
            
            Debug.Log($"Niveau de difficulté augmenté ! Seuil: {currentThreshold} points");
        }
    }
    
    // Augmenter la difficulté globale
    void IncreaseDifficulty()
    {
        // Augmenter la fréquence de spawn des ennemis
        EnemySpawner spawner = FindFirstObjectByType<EnemySpawner>();
        if (spawner != null)
        {
            spawner.IncreaseSpawnFrequency();
        }
    }

    // Perdre une vie
    public void LoseLife()
    {
        lives--;
        Debug.Log("Vie perdue ! Vies restantes: " + lives);
        
        // Faire clignoter le vaisseau
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null)
        {
            player.OnHit();
        }
        
        if (lives <= 0)
        {
            GameOver();
        }
    }

    // Game Over
    void GameOver()
    {
        isGameOver = true;
        
        // Vérifier et sauvegarder le high score
        if (score > highScore)
        {
            highScore = score;
            SaveGame();
            Debug.Log("=== NOUVEAU RECORD ! === Score: " + score);
        }
        
        Debug.Log("=== GAME OVER === Score final: " + score + " | Appuyez sur R pour recommencer");
        
        // Arrêter le temps (pause)
        Time.timeScale = 0f;
    }

    // Redémarrer le jeu
    void RestartGame()
    {
        // Remettre le temps à la normale
        Time.timeScale = 1f;
        
        // Recharger la scène
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    // Sauvegarder le jeu dans un fichier JSON
    void SaveGame()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        
        Debug.Log("Jeu sauvegardé : " + savePath);
    }
    
    // Charger le jeu depuis le fichier JSON
    void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            
            highScore = data.highScore;
            
            Debug.Log("Jeu chargé - High Score: " + highScore);
        }
        else
        {
            Debug.Log("Aucune sauvegarde trouvée, création d'une nouvelle partie");
            highScore = 0;
        }
    }

    // Getters pour l'affichage (si vous ajoutez une UI plus tard)
    public int GetScore()
    {
        return score;
    }
    
    public int GetHighScore()
    {
        return highScore;
    }
    
    public int GetEnemiesKilled()
    {
        return enemiesKilled;
    }

    public int GetLives()
    {
        return lives;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
