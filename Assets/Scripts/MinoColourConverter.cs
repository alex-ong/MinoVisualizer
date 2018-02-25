using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;


public class MinoColourConverter : MonoBehaviour
{
    private float EmptyLum = 0.1f;
    private float SolidLum = 0.25f;
    private float GarbageLum = 0.65f;

    private float satGrey = 0.4f;

    private float satAdjust = 1.0f; //boost saturation
    private float lumAdjust = 1.0f; //boost luminance

    public int playerID;

    private bool showGUI = false;

    public void Start()
    {
        this.LoadFromFile();
    }

    public void Update()
    {
        KeyCode? myKeycode = null;
        if (playerID == 1) myKeycode = KeyCode.Alpha1;
        else if (playerID == 2) myKeycode = KeyCode.Alpha2;
        if (myKeycode != null)
        {
            if (Input.GetKeyDown(myKeycode.Value))
            {
                showGUI = !showGUI;
            }
        }
    }

    public string GetSaveFileName()
    {
        return Application.streamingAssetsPath + "/Player" + playerID.ToString() + ".cfg";
    }

    public void SaveToFile()
    {
        List<string> lines = new List<string>();
        lines.Add(satGrey.ToString());
        lines.Add(EmptyLum.ToString());
        lines.Add(SolidLum.ToString());
        lines.Add(GarbageLum.ToString());
        lines.Add(satAdjust.ToString());
        lines.Add(lumAdjust.ToString());
        File.WriteAllLines(GetSaveFileName(), lines.ToArray());
    }

    public void LoadFromFile()
    {
        string fileNameToLoad = GetSaveFileName();
        try
        {
            string[] lines = File.ReadAllLines(GetSaveFileName());
            satGrey = float.Parse(lines[0]);
            EmptyLum = float.Parse(lines[1]);
            SolidLum = float.Parse(lines[2]);
            GarbageLum = float.Parse(lines[3]);
            satAdjust = float.Parse(lines[4]);
            lumAdjust = float.Parse(lines[5]);
        } catch {
            Debug.Log("Failed to load save file");
        }
    }

    public class ColorPair
    {
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
        return Mathf.Min(Mathf.Abs(hue - p.hue), Mathf.Abs(hue - (p.hue + 1)));
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
            else if (lum < SolidLum)
            {
                t = MinoType.Solid;
            }
            else if (lum < GarbageLum)
            {
                t = MinoType.Garbage;
            }
        }

        if (t == MinoType.Color)
        {
            sat = Mathf.Clamp01(sat * satAdjust);
            lum = Mathf.Clamp01(lum * lumAdjust);
            c = HSLConverter.ToRGB(hue, sat, lum);
        }

        return new MinoColor(c, t);

    }

    public void SliderRect(Rect r, string label, ref float toChange, Vector2 range)
    {
        Rect labelRect = new Rect(r);
        r.width = 0.3f * r.width;
        GUI.Label(r, label);
        r.x += r.width;
        r.width = labelRect.width * 2f / 3;
        toChange = GUI.HorizontalSlider(r, toChange, range.x, range.y);
        r.x += r.width;
        GUI.Label(r, toChange.ToString("0.00"));
    }

    public void OnGUI()
    {
        if (showGUI)
        {
            Rect bounds = new Rect(Screen.width / 4f, Screen.height / 4f, Screen.width / 2.0f, Screen.height);
            float lineHeight = 20f;
            Rect current = new Rect(bounds.x, bounds.y, bounds.width, lineHeight);
            GUI.Label(current, "Modifying player" + playerID.ToString()); current.y += lineHeight;

            SliderRect(current, "Color Piece Minimum saturation", ref satGrey, new Vector2(0f, 1f)); current.y += lineHeight;
            SliderRect(current, "EmptyBlock max Luminance", ref EmptyLum, new Vector2(0f, 1f)); current.y += lineHeight;
            SliderRect(current, "solidblock max Luminance", ref SolidLum, new Vector2(EmptyLum, 1f)); current.y += lineHeight;
            SliderRect(current, "garbageblock max luminance", ref GarbageLum, new Vector2(SolidLum, 1f)); current.y += lineHeight;
            SliderRect(current, "satAdjust", ref satAdjust, new Vector2(0f, 3f)); current.y += lineHeight;
            SliderRect(current, "lumAdjust", ref lumAdjust, new Vector2(0f, 3f)); current.y += lineHeight;
            if (GUI.Button(current, "Save settings")) SaveToFile(); current.y += lineHeight;
            if (GUI.Button(current, "Load Settings")) LoadFromFile();
        }
    }
}
