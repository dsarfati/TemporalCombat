using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    //return false = at least one character alive
    public bool CheckAlive()
    {
        foreach (Transform child in transform)
        {
            PlayerHealth shade = child.GetComponent<PlayerHealth>();
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
