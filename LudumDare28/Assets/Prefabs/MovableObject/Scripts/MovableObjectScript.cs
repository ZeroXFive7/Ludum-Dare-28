using UnityEngine;
using System.Collections;

public class MovableObjectScript : MonoBehaviour {

    public Vector2 objectSpeed = new Vector2(20, 20);
    public float objectDrag = 25.0f;
    private Vector2 movement;
    private bool pressI = false;
    private bool lockMovement = false;
    public float x = 750.0f;
    public float y = 375.0f;
    public Texture2D flower;

    private InventoryScript inventory = new InventoryScript();

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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "SceneTrigger")
        {
            BoxCollider2D boxCollider = collider.gameObject.GetComponent<BoxCollider2D>();
            SceneTriggerScript newScene = (SceneTriggerScript)boxCollider.gameObject.GetComponent<SceneTriggerScript>();
            Application.LoadLevel(newScene.getScene());
        }
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
        else if (collider.tag == "Item")
        {
            BoxCollider2D boxCollider = collider.gameObject.GetComponent<BoxCollider2D>();
            InteractableItemScript item = boxCollider.transform.parent.gameObject.GetComponent<InteractableItemScript>();
            
            if (item != null)
            {
                if (item.pickedUp && pressI)
                {
                    // Add item to your inventory
                    inventory.insertItem(InventoryScript.ITEMS.FLOWER);

                    // Now destroy the item so it dissapears from the world
                    Destroy(item);
                    Destroy(collider.transform.parent.gameObject);
                    lockMovement = false;
                }
                else if (pressI)
                {
                    item.beingPickedUp = true;
                    lockMovement = true;
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
        else if (collider.tag == "Item")
        {
            BoxCollider2D boxCollider = collider.gameObject.GetComponent<BoxCollider2D>();
            InteractableItemScript item = boxCollider.transform.parent.gameObject.GetComponent<InteractableItemScript>();

            if (item != null)
            {
                if (item.pickedUp)
                {
                    item.beingPickedUp = false;
                    lockMovement = false;
                }
            }
        }
    }

    private void getInput()
    {
        pressI = Input.GetKeyUp(KeyCode.I);

        if (lockMovement)
            return;
        
        // Retrieve axis information
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // Movement per direction
        movement = new Vector2(objectSpeed.x * inputX, objectSpeed.y * inputY);
    }

    void OnGUI()
    {
        if (inventory.getItems().Count > 0)
        {
            ArrayList items = inventory.getItems();
            for (int i = 0; i < items.Count*50; i+=50)
            {
                if ((InventoryScript.ITEMS)items[i / 50] == InventoryScript.ITEMS.FLOWER)
                {
                    GUI.backgroundColor = Color.gray;
                    GUI.Box(new Rect(x + i, y, 50f, 50.0f), flower);
                }
            }
        }
    }
}
