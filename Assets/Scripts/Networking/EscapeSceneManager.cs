using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class EscapeSceneManager : NetworkBehaviour
{
    [SerializeField]
    private string m_SceneName = "Multiplayer2";

    public void LoadScene()
    {
        LoadScene(m_SceneName);
    }

    public void LoadScene(string sceneName)
    {
        if (IsServer)
        {
            var status = NetworkManager.SceneManager.LoadScene(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
            if (status != SceneEventProgressStatus.Started)
            {
                Debug.LogWarning($"Failed to load {sceneName} " +
                      $"with a {nameof(SceneEventProgressStatus)}: {status}");
            }
        }
    }
}
