using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    
    public bool CheckAlive()
    {
        CharacterHealth[] healths = GetComponentsInChildren<CharacterHealth>();

        //no characters left
        if(healths.Length == 0)
        {
            return false;
        }

        foreach(var health in healths)
        {
            if(!health.isDead)
            {
                return true;
            }
        }
        return false;


        //foreach (Transform child in transform)
        //{
        //    CharacterHealth shade = child.GetComponent<CharacterHealth>();
        //    if (shade != null)
        //    {
        //        if (!shade.isDead)
        //        {
        //            return false;
        //        }
        //    }

        //}
        //return true;
    }
}
