using UnityEngine;
using System.Collections;

public class Script2 : MonoBehaviour {

    private string pickedUpMessage = "Rose picked up a flower";
    public bool pickedUp
    {
        get
        {
            return pickedUp;
        }
        set
        {
            pickedUp = value;
        }
    }

    public bool beingPickedUp
    {
        get
        {
            return beingPickedUp;
        }
        set
        {
            beingPickedUp = value;
        }
    }

	// Use this for initialization
	void Start () {
        beingPickedUp = false;
        pickedUp = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        /*
        // If it's already picked up, don't pick it up again.
        if (beingPickedUp == true && pickedUp == false)
        {
            GUI.contentColor = Color.black;
            GUI.Label(new Rect(25, 25, 1000, 100), pickedUpMessage);
            pickedUp = true;
        }
         * */
    }
}
