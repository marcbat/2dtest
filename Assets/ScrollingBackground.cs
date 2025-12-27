using UnityEngine;

/// <summary>
/// Script de gestion d'un fond défilant vertical
/// Utilise l'offset de la texture d'un Material pour créer un effet de défilement continu
/// </summary>
public class ScrollingBackground : MonoBehaviour
{
    [Header("Configuration du défilement")]
    [Tooltip("Vitesse de défilement vertical (2-5 recommandé)")]
    [SerializeField] private float scrollSpeed = 2f;
    
    [Tooltip("Material du fond avec texture en mode Repeat")]
    [SerializeField] private Material backgroundMaterial;
    
    // Instance du material pour ne pas affecter d'autres objets
    private Material materialInstance;
    
    private void Start()
    {
        // Crée une instance du material pour cet objet uniquement
        if (backgroundMaterial != null)
        {
            materialInstance = new Material(backgroundMaterial);
            
            // Applique l'instance au SpriteRenderer si présent
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.material = materialInstance;
            }
        }
    }
    
    private void Update()
    {
        // Vérifie que le material instance est créé
        if (materialInstance != null)
        {
            // Calcule l'offset basé sur le temps et la vitesse
            // Multiplie par 0.1f pour un défilement plus subtil
            float offset = Time.time * scrollSpeed * 0.1f;
            
            // Applique l'offset sur l'axe Y (défilement vertical)
            materialInstance.mainTextureOffset = new Vector2(0, offset);
        }
    }
    
    private void OnDestroy()
    {
        // Détruit l'instance du material pour libérer la mémoire
        if (materialInstance != null)
        {
            Destroy(materialInstance);
        }
    }
}
