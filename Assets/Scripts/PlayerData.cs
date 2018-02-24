using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

//result["field"] => field[x][y]
//result["incomingGarbage"] = self.incomingGarbage
public class PlayerData
{
    public MinoType[,] field;
    public int garbagePending;
    public PlayerData(JSONNode j)
    {
        garbagePending = j["incomingGarbage"].AsInt;
        int xMax = j["field"].Count;
        int yMax = j["field"][0].Count;

        field = new MinoType[xMax, yMax];

        for (int x = 0; x < xMax; x++)
        {
            JSONArray columns = j["field"][x] as JSONArray;
            for (int y = 0; y < yMax; y++)
            {
                string colourString = columns[y];
                field[x, y] = Mino.colourStringToMino(colourString);
            }
        }

    }
}