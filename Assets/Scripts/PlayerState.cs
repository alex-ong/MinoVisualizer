using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
public class PlayerState
{
    public List<PlayerData> data = new List<PlayerData>();
    public PlayerState(JSONNode j)
    {
        foreach (JSONNode j2 in j)
        {
            PlayerData pd = new PlayerData(j2);
            data.Add(pd);
        }
    }
}