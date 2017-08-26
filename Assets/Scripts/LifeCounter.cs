using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour {

    public int maxLives = 4;
    public GameObject heart;

	// Use this for initialization
	void Awake () {
		for(int i = 0; i < maxLives; i++)
        {
            Instantiate(heart, transform);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
