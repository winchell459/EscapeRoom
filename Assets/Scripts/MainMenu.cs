using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string scienceRoom = "ScienceRoom", egyptianRoom = "EgyptianRoom";
    public UnityEngine.UI.Button egyptianRoomButton;

    public void ScienceRoomButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scienceRoom);
    }

    public void EgyptianRoomButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(egyptianRoom);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if(PlayerPrefs.GetString("ScienceRoom", "incomplete") == "complete")
        {
            egyptianRoomButton.interactable = true;
        }
    }
}
