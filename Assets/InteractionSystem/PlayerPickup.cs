using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public InspectManager inspectManager;
    public GameObject pickupText;

    private InspectableItem currentItem;

    private void Update()
    {
        if (inspectManager != null && inspectManager.IsInspecting)
            return;

        if (currentItem != null && Input.GetKeyDown(KeyCode.E))
        {
            inspectManager.StartInspect(currentItem);

            if (pickupText != null)
                pickupText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        InspectableItem item = other.GetComponent<InspectableItem>();
        if (item != null)
        {
            currentItem = item;

            if (pickupText != null)
                pickupText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InspectableItem item = other.GetComponent<InspectableItem>();
        if (item != null && currentItem == item)
        {
            currentItem = null;

            if (pickupText != null)
                pickupText.SetActive(false);
        }
    }
}