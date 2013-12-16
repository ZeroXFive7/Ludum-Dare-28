using UnityEngine;
using System.Collections;

public class InteractableItemScript : MonoBehaviour {

    private string pickedUpMessage = "Rose picked up a flower";
    public bool pickedUp { get; set; }
    public bool beingPickedUp { get; set; }

	// Use this for initialization
	void Start () 
    {
        beingPickedUp = false;
        pickedUp = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        // If it's already picked up, don't pick it up again.
        if (beingPickedUp == true)
        {
            GUI.contentColor = Color.black;
            GUI.Label(new Rect(25, 25, 1000, 100), pickedUpMessage);
            pickedUp = true;
        }
    }
}
