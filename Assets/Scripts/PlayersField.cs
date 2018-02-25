using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersField : MonoBehaviour
{
    public MessageReceiver mr;
    public List<PlayerField> playerFields;
    // Use this for initialization 
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (mr.states.Count > 0)
        {
            var ps =  mr.states.Dequeue();

            RenderPs(ps);           
        }
    }

    void RenderPs(PlayerState ps)
    {
        for (int i = 0; i < ps.data.Count; i++)
        {
            PlayerData pd = ps.data[i];
            playerFields[i].PlayData(pd);
            
        }
        
    }
}
