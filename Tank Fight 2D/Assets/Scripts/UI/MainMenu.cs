using System.Net;
using System.Net.Sockets;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField iPInputField;

    void Start()
    {
        iPInputField.text = PlayerPrefs.GetString("iPv4Server", "127.0.0.1");
    }

    public void StartHost()
    {
        NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = GetIpAdress();
        HostSingleton.Instance.GameManager.StartHostAsync();
    }

    public void StartClient()
    {
        if (!string.IsNullOrEmpty(iPInputField.text))
        {
            PlayerPrefs.SetString("iPv4Server", iPInputField.text);
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = iPInputField.text;
            ClientSingleton.Instance.GameManager.StartClientAsync();
        }
    }

    string GetIpAdress()
    {
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        return string.Empty;
    }
}