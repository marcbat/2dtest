using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Type Configuration")]
    public EnemyTypeData enemyType;
    
    // Health system
    private int currentHealth;
    private int maxHealth;
    
    // Combat
    private float moveSpeed;
    private int projectileCount;
    private float fireRate;
    private float nextFireTime = 0f;
    
    // Movement
    private MovementPattern movementPattern;
    private Vector3 moveDirection;
    private Vector3 initialPosition;
    private float movementTime = 0f;
    
    // Prefabs et références
    public GameObject enemyProjectilePrefab;
    public Transform firePoint;
    
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private Color originalColor;
    
    // Collectibles
    private CollectibleSpawner collectibleSpawner;
    
    void Start()
    {
        if (enemyType == null)
        {
            Debug.LogError("EnemyType non assigné sur " + gameObject.name);
            Destroy(gameObject);
            return;
        }
        
        // Initialisation depuis EnemyTypeData
        maxHealth = enemyType.maxHealth;
        currentHealth = maxHealth;
        moveSpeed = enemyType.moveSpeed;
        projectileCount = enemyType.projectileCount;
        fireRate = enemyType.fireRate;
        movementPattern = enemyType.movementPattern;
        
        // Références
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && enemyType.sprite != null)
        {
            spriteRenderer.sprite = enemyType.sprite;
            originalColor = spriteRenderer.color;
        }
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // Trouver le CollectibleSpawner
        collectibleSpawner = FindFirstObjectByType<CollectibleSpawner>();
        
        // Sauvegarder position initiale pour patterns de mouvement
        initialPosition = transform.position;
        
        // Calculer direction initiale selon le pattern
        CalculateInitialDirection();
        
        // Premier tir après un délai aléatoire
        nextFireTime = Time.time + Random.Range(0.5f, 2f);
    }
    
    void CalculateInitialDirection()
    {
        float posX = transform.position.x;
        
        switch (movementPattern)
        {
            case MovementPattern.Vertical:
                moveDirection = Vector3.down;
                break;
                
            case MovementPattern.DiagonalSoft:
                // ±30° max
                float softAngle = (posX < -3f) ? Random.Range(0.3f, 0.577f) : // 30° max vers droite
                                 (posX > 3f) ? Random.Range(-0.577f, -0.3f) :  // 30° max vers gauche
                                 Random.Range(-0.577f, 0.577f);                 // Libre
                moveDirection = new Vector3(softAngle, -1f, 0f).normalized;
                break;
                
            case MovementPattern.DiagonalFast:
                // ±45° max
                float fastAngle = (posX < -3f) ? Random.Range(0.5f, 1f) :
                                 (posX > 3f) ? Random.Range(-1f, -0.5f) :
                                 Random.Range(-1f, 1f);
                moveDirection = new Vector3(fastAngle, -1f, 0f).normalized;
                break;
                
            default:
                moveDirection = Vector3.down;
                break;
        }
    }

    void Update()
    {
        // Mouvement selon le pattern
        MoveAccordingToPattern();
        
        // Tirer si c'est le moment
        if (Time.time >= nextFireTime && CanSeePlayer())
        {
            FireBurst();
            nextFireTime = Time.time + fireRate;
        }
        
        // Détruire si hors écran
        if (transform.position.y < -6f || Mathf.Abs(transform.position.x) > 10f)
        {
            Destroy(gameObject);
        }
    }
    
    void MoveAccordingToPattern()
    {
        movementTime += Time.deltaTime;
        
        switch (movementPattern)
        {
            case MovementPattern.Vertical:
                transform.Translate(Vector3.down * moveSpeed * Time.deltaTime, Space.World);
                break;
                
            case MovementPattern.DiagonalSoft:
            case MovementPattern.DiagonalFast:
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
                break;
                
            case MovementPattern.VerticalZigzag:
                float zigzagX = Mathf.Sin(movementTime * 3f) * 2f * Time.deltaTime;
                transform.Translate(new Vector3(zigzagX, -moveSpeed * Time.deltaTime, 0f), Space.World);
                break;
                
            case MovementPattern.Sinusoidal:
                float sinX = Mathf.Sin(movementTime * 2f) * 3f;
                float targetX = initialPosition.x + sinX;
                float newX = Mathf.Lerp(transform.position.x, targetX, Time.deltaTime * 2f);
                transform.position = new Vector3(newX, transform.position.y - moveSpeed * Time.deltaTime, transform.position.z);
                break;
                
            case MovementPattern.Circular:
                float circleX = Mathf.Cos(movementTime) * 2f;
                float circleY = Mathf.Sin(movementTime) * 2f;
                Vector3 circularOffset = new Vector3(circleX, circleY, 0f) * Time.deltaTime;
                transform.Translate(circularOffset + Vector3.down * moveSpeed * Time.deltaTime, Space.World);
                break;
        }
    }
    
    bool CanSeePlayer()
    {
        // Vérifier si l'ennemi est visible à l'écran
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        return viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1;
    }
    
    void FireBurst()
    {
        if (enemyProjectilePrefab == null || firePoint == null) return;
        
        // Son de tir (un seul pour toute la salve)
        if (enemyType.fireSound != null)
        {
            AudioSource.PlayClipAtPoint(enemyType.fireSound, transform.position, 0.3f);
        }
        
        // Tirer les projectiles en éventail
        if (projectileCount == 1)
        {
            CreateProjectile(0f);
        }
        else
        {
            float totalSpread = 60f;
            float angleStep = totalSpread / (projectileCount - 1);
            float startAngle = -totalSpread / 2f;
            
            for (int i = 0; i < projectileCount; i++)
            {
                float angle = startAngle + (angleStep * i);
                CreateProjectile(angle);
            }
        }
    }
    
    void CreateProjectile(float angle)
    {
        GameObject projectile = Instantiate(enemyProjectilePrefab, firePoint.position, Quaternion.identity);
        
        EnemyProjectile projScript = projectile.GetComponent<EnemyProjectile>();
        if (projScript != null)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector3 direction = rotation * Vector3.down;
            projScript.SetDirection(direction);
        }
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        // Feedback visuel
        StartCoroutine(DamageFlash());
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    IEnumerator DamageFlash()
    {
        if (spriteRenderer == null) yield break;
        
        spriteRenderer.color = enemyType.damageFlashColor;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }
    
    void Die()
    {
        // Son d'explosion
        if (enemyType.explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(enemyType.explosionSound, transform.position, 0.5f);
        }
        
        // Ajouter le score
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(enemyType.scoreValue);
            GameManager.Instance.AddEnemyKilled();
        }
        
        // Spawner un collectible potentiellement
        if (collectibleSpawner != null)
        {
            collectibleSpawner.SpawnCollectible(transform.position, enemyType);
        }
        
        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null && !player.IsInvulnerable())
            {
                player.OnHit();
                
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.LoseLife();
                }
            }
            
            Die();
        }
    }
}
