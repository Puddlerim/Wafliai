using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField]
    DialogueManager dialogueManager;

    [SerializeField]
    Dialogue dialogue;

    bool player_detection = false;
    bool currently_talking = false;
    // Update is called once per frame
    void Update()
    {
        if (player_detection && Input.GetKeyDown(KeyCode.F) && !currently_talking)
        {
            currently_talking = true;
            dialogueManager.BeginDialogue(dialogue);
            UI.instance.SetText("");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Capsule Mesh")
        {
            UI.instance.SetText("Press F to interact");
            player_detection = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        player_detection = false;
        currently_talking = false;
        UI.instance.SetText("");
        dialogueManager.ClearText();
    }
}
