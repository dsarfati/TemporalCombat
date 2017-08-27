using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorSet", menuName = "Colors/Palette")]
public class ColorSet : ScriptableObject {

    public Color[] HUDColors;
    public Color[] TextColors;

}
