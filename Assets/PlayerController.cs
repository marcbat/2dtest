using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Vitesse de rotation en degrés par seconde
    public float rotationSpeed = 100f;

    void Update()
    {
        // Détecter la flèche gauche
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Rotation vers la gauche (sens antihoraire)
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }

        // Détecter la flèche droite
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // Rotation vers la droite (sens horaire)
            transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
        }
    }
}
