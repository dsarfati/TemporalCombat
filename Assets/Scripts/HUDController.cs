using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

    public GameObject playerStats;

	// Use this for initialization
	void Awake () {
	}


    public void AddPlayers(int x)
    {
        for(int i = 0; i < x; i++)
        {
            GameObject playerStatsObj = Instantiate(playerStats, transform);
            LifeCounter counter = playerStatsObj.GetComponentInChildren<LifeCounter>();
            if(counter != null)
            {
                counter.playerId = i;
            }
            Text playerName = playerStatsObj.GetComponentInChildren<Text>();
            playerName.text = "Player " + (i + 1);
            Image playerPlacard = playerStatsObj.transform.GetChild(0).GetComponent<Image>();
            if(playerPlacard != null)
            {
                Color randColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                playerPlacard.color = randColor;
            }
        }
    }
}
