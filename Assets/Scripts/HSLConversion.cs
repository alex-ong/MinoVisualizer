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

    public static Color32 ToRGB(float hue, float sat, float lum)
    {
        byte r, g, b;
        if (sat == 0)
        {
            r = (byte)Math.Round(lum * 255f);
            g = (byte)Math.Round(lum * 255f);
            b = (byte)Math.Round(lum * 255f);
        }
        else
        {
            float t1, t2;
            float th = hue;

            if (lum < 0.5f)
            {
                t2 = lum * (1f + sat);
            }
            else
            {
                t2 = (lum + sat) - (lum * sat);
            }
            t1 = 2f * lum - t2;

            float tr, tg, tb;
            tr = th + (1.0f / 3.0f);
            tg = th;
            tb = th - (1.0f / 3.0f);

            tr = ColorCalc(tr, t1, t2);
            tg = ColorCalc(tg, t1, t2);
            tb = ColorCalc(tb, t1, t2);
            r = (byte)Math.Round(tr * 255d);
            g = (byte)Math.Round(tg * 255d);
            b = (byte)Math.Round(tb * 255d);
        }
        return new Color32(r, g, b, 255);
    }
    private static float ColorCalc(float c, float t1, float t2)
    {

        if (c < 0) c += 1;
        if (c > 1) c -= 1;
        if (6.0f * c < 1.0f) return t1 + (t2 - t1) * 6.0f * c;
        if (2.0f * c < 1.0f) return t2;
        if (3.0f * c < 2.0f) return t1 + (t2 - t1) * (2.0f / 3.0f - c) * 6.0f;
        return t1;
    }
}