using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    // Vitesse de déplacement du projectile
    public float speed = 8f;
    
    // Direction du projectile (par défaut vers le bas)
    private Vector3 direction = Vector3.down;
    
    // Durée de vie du projectile en secondes
    public float lifetime = 5f;
    
    // Définir la direction du projectile
    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
    }

    void Start()
    {
        // Détruire automatiquement le projectile après sa durée de vie
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Déplacer le projectile dans la direction définie
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        
        // Détruire si hors écran
        if (transform.position.y < -6f || transform.position.y > 6f || 
            Mathf.Abs(transform.position.x) > 10f)
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
