using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputText : Puzzle
{
    public InputField inputField;
    public Text inputText;
    public Button button;

    public string passcode = "12345";

    public void ButtonClicked()
    {
        if(inputText.text == passcode)
        {
            inputField.interactable = false;
            FindObjectOfType<GameHandler>().ObjectTriggered(this);
        }
    }
}
