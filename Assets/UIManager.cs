using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Références aux textes UI
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI enemiesKilledText;
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
        
        // Mettre à jour l'affichage du high score, score, ennemis tués et vies
        if (highScoreText != null)
        {
            highScoreText.text = "Meilleur: " + gameManager.GetHighScore();
        }
        
        if (scoreText != null)
        {
            scoreText.text = "Score: " + gameManager.GetScore();
        }
        
        if (enemiesKilledText != null)
        {
            enemiesKilledText.text = "Ennemis: " + gameManager.GetEnemiesKilled();
        }
        
        if (livesText != null)
        {
            livesText.text = "Vies: " + gameManager.GetLives();
        }
    }
}
