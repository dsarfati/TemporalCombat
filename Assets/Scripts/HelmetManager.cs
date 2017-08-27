using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _helmet;

    [SerializeField]
    private Sprite[] _sprites;

    public void SetHelmet(Helmet helmetType)
    {
        print("Setting helmet to " + helmetType);
    }
}
