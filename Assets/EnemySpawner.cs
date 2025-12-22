using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Prefab de l'ennemi à spawner
    public GameObject enemyPrefab;
    
    // Intervalle de temps entre chaque spawn (en secondes)
    public float spawnRate = 2f;
    
    // Intervalle minimum (quand la difficulté augmente)
    public float minSpawnRate = 0.5f;
    
    // Vitesse à laquelle la difficulté augmente
    public float difficultyIncrease = 0.05f;
    
    // Zone de spawn (largeur)
    public float spawnRangeX = 8f;
    
    // Temps avant le prochain spawn
    private float nextSpawnTime = 0f;

    void Start()
    {
        // Premier spawn immédiat
        nextSpawnTime = Time.time + 1f;
    }

    void Update()
    {
        // Vérifier s'il est temps de spawner un ennemi
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            
            // Calculer le prochain spawn (avec augmentation progressive de la difficulté)
            spawnRate = Mathf.Max(minSpawnRate, spawnRate - difficultyIncrease);
            nextSpawnTime = Time.time + spawnRate;
            
            Debug.Log("Prochain spawn dans : " + spawnRate + "s");
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
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        
        Debug.Log("Ennemi spawné à X=" + randomX);
    }
    
    // Méthode pour réinitialiser la difficulté
    public void ResetDifficulty()
    {
        spawnRate = 2f;
    }
}
