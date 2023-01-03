using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ColorMixer : MonoBehaviour
{
    public List<Colors> colors;
    public Color color;
    private void OnValidate()
    {
        Calculate();
    }
    public void Calculate()
    {
        Color result = new Color(0, 0, 0, 0);

        foreach (var item in colors)
        {
            result += item.color;
        }
        result /= colors.Count;
        color = result;
    }
}
[System.Serializable]
public class Colors
{
    public Color color;
}
