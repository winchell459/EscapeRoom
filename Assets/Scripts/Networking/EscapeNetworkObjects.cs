using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EscapeNetworkObjects : NetworkBehaviour
{
    public List<GameObject> networkObjects;
    public List<InputText> networkInputTexts;
    public List<NetworkVariable<string>> networkVariables = new List<NetworkVariable<string>>();
    public NetworkVariable<long> networkVariable = new NetworkVariable<long>();
    public NetworkVariable<bool> textValueChanged = new NetworkVariable<bool>();
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
            //SubmitInputTextValueChangedServerRpc(value, index);
            SubmitInputTextValueChangedServerRpc(value, index);
        }
        Debug.Log($"OnTextInputValueChanged index:{index} value: {value}");
    }

    
    
    [ServerRpc/*(RequireOwnership = false)*/]
    private void SubmitInputTextValueChangedServerRpc(string value, int index)
    {
        Debug.Log($"SubmitInputTextValueChangedServerRpc({value},{index})");
        //networkVariables[index].Value = value;
        networkVariable.Value = StringToInt(value);
        Debug.Log($"{value} -> {networkVariable.Value}:{IntToString(networkVariable.Value)}");

        textValueChanged.Value = true;
    }
    //[ClientRpc]
    //private void SubmitInputTextValueChangedClientRpc(string value, int index)
    //{
    //    Debug.Log($"SubmitInputTextValueChangedClientRpc({value},{index})");
    //    //networkVariables[index].Value = value;
    //    networkVariable.Value = StringToInt(value);
    //    Debug.Log($"{value} -> {networkVariable.Value}:{IntToString(networkVariable.Value)}");
    //}

    
    private void Update()
    {
        if (IsServer)
        {
            foreach (EscapeNetwork.EscapeNetworkPlayer networkPlayer in FindObjectsOfType<EscapeNetwork.EscapeNetworkPlayer>())
            {
                if (networkPlayer.TextInputSubmit.Value)
                {
                    Debug.Log($"networkPlayer.NetworkObjectId: {networkPlayer.NetworkObjectId}");
                    if (networkInputTexts[networkPlayer.TextInputIndex.Value].GetValue() != IntToString(networkVariable.Value))
                        SubmitInputTextValueChangedServerRpc(IntToString(networkPlayer.TextInput.Value), networkPlayer.TextInputIndex.Value);
                    //networkInputTexts[networkPlayer.TextInputIndex.Value].SetValue(IntToString(networkPlayer.TextInput.Value));
                    networkPlayer.TextInputSubmit.Value = false;
                    Debug.Log($"networkPlayer.TextInputSubmit.Value {networkPlayer.TextInputSubmit.Value}");
                }
            }
        }
        
        for (int i = 0; i < networkInputTexts.Count; i += 1)
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
                //bool valueChanging = false;
                //foreach (EscapeNetwork.EscapeNetworkPlayer networkPlayer in FindObjectsOfType<EscapeNetwork.EscapeNetworkPlayer>())
                //{
                //    if (networkPlayer.TextInputIndex.Value == i && networkPlayer)
                //    {
                //        valueChanging = true;
                //    }
                //}
                //if(valueChanging)
                    networkInputTexts[i].SetValue(IntToString(networkVariable.Value));
                //textValueChanged.Value = false;
            }
        }
        
    }
    public int GetTextIndex(InputText inputText)
    {
        int index = -1;
        for (int i = 0; i < networkInputTexts.Count; i += 1)
        {
            if (networkInputTexts[i] == inputText) index = i;
        }
        return index;
    }

    public static long StringToInt(string value)
    {
        long asciiInt = 0;
        for(int i = 0; i < value.Length; i += 1)
        {
            asciiInt += value[i] << 8 * i;
        }

        return asciiInt;
    }

    public static string IntToString(long value)
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
