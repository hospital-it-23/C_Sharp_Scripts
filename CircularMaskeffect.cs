using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CircleMaskEffect : MonoBehaviour
{
    public Material material;
    [Range(0, 1)] public float radius = 0.5f;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material != null)
        {
            material.SetFloat("_Radius", radius);
            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
