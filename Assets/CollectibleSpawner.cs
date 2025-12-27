using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject weaponUpgradePrefab;
    public GameObject healthPrefab;
    public GameObject shieldPrefab;
    public GameObject rapidFirePrefab;
    
    // Spawn un collectible aléatoire à une position donnée selon les drop rates
    public void SpawnCollectible(Vector3 position, EnemyTypeData enemyType)
    {
        if (enemyType == null) return;
        
        float roll = Random.value;
        float cumulative = 0f;
        
        // Weapon Upgrade
        cumulative += GetAdjustedDropRate(enemyType.weaponUpgradeDropRate);
        if (roll < cumulative && weaponUpgradePrefab != null)
        {
            Instantiate(weaponUpgradePrefab, position, Quaternion.identity);
            return;
        }
        
        // Health
        cumulative += GetAdjustedDropRate(enemyType.healthDropRate);
        if (roll < cumulative && healthPrefab != null)
        {
            Instantiate(healthPrefab, position, Quaternion.identity);
            return;
        }
        
        // Shield
        cumulative += GetAdjustedDropRate(enemyType.shieldDropRate);
        if (roll < cumulative && shieldPrefab != null)
        {
            Instantiate(shieldPrefab, position, Quaternion.identity);
            return;
        }
        
        // Rapid Fire
        cumulative += GetAdjustedDropRate(enemyType.rapidFireDropRate);
        if (roll < cumulative && rapidFirePrefab != null)
        {
            Instantiate(rapidFirePrefab, position, Quaternion.identity);
            return;
        }
    }
    
    // Ajuster les drop rates selon le score (dégressif)
    float GetAdjustedDropRate(float baseRate)
    {
        if (GameManager.Instance == null) return baseRate;
        
        int score = GameManager.Instance.GetScore();
        
        // Phase 1 (0-500): 100% des drop rates
        if (score < 500) return baseRate * 1.0f;
        
        // Phase 2 (500-1200): 90%
        if (score < 1200) return baseRate * 0.9f;
        
        // Phase 3 (1200-2000): 80%
        if (score < 2000) return baseRate * 0.8f;
        
        // Phase 4 (2000-3000): 70%
        if (score < 3000) return baseRate * 0.7f;
        
        // Phase 5 (3000-4500): 60%
        if (score < 4500) return baseRate * 0.6f;
        
        // Phase 6 (4500-6000): 50%
        if (score < 6000) return baseRate * 0.5f;
        
        // Phase 7 (6000+): 40%
        return baseRate * 0.4f;
    }
}
