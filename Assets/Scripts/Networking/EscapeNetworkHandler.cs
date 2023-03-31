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
        private static string localIP;
        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 600, 600));
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                StartButtons();
            }
            else
            {
                StatusLabels();
            }

            GUILayout.EndArea();
        }

        static void StartButtons()
        {
            if (GUILayout.Button("Host"))
            {
                if(localIP == "")localIP = GetLocalIPAddress();
                NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = localIP;
                NetworkManager.Singleton.StartHost();
            }
            if (GUILayout.Button("Client"))
            {
                NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = localIP;
                NetworkManager.Singleton.StartClient();
            }
            if (GUILayout.Button("Server"))
            {
                NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = localIP;
                NetworkManager.Singleton.StartServer();
            }
            localIP = GUILayout.TextField(localIP);
        }

        static void StatusLabels()
        {
            var mode = NetworkManager.Singleton.IsHost ?
                "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

            GUILayout.Label("Transport: " +
                NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("Mode: " + mode);
            GUILayout.Label("Connection Address: " + localIP);
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