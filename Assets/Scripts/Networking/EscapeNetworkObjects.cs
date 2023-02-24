using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EscapeNetworkObjects : NetworkBehaviour
{
    public List<GameObject> networkObjects;
    public List<InputText> networkInputTexts;
    public List<NetworkVariable<string>> networkVariables = new List<NetworkVariable<string>>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < networkInputTexts.Count; i += 1)
        {
            networkVariables.Add(new NetworkVariable<string>());
        }
        //add books to the networkObjects
        if (FindObjectOfType<BookShelf>())
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
        for(int i = 0; i < networkObjects.Count; i += 1)
        {
            if (networkObjects[i] == networkObject) return i + 1;
        }
        return 0;
    }

    public void OnTextInputValueChanged(string value, InputText inputText)
    {
        int index = -1;
        for (int i = 0; i < networkInputTexts.Count; i += 1)
        {
            if (networkInputTexts[i] == inputText) index = i;
        }
        if(index > 0)
        {
            SubmitInputTextValueChangedServerRpc(value, index);
        }
    }
    [ServerRpc]
    private void SubmitInputTextValueChangedServerRpc(string value, int index)
    {
        networkVariables[index].Value = value;
    }

    private void Update()
    {
        for(int i = 0; i < networkInputTexts.Count; i += 1)
        {
            if(networkInputTexts[i].GetValue() != networkVariables[i].Value)
            {
                networkInputTexts[i].SetValue(networkVariables[i].Value);
            }
        }
    }

}
