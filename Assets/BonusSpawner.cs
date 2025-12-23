using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    // Prefab du bonus à spawner
    public GameObject bonusPrefab;
    
    // Intervalle de temps entre chaque spawn (en secondes) - beaucoup plus long que les ennemis
    public float spawnRate = 15f;
    
    // Variation aléatoire du délai de spawn (pour rendre imprévisible)
    public float spawnRateVariation = 5f;
    
    // Marge par rapport aux bords de l'écran
    public float screenMargin = 0.5f;
    
    // Zone de spawn calculée dynamiquement
    private float spawnRangeX;
    
    // Temps avant le prochain spawn
    private float nextSpawnTime = 0f;

    void Start()
    {
        // Calculer la zone de spawn en fonction de la caméra
        CalculateSpawnRange();
        
        // Premier spawn après un délai
        ScheduleNextSpawn();
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
    }
    
    void ScheduleNextSpawn()
    {
        // Ajouter une variation aléatoire au délai de spawn
        float randomVariation = Random.Range(-spawnRateVariation, spawnRateVariation);
        nextSpawnTime = Time.time + spawnRate + randomVariation;
        
        Debug.Log($"Prochain bonus dans {(spawnRate + randomVariation):F1}s");
    }

    void Update()
    {
        // Vérifier s'il est temps de spawner un bonus
        if (Time.time >= nextSpawnTime)
        {
            SpawnBonus();
            
            // Programmer le prochain spawn
            ScheduleNextSpawn();
        }
    }

    void SpawnBonus()
    {
        if (bonusPrefab == null)
        {
            Debug.LogWarning("Bonus Prefab n'est pas assigné !");
            return;
        }
        
        // Position aléatoire en X, fixe en Y (en haut de l'écran)
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0f);
        
        // Créer le bonus
        Instantiate(bonusPrefab, spawnPosition, Quaternion.identity);
        
        Debug.Log("Bonus vie spawné à X=" + randomX);
    }
}
