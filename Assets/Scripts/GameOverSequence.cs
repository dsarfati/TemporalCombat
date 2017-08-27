using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers; // need UniRx.Triggers namespace

public class GameOverSequence : MonoBehaviour {

    public GameObject boulder;

	// Use this for initialization
	void Start () {
		
        //stop all movement
        foreach(var x in GameObject.FindObjectsOfType<CharacterMovement>())
        {
            Destroy(x);
        }

        foreach(var character in GameObject.FindGameObjectsWithTag("Character"))
        {
            if(character.GetComponent<Character>().IsActive)
            {
                Vector3 spawnPos = character.transform.position;
                spawnPos.y = 10;
                Instantiate(boulder, spawnPos, Quaternion.identity);
                break;
            }
        }

    }



}
