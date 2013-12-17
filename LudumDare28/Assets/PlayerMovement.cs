using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float RunSpeed = 5.0f;

    private Animator spriteAnimator;

    private Vector3 input;

    private enum FacingDirection { forward, backward, left, right };
    private FacingDirection facing = FacingDirection.forward;

	void Start()
    {
        spriteAnimator = GetComponent<Animator>() as Animator;
	}
	
	void Update()
    {
        GetInput();
	}

    void FixedUpdate()
    {
        rigidbody.velocity = input.normalized * RunSpeed;

        UpdateFacing(new Vector2(input.x, input.z));
        UpdateAnimation();
    }

    void LateUpdate()
    {
        HideOccluders();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "DoorTrigger")
        {
            DoorScript door = FindDoor(collider.gameObject);
            if (door == null)
            {
                return;
            }

            door.Open();

            if (door.TeleporterDestination != null)
            {
                transform.position = door.TeleporterDestination.transform.position;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "DoorTrigger")
        {
            DoorScript door = FindDoor(collider.gameObject);
            if (door != null)
            {
                door.Close();
            }
        }
    }

    private void GetInput()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
    }

    private void UpdateAnimation()
    {
        string animationPrefix;
        if (rigidbody.velocity.sqrMagnitude > 0.0f)
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

    private void HideOccluders()
    {
        float toCamLength = (Camera.main.transform.position - transform.position).magnitude;
        Ray ray = new Ray(transform.position, -Vector3.forward);

        RaycastHit[] results = Physics.RaycastAll(ray, toCamLength);
        foreach (RaycastHit result in results)
        {
            Transform parent = result.transform.gameObject.transform.parent;
            if (parent != null)
            {
                for (int i = 0; i < parent.childCount; ++i)
                {
                    GameObject child = parent.GetChild(i).gameObject;
                    OcclusionTransparency occlusion = child.GetComponent<OcclusionTransparency>();
                    if (child == result.transform.gameObject || occlusion != null && occlusion.IsChainable)
                    {
                        SetTransparency(parent.GetChild(i).gameObject);
                    }
                }
            }
            else
            {
                SetTransparency(result.transform.gameObject);
            }
        }
    }

    private void SetTransparency(GameObject gameObject)
    {
        SpriteRenderer occluder = gameObject.renderer as SpriteRenderer;
        if (occluder == null)
        {
            return;
        }

        OcclusionTransparency config = gameObject.GetComponent<OcclusionTransparency>();

        Color oldColor = occluder.color;
        occluder.color = new Color(oldColor.r, oldColor.g, oldColor.b, config.AlphaAmount);

        config.IsTransparent = true;
    }

    private DoorScript FindDoor(GameObject doorCollider)
    {
        for (int i = 0; i < doorCollider.transform.parent.childCount; ++i)
        {
            GameObject child = doorCollider.transform.parent.GetChild(i).gameObject;
            if (child.tag == "Door")
            {
                return child.GetComponent<DoorScript>();
            }
        }
        return null;
    }
}
