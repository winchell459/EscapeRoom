using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseCode : InputText
{
    public AudioSource morse;

    public void AudioButton()
    {
        morse.Play();
        Debug.Log("audio button");
    }
}
