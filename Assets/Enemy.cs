using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Vitesse de déplacement vers le bas
    public float speed = 3f;
    
    // Points donnés quand l'ennemi est détruit
    public int scoreValue = 10;
    
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
    
    // Référence au SpriteRenderer pour changer la couleur
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Obtenir le SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Déterminer le nombre de projectiles selon le score actuel
        projectileCount = CalculateProjectileCount();
        
        // Changer la couleur selon le nombre de projectiles
        SetColorByProjectileCount();
        
        // Premier tir après un délai aléatoire (pour varier les tirs)
        nextFireTime = Time.time + Random.Range(0.5f, 2f);
    }

    void Update()
    {
        // Déplacement vers le bas
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        
        // Détruire si hors écran (en dessous)
        if (transform.position.y < -6f)
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
        
        Debug.Log($"Ennemi a tiré {projectileCount} projectile(s) !");
    }
    
    // Calculer le nombre de projectiles selon le score
    int CalculateProjectileCount()
    {
        if (GameManager.Instance == null)
            return 1;
        
        int score = GameManager.Instance.GetScore();
        
        // Barème progressif :
        // 0-99 : Toujours 1 projectile
        // 100-199 : 70% → 1 proj, 30% → 3 proj
        // 200-299 : 50% → 1 proj, 50% → 3 proj
        // 300-399 : 30% → 3 proj, 70% → 5 proj
        // 400-499 : 20% → 3 proj, 60% → 5 proj, 20% → 6 proj
        // 500+ : 10% → 3 proj, 40% → 5 proj, 50% → 6 proj
        
        float random = Random.Range(0f, 100f);
        
        if (score < 100)
        {
            return 1; // Toujours tir simple
        }
        else if (score < 200)
        {
            // 70% tir simple, 30% salve de 3
            return random < 70f ? 1 : 3;
        }
        else if (score < 300)
        {
            // 50% tir simple, 50% salve de 3
            return random < 50f ? 1 : 3;
        }
        else if (score < 400)
        {
            // 30% salve de 3, 70% salve de 5
            return random < 30f ? 3 : 5;
        }
        else if (score < 500)
        {
            // 20% → 3, 60% → 5, 20% → 6
            if (random < 20f) return 3;
            else if (random < 80f) return 5;
            else return 6;
        }
        else // 500+
        {
            // 10% → 3, 40% → 5, 50% → 6
            if (random < 10f) return 3;
            else if (random < 50f) return 5;
            else return 6;
        }
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
                // 3 projectiles : centre + 45° gauche/droite
                CreateProjectile(spawnPosition, Vector3.down);
                CreateProjectile(spawnPosition, new Vector3(-1f, -1f, 0f)); // 45° gauche
                CreateProjectile(spawnPosition, new Vector3(1f, -1f, 0f));  // 45° droite
                break;
                
            case 5:
                // 5 projectiles : centre + 30° et 60° de chaque côté
                CreateProjectile(spawnPosition, Vector3.down);
                CreateProjectile(spawnPosition, new Vector3(-0.5f, -1f, 0f));  // ~30° gauche
                CreateProjectile(spawnPosition, new Vector3(0.5f, -1f, 0f));   // ~30° droite
                CreateProjectile(spawnPosition, new Vector3(-1f, -0.8f, 0f));  // ~60° gauche
                CreateProjectile(spawnPosition, new Vector3(1f, -0.8f, 0f));   // ~60° droite
                break;
                
            case 6:
                // 6 projectiles : éventail complet
                CreateProjectile(spawnPosition, Vector3.down);
                CreateProjectile(spawnPosition, new Vector3(-0.5f, -1f, 0f));  // ~30° gauche
                CreateProjectile(spawnPosition, new Vector3(0.5f, -1f, 0f));   // ~30° droite
                CreateProjectile(spawnPosition, new Vector3(-1f, -0.8f, 0f));  // ~60° gauche
                CreateProjectile(spawnPosition, new Vector3(1f, -0.8f, 0f));   // ~60° droite
                CreateProjectile(spawnPosition, new Vector3(-1.2f, -0.5f, 0f)); // ~75° gauche
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
    
    // Définir la couleur selon le nombre de projectiles
    void SetColorByProjectileCount()
    {
        if (spriteRenderer == null) return;
        
        // Palette de couleurs selon la dangerosité
        switch (projectileCount)
        {
            case 1:
                spriteRenderer.color = Color.white; // Blanc - Faible
                break;
            case 3:
                spriteRenderer.color = new Color(1f, 0.92f, 0.016f); // Jaune - Moyen
                break;
            case 5:
                spriteRenderer.color = new Color(1f, 0.5f, 0f); // Orange - Dangereux
                break;
            case 6:
                spriteRenderer.color = new Color(1f, 0f, 0f); // Rouge - Très dangereux
                break;
            default:
                spriteRenderer.color = Color.white;
                break;
        }
    }
}
