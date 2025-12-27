using UnityEngine;

// Enum pour les patterns de mouvement
public enum MovementPattern
{
    Vertical,           // Descente droite
    DiagonalSoft,       // ±30° max par rapport verticale
    VerticalZigzag,     // Oscille horizontalement
    DiagonalFast,       // ±45° par rapport verticale
    Sinusoidal,         // Mouvement sinusoïdal
    Circular            // Mouvement circulaire + descente
}

[CreateAssetMenu(fileName = "EnemyType", menuName = "Game/EnemyType")]
public class EnemyTypeData : ScriptableObject
{
    [Header("Identification")]
    public string enemyName;
    
    [Header("Combat")]
    public int maxHealth = 1;
    public float moveSpeed = 2.0f;
    public int projectileCount = 1;
    public float fireRate = 2.5f;
    
    [Header("Movement")]
    public MovementPattern movementPattern = MovementPattern.Vertical;
    
    [Header("Rewards")]
    public int scoreValue = 10;
    
    [Header("Visuals")]
    public Sprite sprite;
    public Color damageFlashColor = Color.white;
    
    [Header("Audio")]
    public AudioClip fireSound;
    public AudioClip explosionSound;
    
    [Header("Collectibles Drop Rates")]
    [Range(0f, 1f)]
    public float weaponUpgradeDropRate = 0.15f;
    [Range(0f, 1f)]
    public float healthDropRate = 0.10f;
    [Range(0f, 1f)]
    public float shieldDropRate = 0.08f;
    [Range(0f, 1f)]
    public float rapidFireDropRate = 0.12f;
}
