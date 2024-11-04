using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewColorData", menuName = "Colors/Color Data", order = 1)]
public class ColorData : ScriptableObject
{
    public Color[] colors;
}
