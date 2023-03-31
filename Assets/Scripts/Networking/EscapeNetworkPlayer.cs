using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

namespace EscapeNetwork
{
    public class EscapeNetworkPlayer : NetworkBehaviour
    {
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
        public NetworkVariable<Vector3> Rotation = new NetworkVariable<Vector3>();
        public NetworkVariable<Vector3> HeadPosition = new NetworkVariable<Vector3>();
        public NetworkVariable<Vector3> HeadRotation = new NetworkVariable<Vector3>();

        public GameObject localPlayerPrefab;
        [SerializeField] private Transform playerBody, playerHead;

        public NetworkVariable<int> HoldingObjectID = new NetworkVariable<int>();
        public int holdingOjectID = 0; 
        

        public Transform holdingObject;
        public GameObject item;

        public NetworkVariable<FixedString32Bytes> TextInput = new NetworkVariable<FixedString32Bytes>();
        public NetworkVariable<int> TextInputIndex = new NetworkVariable<int>();
        public NetworkVariable<bool> TextInputFlag = new NetworkVariable<bool>();

        public override void OnNetworkSpawn()
        {
            OnSceneStart();
            
        }
        public void OnSceneStart()
        {
            Transform spawnPoint = GameObject.Find("Spawn Point").transform;
            if (spawnPoint)
            {
                transform.position = spawnPoint.position;
                transform.forward = spawnPoint.forward;
            }
            if (IsOwner)
            {
                if(playerBody)playerBody.gameObject.SetActive(false);
                playerBody = Instantiate(localPlayerPrefab, transform.position, Quaternion.identity).transform;
                playerHead = playerBody.GetChild(0);
                FindObjectOfType<Grabber>().networkPlayer = this;

            }
            FindObjectOfType<GameHandler>().AddNetworkPlayers(playerBody);
            

            
            
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
        }
        public void OnTextInputValueChanged(string value, int index)
        {
            if (IsOwner) SubmitInputTextValueChangedServerRpc(value, index);
        }

        [ServerRpc]
        private void SubmitInputTextValueChangedServerRpc(string value, int index)
        {
            if (!TextInputFlag.Value)
            {
                TextInput.Value = value;
                TextInputIndex.Value = index;
                TextInputFlag.Value = true;
            }
        }
        [ServerRpc]
        void SubmitPositionServerRpc(Vector3 pos, Vector3 rot, Vector3 headPos, Vector3 headRot)
        {
            Position.Value = pos;
            Rotation.Value = rot;
            HeadPosition.Value = headPos;
            HeadRotation.Value = headRot;
        }

        [ServerRpc]
        void SubmitGrabberObjectClickServerRpc(int objectID)
        {
            if (HoldingObjectID.Value == objectID) objectID = -objectID;
            HoldingObjectID.Value = objectID;
        }
        public void SubmitGrabberObjectClick(GameObject clickedObject)
        {
            SubmitGrabberObjectClickServerRpc(FindObjectOfType<EscapeNetworkObjects>().GetNetworkObjectID(clickedObject));
        }

        // Update is called once per frame
        void Update()
        {
            if (IsOwner)
            {
                if (!playerBody) OnSceneStart();
                SubmitPositionServerRpc(playerBody.position, playerBody.localEulerAngles, playerHead.localPosition, playerHead.localEulerAngles);
            }
            else
            {
                playerBody.position = Position.Value;
                playerBody.localEulerAngles = Rotation.Value;
                playerHead.localPosition = HeadPosition.Value;
                playerHead.localEulerAngles = HeadRotation.Value;

                if(HoldingObjectID.Value != holdingOjectID)
                {
                    
                    if(holdingObject == null)
                    {
                        Debug.Log("Object clicked");
                        GameObject clicked = FindObjectOfType<EscapeNetworkObjects>().GetNetworkObject(HoldingObjectID.Value);
                        Debug.Log("Object clicked " + clicked.name);
                        if (clicked)
                        {
                            Grabber.ObjectClicked(clicked.transform, ref holdingObject, ref item, playerHead);
                            
                        }
                    }
                    else
                    {
                        Debug.Log("Object dropped");
                        Grabber.AlreadyHoldingObject(ref holdingObject, ref item, playerHead);
                    }
                    holdingOjectID = HoldingObjectID.Value;
                }
            }
        }
    }
}


