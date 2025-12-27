using UnityEngine;

/// <summary>
/// Scrolling background using material texture offset.
/// This approach requires a Material with a tileable texture and Wrap Mode set to Repeat.
/// 
/// Setup instructions:
/// 1. Create a Material in Assets/Materials/ with Shader: Sprites/Default
/// 2. Assign a tileable texture to the Material
/// 3. Set texture Wrap Mode to Repeat in the texture import settings
/// 4. Attach this script to a Sprite GameObject
/// 5. Assign the Material to the backgroundMaterial field
/// 6. Adjust scrollSpeed (recommended: 2-5)
/// 7. Position the GameObject at z=10 to keep it behind other elements
/// </summary>
public class ScrollingBackground : MonoBehaviour
{
    // Vitesse de défilement du fond
    public float scrollSpeed = 2f;
    // Matériau utilisé pour l'arrière-plan
    public Material backgroundMaterial;
    
    void Update()
    {
        if (backgroundMaterial != null)
        {
            float offset = Time.time * scrollSpeed * 0.1f;
            backgroundMaterial.mainTextureOffset = new Vector2(0, offset);
        }
    }
}
