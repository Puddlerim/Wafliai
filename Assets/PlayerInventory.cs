using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public int collectedItems = 0;
    public int requiredItems = 2;

    public TextMeshProUGUI itemsText;

    void Start()
    {
        UpdateText();
    }

    public void AddItem()
    {
        collectedItems++;
        Debug.Log("PLAYER INVENTORY: dabar turi item'u: " + collectedItems);
        UpdateText();
    }

    public bool HasRequiredItems()
    {
        return collectedItems >= requiredItems;
    }

    void UpdateText()
    {
        if (itemsText != null)
        {
            itemsText.text = "Picked up items: " + collectedItems;
        }
    }
}