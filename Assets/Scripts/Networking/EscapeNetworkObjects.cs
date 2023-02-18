using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeNetworkObjects : MonoBehaviour
{
    public List<GameObject> networkObjects;
    public List<UnityEngine.UI.InputField> networkInputFields;

    private void Start()
    {
        AddNetworkObjects("Grabable");
        //AddNetworkObjects("Book");
        if (FindObjectOfType<BookShelf>())
        {
            foreach(GameObject book in FindObjectOfType<BookShelf>().GetBooks())
            {
                networkObjects.Add(book);
            }
        }
        AddNetworkObjects("InputField");
        AddNetworkObjects("Button");
        AddNetworkObjects("Flask");
        AddNetworkObjects("Statue");
        AddNetworkObjects("Locked");
        AddNetworkObjects("Door");
        AddNetworkObjects("Clock");
        
    }

    private void AddNetworkObjects(string tag)
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
        {
            networkObjects.Add(go);
        }
    }
    public GameObject GetNetworkObject(int objectID)
    {
        if(objectID > 0 && objectID <= networkObjects.Count)
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
        for(int i = 0; i < networkObjects.Count; i += 1)
        {
            if (networkObjects[i] == networkObject) return i+1;
        }
        return 0;
    }
}
