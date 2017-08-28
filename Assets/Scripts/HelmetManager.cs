using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _helmet;

    [SerializeField]
    private Sprite[] _sprites;

    private WeaponManager _weaponMgr;

    void Awake()
    {
        _weaponMgr = GetComponent<WeaponManager>();
    }

    public void SetHelmet(Helmet helmetType)
    {
        print("Setting helmet to " + helmetType);

        _helmet.sprite = _sprites[(int)helmetType];

        _weaponMgr.SetHelmet(helmetType);
    }
}
