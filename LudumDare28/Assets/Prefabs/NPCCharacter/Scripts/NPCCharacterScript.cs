using UnityEngine;
using System.Collections;

public class NPCCharacterScript : MonoBehaviour {

    public Vector2 objectSpeed = new Vector2(20, 20);
    public float objectDrag = 25.0f;
    private Vector2 movement;
    private bool inConversation = false;
    public string[] conversationPieces;
    public bool useCustomColliderBox = false;
    private int conversationPlace = -1; // so it's incremented to the beginning of the conversation at first.

    // Use this for initialization
    void Start()
    {
        if (!useCustomColliderBox)
        {
            Vector3 actualSize = getActualSize();
        // change the boxColliderSize to be the size of the image.
            BoxCollider2D box = (BoxCollider2D) this.gameObject.collider2D;
            if (box != null)
            {
                box.size = actualSize;
            }

            BoxCollider2D childTrigger = (BoxCollider2D)this.gameObject.transform.GetChild(0).collider2D;
            if (childTrigger != null)
            {
                // make the trigger slightly larger than the npc.
                childTrigger.size = actualSize*1.1f;
            }

        
        }
        
    }

    void Update()
    {
        // Retrieve axis information
        //float inputX = Input.GetAxis("Horizontal");
        //float inputY = Input.GetAxis("Vertical");

        // Movement per direction. NPC won't move right now.
        movement = new Vector2(0.0f, 0.0f);
    }

    /*
     * Do all physics based updates here
     */
    void FixedUpdate()
    {
        // This keeps the object from having mementum
        if (this.rigidbody2D != null)
        {
            rigidbody2D.drag = objectDrag;
            rigidbody2D.velocity = movement;
        }
    }

    public void continueConversation()
    {
        inConversation = true;
        conversationPlace++;

        if (conversationPlace >= conversationPieces.Length)
        {
            leaveConversation();
        }
        
    }

    public void leaveConversation()
    {
        inConversation = false;
        conversationPlace = -1;
    }

    void OnGUI()
    {
        if (inConversation)
        {
            GUI.contentColor = Color.black;
            GUI.Label(new Rect(25, 25, 1000, 100), (string) conversationPieces[conversationPlace]); 
        }
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
