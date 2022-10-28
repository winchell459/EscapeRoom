using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HieroglyphPuzzle : MonoBehaviour
{
    public Hieroglyph[] glyphs;
    public string[] answer;

    private bool completed = false;
    public Transform hiddenObject;

    public void GlyphChanged()
    {
        bool correct = true;
        for(int i = 0; i < glyphs.Length; i += 1)
        {
            if (glyphs[i].text.text != answer[i]) correct = false;
        }

        if (correct)
        {
            completed = true;
            foreach(Hieroglyph glyph in glyphs)
            {
                glyph.active = false;
            }
            hiddenObject.gameObject.SetActive(true);
        }
    }
}
