﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerField : MonoBehaviour
{
    MinoRenderer[,] renderers;
    public void PlayData(PlayerData pd)
    {
        for (int x = 0; x < CONSTANTS.FIELD_WIDTH; x++)
        {
            for (int y = 0; y < CONSTANTS.FIELD_HEIGHT; y++)
            {
                renderers[x, y].SetMinoColor(pd.field[x, y]);
            }
        }

        //something for garbage.
    }

    public GameObject prefab;

    // Use this for initialization
    void Start()
    {
        renderers = new MinoRenderer[CONSTANTS.FIELD_WIDTH, CONSTANTS.FIELD_HEIGHT];
        for (int x = 0; x < CONSTANTS.FIELD_WIDTH; x++)
        {
            for (int y = 0; y < CONSTANTS.FIELD_HEIGHT; y++)
            {
                GameObject go = GameObject.Instantiate(prefab);
                go.SetActive(true);
                go.transform.SetParent(this.transform);
                go.transform.localPosition = new Vector3(x, CONSTANTS.FIELD_HEIGHT - y, 0.0f);
                renderers[x, y] = go.GetComponent<MinoRenderer>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
