using UnityEngine;

public class InspectableItem : MonoBehaviour
{
    [Header("World Object")]
    public GameObject worldObject;

    [Header("Inspect Prefab")]
    public GameObject inspectPrefab;

    [Header("Prompt")]
    public string promptText = "Press E to inspect";

    [Header("Pickup Counter")]
    public PickupCounter pickupCounter;

    private bool hasBeenPickedUp = false;

    private void Start()
    {
        if (pickupCounter == null)
            pickupCounter = FindObjectOfType<PickupCounter>();
    }

    public void OnPickedUp()
    {
        if (hasBeenPickedUp)
            return;

        hasBeenPickedUp = true;

        if (pickupCounter != null)
            pickupCounter.AddPickup();

        if (worldObject != null)
            worldObject.SetActive(false);
        else
            gameObject.SetActive(false);
    }

    public void OnInspectClosed()
    {
        // Leave empty for now.
    }
}