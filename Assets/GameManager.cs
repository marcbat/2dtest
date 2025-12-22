using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton pour accès global
    public static GameManager Instance { get; private set; }
    
    // Score du joueur
    private int score = 0;
    
    // Nombre de vies
    public int lives = 3;
    
    // Game Over
    private bool isGameOver = false;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        Debug.Log("Jeu démarré - Vies: " + lives + " | Score: " + score);
    }

    void Update()
    {
        // Redémarrer le jeu avec R en cas de Game Over
        if (isGameOver && Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }

    // Ajouter des points au score
    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
    }

    // Perdre une vie
    public void LoseLife()
    {
        lives--;
        Debug.Log("Vie perdue ! Vies restantes: " + lives);
        
        if (lives <= 0)
        {
            GameOver();
        }
    }

    // Game Over
    void GameOver()
    {
        isGameOver = true;
        Debug.Log("=== GAME OVER === Score final: " + score + " | Appuyez sur R pour recommencer");
        
        // Arrêter le temps (pause)
        Time.timeScale = 0f;
    }

    // Redémarrer le jeu
    void RestartGame()
    {
        // Remettre le temps à la normale
        Time.timeScale = 1f;
        
        // Recharger la scène
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Getters pour l'affichage (si vous ajoutez une UI plus tard)
    public int GetScore()
    {
        return score;
    }

    public int GetLives()
    {
        return lives;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
