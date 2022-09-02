using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseCode : InputText
{
    public AudioSource audio;

    public void AudioButton()
    {
        audio.Play();
    }
}
