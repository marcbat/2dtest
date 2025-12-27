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
    
    private void Update()
    {
        // Vérifie que le material est assigné
        if (backgroundMaterial != null)
        {
            // Calcule l'offset basé sur le temps et la vitesse
            // Multiplie par 0.1f pour un défilement plus subtil
            float offset = Time.time * scrollSpeed * 0.1f;
            
            // Applique l'offset sur l'axe Y (défilement vertical)
            backgroundMaterial.mainTextureOffset = new Vector2(0, offset);
        }
    }
    
    private void OnDestroy()
    {
        // Reset l'offset du material quand l'objet est détruit
        // Important pour éviter des problèmes en mode édition
        if (backgroundMaterial != null)
        {
            backgroundMaterial.mainTextureOffset = Vector2.zero;
        }
    }
}
