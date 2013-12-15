using UnityEngine;
using System.Collections;

public class NPCCharacterScript : MonoBehaviour {

    public Vector2 objectSpeed = new Vector2(20, 20);
    public float objectDrag = 25.0f;
    private Vector2 movement;
    private bool inConversation = false;
    private ArrayList conversationPieces = new ArrayList();
    private int conversationPlace = -1; // so it's incremented to the beginning of the conversation at first.

    // Use this for initialization
    void Start()
    {
        conversationPieces.Add("Hello, I'm an NPC");
        conversationPieces.Add("I think Max, Jake, and Josh are awesome!");
        conversationPieces.Add("I also hate people. Go away!");
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
        rigidbody2D.drag = objectDrag;
        rigidbody2D.velocity = movement;
    }

    public void setConversation(ArrayList conversation)
    {
        conversationPieces = conversation;
    }

    public void continueConversation()
    {
        inConversation = true;
        conversationPlace++;

        if (conversationPlace >= conversationPieces.Count)
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
}
