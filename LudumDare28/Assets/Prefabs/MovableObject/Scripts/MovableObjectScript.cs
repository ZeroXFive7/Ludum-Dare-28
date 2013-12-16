using UnityEngine;
using System.Collections;

public class MovableObjectScript : MonoBehaviour {

    public float objectSpeed = 20;
    public float objectDrag = 25.0f;
    private Vector2 movement;
    private bool pressI = false;
    private bool lockMovement = false;
    public float x = 750.0f;
    public float y = 375.0f;
    public Texture2D flower;

    private InventoryScript inventory = new InventoryScript();
    private Animator spriteAnimator;

    private enum FacingDirection { forward, backward, left, right };
    private FacingDirection facing = FacingDirection.forward;

    private string lastSceneTriggerTag = "SceneTrigger";
    private string destinationTag = null;

    // Use this for initialization
	void Start () {
        spriteAnimator = GetComponent<Animator>() as Animator;
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

        Vector2 normalizedVelocity = rigidbody2D.velocity.normalized;
        UpdateFacing(normalizedVelocity);

        UpdateAnimation();
    }

    void OnLevelWasLoaded()
    {
        SceneTriggerScript trigger = GameObject.FindGameObjectWithTag(destinationTag).GetComponent<SceneTriggerScript>() as SceneTriggerScript;

        UpdateFacing(trigger.Forward);
        transform.position = trigger.transform.position + new Vector3(trigger.Forward.x, trigger.Forward.y, 0.0f) * rigidbody2D.renderer.bounds.size.y;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Contains("SceneTrigger"))
        {
            BoxCollider2D boxCollider = collider.gameObject.GetComponent<BoxCollider2D>();

            SceneTriggerScript exitTrigger = (SceneTriggerScript)boxCollider.gameObject.GetComponent<SceneTriggerScript>();
            lastSceneTriggerTag = exitTrigger.gameObject.tag;
            destinationTag = exitTrigger.DestinationTag == null ? lastSceneTriggerTag : exitTrigger.DestinationTag;

            DontDestroyOnLoad(gameObject);
            Application.LoadLevel(exitTrigger.getScene());
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
        movement = new Vector2(inputX, inputY).normalized * objectSpeed;
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

    private void UpdateAnimation()
    {
        string animationPrefix;
        if (rigidbody2D.velocity.sqrMagnitude > 0.0f)
        {
            animationPrefix = "player_run_";
        }
        else
        {
            animationPrefix = "player_stand_";
        }

        spriteAnimator.Play(animationPrefix + facing.ToString());
    }

    private void UpdateFacing(Vector2 facingVector)
    {
        float downDot = Vector2.Dot(-Vector2.up, facingVector);
        float upDot = Vector2.Dot(Vector2.up, facingVector);
        float leftDot = Vector2.Dot(-Vector2.right, facingVector);
        float rightDot = Vector2.Dot(Vector2.right, facingVector);

        if (rightDot > 0.5f)
        {
            facing = FacingDirection.right;
        }
        if (leftDot > 0.5f)
        {
            facing = FacingDirection.left;
        }
        if (upDot > 0.5f)
        {
            facing = FacingDirection.backward;
        }
        if (downDot > 0.5f)
        {
            facing = FacingDirection.forward;
        }
    }
}
