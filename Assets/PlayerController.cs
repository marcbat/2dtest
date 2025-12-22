using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Vitesse de rotation en degrés par seconde
    public float rotationSpeed = 300f;
    
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

        // Détecter la flèche gauche
        if (keyboard.leftArrowKey.isPressed)
        {
            Debug.Log("Flèche gauche détectée - Rotation en cours");
            // Rotation vers la gauche (sens antihoraire)
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }

        // Détecter la flèche droite
        if (keyboard.rightArrowKey.isPressed)
        {
            Debug.Log("Flèche droite détectée - Rotation en cours");
            // Rotation vers la droite (sens horaire)
            transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
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
