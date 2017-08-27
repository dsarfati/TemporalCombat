using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{

    public ColorSet palette;
    public GameObject playerStats;

    // Use this for initialization
    void Awake()
    {
    }


    public void AddPlayers(int x)
    {
        //for(int i = 0; i < x.Length; i++)
        //{
        GameObject playerStatsObj = Instantiate(playerStats, transform);
        LifeCounter counter = playerStatsObj.GetComponentInChildren<LifeCounter>();
        if (counter != null)
        {
            counter.playerId = x;
        }
        Image playerPlacard = playerStatsObj.transform.GetChild(0).GetComponent<Image>();
        if (playerPlacard != null)
        {
            playerPlacard.color = palette.HUDColors[x - 1];
        }
        //}
    }
}
