using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] private Transform character;

    public float sensitivity = 2f;
    public float smoothing = 1.5f;

    private Vector2 velocity;
    private Vector2 frameVelocity;
    private bool canLook = true;

    void Reset()
    {
        FirstPersonMovement movement = GetComponentInParent<FirstPersonMovement>();
        if (movement != null)
            character = movement.transform;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (character == null)
            return;

        if (!canLook)
        {
            frameVelocity = Vector2.zero;
            return;
        }

        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1f / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90f, 90f);

        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }

    public void FreezeLook()
    {
        Debug.Log("FreezeLook called on " + gameObject.name);
        canLook = false;
        frameVelocity = Vector2.zero;
    }

    public void ResumeLook()
    {
        canLook = true;
        frameVelocity = Vector2.zero;

        float pitch = transform.localEulerAngles.x;
        if (pitch > 180f) pitch -= 360f;

        float yaw = character.localEulerAngles.y;
        if (yaw > 180f) yaw -= 360f;

        velocity = new Vector2(yaw, -pitch);
    }
}