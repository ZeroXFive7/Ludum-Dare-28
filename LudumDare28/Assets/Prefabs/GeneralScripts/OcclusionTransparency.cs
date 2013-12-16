using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class OcclusionTransparency : MonoBehaviour
{
    public float AlphaAmount = 0.4f;
    public bool IsChainable = false;
    public bool IsTransparent = false;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = renderer as SpriteRenderer;
    }

	void Update()
    {
        if (IsTransparent)
        {
            Color oldColor = spriteRenderer.color;
            spriteRenderer.color = new Color(oldColor.r, oldColor.g, oldColor.b, 1.0f);
        }
	}
}
