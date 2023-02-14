using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeNetworkObjects : MonoBehaviour
{
    public GameObject[] networkObjects;
    public GameObject GetNetworkObject(int objectID)
    {
        if(objectID > 0 && objectID <= networkObjects.Length)
        {
            return networkObjects[objectID-1];
        }
        else
        {
            return null;
        }
    }
    public int GetNetworkObjectID(GameObject networkObject)
    {
        for(int i = 0; i < networkObjects.Length; i += 1)
        {
            if (networkObjects[i] == networkObject) return i+1;
        }
        return 0;
    }
}
