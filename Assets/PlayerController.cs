using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Vitesse de déplacement latéral
    public float moveSpeed = 5f;
    
    // Limites de déplacement sur l'axe X
    public float minX = -8f;
    public float maxX = 8f;
    
    // Prefab du projectile à instancier
    public GameObject projectilePrefab;
    
    // Point de spawn du projectile (position devant le vaisseau)
    public Transform firePoint;
    
    // Délai entre deux tirs (cadence de tir)
    public float fireRate = 0.1f;
    
    // Temps depuis le dernier tir
    private float nextFireTime = 0f;

    void Start()
    {
        Debug.Log("PlayerController activé sur " + gameObject.name);
    }

    void Update()
    {
        // Récupérer le clavier actuel
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // Déplacement latéral
        float moveDirection = 0f;
        
        // Détecter la flèche gauche
        if (keyboard.leftArrowKey.isPressed)
        {
            moveDirection = -1f;
        }

        // Détecter la flèche droite
        if (keyboard.rightArrowKey.isPressed)
        {
            moveDirection = 1f;
        }
        
        // Appliquer le déplacement
        if (moveDirection != 0f)
        {
            Vector3 newPosition = transform.position;
            newPosition.x += moveDirection * moveSpeed * Time.deltaTime;
            
            // Limiter le déplacement dans les bornes
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            
            transform.position = newPosition;
        }
        
        // Gérer le tir avec la touche Espace
        if (keyboard.spaceKey.isPressed && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }
    
    void Fire()
    {
        // Vérifier que le prefab et le point de tir sont assignés
        if (projectilePrefab == null)
        {
            Debug.LogWarning("Projectile Prefab n'est pas assigné dans l'Inspector !");
            return;
        }
        
        // Déterminer la position de spawn
        Vector3 spawnPosition = firePoint != null ? firePoint.position : transform.position;
        
        // Créer le projectile à la position du vaisseau avec sa rotation
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, transform.rotation);
        
        Debug.Log("Projectile tiré !");
    }
}
