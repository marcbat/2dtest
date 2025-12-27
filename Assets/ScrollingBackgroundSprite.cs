using UnityEngine;

/// <summary>
/// Scrolling background using double buffering technique with two sprite objects.
/// This approach provides smooth scrolling without requiring a Material.
/// 
/// Setup instructions:
/// 1. Create an empty GameObject named "BackgroundManager"
/// 2. Create two child Sprite GameObjects (Background1 and Background2)
/// 3. Position Background1 at (0, 0, 10)
/// 4. Position Background2 at (0, spriteHeight, 10) - stack vertically above Background1
/// 5. Assign the same sprite/texture to both
/// 6. Attach this script to the BackgroundManager GameObject
/// 7. Drag Background1 and Background2 to the respective Transform fields
/// 8. Adjust scrollSpeed (recommended: 2-5)
/// 9. Configure Sorting Layer to "Background" (Order: -10) to stay behind other elements
/// 
/// How it works:
/// - Both backgrounds move downward continuously
/// - When one moves off-screen below, it teleports above the other
/// - Creates seamless infinite scrolling effect
/// </summary>
public class ScrollingBackgroundSprite : MonoBehaviour
{
    // Vitesse de défilement du fond
    public float scrollSpeed = 2f;
    // Premier sprite de l'arrière-plan
    public Transform background1;
    // Deuxième sprite de l'arrière-plan
    public Transform background2;
    
    private float spriteHeight;
    
    void Start()
    {
        if (background1 != null && background1.GetComponent<SpriteRenderer>() != null)
        {
            spriteHeight = background1.GetComponent<SpriteRenderer>().bounds.size.y;
        }
        else
        {
            Debug.LogWarning("ScrollingBackgroundSprite: background1 or its SpriteRenderer is missing!");
        }
    }
    
    void Update()
    {
        if (background1 == null || background2 == null)
        {
            return;
        }
        
        // Déplacer les deux arrière-plans vers le bas
        background1.position += Vector3.down * scrollSpeed * Time.deltaTime;
        background2.position += Vector3.down * scrollSpeed * Time.deltaTime;
        
        // Réinitialiser background1 quand il sort de l'écran
        if (background1.position.y < -spriteHeight)
        {
            background1.position = new Vector3(0, background2.position.y + spriteHeight, 10);
        }
        
        // Réinitialiser background2 quand il sort de l'écran
        if (background2.position.y < -spriteHeight)
        {
            background2.position = new Vector3(0, background1.position.y + spriteHeight, 10);
        }
    }
}
