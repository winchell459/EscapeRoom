using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InputText : Puzzle
{
    public InputField inputField;
    public Text inputText;
    public Button button;
    public GameObject cabinet;
    public GameObject computer;
    public GameObject itemDrop;

    public string passcode = "12345";
    public bool changed = false;

    public void ButtonClicked()
    {
        Debug.Log(inputField.text);
        if (inputField.text == passcode)
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
            else if (itemDrop != null)
            {
                itemDrop.SetActive(true);
            }
            if(FindObjectOfType<GameHandler>()) FindObjectOfType<GameHandler>().ObjectTriggered(this);
        }
    }

    public void OnValueChanged(string value)
    {
        int index = FindObjectOfType<EscapeNetworkObjects>().GetTextIndex(this);
        changed = true;
        FindObjectOfType<Grabber>().networkPlayer.OnTextInputValueChanged(value, index);
    }

    public FixedString32Bytes GetValue()
    {
        return new FixedString32Bytes(inputField.text);
    }

    public void SetValue(FixedString32Bytes value)
    {
        inputField.SetTextWithoutNotify(value.ToString());
    }
}
