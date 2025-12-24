using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Types")]
    public EnemyTypeData scoutType;       // HP 1
    public EnemyTypeData fighterType;     // HP 2
    public EnemyTypeData bomberType;      // HP 3
    public EnemyTypeData interceptorType; // HP 2
    public EnemyTypeData assaultType;     // HP 4
    public EnemyTypeData dreadnoughtType; // HP 5
    
    // Prefab de l'ennemi à spawner
    public GameObject enemyPrefab;
    
    // Intervalle de temps initial entre chaque spawn (en secondes)
    public float initialSpawnRate = 2.5f;
    
    // Intervalle minimum (vitesse maximale)
    public float minSpawnRate = 1.2f;
    
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
    
    // Méthode appelée pour mettre à jour la fréquence de spawn
    public void UpdateSpawnRate(float newRate)
    {
        currentSpawnRate = Mathf.Max(newRate, minSpawnRate);
        Debug.Log($"Spawn rate mis à jour: {currentSpawnRate:F2}s");
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("Enemy Prefab n'est pas assigné !");
            return;
        }
        
        if (GameManager.Instance == null) return;
        
        int score = GameManager.Instance.GetScore();
        
        // Déterminer le nombre d'ennemis et le spawn rate selon le score
        int enemyCount = GetEnemyCount(score);
        currentSpawnRate = GetSpawnRate(score);
        
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
            
            // Déterminer le type d'ennemi selon la phase
            EnemyTypeData enemyType = SelectEnemyType(score);
            
            // Assigner le type à l'ennemi
            Enemy enemyScript = enemyObject.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.enemyType = enemyType;
            }
        }
        
        Debug.Log($"Vague de {enemyCount} ennemi(s) spawnée (score: {score})");
    }
    
    // Barème progressif : Nombre d'ennemis par vague
    int GetEnemyCount(int score)
    {
        if (score < 500) return 1;
        if (score < 1200) return Random.Range(1, 3); // 1-2
        if (score < 2000) return 2;
        if (score < 3000) return Random.Range(2, 4); // 2-3
        if (score < 4500) return 3;
        if (score < 6000) return Random.Range(3, 5); // 3-4
        return 4; // 6000+
    }
    
    // Barème progressif : Fréquence de spawn
    float GetSpawnRate(int score)
    {
        if (score < 500) return 2.5f;
        if (score < 1200) return 2.2f;
        if (score < 2000) return 2.0f;
        if (score < 3000) return 1.8f;
        if (score < 4500) return 1.6f;
        if (score < 6000) return 1.4f;
        return 1.2f; // 6000+
    }
    
    // Sélectionner un type d'ennemi selon la distribution des phases
    EnemyTypeData SelectEnemyType(int score)
    {
        float roll = Random.value * 100f;
        
        // Phase 1 (0-500): 100% Scout
        if (score < 500)
        {
            return scoutType;
        }
        
        // Phase 2 (500-1200): 70% Scout, 30% Fighter
        if (score < 1200)
        {
            if (roll < 70f) return scoutType;
            return fighterType;
        }
        
        // Phase 3 (1200-2000): 40% Scout, 40% Fighter, 20% Bomber
        if (score < 2000)
        {
            if (roll < 40f) return scoutType;
            if (roll < 80f) return fighterType;
            return bomberType;
        }
        
        // Phase 4 (2000-3000): 20% Scout, 30% Fighter, 30% Bomber, 20% Interceptor
        if (score < 3000)
        {
            if (roll < 20f) return scoutType;
            if (roll < 50f) return fighterType;
            if (roll < 80f) return bomberType;
            return interceptorType;
        }
        
        // Phase 5 (3000-4500): 10% Fighter, 25% Bomber, 25% Interceptor, 30% Assault, 10% Dreadnought
        if (score < 4500)
        {
            if (roll < 10f) return fighterType;
            if (roll < 35f) return bomberType;
            if (roll < 60f) return interceptorType;
            if (roll < 90f) return assaultType;
            return dreadnoughtType;
        }
        
        // Phase 6 (4500-6000): 5% Scout, 15% Fighter, 20% Bomber, 20% Interceptor, 25% Assault, 15% Dreadnought
        if (score < 6000)
        {
            if (roll < 5f) return scoutType;
            if (roll < 20f) return fighterType;
            if (roll < 40f) return bomberType;
            if (roll < 60f) return interceptorType;
            if (roll < 85f) return assaultType;
            return dreadnoughtType;
        }
        
        // Phase 7 (6000+): 10% Bomber, 20% Interceptor, 40% Assault, 30% Dreadnought
        if (roll < 10f) return bomberType;
        if (roll < 30f) return interceptorType;
        if (roll < 70f) return assaultType;
        return dreadnoughtType;
    }
    
    // Méthode pour réinitialiser la difficulté
    public void ResetDifficulty()
    {
        currentSpawnRate = initialSpawnRate;
    }
}
