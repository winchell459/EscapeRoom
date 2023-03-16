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
    public bool changed;

    public string passcode = "12345";

    public void ButtonClicked()
    {
        Debug.Log(inputText.text);
        if(inputText.text == passcode)
        {
            inputField.interactable = false;
            Debug.Log("correct");
            cabinet.SendMessage("Unlocked");
            if(FindObjectOfType<GameHandler>()) FindObjectOfType<GameHandler>().ObjectTriggered(this);
        }
    }

    public void OnValueChanged(string value)
    {
        //Debug.Log($"OnValueChanged({value})");
        int index = FindObjectOfType<EscapeNetworkObjects>().GetTextIndex(this);
        changed = true;
        FindObjectOfType<Grabber>().networkPlayer.OnTextInputValueChanged(value, index);
    }
    public FixedString32Bytes GetValue()
    {
        return new FixedString32Bytes(inputText.text);
    }
    public void SetValue(FixedString32Bytes value)
    {
        Debug.Log($"SetValue({value})");
        inputField.SetTextWithoutNotify(value.ToString());
    }
}
