using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Mino : Singleton<Mino>
{
    public float EmptyLum = 0.1f;
    public float SolidLum = 0.3f;
    public float GarbageLum = 0.5f;    
    
    public float satGrey = 0.4f;


    public class ColorPair {
        public MinoType t;
        public float hue;
        public ColorPair(MinoType t, float h)
        {
            this.t = t;
            hue = h;
        }
    }

    public float distance(float hue, ColorPair p)
    {
        return Mathf.Min(Mathf.Abs(hue-p.hue), Mathf.Abs(hue-(p.hue+1)));
    }

    public MinoColor colourStringToMino(string s)
    {

        int r = int.Parse(s.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
        int g = int.Parse(s.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
        int b = int.Parse(s.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);

        Color32 c = new Color32((byte)r, (byte)g, (byte)b, 255);
        MinoType t = MinoType.Color;
        float hue;
        float sat;
        float lum;
        HSLConverter.RGB2HSL(r, g, b, out hue, out sat, out lum);       
        if (sat < satGrey)
        {
            if (lum < EmptyLum)
            {
                t = MinoType.Empty;
            }
            else if (lum  < SolidLum)
            {
                t = MinoType.Solid;
            }
            else if (lum < GarbageLum) 
            {
                t = MinoType.Garbage;
            }
            
        } 

        return new MinoColor(c, t);

    }
}
