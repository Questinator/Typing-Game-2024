using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueBox;

    [SerializeField]
    private TMP_Text dialogueTextBox;

    /// <summary>
    /// Open the dialogue box and show the given text
    /// </summary>
    /// <param name="text">Text to show in the dialogue box</param>
    public void WriteDialogueBoxText(string text)
    {
        dialogueBox.SetActive(true);
        dialogueTextBox.text = text;
    }

    /// <summary>
    /// Close the dialogue box
    /// </summary>
    public void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
    }
}
