using UnityEngine;

public class ColorSetter : MonoBehaviour
{
    public MeshRenderer[] meshRenderers;

    public void SetColor(Material material)
    {
        for(int i = 0; i < meshRenderers.Length; i++)
        {
            var mats = meshRenderers[i].materials;
            mats[0] = material;
            meshRenderers[i].materials = mats;
        }
    }
}
