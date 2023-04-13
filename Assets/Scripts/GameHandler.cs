using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject playerPrefab;
    public Puzzle[] objectTriggers;
    private bool[] triggeredObjects;
    public List<Transform> players = new List<Transform>();
    private List<bool> playersTriggered = new List<bool>();

    // Start is called before the first frame update
    void Start()
    {
        triggeredObjects = new bool[objectTriggers.Length];
        if (!GameObject.Find("NetworkManager"))
        {
            SpawnPlayer(GetSpawnPoint(transform).position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (AllObjectsTriggered()) LevelComplete();
    }

    private bool AllObjectsTriggered()
    {
        foreach (bool triggered in triggeredObjects)
        {
            if (triggered == false) return false;
        }
        return true;
    }

    private void GameComplete()
    {
        SceneUtility.LoadScene("MainMenu");
    }
    private void NetworkGameComplete()
    {
        FindObjectOfType<EscapeSceneManager>().LoadScene();
    }

    private void LevelComplete()
    {
        SceneUtility.LoadNextScene();
    }

    public void ObjectTriggered(Puzzle objectTrigger)
    {
        for (int i = 0; i < objectTriggers.Length; i += 1)
        {
            if (objectTriggers[i] == objectTrigger)
            {
                triggeredObjects[i] = true;
            }
        }
    }

    public string sceneCompletedName = "ScienceRoom";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !GameObject.Find("NetworkManager"))
        {
            PlayerPrefs.SetString(sceneCompletedName, "complete");
            GameComplete();
        }else if (other.CompareTag("Player"))
        {
            
            if (HandleNetworkGameCompleteCheck(other.transform)) NetworkGameComplete();
        }
    }

    private bool HandleNetworkGameCompleteCheck(Transform triggeringPlayer)
    {
        bool allTriggered = true;
        SetNetworkPlayerTriggered(triggeringPlayer);
        foreach(EscapeNetwork.EscapeNetworkPlayer player in FindObjectsOfType<EscapeNetwork.EscapeNetworkPlayer>())
        {
            if (!NetworkPlayerTriggered(player.Body))
            {
                return false;
            }
        }
        return allTriggered;
    }
    private void SetNetworkPlayerTriggered(Transform player)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] == player)
            {
                playersTriggered[i] = true;
                return;
            }
        }
        AddNetworkPlayers(player);
        playersTriggered[players.Count - 1] = true;
    }
    private bool NetworkPlayerTriggered(Transform player)
    {
        for(int i = 0; i < players.Count; i++)
        {
            if (players[i] == player) return playersTriggered[i];
        }
        AddNetworkPlayers(player);
        return false;
    }

    public void AddNetworkPlayers(Transform player)
    {
        players.Add(player);
        playersTriggered.Add(false);
    }
    public Transform GetSpawnPoint(Transform defaultPoint)
    {
        Transform spawnPoint = GameObject.Find("Spawn Point").transform;
        if (spawnPoint) return spawnPoint;
        else return defaultPoint;
    }
    public GameObject SpawnPlayer(Vector3 spawnPoint)
    {
        return Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
    }
}
