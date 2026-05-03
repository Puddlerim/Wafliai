using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int collectedItems = 0;
    public int requiredItems = 2;

    public bool HasRequiredItems()
    {
        return collectedItems >= requiredItems;
    }

    public void AddItem()
    {
        collectedItems++;
        Debug.Log("Surinkta item'u: " + collectedItems);
    }
}