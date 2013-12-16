using UnityEngine;
using System.Collections;

public class UnmovableObjectScript : MonoBehaviour {

    private Vector3 origSize;
    public bool useCustomColliderBox = false;
	// Use this for initialization
	void Start () {

        Vector3 actualSize = getActualSize();
        // change the boxColliderSize to be the size of the image.
        if (!useCustomColliderBox)
        {
            BoxCollider2D box = (BoxCollider2D)this.gameObject.collider2D;
            if (box != null)
            {
                box.size = actualSize;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    /*
     * Do all physics based updates here. Currently, not movable objects shouldn't do anything
     * but we may want to change that later.
    */
    void FixedUpdate()
    {
        
    }

    private Vector3 getActualSize()
    {
        SpriteRenderer sr = this.gameObject.GetComponent("SpriteRenderer") as SpriteRenderer;
        Vector3 size = this.gameObject.transform.localScale;
        float x = sr.bounds.size.x / size.x;
        float y = sr.bounds.size.y / size.y;
        float z = sr.bounds.size.z / size.z;
        return new Vector3(x, y, z);
    }
}
