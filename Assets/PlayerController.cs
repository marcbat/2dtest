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
    private float minY;
    private float maxY;
    
    // Prefab du projectile à instancier
    public GameObject projectilePrefab;
    
    // Point de spawn du projectile (position devant le vaisseau)
    public Transform firePoint;
    
    // Système d'armes à 6 niveaux
    public enum WeaponLevel
    {
        Single,         // Level 0: 1 projectile, 0.5s
        SingleFast,     // Level 1: 1 projectile, 0.4s
        Double,         // Level 2: 2 projectiles, 0.4s
        DoubleFast,     // Level 3: 2 projectiles, 0.3s
        Diagonal,       // Level 4: 2 diagonaux 30°, 0.3s
        DoubleDiagonal  // Level 5: 4 projectiles, 0.3s
    }
    
    private WeaponLevel currentWeaponLevel = WeaponLevel.Single;
    
    // Temps depuis le dernier tir
    private float nextFireTime = 0f;
    
    // Buffs temporaires
    private bool hasShield = false;
    private float shieldEndTime = 0f;
    private bool hasRapidFire = false;
    private float rapidFireEndTime = 0f;
    
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
    
    // Son du tir
    public AudioClip fireSound;
    
    // AudioSource pour jouer les sons
    private AudioSource audioSource;

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
        
        // Récupérer ou créer l'AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
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
    
    // Upgrade weapon level par collectible
    public void UpgradeWeapon()
    {
        if (currentWeaponLevel < WeaponLevel.DoubleDiagonal)
        {
            currentWeaponLevel++;
            Debug.Log($"Arme améliorée au niveau {currentWeaponLevel}");
        }
        else
        {
            Debug.Log("Arme au niveau maximum !");
        }
    }
    
    // Activer le shield (invincibilité temporaire)
    public void ActivateShield(float duration)
    {
        hasShield = true;
        shieldEndTime = Time.time + duration;
        Debug.Log($"Shield activé pour {duration}s");
        
        // TODO: Afficher un effet visuel de shield
    }
    
    // Activer rapid fire (cadence x2 temporaire)
    public void ActivateRapidFire(float duration)
    {
        hasRapidFire = true;
        rapidFireEndTime = Time.time + duration;
        Debug.Log($"Rapid Fire activé pour {duration}s");
    }
    
    // Check si le joueur est invulnérable
    public bool IsInvulnerable()
    {
        return hasShield;
    }
    
    // Obtenir la cadence de tir actuelle selon le niveau d'arme et les buffs
    float GetFireRate()
    {
        float baseRate = currentWeaponLevel switch
        {
            WeaponLevel.Single => 0.5f,
            WeaponLevel.SingleFast => 0.4f,
            WeaponLevel.Double => 0.4f,
            WeaponLevel.DoubleFast => 0.3f,
            WeaponLevel.Diagonal => 0.3f,
            WeaponLevel.DoubleDiagonal => 0.3f,
            _ => 0.5f
        };
        
        // Rapid fire divise le cooldown par 2
        return hasRapidFire ? baseRate / 2f : baseRate;
    }
    
    void CalculateScreenBounds()
    {
        if (Camera.main == null)
        {
            Debug.LogError("Aucune caméra principale trouvée !");
            minX = -8f;
            maxX = 8f;
            minY = -5f;
            maxY = 0f; // Moitié de l'écran
            return;
        }
        
        // Obtenir les limites de la caméra en coordonnées monde
        float camHeight = Camera.main.orthographicSize * 2f;
        float camWidth = camHeight * Camera.main.aspect;
        
        // Définir les limites horizontales avec une marge
        minX = -camWidth / 2f + screenMargin;
        maxX = camWidth / 2f - screenMargin;
        
        // Définir les limites verticales : moitié inférieure de l'écran
        minY = -Camera.main.orthographicSize + screenMargin; // Bas de l'écran
        maxY = 0f; // Moitié de l'écran (centre)
        
        Debug.Log($"Limites calculées: X entre {minX:F2} et {maxX:F2}, Y entre {minY:F2} et {maxY:F2}");
    }

    void Update()
    {
        // Récupérer le clavier actuel
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // Déplacement latéral
        float moveDirectionX = 0f;
        
        // Détecter la flèche gauche
        if (keyboard.leftArrowKey.isPressed)
        {
            moveDirectionX = -1f;
        }

        // Détecter la flèche droite
        if (keyboard.rightArrowKey.isPressed)
        {
            moveDirectionX = 1f;
        }
        
        // Déplacement vertical
        float moveDirectionY = 0f;
        
        // Détecter la flèche haut
        if (keyboard.upArrowKey.isPressed)
        {
            moveDirectionY = 1f;
        }
        
        // Détecter la flèche bas
        if (keyboard.downArrowKey.isPressed)
        {
            moveDirectionY = -1f;
        }
        
        // Appliquer le déplacement horizontal et vertical
        if (moveDirectionX != 0f || moveDirectionY != 0f)
        {
            Vector3 newPosition = transform.position;
            newPosition.x += moveDirectionX * currentMoveSpeed * Time.deltaTime;
            newPosition.y += moveDirectionY * currentMoveSpeed * Time.deltaTime;
            
            // Limiter le déplacement dans les bornes
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
            
            transform.position = newPosition;
        }
        
        // Tir MANUEL avec la touche Espace
        if (keyboard.spaceKey.isPressed && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + GetFireRate();
        }
        
        // Gérer les buffs temporaires
        if (hasShield && Time.time >= shieldEndTime)
        {
            hasShield = false;
            Debug.Log("Shield désactivé");
        }
        
        if (hasRapidFire && Time.time >= rapidFireEndTime)
        {
            hasRapidFire = false;
            Debug.Log("Rapid Fire désactivé");
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
        
        // Tirer selon le pattern
        FirePattern(spawnPosition);
        
        // Jouer le son de tir une seule fois
        if (fireSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }
    
    void FirePattern(Vector3 spawnPosition)
    {
        // Pattern de tir selon le niveau d'arme
        switch (currentWeaponLevel)
        {
            case WeaponLevel.Single:
            case WeaponLevel.SingleFast:
                // 1 projectile droit
                CreateProjectile(spawnPosition, Vector3.up);
                break;
                
            case WeaponLevel.Double:
            case WeaponLevel.DoubleFast:
                // 2 projectiles écartés de 10°
                CreateProjectile(spawnPosition, Quaternion.Euler(0, 0, 10f) * Vector3.up);
                CreateProjectile(spawnPosition, Quaternion.Euler(0, 0, -10f) * Vector3.up);
                break;
                
            case WeaponLevel.Diagonal:
                // 2 projectiles diagonaux 30°
                CreateProjectile(spawnPosition, Quaternion.Euler(0, 0, 30f) * Vector3.up);
                CreateProjectile(spawnPosition, Quaternion.Euler(0, 0, -30f) * Vector3.up);
                break;
                
            case WeaponLevel.DoubleDiagonal:
                // 4 projectiles: 2 droits + 2 diagonaux
                CreateProjectile(spawnPosition, Quaternion.Euler(0, 0, 10f) * Vector3.up);
                CreateProjectile(spawnPosition, Quaternion.Euler(0, 0, -10f) * Vector3.up);
                CreateProjectile(spawnPosition, Quaternion.Euler(0, 0, 30f) * Vector3.up);
                CreateProjectile(spawnPosition, Quaternion.Euler(0, 0, -30f) * Vector3.up);
                break;
        }
    }
    
    void CreateProjectile(Vector3 position, Vector3 direction)
    {
        // Calculer la rotation pour orienter le projectile dans la bonne direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        
        GameObject projectile = Instantiate(projectilePrefab, position, rotation);
        
        // Si le projectile a un script avec une direction personnalisée, l'utiliser
        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.SetDirection(direction);
        }
    }
}
