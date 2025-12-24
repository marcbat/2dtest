using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Level de l'ennemi (1 à 4)
    public int level = 1;
    
    // Vitesse de déplacement vers le bas
    public float speed = 3f;
    
    // Points donnés quand l'ennemi est détruit
    private int scoreValue = 10;
    
    // Prefab du projectile ennemi à instancier
    public GameObject enemyProjectilePrefab;
    
    // Point de spawn du projectile (position devant l'ennemi)
    public Transform firePoint;
    
    // Délai entre deux tirs (cadence de tir)
    public float fireRate = 2f;
    
    // Temps depuis le dernier tir
    private float nextFireTime = 0f;
    
    // Nombre de projectiles que cet ennemi tire (déterminé au spawn)
    private int projectileCount = 1;
    
    // Direction de déplacement (verticale avec légère diagonale)
    private Vector3 moveDirection;
    
    // Référence au SpriteRenderer pour changer le sprite
    private SpriteRenderer spriteRenderer;
    
    // Sprites selon le niveau de dangerosite
    public Sprite enemySprite1; // 1 projectile - Faible
    public Sprite enemySprite3; // 3 projectiles - Moyen
    public Sprite enemySprite5; // 5 projectiles - Dangereux
    public Sprite enemySprite6; // 6 projectiles - Très dangereux
    
    // Sons
    public AudioClip fireSound;
    public AudioClip explosionSound;
    
    // AudioSource pour jouer les sons
    private AudioSource audioSource;

    void Start()
    {
        // Obtenir le SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Récupérer ou créer l'AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // Déterminer une direction diagonale adaptée à la position de spawn
        CalculateSafeDirection();
        
        // Configurer l'ennemi selon son level
        ConfigureByLevel();
        
        // Premier tir après un délai aléatoire (pour varier les tirs)
        nextFireTime = Time.time + Random.Range(0.5f, 2f);
    }
    
    // Calculer une direction qui garantit que l'ennemi reste visible jusqu'en bas
    void CalculateSafeDirection()
    {
        float posX = transform.position.x;
        float randomX;
        
        // Si l'ennemi spawn à gauche (x < -3), il doit aller vers le centre/droite
        if (posX < -3f)
        {
            randomX = Random.Range(0.3f, 1f); // Force vers la droite
        }
        // Si l'ennemi spawn à droite (x > 3), il doit aller vers le centre/gauche
        else if (posX > 3f)
        {
            randomX = Random.Range(-1f, -0.3f); // Force vers la gauche
        }
        // Si l'ennemi spawn au centre, trajectoire libre
        else
        {
            randomX = Random.Range(-1f, 1f); // Libre dans toutes les directions
        }
        
        moveDirection = new Vector3(randomX, -1f, 0f).normalized;
    }
    
    // Configurer l'ennemi selon son level
    void ConfigureByLevel()
    {
        switch (level)
        {
            case 1:
                projectileCount = 1;
                fireRate = 2.5f;
                scoreValue = 10;
                if (enemySprite1 != null)
                    spriteRenderer.sprite = enemySprite1;
                break;
            case 2:
                projectileCount = 3;
                fireRate = 2.0f;
                scoreValue = 25;
                if (enemySprite3 != null)
                    spriteRenderer.sprite = enemySprite3;
                break;
            case 3:
                projectileCount = 5;
                fireRate = 1.5f;
                scoreValue = 50;
                if (enemySprite5 != null)
                    spriteRenderer.sprite = enemySprite5;
                break;
            case 4:
                projectileCount = 6;
                fireRate = 1.0f;
                scoreValue = 100;
                if (enemySprite6 != null)
                    spriteRenderer.sprite = enemySprite6;
                break;
            default:
                projectileCount = 1;
                fireRate = 2.5f;
                scoreValue = 10;
                if (enemySprite1 != null)
                    spriteRenderer.sprite = enemySprite1;
                break;
        }
        
        Debug.Log($"Ennemi Level {level} configuré: {projectileCount} projectiles, cadence {fireRate}s, {scoreValue} points");
    }

    void Update()
    {
        // Déplacement selon la direction diagonale déterminée au spawn
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
        
        // Détruire si hors écran (en dessous ou sur les côtés)
        if (transform.position.y < -6f || Mathf.Abs(transform.position.x) > 10f)
        {
            Destroy(gameObject);
            return;
        }
        
        // Gérer le tir automatique
        if (Time.time >= nextFireTime && enemyProjectilePrefab != null)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }
    
    void Fire()
    {
        // Déterminer la position de spawn
        Vector3 spawnPosition = firePoint != null ? firePoint.position : transform.position;
        
        // Tirer les projectiles (utilise le nombre déterminé au spawn)
        FireBurst(spawnPosition, projectileCount);
        
        // Jouer le son de tir une seule fois
        if (fireSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
        
        Debug.Log($"Ennemi a tiré {projectileCount} projectile(s) !");
    }
    
    void FireBurst(Vector3 spawnPosition, int projectileCount)
    {
        // Patterns de tir selon le nombre de projectiles
        switch (projectileCount)
        {
            case 1:
                // 1 projectile : tout droit
                CreateProjectile(spawnPosition, Vector3.down);
                break;
                
            case 3:
                // 3 projectiles : centre + 60° gauche/droite
                CreateProjectile(spawnPosition, Vector3.down);
                CreateProjectile(spawnPosition, new Vector3(-1f, -0.6f, 0f)); // ~60° gauche
                CreateProjectile(spawnPosition, new Vector3(1f, -0.6f, 0f));  // ~60° droite
                break;
                
            case 5:
                // 5 projectiles : éventail large de -80° à +80°
                CreateProjectile(spawnPosition, Vector3.down);                // 0° (centre)
                CreateProjectile(spawnPosition, new Vector3(-0.7f, -1f, 0f)); // ~35° gauche
                CreateProjectile(spawnPosition, new Vector3(0.7f, -1f, 0f));  // ~35° droite
                CreateProjectile(spawnPosition, new Vector3(-1.2f, -0.3f, 0f)); // ~75° gauche
                CreateProjectile(spawnPosition, new Vector3(1.2f, -0.3f, 0f));  // ~75° droite
                break;
                
            case 6:
                // 6 projectiles : éventail complet incluant l'horizontal
                CreateProjectile(spawnPosition, Vector3.down);                // 0° (centre)
                CreateProjectile(spawnPosition, new Vector3(-0.5f, -1f, 0f)); // ~27° gauche
                CreateProjectile(spawnPosition, new Vector3(0.5f, -1f, 0f));  // ~27° droite
                CreateProjectile(spawnPosition, new Vector3(-1f, -0.5f, 0f)); // ~63° gauche
                CreateProjectile(spawnPosition, new Vector3(1f, -0.5f, 0f));  // ~63° droite
                CreateProjectile(spawnPosition, new Vector3(-1f, 0f, 0f));    // 90° horizontal gauche
                break;
        }
    }
    
    void CreateProjectile(Vector3 position, Vector3 direction)
    {
        GameObject projectile = Instantiate(enemyProjectilePrefab, position, Quaternion.identity);
        EnemyProjectile script = projectile.GetComponent<EnemyProjectile>();
        if (script != null)
        {
            script.SetDirection(direction);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enemy: Collision détectée avec " + collision.gameObject.name + " (Tag: " + collision.tag + ")");
        
        // Si touché par un projectile
        if (collision.CompareTag("Projectile"))
        {
            // Ajouter les points au score et incrémenter le compteur d'ennemis tués
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(scoreValue);
                GameManager.Instance.AddEnemyKilled();
            }
            
            // Jouer le son d'explosion avant de détruire
            if (explosionSound != null && audioSource != null)
            {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            }
            
            // Détruire le projectile et l'ennemi
            Destroy(collision.gameObject);
            Destroy(gameObject);
            
            Debug.Log("Ennemi détruit ! +" + scoreValue + " points");
        }
        // Si touche le joueur
        else if (collision.CompareTag("Player"))
        {
            // Enlever une vie
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoseLife();
            }
            
            // Détruire l'ennemi
            Destroy(gameObject);
            
            Debug.Log("Le joueur a été touché !");
        }
    }
}
