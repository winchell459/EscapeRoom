using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutpostTerminal : MonoBehaviour
{
    public GameObject[] terminalObjects;
    // Start is called before the first frame update
    void Start()
    {
        ToggleObjects(false);
    }

    public void TerminalButton()
    {
        if (terminalObjects.Length > 0) ToggleObjects(!terminalObjects[0].activeSelf);
    }

    private void ToggleObjects(bool on)
    {
        foreach (GameObject to in terminalObjects) to.SetActive(on);
    }
}
