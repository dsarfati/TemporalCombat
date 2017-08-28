using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _weapon;

    [SerializeField]
    private Sprite[] _sprites;

    public void SetHelmet(Helmet helmetType)
    {
        print("Setting helmet to " + helmetType);

        _weapon.sprite = _sprites[(int)helmetType];
    }
}
