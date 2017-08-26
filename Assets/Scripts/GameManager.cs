using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

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

        this.ReceiveAll<DeathEvent>()
            .Subscribe(_ =>
            {
                CheckGameOver();
            });


    }

    //TODO: choose spawnpoints from stage
    void SpawnPlayer(int i)
    {

        GameObject newPlayer = Instantiate(player);
        newPlayer.name = "Player " + i;
        Player playerScript = newPlayer.GetComponent<Player>();
        if(playerScript != null)
        {
            playerScript.playerId = i;
        }
        
        Assets.Scripts.ControllerInput inputScript = newPlayer.GetComponent<Assets.Scripts.ControllerInput>();
        if (inputScript != null)
        {
            inputScript.PlayerNumber = i;
            newPlayer.GetComponent<CharacterManager>().Initialize(i);
        }

    }

    private void CheckGameOver()
    {
        Debug.Log("CHGECKING GAME OVER");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        int numPlayersAlive = 0;
        foreach(var player in players)
        {
            if(player.GetComponent<PlayerManager>().CheckAlive())
            {
                numPlayersAlive++;
            }
        }

        if(numPlayersAlive <= 1)
        {
            RunGameOverSequence();
        }
    }

    private void RunGameOverSequence()
    {
        Debug.Log("GAME OVER ASDJFKLSDJFFDJSKLDSFKJLDSFJLK");
    }

}
