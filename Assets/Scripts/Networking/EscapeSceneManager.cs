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
        if (IsServer)
        {
            var status = NetworkManager.SceneManager.LoadScene(m_SceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
            if (status != SceneEventProgressStatus.Started)
            {
                Debug.LogWarning($"Failed to load {m_SceneName} " +
                      $"with a {nameof(SceneEventProgressStatus)}: {status}");
            }
        }
    }
}
