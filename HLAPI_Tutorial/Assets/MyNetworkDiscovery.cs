﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MyNetworkDiscovery : NetworkDiscovery
{

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        //서버로부터 브로드캐스트 메시지를 받았을 때 실행됩니다.
        base.OnReceivedBroadcast(fromAddress, data);
        Debug.Log("recived broadcast from : " + fromAddress);

        StartCoroutine("TweenAnim");
    }
}