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
        float r2 = r / 255f;
        float g2 = g / 255f;
        float b2 = b / 255f;

        float[] values = new float[] { r2, g2, b2 };
        Array.Sort(values);
        float min = values[0];
        float max = values[2];

        l = (min + max) / 2.0f;
        if (min == max)
        {
            s = 0.0f;
        }
        else if (l < 0.5f)
        {
            s = (max - min) / (max + min);
        }
        else
        {
            s = (max - min) / (2.0f - max - min);
        }

        if (s == 0) //no saturation = no hue
        {
            h = 0;
        }
        else
        {
            if (r2 == max)
            {
                h = (g2 - b2) / (max - min);
            }
            else if (g2 == max)
            {
                h = 2.0f + (b2 - r2) / (max - min);
            }
            else
            {
                h = 4.0f + (r2 - g2) / (max - min);
            }
            h *= 60f;
            if (h < 0)
            {
                h += 360f;
            }

            h /= 360f;
        }
    }       
}