﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;

public class AstronautSend : MonoBehaviour
{
    //Temp code to send message to MCC
    public void Send()
    {
        //FIXME replace with MCC address
        string url = "ws://35.2.19.221:8999";
        using (var ws = new WebSocket(url))
        {
            string message = "{\"message_type\":\"task_completed\",\"task_id\":1}";
            ws.Connect();
            Debug.Log("Connected");
            ws.Send(message);
        }
    }
}