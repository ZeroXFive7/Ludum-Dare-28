using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour 
{
    public GameObject TeleporterDestination = null;

    private Animator animator;

	void Start() 
    {
        animator = GetComponent<Animator>();
	}

    public void Open()
    {
        if (animator != null)
        {
            animator.Play(gameObject.name + "_open");
        }
    }

    public void Close()
    {
        if (animator != null)
        {
            animator.Play(gameObject.name + "_closed");
        }
    }
}
