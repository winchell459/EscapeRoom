using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public Puzzle[] objectTriggers;
    private bool[] triggeredObjects;
    public bool puzzleComplete; 

    // Start is called before the first frame update
    void Start()
    {
        triggeredObjects = new bool[objectTriggers.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (AllObjectsTriggered()) PuzzleComplete();
    }

    protected virtual void PuzzleComplete()
    {

    }

    private bool AllObjectsTriggered()
    {
        if (objectTriggers.Length == 0) return false;
        foreach (bool triggered in triggeredObjects)
        {
            if (triggered == false) return false;
        }
        return true;
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
}
