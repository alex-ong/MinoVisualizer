using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Mino 
{
    public static MinoType colourStringToMino(string s)
    {

        int r = int.Parse(s.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
        int g = int.Parse(s.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
        int b = int.Parse(s.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);

        float hue;
        float sat;
        float lum;
        HSLConverter.RGB2HSL(r, g, b, out hue, out sat, out lum);
        return (MinoType)(int)(Random.Range(0, 10));        
    }
}
