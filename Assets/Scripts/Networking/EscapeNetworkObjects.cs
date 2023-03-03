using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EscapeNetworkObjects : NetworkBehaviour
{
    public List<GameObject> networkObjects;
    public List<InputText> networkInputTexts;
    public List<NetworkVariable<string>> networkVariables = new List<NetworkVariable<string>>();
    public NetworkVariable<int> networkVariable = new NetworkVariable<int>();
    private void Awake()
    {
        for (int i = 0; i < networkInputTexts.Count; i += 1)
        {
            networkVariables.Add(new NetworkVariable<string>(""));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
        if(index >= 0)
        {
            SubmitInputTextValueChangedClientRpc(value, index);
        }
        Debug.Log($"OnTextInputValueChanged index:{index}");
    }

    [ClientRpc]
    private void SubmitInputTextValueChangedClientRpc(string value, int index)
    {
        Debug.Log($"SubmitInputTextValueChangedServerRpc({value},{index})");
        //networkVariables[index].Value = value;
        networkVariable.Value = StringToInt(value);
        Debug.Log($"{value} -> {networkVariable.Value}:{IntToString(networkVariable.Value)}");
    }

    private void Update()
    {
        for(int i = 0; i < networkInputTexts.Count; i += 1)
        {
            //if (networkInputTexts[i].GetValue() != networkVariables[i].Value)
            //{
            //    networkInputTexts[i].SetValue(networkVariables[i].Value);
            //}
            //if (networkInputTexts[i].GetValue() != networkVariable.Value)
            //{
            //    networkInputTexts[i].SetValue(networkVariable.Value);
            //}
            if (networkInputTexts[i].GetValue() != IntToString(networkVariable.Value))
            {
                networkInputTexts[i].SetValue(IntToString(networkVariable.Value));
            }
        }
    }

    private int StringToInt(string value)
    {
        int asciiInt = 0;
        for(int i = 0; i < value.Length; i += 1)
        {
            asciiInt += value[i] << 8 * i;
        }
        return asciiInt;
    }

    private string IntToString(int value)
    {
        string str = "";
        while(value > 0)
        {
            str += (char)(value % 256);
            value = value >> 8;
        }
        return str;
    }

}
