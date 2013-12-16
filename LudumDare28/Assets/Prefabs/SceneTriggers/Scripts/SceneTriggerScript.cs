using UnityEngine;
using System.Collections;

public class SceneTriggerScript : MonoBehaviour {

    public Vector2 Forward = -Vector2.up;
    public string triggerToScene;
    public string DestinationTag = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public string getScene()
    {
        return triggerToScene;
    }
}
