using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject hud;

    // Use this for initialization
    void Awake()
    {
        //grab gamesettings from stream later
        GameSettings gs;
        gs.numPlayers = 4;
        
        for (int i = 0; i < gs.numPlayers; i++)
        {
            SpawnPlayer(i);
        }

        GameObject canvasObj = GameObject.FindGameObjectWithTag("Canvas");
        if (canvasObj != null)
        {
            GameObject hudObj = Instantiate(hud, canvasObj.transform);

            HUDController hudCtrl = hudObj.GetComponent<HUDController>();

            if (hudCtrl != null)
            {
                hudCtrl.AddPlayers(gs.numPlayers);
            }
        }


    }

    //TODO: choose spawnpoints from stage
    void SpawnPlayer(int i)
    {

        GameObject newPlayer = Instantiate(player);
        newPlayer.name = "Player " + i;
        Assets.Scripts.ControllerInput inputScript = newPlayer.GetComponent<Assets.Scripts.ControllerInput>();
        if (inputScript != null)
        {
            inputScript.PlayerNumber = i;
            newPlayer.GetComponent<CharacterManager>().Initialize(i);
        }

    }

}
