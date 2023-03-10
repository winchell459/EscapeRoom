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
    public GameObject computer;

    public string passcode = "12345";

    public void ButtonClicked()
    {
        Debug.Log(inputText.text);
        if (inputText.text == passcode)
        {
            inputField.interactable = false;
            Debug.Log("correct");
            if (cabinet != null)
            {
                cabinet.SendMessage("Unlocked");
            }
            else if (computer != null)
            {
                computer.SendMessage("CorrectPassword");
            }
            FindObjectOfType<GameHandler>().ObjectTriggered(this);
        }
    }
}
