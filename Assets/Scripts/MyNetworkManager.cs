using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : MonoBehaviour {

    //UNet устарела, но новая система находится в стадии разработки, поэтому я использую UNet
    NetworkClient myClient;
    string serverAddress = "127.0.0.1";
    int serverPort = 4444;

    SceneMng sceneMng;

    private void Awake()
    {
        SetupClient();
    }

    private void Start()
    {
        sceneMng = FindObjectOfType<SceneMng>();
    }

    // Create a client and connect to the server port
    public void SetupClient()
    {
        myClient = new NetworkClient();
        myClient.Connect(serverAddress, serverPort);

        //регистрирация функции обратного вызова для события подключения
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        
        //регистрирация функции обратного вызова для события неуспешного подключения
        myClient.RegisterHandler(MsgType.Disconnect, OnDisconnected);
        
        Debug.Log("myClient.Connect(127.0.0.1 4444)");
    }

    public void OnConnected(NetworkMessage netMsg)
    {
        sceneMng.LoadScene("MenuScene");
        Debug.Log("Connected to server");
    }

    void OnDisconnected(NetworkMessage netMsg)
    {
        sceneMng.LoadScene("MenuScene");
        Debug.Log("Discoonnected to server");
    }
}
