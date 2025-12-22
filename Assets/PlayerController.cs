using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Vitesse de rotation en degrés par seconde
    public float rotationSpeed = 100f;

    void Start()
    {
        Debug.Log("PlayerController activé sur " + gameObject.name);
    }

    void Update()
    {
        // Récupérer le clavier actuel
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // Détecter la flèche gauche
        if (keyboard.leftArrowKey.isPressed)
        {
            Debug.Log("Flèche gauche détectée - Rotation en cours");
            // Rotation vers la gauche (sens antihoraire)
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }

        // Détecter la flèche droite
        if (keyboard.rightArrowKey.isPressed)
        {
            Debug.Log("Flèche droite détectée - Rotation en cours");
            // Rotation vers la droite (sens horaire)
            transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
        }
    }
}
