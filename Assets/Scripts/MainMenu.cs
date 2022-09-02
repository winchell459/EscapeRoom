using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "SampleScene";
    public void StartButton()
    {
        SceneUtility.LoadScene(gameSceneName);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
