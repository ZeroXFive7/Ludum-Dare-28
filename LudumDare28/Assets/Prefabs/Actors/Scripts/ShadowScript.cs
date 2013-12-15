using UnityEngine;
using System.Collections;

public class ShadowScript : MonoBehaviour
{
    private DayNightScript dayNightSystem;
    private SpriteRenderer shadowRenderer;

    private Vector2 defaultScale;

	// Use this for initialization
	void Start()
    {
        dayNightSystem = FindObjectOfType<DayNightScript>() as DayNightScript;
        shadowRenderer = gameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

        Vector2 parentColliderSize = transform.parent.gameObject.GetComponent<BoxCollider2D>().size;
        transform.position -= new Vector3(0.0f, 0.5f * parentColliderSize.y, 0.0f);
        defaultScale = new Vector3(parentColliderSize.x, parentColliderSize.y / 2.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update()
    {
        Vector2 sunDirection = dayNightSystem.SunDirection;
        Debug.Log(sunDirection);

        float dot = Vector2.Dot(sunDirection, Vector2.up);
        transform.position = new Vector3(sunDirection.x / 2.0f, transform.position.y, 0.0f);
        transform.localScale = defaultScale + new Vector2(1.0f - dot, 0.0f);

        if (dot < 0.0f)
        {
            shadowRenderer.enabled = false;
        }
        else
        {
            shadowRenderer.enabled = true;
        }
	}
}
