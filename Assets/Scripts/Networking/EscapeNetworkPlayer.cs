using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

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
        public int holdingObjectID = 0; // 0 = not holding object
        private EscapeNetworkObjects networkObjects;

        public Transform holdingObject;
        public GameObject item;

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                playerBody.gameObject.SetActive(false);
                playerBody = Instantiate(localPlayerPrefab, transform.position + Vector3.up * 2, Quaternion.identity).transform;
                playerHead = playerBody.GetChild(0);
                FindObjectOfType<Grabber>().networkPlayer = this;
            }
            networkObjects = FindObjectOfType<EscapeNetworkObjects>();
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
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
            HoldingObjectID.Value = objectID;
        }

        public void SubmitGrabberObjectClick(GameObject clickedObject)
        {
            SubmitGrabberObjectClickServerRpc(networkObjects.GetNetworkObjectID(clickedObject));
        }

        // Update is called once per frame
        void Update()
        {
            if (IsOwner)
            {
                SubmitPositionServerRpc(playerBody.position, playerBody.localEulerAngles, playerHead.localPosition, playerHead.localEulerAngles);
            }
            else
            {
                playerBody.position = Position.Value;
                playerBody.localEulerAngles = Rotation.Value;
                playerHead.localPosition = HeadPosition.Value;
                playerHead.localEulerAngles = HeadRotation.Value;

                if(HoldingObjectID.Value != holdingObjectID)
                {
                    if(holdingObject == null)
                    {
                        GameObject clicked = networkObjects.GetNetworkObject(HoldingObjectID.Value);
                        if (clicked)
                        {
                            Grabber.ObjectClicked(clicked.transform, ref holdingObject, ref item, playerHead);
                        }
                    }
                    else
                    {
                        Grabber.AlreadyHoldingObject(ref holdingObject, ref item, playerHead);
                    }
                    holdingObjectID = HoldingObjectID.Value;
                }
            }
        }
    }
}


