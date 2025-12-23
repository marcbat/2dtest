using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Références aux textes UI
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    
    // Référence au GameManager
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        
        if (gameManager == null)
        {
            Debug.LogError("GameManager non trouvé !");
        }
    }

    void Update()
    {
        if (gameManager == null) return;
        
        // Mettre à jour l'affichage du score et des vies
        if (scoreText != null)
        {
            scoreText.text = "Score: " + gameManager.GetScore();
        }
        
        if (livesText != null)
        {
            livesText.text = "Vies: " + gameManager.GetLives();
        }
    }
}
