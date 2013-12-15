using UnityEngine;
using System.Collections;

public class NPCCharacterScript : MonoBehaviour {

    public Vector2 objectSpeed = new Vector2(20, 20);
    public float objectDrag = 25.0f;
    private Vector2 movement;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        // Retrieve axis information
        //float inputX = Input.GetAxis("Horizontal");
        //float inputY = Input.GetAxis("Vertical");

        // Movement per direction
        //movement = new Vector2(objectSpeed.x * inputX, objectSpeed.y * inputY);
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

    public string getConversation()
    {
        return "Hello, I'm an NPC!";
    }
}
