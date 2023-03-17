using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : Puzzle
{
    public GameObject triggerObject;
    public GameObject completeObject;
    private void OnTriggerStay(Collider other)
    {
        if (!puzzleComplete && other.gameObject == triggerObject)
        {
            FindObjectOfType<GameHandler>().ObjectTriggered(this);
            if (completeObject) completeObject.SendMessage("ObjectTriggered", this);
            Debug.Log("object triggered");
            puzzleComplete = true;
        }
    }
}
