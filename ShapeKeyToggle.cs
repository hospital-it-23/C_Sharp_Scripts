using UnityEngine;

public class SingleBlendShapeController : MonoBehaviour
{
    public string blendShapeName = "";         // Leave empty if using index directly
    public int blendShapeIndex = 20;          // Index for the 21st blend shape (0-based index)
    public float transitionDuration = 1.0f;   // The duration of the transition in seconds
    public KeyCode toggleKey = KeyCode.K;     // The key to toggle the blend shape value

    private SkinnedMeshRenderer skinnedMeshRenderer; // Reference to the SkinnedMeshRenderer component
    private bool blendShapeEnabled = false;         // State of the blend shape
    private float currentWeight = 0f;               // Current weight of the blend shape
    private float targetWeight = 0f;                // Target weight of the blend shape
    private float transitionStartTime = 0f;         // Start time of the current transition

    void Start()
    {
        // Assign the SkinnedMeshRenderer component
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        if (skinnedMeshRenderer == null)
        {
            Debug.LogError("SkinnedMeshRenderer component is not found on the object or its children!");
            return;
        }

        // If using the blend shape name, resolve the index
        if (!string.IsNullOrEmpty(blendShapeName))
        {
            blendShapeIndex = GetBlendShapeIndex(blendShapeName);
            if (blendShapeIndex == -1)
            {
                Debug.LogError("Blend shape '" + blendShapeName + "' does not exist!");
                return;
            }
        }
    }

    void Update()
    {
        // Toggle blend shape value when the toggle key is pressed
        if (Input.GetKeyDown(toggleKey))
        {
            blendShapeEnabled = !blendShapeEnabled;
            targetWeight = blendShapeEnabled ? 100f : 0f;
            transitionStartTime = Time.time;
        }

        // Smoothly transition the blend shape weight
        if (Mathf.Abs(currentWeight - targetWeight) > Mathf.Epsilon)
        {
            float elapsedTime = Time.time - transitionStartTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);
            currentWeight = Mathf.Lerp(currentWeight, targetWeight, t);
            skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, currentWeight);
        }
    }

    int GetBlendShapeIndex(string name)
    {
        for (int i = 0; i < skinnedMeshRenderer.sharedMesh.blendShapeCount; i++)
        {
            if (skinnedMeshRenderer.sharedMesh.GetBlendShapeName(i) == name)
            {
                return i;
            }
        }
        return -1;
    }
}
