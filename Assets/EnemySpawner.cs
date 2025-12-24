using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Prefab de l'ennemi à spawner
    public GameObject enemyPrefab;
    
    // Intervalle de temps initial entre chaque spawn (en secondes)
    public float initialSpawnRate = 2.0f;
    
    // Intervalle minimum (vitesse maximale)
    public float minSpawnRate = 0.8f;
    
    // Réduction du délai à chaque augmentation de difficulté
    public float spawnRateDecrease = 0.1f;
    
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
        
        // Déterminer le nombre d'ennemis à spawner selon le score
        int enemyCount = CalculateEnemyCount();
        
        // Spawner plusieurs ennemis
        for (int i = 0; i < enemyCount; i++)
        {
            // Position aléatoire en X avec espacement si plusieurs ennemis
            float randomX;
            if (enemyCount == 1)
            {
                randomX = Random.Range(-spawnRangeX, spawnRangeX);
            }
            else
            {
                // Espacer les ennemis pour éviter qu'ils se chevauchent
                float spacing = (spawnRangeX * 2f) / (enemyCount + 1);
                randomX = -spawnRangeX + spacing * (i + 1) + Random.Range(-0.5f, 0.5f);
            }
            
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
                }
            }
        }
        
        if (GameManager.Instance != null)
        {
            Debug.Log($"Vague de {enemyCount} ennemi(s) spawnée (score: {GameManager.Instance.GetScore()})");
        }
    }
    
    // Calculer le nombre d'ennemis par vague selon le barème progressif
    int CalculateEnemyCount()
    {
        if (GameManager.Instance == null) return 1;
        
        int score = GameManager.Instance.GetScore();
        
        // Phase 1 (0-300) : 1 ennemi
        if (score < 300) return 1;
        
        // Phase 2 (300-800) : 2 ennemis
        if (score < 800) return 2;
        
        // Phase 3 (800-1300) : 2-3 ennemis (aléatoire)
        if (score < 1300) return Random.Range(0f, 100f) < 50f ? 2 : 3;
        
        // Phase 4 (1300-2000) : 3 ennemis
        if (score < 2000) return 3;
        
        // Phase 5 (2000-3000) : 3-4 ennemis (aléatoire)
        if (score < 3000) return Random.Range(0f, 100f) < 50f ? 3 : 4;
        
        // Phase 6 (3000-4000) : 4 ennemis
        if (score < 4000) return 4;
        
        // Phase 7 (4000+) : 5 ennemis (maximum)
        return 5;
    }
    
    // Calculer le level de l'ennemi en fonction du score du joueur
    // Les ennemis sont du niveau du joueur ou proche, pour maintenir l'équilibre
    int CalculateEnemyLevel(int score)
    {
        float random = Random.Range(0f, 100f);
        
        // Phase 1 (0-500) : 100% Level 1
        if (score < 500)
        {
            return 1;
        }
        // Phase 2 (500-1000) : 80% Level 1, 20% Level 2
        else if (score < 1000)
        {
            return random < 80f ? 1 : 2;
        }
        // Phase 3 (1000-1500) : 50% Level 1, 40% Level 2, 10% Level 3
        else if (score < 1500)
        {
            if (random < 50f) return 1;
            else if (random < 90f) return 2;
            else return 3;
        }
        // Phase 4 (1500-2000) : 30% Level 1, 50% Level 2, 20% Level 3
        else if (score < 2000)
        {
            if (random < 30f) return 1;
            else if (random < 80f) return 2;
            else return 3;
        }
        // Phase 5 (2000-3000) : 10% Level 1, 40% Level 2, 40% Level 3, 10% Level 4
        else if (score < 3000)
        {
            if (random < 10f) return 1;
            else if (random < 50f) return 2;
            else if (random < 90f) return 3;
            else return 4;
        }
        // Phase 6 (3000-4000) : 5% Level 1, 25% Level 2, 50% Level 3, 20% Level 4
        else if (score < 4000)
        {
            if (random < 5f) return 1;
            else if (random < 30f) return 2;
            else if (random < 80f) return 3;
            else return 4;
        }
        // Phase 7 (4000+) : 20% Level 2, 50% Level 3, 30% Level 4
        else
        {
            if (random < 20f) return 2;
            else if (random < 70f) return 3;
            else return 4;
        }
    }
    
    // Méthode pour réinitialiser la difficulté
    public void ResetDifficulty()
    {
        currentSpawnRate = initialSpawnRate;
    }
}
