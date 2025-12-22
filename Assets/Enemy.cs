using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Vitesse de déplacement vers le bas
    public float speed = 3f;
    
    // Points donnés quand l'ennemi est détruit
    public int scoreValue = 10;

    void Update()
    {
        // Déplacement vers le bas
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        
        // Détruire si hors écran (en dessous)
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enemy: Collision détectée avec " + collision.gameObject.name + " (Tag: " + collision.tag + ")");
        
        // Si touché par un projectile
        if (collision.CompareTag("Projectile"))
        {
            // Ajouter les points au score
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(scoreValue);
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
