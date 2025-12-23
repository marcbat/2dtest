using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Prefab de l'ennemi à spawner
    public GameObject enemyPrefab;
    
    // Intervalle de temps initial entre chaque spawn (en secondes)
    public float initialSpawnRate = 2f;
    
    // Intervalle minimum (vitesse maximale)
    public float minSpawnRate = 0.3f;
    
    // Réduction du délai à chaque augmentation de difficulté
    public float spawnRateDecrease = 0.15f;
    
    // Marge par rapport aux bords de l'écran
    public float screenMargin = 0.5f;
    
    // Zone de spawn calculée dynamiquement
    private float spawnRangeX;
    
    // Temps avant le prochain spawn
    private float nextSpawnTime = 0f;
    
    // Délai actuel entre les spawns
    private float currentSpawnRate;

    void Start()
    {
        // Calculer la zone de spawn en fonction de la caméra
        CalculateSpawnRange();
        
        // Initialiser le délai de spawn
        currentSpawnRate = initialSpawnRate;
        
        // Premier spawn immédiat
        nextSpawnTime = Time.time + 1f;
    }
    
    void CalculateSpawnRange()
    {
        if (Camera.main == null)
        {
            Debug.LogError("Aucune caméra principale trouvée !");
            spawnRangeX = 8f;
            return;
        }
        
        // Obtenir les limites de la caméra en coordonnées monde
        float camHeight = Camera.main.orthographicSize * 2f;
        float camWidth = camHeight * Camera.main.aspect;
        
        // Définir la zone de spawn avec une marge
        spawnRangeX = (camWidth / 2f) - screenMargin;
        
        Debug.Log($"Zone de spawn calculée: X entre -{spawnRangeX:F2} et {spawnRangeX:F2}");
    }

    void Update()
    {
        // Vérifier s'il est temps de spawner un ennemi
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            
            // Calculer le prochain spawn
            nextSpawnTime = Time.time + currentSpawnRate;
        }
    }
    
    // Méthode appelée par le GameManager pour augmenter la fréquence de spawn
    public void IncreaseSpawnFrequency()
    {
        if (currentSpawnRate > minSpawnRate)
        {
            currentSpawnRate -= spawnRateDecrease;
            currentSpawnRate = Mathf.Max(currentSpawnRate, minSpawnRate);
            Debug.Log($"Fréquence de spawn augmentée ! Délai: {currentSpawnRate:F2}s (spawn toutes les {currentSpawnRate:F2}s)");
        }
        else
        {
            Debug.Log("Fréquence de spawn maximale atteinte !");
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("Enemy Prefab n'est pas assigné !");
            return;
        }
        
        // Position aléatoire en X, fixe en Y (en haut de l'écran)
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0f);
        
        // Créer l'ennemi
        GameObject enemyObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        
        // Calculer le level de l'ennemi selon le niveau du joueur (basé sur le score)
        if (GameManager.Instance != null)
        {
            int currentScore = GameManager.Instance.GetScore();
            int enemyLevel = CalculateEnemyLevel(currentScore);
            
            // Assigner le level à l'ennemi
            Enemy enemyScript = enemyObject.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.level = enemyLevel;
                Debug.Log($"Ennemi spawné avec level {enemyLevel} (score: {currentScore})");
            }
        }
    }
    
    // Calculer le level de l'ennemi en fonction du score du joueur
    // Les ennemis sont du niveau du joueur ou proche, pour maintenir l'équilibre
    int CalculateEnemyLevel(int score)
    {
        // Calculer le niveau du joueur (tous les 250 points)
        int playerLevel = Mathf.Clamp(1 + (score / 250), 1, 4);
        
        float random = Random.Range(0f, 100f);
        
        switch (playerLevel)
        {
            case 1: // Score 0-249 : Uniquement des ennemis Level 1
                return 1;
                
            case 2: // Score 250-499 : Principalement Level 2, quelques Level 1
                return random < 20f ? 1 : 2;
                
            case 3: // Score 500-749 : Principalement Level 3, un peu de Level 2 et 4
                if (random < 15f) return 2;
                else if (random < 80f) return 3;
                else return 4;
                
            case 4: // Score 750+ : Principalement Level 4, quelques Level 3
                return random < 30f ? 3 : 4;
                
            default:
                return 1;
        }
    }
    
    // Méthode pour réinitialiser la difficulté
    public void ResetDifficulty()
    {
        currentSpawnRate = initialSpawnRate;
    }
}
