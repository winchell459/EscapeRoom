using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

namespace EscapeNetwork
{
    public class EscapeNetworkHandler : MonoBehaviour
    {
        public GameObject networkSetupPanel, joinNetworkPanel;
        public UnityEngine.UI.InputField joinCodeText;
        public float joinWaitTime = 3;
        private float joinWaitStarted = 0;

        private static string localIP = "";

        public void BackButton()
        {
            joinNetworkPanel.SetActive(false);
        }
        public void HostRoomButton()
        {
            joinWaitStarted = Time.time;
            networkSetupPanel.SetActive(false);
            StartHost();
        }
        public void JoinRoomButton()
        {
            joinNetworkPanel.SetActive(true);
        }
        public void JoinButton()
        {
            if (joinCodeText.text != "")
            {
                joinWaitStarted = Time.time;
                networkSetupPanel.SetActive(false);
                localIP = joinCodeText.text;
                StartClient();
            }

        }

        void OnGUI()
        {
            if (NetworkManager.Singleton)
            {
                GUILayout.BeginArea(new Rect(10, 10, 600, 600));
                if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
                {
                    //StartButtons();
                    if (joinWaitStarted + joinWaitTime < Time.time && networkSetupPanel) networkSetupPanel.SetActive(true);
                }
                else
                {
                    StatusLabels();
                }

                GUILayout.EndArea();
            }
            else
            {
                Destroy(gameObject);
            }

        }

        static void StartButtons()
        {
            if (GUILayout.Button("Host"))
            {
                StartHost();
            }
            if (GUILayout.Button("Client"))
            {
                StartClient();
            }
            //if (GUILayout.Button("Server"))
            //{
            //    NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = localIP;
            //    NetworkManager.Singleton.StartServer();
            //}
            localIP = GUILayout.TextField(localIP);
        }

        public static async void StartHost()
        {
            RelayManager relayManager = FindObjectOfType<RelayManager>();
            //if (localIP == "") localIP = GetLocalIPAddress();
            //NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = localIP;
            if (relayManager.IsRelayEnabled)
                await relayManager.SetupRelay();
            NetworkManager.Singleton.StartHost();
            localIP = relayManager.joinCode;
        }

        public static async void StartClient()
        {
            RelayManager relayManager = FindObjectOfType<RelayManager>();
            //NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = localIP;
            if (relayManager.IsRelayEnabled)
            {
                await relayManager.JoinRelay(localIP);
            }
            NetworkManager.Singleton.StartClient();

        }

        static void StatusLabels()
        {
            var mode = NetworkManager.Singleton.IsHost ?
                "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

            GUILayout.Label("Transport: " +
                NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("Mode: " + mode);
            GUILayout.Label("Join Code: " + localIP);
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    Debug.Log(ip.ToString());
                    return ip.ToString();
                }
            }
            throw new System.Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}