using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField]
    DialogueManager dialogueManager;

    [SerializeField]
    Dialogue dialogue;

    bool player_detection = false;
    // Update is called once per frame
    void Update()
    {
        if (player_detection && Input.GetKeyDown(KeyCode.E))
        {
            dialogueManager.BeginDialogue(dialogue);
            UI.instance.SetText("");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Capsule Mesh")
        {
            UI.instance.SetText("Press E to interact");
            player_detection = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        player_detection = false;
        UI.instance.SetText("");
        dialogueManager.ClearText();
    }
}
