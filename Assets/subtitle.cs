using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    public GameObject subtitleRoot; // optional, assign panel or text parent

    public void ShowSubtitle(string text)
    {
        if (subtitleRoot != null)
        {
            subtitleRoot.SetActive(true);
        }

        subtitleText.text = text;
    }

    public void HideSubtitle()
    {
        subtitleText.text = "";

        if (subtitleRoot != null)
        {
            subtitleRoot.SetActive(false);
        }
    }
}