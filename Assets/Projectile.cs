using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Vitesse de déplacement du projectile
    public float speed = 10f;
    
    // Durée de vie du projectile en secondes
    public float lifetime = 3f;

    void Start()
    {
        // Détruire automatiquement le projectile après sa durée de vie
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Déplacer le projectile vers le haut (direction du vaisseau)
        // En 2D, "haut" correspond à transform.up
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
