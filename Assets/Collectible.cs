using UnityEngine;

public enum CollectibleType
{
    WeaponUpgrade,
    Health,
    Shield,
    RapidFire
}

public class Collectible : MonoBehaviour
{
    public CollectibleType type;
    public float moveSpeed = 2f;
    public AudioClip collectSound;
    
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    
    void Update()
    {
        // Descendre lentement
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        
        // Détruire si hors écran
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                ApplyEffect(player);
                
                // Jouer le son de collecte
                if (collectSound != null)
                {
                    AudioSource.PlayClipAtPoint(collectSound, transform.position);
                }
                
                Destroy(gameObject);
            }
        }
    }
    
    void ApplyEffect(PlayerController player)
    {
        switch (type)
        {
            case CollectibleType.WeaponUpgrade:
                player.UpgradeWeapon();
                Debug.Log("Weapon upgraded!");
                break;
                
            case CollectibleType.Health:
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.AddLife(1);
                }
                Debug.Log("Health collected!");
                break;
                
            case CollectibleType.Shield:
                player.ActivateShield(5f); // 5 secondes d'invincibilité
                Debug.Log("Shield activated!");
                break;
                
            case CollectibleType.RapidFire:
                player.ActivateRapidFire(10f); // 10 secondes de cadence x2
                Debug.Log("Rapid fire activated!");
                break;
        }
    }
}
