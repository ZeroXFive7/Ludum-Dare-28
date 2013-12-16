using UnityEngine;
using System.Collections;

public class MovableObjectScript : MonoBehaviour {

    public Vector2 objectSpeed = new Vector2(20, 20);
    public float objectDrag = 25.0f;
    private Vector2 movement;
    private bool pressI = false;

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
                if (pressI)
                {
                    npc.continueConversation();
                    pressI = false;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "NPC")
        {
            BoxCollider2D boxCollider = collider.gameObject.GetComponent<BoxCollider2D>();
            NPCCharacterScript npc = boxCollider.transform.parent.gameObject.GetComponent<NPCCharacterScript>();

            if (npc != null)
            {
                // Leave the conversation if needed
                npc.leaveConversation();
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

        pressI = Input.GetKeyUp(KeyCode.I);
    }
}
