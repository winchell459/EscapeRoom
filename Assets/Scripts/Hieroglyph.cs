using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hieroglyph : MonoBehaviour
{
    public string[] hieroglyphics;
    
    int index = 0;
    public Text text;

    public bool active = true;
    public void Selected()
    {
        if (active)
        {
            index = (index + 1) % hieroglyphics.Length;
            text.text = hieroglyphics[index];
            FindObjectOfType<HieroglyphPuzzle>().GlyphChanged();
        }
        
    }

}
