using TMPro;
using UnityEngine;

public class PickupCounter : MonoBehaviour
{
    public TextMeshProUGUI counterText;

    private int pickupCount = 0;

    private void Start()
    {
        UpdateCounterText();
    }

    public void AddPickup()
    {
        pickupCount++;
        UpdateCounterText();
    }

    private void UpdateCounterText()
    {
        counterText.text = "Picked up items: " + pickupCount;
    }
}