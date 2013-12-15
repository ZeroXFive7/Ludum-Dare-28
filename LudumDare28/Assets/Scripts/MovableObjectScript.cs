using UnityEngine;
using System.Collections;

public class MovableObjectScript : MonoBehaviour {

    public Vector2 objectSpeed = new Vector2(20, 20);
    public float objectDrag = 25.0f;
    private Vector2 movement;
    private bool pressA = false;

    // Use this for initialization
	void Start () {
	
	}

    void Update()
    {
        getInput();
    }

    /*
     * Do all physics based updates here
     */
    void FixedUpdate()
    {
        // Move the game object
        rigidbody2D.drag = objectDrag;
        rigidbody2D.velocity = movement;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "NPC")
        {
            BoxCollider2D boxCollider = collider.gameObject.GetComponent<BoxCollider2D>();
            NPCCharacterScript npc = boxCollider.transform.parent.gameObject.GetComponent<NPCCharacterScript>();

            if (npc != null)
            {
                // Avoid friendly fire
                if (pressA)
                {
                    Debug.Log(npc.getConversation());
                }
            }
        }
    }

    private void getInput()
    {
        // Retrieve axis information
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // Movement per direction
        movement = new Vector2(objectSpeed.x * inputX, objectSpeed.y * inputY);

        pressA = Input.GetKey(KeyCode.I);
    }
}
