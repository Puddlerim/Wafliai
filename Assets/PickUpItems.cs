using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    public GameObject pickupOB;
    public GameObject activatingOB;
    public GameObject pickupText;

    public PickupCounter pickupCounter;

    public bool inReach;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;

            if (pickupText != null)
                pickupText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;

            if (pickupText != null)
                pickupText.SetActive(false);
        }
    }

    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            if (pickupOB != null)
                pickupOB.SetActive(false);

            if (activatingOB != null)
                activatingOB.SetActive(true);

            if (pickupText != null)
                pickupText.SetActive(false);

            if (pickupCounter != null)
                pickupCounter.AddPickup();

            inReach = false;
        }
    }
}