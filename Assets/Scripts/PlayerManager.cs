using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    //return false = at least one character alive
    public bool CheckAlive()
    {
        foreach (Transform child in transform)
        {
            CharacterHealth shade = child.GetComponent<CharacterHealth>();
            if (shade != null)
            {
                if (!shade.isDead)
                {
                    return false;
                }
            }

        }
        return true;
    }
}
