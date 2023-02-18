using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeNetworkObjects : MonoBehaviour
{
    public List<GameObject> networkObjects;

    // Start is called before the first frame update
    void Start()
    {
        if(FindObjectOfType<BookShelf>())
        {
            foreach(GameObject go in FindObjectOfType<BookShelf>().GetBooks())
            {
                networkObjects.Add(go);
            }
        }
    }

    public GameObject GetNetworkObject(int objectID)
    {
        if(objectID > 0 && objectID <= networkObjects.Count)
        {
            return networkObjects[objectID - 1];
        }
        else
        {
            return null;
        }
    }

    public int GetNetworkObjectID(GameObject networkObject)
    {
        for(int i = 0; i < networkObjects.Count; i++)
        {
            if(networkObjects[i] == networkObject)
            {
                return i + 1;
            }
        }
        return 0;
    }
}
