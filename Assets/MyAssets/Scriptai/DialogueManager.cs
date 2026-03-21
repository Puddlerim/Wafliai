using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    GameObject dialoguePanel;

    [SerializeField]
    TextMeshProUGUI dialogueText;

    [SerializeField]
    GameObject buttonPrefab;

    [SerializeField]
    Transform buttonsParent;
    public void BeginDialogue(Dialogue dialogue)
    {
        if (dialogue.Choices.Count == 0)
        {
            dialoguePanel.SetActive(false);

            FirstPersonMovement.ToggleMovement(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else { 
            FirstPersonMovement.ToggleMovement(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            dialoguePanel.SetActive(true);

            ClearChoices();
            AnimateText(dialogue);
        }
    }
    public void ClearText()
    {
        dialogueText.text = "";
    }
    void ClearChoices()
    {
        foreach (Transform child in buttonsParent)
            Destroy(child.gameObject);
    }
    void AnimateText(Dialogue dialogue)
    {
        IEnumerator TypeText(string text)
        {
            StringBuilder textToShow = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                textToShow.Append(text[i]);
                dialogueText.text = textToShow.ToString();

                yield return new WaitForSeconds(1f / 20f);
            }
            ShowChoices(dialogue);
        }
        StartCoroutine(TypeText(dialogue.DialogueText));
    }
    void ShowChoices(Dialogue dialogue)
    {
        foreach (Dialogue choice in dialogue.Choices)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonsParent);

            newButton.GetComponentInChildren<TextMeshProUGUI>().text = choice.OptionName;
            newButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                BeginDialogue(choice);
            });
        }
    }
}
