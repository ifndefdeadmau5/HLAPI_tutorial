using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MyNetManager : NetworkManager /* NetworkManager 를 상속합니다. */
{
    NetworkClient myClient;
    public NetworkDiscovery discovery;

    void Start()
    {
        

    }
    /* 확장 함수 */
    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("OnStartServer( )");
    }

    public override void OnStartHost()
    {
        base.OnStartHost();
        Debug.Log("OnStartHost( )");
    }


    public override void OnStartClient(NetworkClient client)
    {
        Debug.Log("OnStartClient( )");
    }

    public override void OnStopClient()
    {
        Debug.Log("OnStopClient( )");
    }

    /* 사용자 정의 함수 */
    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
    }


    public void SetupServer()
    {
        Debug.Log("SetupServer()");
        StartServer();
        discovery.Initialize();
        discovery.StartAsServer();

        NetworkServer.Listen(4444);
        NetworkServer.RegisterHandler( MyMsgType.CustomMsgType, OnMessage);
        //NetworkServer.SendToAll(MsgType.Rpc, )

    }

    public void SetupClient()
    {
        StartClient();
        Debug.Log("SetupClient()");

        discovery.Initialize();
        discovery.StartAsClient();

        myClient = new NetworkClient();
        myClient.Connect("127.0.0.1", 4444);
    }

    public class MyMsgType
    {
        public static short CustomMsgType = MsgType.Highest + 1;
    };

    public class MyMessage : MessageBase
    {
        public int number;
    }

    public void SendMessage(int number)
    {
        MyMessage msg = new MyMessage();
        msg.number = number;

        myClient.Send(MyMsgType.CustomMsgType, msg);
    }

    public void OnMessage(NetworkMessage netMsg)
    {
        MyMessage msg = netMsg.ReadMessage<MyMessage>();
        Debug.Log("OnMessage " + msg.number);
    }

}