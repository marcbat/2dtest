using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Vitesse de déplacement du projectile
    public float speed = 10f;
    
    // Direction du projectile (par défaut vers le haut)
    private Vector3 direction = Vector3.up;
    
    // Durée de vie du projectile en secondes
    public float lifetime = 3f;
    
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
        if (transform.position.y > 6f || Mathf.Abs(transform.position.x) > 10f)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Le projectile est détruit par l'ennemi, pas besoin de gérer ici
        // La logique de collision est dans Enemy.cs
    }
}
