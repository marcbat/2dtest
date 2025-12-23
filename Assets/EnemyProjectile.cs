using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    // Vitesse de déplacement du projectile
    public float speed = 8f;
    
    // Durée de vie du projectile en secondes
    public float lifetime = 5f;

    void Start()
    {
        // Détruire automatiquement le projectile après sa durée de vie
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Déplacer le projectile vers le bas
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        
        // Détruire si hors écran (en bas)
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Si touche le joueur
        if (collision.CompareTag("Player"))
        {
            // Enlever une vie
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoseLife();
            }
            
            // Détruire le projectile
            Destroy(gameObject);
            
            Debug.Log("Le joueur a été touché par un projectile ennemi !");
        }
    }
}
