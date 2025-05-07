using UnityEngine;

public class InitializeShapeKeys : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public bool initializeOnStart = true;

    void Start()
    {
        if (initializeOnStart)
        {
            InitializeAllShapeKeysTo100();
        }
    }

    [ContextMenu("Initialize All Shape Keys to 100")]
    public void InitializeAllShapeKeysTo100()
    {
        if (skinnedMeshRenderer == null)
        {
            skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
            if (skinnedMeshRenderer == null)
            {
                Debug.LogError("No SkinnedMeshRenderer found!");
                return;
            }
        }

        Mesh mesh = skinnedMeshRenderer.sharedMesh;
        for (int i = 0; i < mesh.blendShapeCount; i++)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(i, 100f);
        }

        Debug.Log($"Initialized all {mesh.blendShapeCount} shape keys to 100");
    }

    [ContextMenu("Reset All Shape Keys to 0")]
    public void ResetAllShapeKeysTo0()
    {
        if (skinnedMeshRenderer == null)
        {
            skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
            if (skinnedMeshRenderer == null)
            {
                Debug.LogError("No SkinnedMeshRenderer found!");
                return;
            }
        }

        Mesh mesh = skinnedMeshRenderer.sharedMesh;
        for (int i = 0; i < mesh.blendShapeCount; i++)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(i, 0f);
        }

        Debug.Log($"Reset all {mesh.blendShapeCount} shape keys to 0");
    }
}