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
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && players.Count == 0)
        {
            PlayerPrefs.SetString(sceneCompletedName, "complete");
            GameComplete();
        }else if (other.CompareTag("Player"))
        {
            bool allTriggered = true;
            for(int i = 0; i < players.Count; i++)
            {
                if (players[i] == null || players[i] == other.transform)
                {
                    playersTriggered[i] = true;
                }
                if (!playersTriggered[i]) allTriggered = false;
            }
            if (allTriggered) NetworkGameComplete();
        }
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
