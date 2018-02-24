using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;

public class MessageReceiver : MonoBehaviour
{
    public MyTcpListener listener;
    // Use this for initialization
    public Queue<PlayerState> states = new Queue<PlayerState>();

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        JSONNode jso = listener.getObject();
        if (jso != null)
        {
            PlayerState pd = new PlayerState(jso);
            states.Enqueue(pd);
        }
    }
    
}
