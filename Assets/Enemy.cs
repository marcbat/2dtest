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

    void Start()
    {
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
        
        // Créer le projectile à la position de l'ennemi
        Instantiate(enemyProjectilePrefab, spawnPosition, Quaternion.identity);
        
        Debug.Log("Ennemi a tiré !");
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
}
