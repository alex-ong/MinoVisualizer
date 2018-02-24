using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MinoType
{
    //J,
    //I,
    //Z,
    //L,
    //O,
    //T,
    //S,
    Color,
    Garbage,
    Empty,
    Solid
}

public class MinoColor
{
    public Color c;
    public MinoType type;
    public MinoColor(Color c, MinoType t)
    {
        this.c = c;
        this.type = t;
    }
}