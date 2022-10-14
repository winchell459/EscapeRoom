using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microscope : MonoBehaviour
{
    public GameObject microscopeView;
    bool open = false;
    public void Selected()
    {
        if (!open)
        {
            microscopeView.SetActive(true);
            open = true;
        }
    }

    private void Update()
    {
        if (open && Input.GetKeyUp(KeyCode.Escape))
        {
            microscopeView.SetActive(false);
            open = false;
        }
    }
}
