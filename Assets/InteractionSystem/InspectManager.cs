using UnityEngine;
using UnityEngine.Rendering;

public class InspectManager : MonoBehaviour
{
    public Transform inspectPoint;
    public float rotationSpeed = 180f;
    public float zoomSpeed = 2f;
    public float minDistance = 0.4f;
    public float maxDistance = 1.5f;
    public float startDistance = 0.8f;

    public FirstPersonLook playerLookScript;
    public FirstPersonMovement playerMoveScript;

    public Volume inspectVolume;

    private GameObject currentInspectObject;
    private InspectableItem currentItem;
    private bool isInspecting = false;
    private float currentDistance;

    public bool IsInspecting => isInspecting;

    void Awake()
    {
        // Defensive auto-assignment if references were not set in the Inspector
        if (playerLookScript == null)
            playerLookScript = FindObjectOfType<FirstPersonLook>();
        if (playerMoveScript == null)
            playerMoveScript = FindObjectOfType<FirstPersonMovement>();
    }

    public void StartInspect(InspectableItem item)
    {
        if (item == null || item.inspectPrefab == null || inspectPoint == null)
            return;

        if (isInspecting)
            return;

        currentItem = item;
        currentItem.OnPickedUp();

        // Spawn directly as a child of the inspect point.
        currentInspectObject = Instantiate(item.inspectPrefab, inspectPoint);
        currentInspectObject.transform.localPosition = Vector3.forward * startDistance;
        currentInspectObject.transform.localRotation = Quaternion.identity;
        currentInspectObject.transform.localScale = Vector3.one;

        currentDistance = startDistance;
        isInspecting = true;

        // Freeze camera look, but DO NOT disable the script.
        if (playerLookScript != null)
            playerLookScript.FreezeLook();

        if (playerMoveScript != null)
            playerMoveScript.enabled = false;

        if (inspectVolume != null)
            inspectVolume.enabled = true;

        // Optional but recommended for inspect mode.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Stop physics from affecting the inspect object.
        Rigidbody[] rigidbodies = currentInspectObject.GetComponentsInChildren<Rigidbody>(true);
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }

    public void StopInspect()
    {
        if (!isInspecting)
            return;

        if (currentInspectObject != null)
            Destroy(currentInspectObject);

        currentInspectObject = null;

        if (currentItem != null)
            currentItem.OnInspectClosed();

        currentItem = null;
        isInspecting = false;

        if (playerLookScript != null)
            playerLookScript.ResumeLook();

        if (playerMoveScript != null)
            playerMoveScript.enabled = true;

        if (inspectVolume != null)
            inspectVolume.enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!isInspecting || currentInspectObject == null)
            return;

        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        // Rotate the object around its own local axes.
        currentInspectObject.transform.Rotate(Vector3.up, -mouseX * rotationSpeed * Time.deltaTime, Space.Self);
        currentInspectObject.transform.Rotate(Vector3.right, mouseY * rotationSpeed * Time.deltaTime, Space.Self);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.001f)
        {
            currentDistance -= scroll * zoomSpeed;
            currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
            currentInspectObject.transform.localPosition = Vector3.forward * currentDistance;
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            StopInspect();
        }
    }
}