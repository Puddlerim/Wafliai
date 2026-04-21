using UnityEngine;

public class InspectableItem : MonoBehaviour
{
    [Header("World Object")]
    public GameObject worldObject;

    [Header("Inspect Prefab")]
    public GameObject inspectPrefab;

    [Header("Prompt")]
    public string promptText = "Press E to inspect";

    public void OnPickedUp()
    {
        if (worldObject != null)
            worldObject.SetActive(false);
        else
            gameObject.SetActive(false);
    }

    public void OnInspectClosed()
    {
        // Leave empty for now.
        // Later you can use this if you want the item to return to the world,
        // go into inventory, play a sound, etc.
    }
}