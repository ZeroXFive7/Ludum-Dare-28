using UnityEngine;
using System.Collections;

public class ShadowScript : MonoBehaviour
{
    private DayNightScript dayNightSystem;
    private SpriteRenderer shadowRenderer;

    private Vector2 defaultScale;
    private BoxCollider parentCollider;

	// Use this for initialization
	void Start()
    {
        dayNightSystem = FindObjectOfType<DayNightScript>() as DayNightScript;
        shadowRenderer = gameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

        parentCollider = transform.parent.gameObject.GetComponent<BoxCollider>();
        defaultScale = new Vector3(parentCollider.size.x, parentCollider.size.z, 1.0f);
	}
	
	// Update is called once per frame
	void Update()
    {
        Vector2 sunDirection = dayNightSystem.SunDirection;

        float dot = Vector2.Dot(sunDirection, Vector2.up);
        transform.localPosition = new Vector3(sunDirection.x * 0.3f, parentCollider.center.y - parentCollider.size.y / 2.0f, 0.0f);
        transform.localScale = defaultScale + new Vector2((1.0f - dot) / 2.0f, 0.0f);

        shadowRenderer.color = new Color(1.0f, 1.0f, 1.0f, dot);
	}
}
