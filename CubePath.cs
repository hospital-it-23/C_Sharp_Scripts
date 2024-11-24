using UnityEngine;

public class CubePathContinuousControl : MonoBehaviour
{
    public Transform cube; // Assign the cube object here
    public Transform[] checkpoints; // Assign checkpoint positions in the Inspector
    public float moveSpeed = 2f; // Speed of movement
    private int currentCheckpoint = 0; // Index of the current checkpoint
    private float progress = 0f; // Tracks the progress between checkpoints (0 to 1)

    void Update()
    {
        // Ensure there are checkpoints to follow
        if (checkpoints.Length < 2 || cube == null)
            return;

        // Calculate the start and target checkpoints
        Transform startCheckpoint = checkpoints[currentCheckpoint];
        Transform targetCheckpoint = checkpoints[Mathf.Clamp(currentCheckpoint + 1, 0, checkpoints.Length - 1)];

        // Move forward with W
        if (Input.GetKey(KeyCode.W))
        {
            progress += moveSpeed * Time.deltaTime;
            if (progress >= 1f)
            {
                progress = 0f;
                currentCheckpoint = Mathf.Min(currentCheckpoint + 1, checkpoints.Length - 2); // Clamp to avoid out-of-bounds
            }
        }

        // Move backward with S
        if (Input.GetKey(KeyCode.S))
        {
            progress -= moveSpeed * Time.deltaTime;
            if (progress <= 0f)
            {
                progress = 1f;
                currentCheckpoint = Mathf.Max(currentCheckpoint - 1, 0); // Clamp to avoid out-of-bounds
            }
        }

        // Interpolate the cube's position
        cube.position = Vector3.Lerp(startCheckpoint.position, targetCheckpoint.position, Mathf.Clamp01(progress));
    }

    void OnDrawGizmos()
    {
        // Visualize the path in the Scene view
        Gizmos.color = Color.blue;
        if (checkpoints != null && checkpoints.Length > 1)
        {
            for (int i = 0; i < checkpoints.Length - 1; i++)
            {
                Gizmos.DrawLine(checkpoints[i].position, checkpoints[i + 1].position);
            }
        }
    }
}
