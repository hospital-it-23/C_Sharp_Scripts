using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float rotationSpeed = 200f; // Speed of camera rotation

    private float xRotation = 0f; // To keep track of the camera's rotation along the X-axis
    private float yRotation = 0f; // To keep track of the camera's rotation along the Y-axis

    void Update()
    {
        // Key input for looking up, down, left, and right
        if (Input.GetKey(KeyCode.Keypad8)) // Look up
        {
            xRotation -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Keypad2)) // Look down
        {
            xRotation += rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Keypad4)) // Look left
        {
            yRotation -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Keypad6)) // Look right
        {
            yRotation += rotationSpeed * Time.deltaTime;
        }

        // Clamp vertical rotation to prevent flipping
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply the rotation to the camera
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
