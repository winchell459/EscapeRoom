using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

public class EscapeNetworkObjects : NetworkBehaviour
{
    public List<GameObject> networkObjects;
    public List<InputText> networkInputTexts;

    public NetworkVariable<FixedString32Bytes> networkVariable1 = new NetworkVariable<FixedString32Bytes>();
    public NetworkVariable<FixedString32Bytes> networkVariable2 = new NetworkVariable<FixedString32Bytes>();
    public NetworkVariable<FixedString32Bytes> networkVariable3 = new NetworkVariable<FixedString32Bytes>();
    public NetworkVariable<FixedString32Bytes> networkVariable4 = new NetworkVariable<FixedString32Bytes>();

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

    NetworkVariable<FixedString32Bytes> GetNetworkVariable(int index)
    {
        switch (index)
        {
            case 0:
                return networkVariable1;
            case 1:
                return networkVariable2;
            case 2:
                return networkVariable3;
            default:
                return networkVariable4;
        }
    }

    public int GetTextIndex(InputText inputText)
    {
        int index = -1;
        for (int i = 0; i < networkInputTexts.Count; i++)
        {
            if(networkInputTexts[i] == inputText) index = i;
        }
        return index;
    }
    
    [ServerRpc]
    private void SubmitInputTextValueChangedServerRPC(FixedString32Bytes value, int index)
    {
        GetNetworkVariable(index).Value = value;
    }

    public void OnInputTextValueChanged(FixedString32Bytes value, InputText inputText)
    {
        int index = GetTextIndex(inputText);
        if (index >= 0) SubmitInputTextValueChangedServerRPC(value, index);
        
    }

    private void Update()
    {
        if(IsServer)
        {
            foreach(EscapeNetwork.EscapeNetworkPlayer networkPlayer in FindObjectsOfType<EscapeNetwork.EscapeNetworkPlayer>())
            {
                if(networkPlayer.TextInputFlag.Value)
                {
                    SubmitInputTextValueChangedServerRPC(networkPlayer.TextInput.Value, networkPlayer.TextInputIndex.Value);
                    networkPlayer.TextInputFlag.Value = false;
                }
            }
        }

        for(int i = 0; i < networkInputTexts.Count; i++)
        {
            if(!networkInputTexts[i].changed && networkInputTexts[i].GetValue() != GetNetworkVariable(i).Value)
            {
                networkInputTexts[i].SetValue(GetNetworkVariable(i).Value);
            }
            else if(networkInputTexts[i].changed && networkInputTexts[i].GetValue() == GetNetworkVariable(i).Value)
            {
                networkInputTexts[i].changed = false;
            }
        }
    }
}
