using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject armsWithGun;
    public GameObject pickupText;

    private bool inReach = false;
    private PlayerInventory playerInventory;

    void Start()
    {
        if (armsWithGun != null)
        {
            armsWithGun.SetActive(false);
        }

        if (pickupText != null)
        {
            pickupText.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerInventory foundInventory = other.GetComponentInParent<PlayerInventory>();

        if (foundInventory == null)
        {
            foundInventory = other.transform.root.GetComponentInChildren<PlayerInventory>();
        }

        if (other.CompareTag("Reach") || foundInventory != null)
        {
            inReach = true;
            playerInventory = foundInventory;

            Debug.Log("Prie ginklo. Inventory rastas: " + (playerInventory != null));

            if (pickupText != null)
            {
                pickupText.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        PlayerInventory foundInventory = other.GetComponentInParent<PlayerInventory>();

        if (foundInventory == null)
        {
            foundInventory = other.transform.root.GetComponentInChildren<PlayerInventory>();
        }

        if (other.CompareTag("Reach") || foundInventory != null)
        {
            inReach = false;
            playerInventory = null;

            if (pickupText != null)
            {
                pickupText.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.F))
        {
            TryPickUpWeapon();
        }
    }

    void TryPickUpWeapon()
    {
        if (playerInventory == null)
        {
            Debug.Log("Negali paimti ginklo: PlayerInventory nerastas.");
            return;
        }

        if (!playerInventory.HasRequiredItems())
        {
            Debug.Log("Negali paimti ginklo: reikia 2 item'u. Dabar turi: " + playerInventory.collectedItems);
            return;
        }

        if (armsWithGun != null)
        {
            armsWithGun.SetActive(true);
        }
        else
        {
            Debug.Log("KLAIDA: Arms With Gun nepriskirtas WeaponPickup Inspector lange.");
            return;
        }

        if (pickupText != null)
        {
            pickupText.SetActive(false);
        }

        Debug.Log("Ginklas paimtas. Rankos ir ginklas ijungti.");

        Destroy(gameObject);
    }
}