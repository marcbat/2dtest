using System;

// Classe pour stocker les données à sauvegarder
[Serializable]
public class SaveData
{
    public int highScore = 0;
    
    // Constructeur
    public SaveData()
    {
        highScore = 0;
    }
}
