using UnityEngine;
using System;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Messaging
{
    public ConcurrentQueue<string> messageQueue;
    public List<Chat> chatList;
    public List<int> priority; //FIXME find better datastructure for this
    public HashSet<string> chatLookup;

    public Messaging()
    {
        messageQueue = new ConcurrentQueue<string>();
        chatList = new List<Chat>();
        priority = new List<int>();
        chatLookup = new HashSet<string>();
    }
}
// Class presentation of a sent message within a chat
public class Message
{
    public string chatID;
    public string content;
    public string sender;
    public string timeStamp;

    public Message(string text, string sender, string timeStamp)
    {
        this.content = text;
        this.sender = sender;
        this.timeStamp = timeStamp;
    }
}
public class Chat
{
    public string title;
    public string chatID;
    public List<string> members;
    public List<Message> messages;

    public Chat(string ID, HashSet<string> members)
    {
        this.chatID = ID;
        this.members = members.ToList();
        this.messages = new List<Message>();
        this.title = string.Join(", ", members.ToList());
        Debug.Log("HELOO");
        Debug.Log(this.title);
    }
    public Chat(string ID, List<string> members)
    {
        this.chatID = ID;
        this.members = members;
        this.messages = new List<Message>();
        this.title = string.Join(", ", members);
        Debug.Log("HELOO");
        Debug.Log(this.title);
    }
}

public class MessageJson : JsonMessage
{
    public List<string> recipients;
    public string timeStamp;
    public string sender;
    public string content;
    public string chatID;
}

public class GroupClass : JsonMessage
{
    public List<string> recipients;
    public string chatID;
}