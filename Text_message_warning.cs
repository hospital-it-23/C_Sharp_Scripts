using UnityEngine;
using UnityEngine.UI;

public class CubeCollisionWarning : MonoBehaviour
{
    public Text collisionWarningText; // Reference to the UI Text component for warnings

    void Start()
    {
        // Clear any existing warning message in the UI text
        if (collisionWarningText != null)
        {
            collisionWarningText.text = "";
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the cube collides with an object tagged "Tube"
        if (collision.gameObject.CompareTag("Tube"))
        {
            // Display the warning message in the UI text
            if (collisionWarningText != null)
            {
                collisionWarningText.text = "Warning! Cube is touching the tube!";
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Check if the cube stops colliding with an object tagged "Tube"
        if (collision.gameObject.CompareTag("Tube"))
        {
            // Clear the warning message from the UI text
            if (collisionWarningText != null)
            {
                collisionWarningText.text = "";
            }
        }
    }
}
