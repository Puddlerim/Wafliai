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
            armsWithGun.SetActive(false);

        if (pickupText != null)
            pickupText.SetActive(false);
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

            Debug.Log("GINKLAS: Inventory rastas: " + (playerInventory != null));

            if (playerInventory != null)
            {
                Debug.Log("GINKLAS: PlayerInventory item'u kiekis: " + playerInventory.collectedItems);
            }

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
        if (inReach && Input.GetKeyDown(KeyCode.F))
        {
            TryPickUpWeapon();
        }
    }

    void TryPickUpWeapon()
    {
        if (playerInventory == null)
        {
            Debug.Log("GINKLAS KLAIDA: PlayerInventory nerastas.");
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
            Debug.Log("GINKLAS KLAIDA: Arms With Gun nepriskirtas.");
            return;
        }

        if (pickupText != null)
            pickupText.SetActive(false);

        Debug.Log("Ginklas paimtas.");

        Destroy(gameObject);
    }
}