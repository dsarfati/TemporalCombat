using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour {

    public GameObject playerStats;

	// Use this for initialization
	void Awake () {
	}


    public void AddPlayers(int x)
    {
        for(int i = 0; i < x; i++)
        {
            Instantiate(playerStats, transform);
        }
    }
}
