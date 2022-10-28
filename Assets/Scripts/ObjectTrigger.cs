using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : Puzzle
{
    public GameObject triggerObject;

    public GameObject completeObject;

    private void OnTriggerStay(Collider other) 
    {
        if(other.gameObject == triggerObject)
        {
            //FindObjectOfType<GameHandler>().ObjectTriggered(this);
            completeObject.SendMessage("ObjectTriggered", this);
            Debug.Log("object triggered");
        }
    }
}
