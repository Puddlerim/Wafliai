using UnityEngine;

public class CollectItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory inventory = other.transform.root.GetComponentInChildren<PlayerInventory>();

        if (inventory != null)
        {
            inventory.AddItem();
            Destroy(gameObject);
        }
    }
}