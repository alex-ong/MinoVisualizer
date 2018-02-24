using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Given a Color (RGB Struct) in range of 0-255

// Return H,S,L in range of 0-1

public static class HSLConverter
{
    public static void RGB2HSL(int r, int g, int b, out float h, out float s, out float l)

    {        
        float v;
        float m;
        float vm;
        float r2, g2, b2;

        h = 0; // default to black
        s = 0;
        l = 0;
        v = Mathf.Max(r, g);
        v = Mathf.Max(v, b);
        m = Mathf.Min(r, g);
        m = Mathf.Min(m, b);
        l = (m + v) / 2.0f;
        if (l <= 0.0)
        {
            return;
        }
        vm = v - m;
        s = vm;
        if (s > 0.0f)
        {
            s /= (l <= 0.5f) ? (v + m) : (2.0f - v - m);
        }
        else
        {
            return;
        }
        r2 = (v - r) / vm;
        g2 = (v - g) / vm;
        b2 = (v - b) / vm;
        if (r == v)
        {
            h = (g == m ? 5.0f + b2 : 1.0f - g2);
        }
        else if (g == v)
        {
            h = (b == m ? 1.0f + r2 : 3.0f - b2);
        }
        else
        {
            h = (r == m ? 3.0f + g2 : 5.0f - r2);
        }
        h /= 6.0f;
    }
}