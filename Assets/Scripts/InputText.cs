using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputText : Puzzle
{
    public InputField inputField;
    public Text inputText;
    public Button button;
    public GameObject cabinet;

    public string passcode = "12345";

    public void ButtonClicked()
    {
        Debug.Log(inputText.text);
        if (inputText.text == passcode)
        {
            inputField.interactable = false;
            Debug.Log("correct");
            cabinet.SendMessage("Unlocked");
            FindObjectOfType<GameHandler>().ObjectTriggered(this);
        }
    }
}
