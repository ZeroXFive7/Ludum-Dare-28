using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour 
{
    private Animator animator;

	void Start() 
    {
        animator = GetComponent<Animator>();
	}

    public void Open()
    {
        animator.Play(gameObject.name + "_open");
    }

    public void Close()
    {
        animator.Play(gameObject.name + "_closed");
    }
}
