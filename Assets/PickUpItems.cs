using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    public GameObject pickupOB;
    public GameObject activatingOB;
    public GameObject pickupText;

    public bool inReach;

    private PlayerInventory playerInventory;

    void Start()
    {
        if (pickupText != null)
            pickupText.SetActive(false);

        if (activatingOB != null)
            activatingOB.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;

            playerInventory = other.GetComponentInParent<PlayerInventory>();

            if (playerInventory == null)
            {
                playerInventory = other.transform.root.GetComponentInChildren<PlayerInventory>();
            }

            Debug.Log("Prie item. Inventory rastas: " + (playerInventory != null));

            if (pickupText != null)
                pickupText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            playerInventory = null;

            if (pickupText != null)
                pickupText.SetActive(false);
        }
    }

    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        if (playerInventory == null)
        {
            Debug.Log("KLAIDA: PlayerInventory nerastas, item nepridetas.");
            return;
        }

        playerInventory.AddItem();

        if (pickupOB != null)
            pickupOB.SetActive(false);
        else
            gameObject.SetActive(false);

        if (activatingOB != null)
            activatingOB.SetActive(true);

        if (pickupText != null)
            pickupText.SetActive(false);

        inReach = false;

        Debug.Log("Item paimtas. Dabar turi item'u: " + playerInventory.collectedItems);
    }
}