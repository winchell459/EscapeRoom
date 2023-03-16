using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

public class EscapeNetworkObjects : NetworkBehaviour
{
    public List<GameObject> networkObjects;
    public List<InputText> networkInputTexts;

    public NetworkVariable<FixedString32Bytes> networkVariable = new NetworkVariable<FixedString32Bytes>();
    public NetworkVariable<FixedString32Bytes> networkVariable2 = new NetworkVariable<FixedString32Bytes>();
    
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
            SubmitInputTextValueChangedServerRpc(value, index);
        }
        Debug.Log($"OnTextInputValueChanged index:{index} value: {value}");
    }

    
    
    [ServerRpc(RequireOwnership = false)]
    private void SubmitInputTextValueChangedServerRpc(FixedString32Bytes value, int index)
    {
        Debug.Log($"SubmitInputTextValueChangedServerRpc({value},{index})");

        GetNetworkVariable(index).Value = value;

    }
    NetworkVariable<FixedString32Bytes> GetNetworkVariable(int index)
    {
        switch (index)
        {
            case 0:
                return networkVariable;
            case 1:
                return networkVariable2;
            default:
                return null;
        }
    }

    
    private void Update()
    {
        if (IsServer)
        {
            foreach (EscapeNetwork.EscapeNetworkPlayer networkPlayer in FindObjectsOfType<EscapeNetwork.EscapeNetworkPlayer>())
            {
                if (networkPlayer.TextInputSubmit.Value)
                {
                    SubmitInputTextValueChangedServerRpc(networkPlayer.TextInput.Value, networkPlayer.TextInputIndex.Value);
                    
                    networkPlayer.TextInputSubmit.Value = false;
                    Debug.Log($"networkPlayer.TextInputSubmit.Value {networkPlayer.TextInputSubmit.Value}");
                }
            }
        }
        
        for (int i = 0; i < networkInputTexts.Count; i += 1)
        {
            
            if (!networkInputTexts[i].changed && networkInputTexts[i].GetValue() != GetNetworkVariable(i).Value)
            {
                
                networkInputTexts[i].SetValue(GetNetworkVariable(i).Value);
                
            }else if(networkInputTexts[i].GetValue() == GetNetworkVariable(i).Value)
                networkInputTexts[i].changed = false;
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


}
