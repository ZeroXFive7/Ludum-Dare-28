using UnityEngine;
using System.Collections;

public class InventoryScript : MonoBehaviour {

    private ArrayList items = new ArrayList();
    public enum ITEMS { FLOWER };
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void insertItem(ITEMS item)
    {
        items.Add(item);
    }

    public ArrayList getItems()
    {
        return items;
    }
}
