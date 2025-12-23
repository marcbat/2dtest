using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Prefab de l'ennemi à spawner
    public GameObject enemyPrefab;
    
    // Intervalle de temps entre chaque vague (en secondes)
    public float spawnRate = 3f;
    
    // Nombre d'ennemis au départ
    public int initialEnemyCount = 1;
    
    // Nombre maximum d'ennemis par vague
    public int maxEnemyCount = 8;
    
    // Marge par rapport aux bords de l'écran
    public float screenMargin = 0.5f;
    
    // Zone de spawn calculée dynamiquement
    private float spawnRangeX;
    
    // Temps avant le prochain spawn
    private float nextSpawnTime = 0f;
    
    // Nombre actuel d'ennemis par vague
    private int currentEnemyCount;

    void Start()
    {
        // Calculer la zone de spawn en fonction de la caméra
        CalculateSpawnRange();
        
        // Initialiser le nombre d'ennemis
        currentEnemyCount = initialEnemyCount;
        
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
        // Vérifier s'il est temps de spawner une vague
        if (Time.time >= nextSpawnTime)
        {
            SpawnWave();
            
            // Calculer le prochain spawn
            nextSpawnTime = Time.time + spawnRate;
        }
    }
    
    // Méthode appelée par le GameManager pour augmenter le nombre d'ennemis
    public void IncreaseEnemyCount()
    {
        if (currentEnemyCount < maxEnemyCount)
        {
            currentEnemyCount++;
            Debug.Log($"Ennemis par vague augmentés à {currentEnemyCount}");
        }
    }

    void SpawnWave()
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("Enemy Prefab n'est pas assigné !");
            return;
        }
        
        // Spawner plusieurs ennemis
        for (int i = 0; i < currentEnemyCount; i++)
        {
            // Position aléatoire en X, fixe en Y (en haut de l'écran)
            float randomX = Random.Range(-spawnRangeX, spawnRangeX);
            Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0f);
            
            // Créer l'ennemi
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
    
    // Méthode pour réinitialiser la difficulté
    public void ResetDifficulty()
    {
        currentEnemyCount = initialEnemyCount;
    }
}
