using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public Puzzle[] objectTriggers;
    private bool[] triggeredObjects;

    // Start is called before the first frame update
    void Start()
    {
        triggeredObjects = new bool[objectTriggers.Length];
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
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetString(sceneCompletedName, "complete");
            GameComplete();
        }
    }
}
