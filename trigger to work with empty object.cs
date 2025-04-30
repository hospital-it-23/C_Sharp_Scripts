using UnityEngine;

public class BlendShapeController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer; // Assign your character's mesh

    [Header("Settings")]
    [SerializeField] private float blendShapeToggleValue = 100f; // Set to 0 or 100 when toggled

    // Toggle blend shape when entering a trigger named "E1", "E2", etc.
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.name.StartsWith("E")) return;

        // Extract number from trigger name (e.g., "E5" → 5)
        if (int.TryParse(other.gameObject.name.Substring(1), out int triggerNumber))
        {
            int blendShapeIndex = triggerNumber - 1; // E1 = index 0, E2 = index 1, etc.
            ToggleBlendShape(blendShapeIndex);
        }
        else
        {
            Debug.LogError("Invalid trigger name: " + other.gameObject.name);
        }
    }

    private void ToggleBlendShape(int index)
    {
        if (index < 0 || index >= skinnedMeshRenderer.sharedMesh.blendShapeCount)
        {
            Debug.LogError("Invalid blend shape index: " + index);
            return;
        }

        // Get current weight and flip it (0 ↔ 100)
        float currentWeight = skinnedMeshRenderer.GetBlendShapeWeight(index);
        skinnedMeshRenderer.SetBlendShapeWeight(index, Mathf.Abs(currentWeight - blendShapeToggleValue));
    }
}
