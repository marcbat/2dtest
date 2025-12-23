using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // Vitesse de déplacement latéral initiale
    public float baseMoveSpeed = 5f;
    
    // Augmentation de vitesse à chaque niveau
    public float speedIncreasePerLevel = 0.5f;
    
    // Vitesse maximale
    public float maxMoveSpeed = 12f;
    
    // Vitesse actuelle (modifiable pendant le jeu)
    private float currentMoveSpeed;
    
    // Marge par rapport aux bords de l'écran
    public float screenMargin = 0.5f;
    
    // Limites calculées dynamiquement
    private float minX;
    private float maxX;
    
    // Prefab du projectile à instancier
    public GameObject projectilePrefab;
    
    // Point de spawn du projectile (position devant le vaisseau)
    public Transform firePoint;
    
    // Délai entre deux tirs (cadence de tir)
    public float fireRate = 0.1f;
    
    // Temps depuis le dernier tir
    private float nextFireTime = 0f;
    
    // Référence au SpriteRenderer pour le clignotement
    private SpriteRenderer spriteRenderer;
    
    // Couleur d'origine du vaisseau
    private Color originalColor;
    
    // Couleur de clignotement lors d'un hit
    public Color hitColor = Color.yellow;
    
    // Durée du clignotement
    public float blinkDuration = 0.5f;
    
    // Fréquence du clignotement (nombre de fois par seconde)
    public float blinkFrequency = 10f;

    void Start()
    {
        Debug.Log("PlayerController activé sur " + gameObject.name);
        
        // Initialiser la vitesse
        currentMoveSpeed = baseMoveSpeed;
        
        // Récupérer le SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        
        // Calculer les limites en fonction de la caméra
        CalculateScreenBounds();
    }
    
    // Méthode appelée quand le vaisseau est touché
    public void OnHit()
    {
        StartCoroutine(BlinkCoroutine());
    }
    
    // Coroutine pour gérer le clignotement
    private IEnumerator BlinkCoroutine()
    {
        if (spriteRenderer == null) yield break;
        
        float elapsed = 0f;
        float blinkInterval = 1f / blinkFrequency / 2f; // Diviser par 2 pour alternance on/off
        bool isYellow = true;
        
        while (elapsed < blinkDuration)
        {
            // Alterner entre jaune et couleur originale
            spriteRenderer.color = isYellow ? hitColor : originalColor;
            isYellow = !isYellow;
            
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }
        
        // Remettre la couleur d'origine à la fin
        spriteRenderer.color = originalColor;
    }
    
    // Méthode appelée par le GameManager pour augmenter la vitesse
    public void IncreaseSpeed()
    {
        if (currentMoveSpeed < maxMoveSpeed)
        {
            currentMoveSpeed += speedIncreasePerLevel;
            currentMoveSpeed = Mathf.Min(currentMoveSpeed, maxMoveSpeed);
            Debug.Log($"Vitesse du joueur augmentée à {currentMoveSpeed:F1}");
        }
    }
    
    void CalculateScreenBounds()
    {
        if (Camera.main == null)
        {
            Debug.LogError("Aucune caméra principale trouvée !");
            minX = -8f;
            maxX = 8f;
            return;
        }
        
        // Obtenir les limites de la caméra en coordonnées monde
        float camHeight = Camera.main.orthographicSize * 2f;
        float camWidth = camHeight * Camera.main.aspect;
        
        // Définir les limites avec une marge
        minX = -camWidth / 2f + screenMargin;
        maxX = camWidth / 2f - screenMargin;
        
        Debug.Log($"Limites calculées: X entre {minX:F2} et {maxX:F2}");
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
            newPosition.x += moveDirection * currentMoveSpeed * Time.deltaTime;
            
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
