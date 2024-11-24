using UnityEngine;

public class CubePathAndFreeMove : MonoBehaviour
{
    public Transform cube; // Assign the cube object here
    public Transform[] checkpoints; // Assign checkpoint positions in the Inspector
    public float pathMoveSpeed = 2f; // Speed of movement along the path
    public float freeMoveSpeed = 5f; // Speed of free movement
    private int currentCheckpoint = 0; // Index of the current checkpoint
    private float progress = 0f; // Tracks progress between checkpoints (0 to 1)
    private bool atEnd = false; // Flag to indicate if the cube has reached the end

    void Update()
    {
        if (atEnd)
        {
            HandleFreeMovement();
        }
        else
        {
            FollowPath();
        }
    }

    // Handles movement along the path
    void FollowPath()
    {
        if (checkpoints.Length < 2 || cube == null)
            return;

        // Calculate the start and target checkpoints
        Transform startCheckpoint = checkpoints[currentCheckpoint];
        Transform targetCheckpoint = checkpoints[Mathf.Clamp(currentCheckpoint + 1, 0, checkpoints.Length - 1)];

        // Move forward with W
        if (Input.GetKey(KeyCode.W))
        {
            progress += pathMoveSpeed * Time.deltaTime;
            if (progress >= 1f)
            {
                progress = 0f;
                currentCheckpoint++;

                // If the cube reaches the last checkpoint, enable free movement
                if (currentCheckpoint >= checkpoints.Length - 1)
                {
                    atEnd = true;
                    return;
                }
            }
        }

        // Move backward with S
        if (Input.GetKey(KeyCode.S))
        {
            progress -= pathMoveSpeed * Time.deltaTime;
            if (progress <= 0f)
            {
                progress = 1f;
                currentCheckpoint = Mathf.Max(currentCheckpoint - 1, 0); // Clamp to avoid out-of-bounds
            }
        }

        // Interpolate the cube's position
        cube.position = Vector3.Lerp(startCheckpoint.position, targetCheckpoint.position, Mathf.Clamp01(progress));
    }

    // Handles free movement in 3D space with WASD
    void HandleFreeMovement()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            direction += Vector3.forward; // Move forward
        if (Input.GetKey(KeyCode.S))
            direction += Vector3.back; // Move backward
        if (Input.GetKey(KeyCode.A))
            direction += Vector3.left; // Move left
        if (Input.GetKey(KeyCode.D))
            direction += Vector3.right; // Move right
        if (Input.GetKey(KeyCode.Space))
            direction += Vector3.up; // Move upward
        if (Input.GetKey(KeyCode.LeftShift))
            direction += Vector3.down; // Move downward

        // Normalize the direction and move the cube
        direction = direction.normalized;
        cube.position += direction * freeMoveSpeed * Time.deltaTime;
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
