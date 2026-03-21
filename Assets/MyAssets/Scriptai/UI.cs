using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Text = default;
  
    public static UI instance;
    private void Awake()
    {
        instance = this;
    }
    public void SetText(string subtitle)
    {
        Text.text = subtitle;
    }
}
