using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;
using TMPro;
using System.Linq;
using System.Text.Json.Serialization;


public class MessageHandler: MonoBehaviour
{
    public TextMeshPro window;
    public TextMeshPro recipientNames;
    public string address;
    public int port;
    private WebSocket connection;
    public HashSet<string> recipientList = new HashSet<string>();

    private void Start()
    {
        address = "127.0.0.1";
        port = 6969;
        //string url = "ws://" + address + ":" + port;
        string url1 = "ws://127.0.0.1:4242";
        connection = new WebSocket(url1);
        // Set behavior for this websocket when message is recieved
        connection.OnMessage += (sender, e) =>
        {
            OnMessage(e);
            Debug.Log("MCC message: " + e.Data);
        };
        connection.Connect();
        connection.Send("{\"message_type\": \"registration\",\"username\": \"Joel\"}");
        Debug.Log("Connected to server");
    }

    // Continously checks if a new message has been recieved
    private void Update()
    {
        while (Simulation.User.AstronautMessaging.messageQueue.TryDequeue(out string message))
        {
            MessageJson readIn = JsonConvert.DeserializeObject<MessageJson>(message);
            HandleMessage(readIn.message_type, message);
        }
    }

    // Function to display contents of the recipient list
    public void RecipientHandler(string name)
    {
        if (!recipientList.Contains(name))
        {
            recipientList.Add(name);
        }
        else
        {
            recipientList.Remove(name);
        }

        string allNames = "";
        foreach( string Name in recipientList)
        {
            allNames += Name + " ";
        }

        recipientNames.text = allNames;

        return;
    }

    public void SendMCC(int messageTemplate)
    {
        string sender = "Joel";
        List<string> recipients = recipientList.ToList();
        string testmsg = "shitsfucked";
        //string content = "The missile knows where it is at all times. It knows this because it knows where it isn't. By subtracting where it is from where it isn't, or where it isn't from where it is (whichever is greater)"
        switch (messageTemplate)
        {
            case 0:
                testmsg = "Hello";
                break;
            case 1:
                testmsg = "Bitch";
                break;
            case 2:
                testmsg = "pls";
                break;
        }
        //FIXME Example message with timestamp included
        //{
        //    "message_type": "dm",
        //    "recipients":
        //     [
        //        "Patrick", "Jason"
        //     ]
        //    "sender": "Joel"
        //    "timeStamp": DateTime object
        //    "content": "wtfwwtf"
        //}
        MessageJson message = new MessageJson();
        message.message_type = "dm";
        message.recipients = recipients;
        message.currentTime = DateTime.Now.ToString("HH:mm");
        message.content = testmsg;
        message.sender = sender;
        string jsonMessage = JsonConvert.SerializeObject(message, Formatting.Indented);
        connection.Send(jsonMessage);
        Debug.Log("Sent: " + message);
    }

    private void OnMessage(MessageEventArgs e)
    {
        Debug.Log(e.Data);
        Simulation.User.AstronautMessaging.messageQueue.Enqueue(e.Data);
    }

    private void HandleMessage(string messageType, string message)
    {
        switch (messageType)
        {
            case "dm":
                
                Debug.Log("recieved message");
                MessageJson readin = JsonConvert.DeserializeObject<MessageJson>(message);
                string printOut = "Sender: " + readin.sender + "\n" + "Message: " + readin.content + "\n" + "Time: " + readin.currentTime;
                window.text = printOut;
                break;
        }
    }

}
