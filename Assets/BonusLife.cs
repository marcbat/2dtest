using UnityEngine;

public class BonusLife : MonoBehaviour
{
    // Vitesse de déplacement vers le bas
    public float speed = 2f;
    
    // Vie supplémentaire donnée
    public int lifeBonus = 1;

    void Update()
    {
        // Déplacement vers le bas (plus lent que les ennemis)
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        
        // Détruire si hors écran (en dessous)
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Si touché par un projectile du joueur
        if (collision.CompareTag("Projectile"))
        {
            // Ajouter une vie
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddLife(lifeBonus);
            }
            
            // Détruire le projectile et le bonus
            Destroy(collision.gameObject);
            Destroy(gameObject);
            
            Debug.Log("Bonus vie récupéré ! +" + lifeBonus + " vie(s)");
        }
        // Si touche le joueur directement (ramassage)
        else if (collision.CompareTag("Player"))
        {
            // Ajouter une vie
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddLife(lifeBonus);
            }
            
            // Détruire le bonus
            Destroy(gameObject);
            
            Debug.Log("Bonus vie ramassé ! +" + lifeBonus + " vie(s)");
        }
    }
}
