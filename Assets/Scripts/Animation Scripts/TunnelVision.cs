using UnityEngine;
using System.Collections;

public class TunnelVision : MonoBehaviour {

    public MeshRenderer selfRenderer;

    void Update()
    {
        Material mat = selfRenderer.material;

        float xOffset = mat.mainTextureOffset.x + Time.deltaTime * 0.05f;
        float yOffset = mat.mainTextureOffset.y + Time.deltaTime * 0.05f;

        float xScale = mat.mainTextureScale.x + Time.deltaTime * 0.25f * Random.Range(-0.2f, 0.2f);
        float yScale = mat.mainTextureScale.y + Time.deltaTime * 0.25f * Random.Range(-0.2f, 0.2f);

        Color currentColor = mat.GetColor("_ReflectColour") + new Color(Random.Range(-1.0f, 1.0f) * 1.5f * Time.deltaTime, 
                                                                        Random.Range(-1.0f, 1.0f) * 1.5f * Time.deltaTime, 
                                                                        Random.Range(-1.0f, 1.0f) * 1.5f * Time.deltaTime,
                                                                        0);
        mat.mainTextureOffset = new Vector2(xOffset, yOffset);        
        mat.mainTextureScale = new Vector2(xScale, yScale);
        mat.SetColor("_ReflectColour", currentColor);
    }
}
