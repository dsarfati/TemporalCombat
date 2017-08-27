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

<<<<<<< HEAD
                HUDController hudCtrl = hudObj.GetComponent<HUDController>();
=======
        this.ReceiveAll<DeathEvent>()
            .Subscribe(_ =>
            {
                CheckGameOver();
            });

>>>>>>> 947649d6e054452d55bf14411f7e9a7526493de6

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
        Player playerScript = newPlayer.GetComponent<Player>();
        if(playerScript != null)
        {
            playerScript.playerId = i;
        }
        
        Assets.Scripts.ControllerInput inputScript = newPlayer.GetComponent<Assets.Scripts.ControllerInput>();
        if (inputScript != null)
        {
            inputScript.PlayerNumber = i-1;
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
