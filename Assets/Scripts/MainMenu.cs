using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string scienceRoom = "ScienceRoom", egyptianRoom = "EgyptianRoom", dungeonRoom = "DungeonRoom", outpostRoom = "OutpostRoom";
    public UnityEngine.UI.Button egyptianRoomButton;
    public UnityEngine.UI.Button dungeonRoomButton;
    public UnityEngine.UI.Button outpostRoomButton;

    public void ScienceRoomButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scienceRoom);
    }

    public void EgyptionRoomButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(egyptianRoom);
    }
    public void DungeonRoomButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(dungeonRoom);
    }
    public void OutpostRoomButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(outpostRoom);
    }
    public void MultiplayerButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Multiplayer");
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (PlayerPrefs.GetString("ScienceRoom", "incomplete") == "complete")
        {
            egyptianRoomButton.interactable = true;
        }
        if (PlayerPrefs.GetString("EgyptianRoom", "incomplete") == "complete")
        {
            dungeonRoomButton.interactable = true;
        }
        if (PlayerPrefs.GetString("DungeonRoom", "incomplete") == "complete")
        {
            outpostRoomButton.interactable = true;
        }

        GameObject networkManager = GameObject.Find("NetworkManager");
        if (networkManager) Destroy(networkManager);
    }

    public void ResetGameButton()
    {
        PlayerPrefs.SetString(scienceRoom, "incomplete");
        PlayerPrefs.SetString(egyptianRoom, "incomplete");
        PlayerPrefs.SetString(dungeonRoom, "incomplete");
        PlayerPrefs.SetString(outpostRoom, "incomplete");
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
