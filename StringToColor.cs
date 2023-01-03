using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringToColor
{
    public static Color ColorFromString(string _color)
    {
        _color = _color.Remove(0, 5).Replace(")", string.Empty).Replace(" ", string.Empty);
        
        string[] colors = _color.Split(',');

        Color newColor = new Color(float.Parse(colors[0]) / 1000f, float.Parse(colors[1]) / 1000f, float.Parse(colors[2]) / 1000f, float.Parse(colors[3]));
        return newColor;
    }
}
