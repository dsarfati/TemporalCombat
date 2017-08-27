using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameManager : MonoBehaviour
{
    public static List<GameSettings> players = new List<GameSettings>();
    public GameObject player;
    public GameObject hud;

    // Use this for initialization
    public void Awake()
    {
        foreach (var gs in players)
        {
            if (gs.PlayerId == -1) continue;
            //grab gamesettings from stream later
            print("maaaagic");
            //for (int i = 0; i < gs.PlayerIds.Length; i++)
            //{
            SpawnPlayer(gs.PlayerId);
            //}

            GameObject canvasObj = GameObject.FindGameObjectWithTag("Canvas");
            if (canvasObj != null)
            {
                GameObject hudObj = Instantiate(hud, canvasObj.transform);

                HUDController hudCtrl = hudObj.GetComponent<HUDController>();

                if (hudCtrl != null)
                {
                    hudCtrl.AddPlayers(gs.PlayerId);
                }
            } 
        }
        players = new List<GameSettings>();
    }

    //TODO: choose spawnpoints from stage
    void SpawnPlayer(int i)
    {

        GameObject newPlayer = Instantiate(player);
        newPlayer.name = "Player " + i;
        Assets.Scripts.ControllerInput inputScript = newPlayer.GetComponent<Assets.Scripts.ControllerInput>();
        if (inputScript != null)
        {
            inputScript.PlayerNumber = i-1;
            newPlayer.GetComponent<CharacterManager>().Initialize(i);
        }

    }

}
