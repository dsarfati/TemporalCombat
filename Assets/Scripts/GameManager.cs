using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameManager : MonoBehaviour
{
    public static List<GameSettings> players = new List<GameSettings>();
    public GameObject player;
    public GameObject hud;
    public GameObject boulder;
    private bool gameover = false;

    // Use this for initialization
    public void Awake()
    {
        this.ReceiveAll<DeathEvent>()
            .Subscribe(_ =>
            {
                CheckGameOver();
            }).AddTo(this);
        
        var hudCtrl = GameObject.FindObjectOfType<HUDController>();

        foreach (var gs in players)
        {
            if (gs.PlayerId == -1) continue;
            //grab gamesettings from stream later
            print("maaaagic");
            //for (int i = 0; i < gs.PlayerIds.Length; i++)
            //{
            if (hudCtrl != null)
            {
                hudCtrl.AddPlayers(gs.PlayerId);
            }
            SpawnPlayer(gs.PlayerId);
            //}


        }
        players = new List<GameSettings>();
    }

    //TODO: choose spawnpoints from stage
    void SpawnPlayer(int i)
    {

        GameObject newPlayer = Instantiate(player);
        newPlayer.name = "Player " + i;
        Player playerScript = newPlayer.GetComponent<Player>();
        if (playerScript != null)
        {
            playerScript.playerId = i;
        }

        Assets.Scripts.ControllerInput inputScript = newPlayer.GetComponent<Assets.Scripts.ControllerInput>();
        if (inputScript != null)
        {
            inputScript.PlayerNumber = i - 1;
            newPlayer.GetComponent<CharacterManager>().Initialize(i);
        }

    }

    private void CheckGameOver()
    {
        Debug.Log("CHGECKING GAME OVER");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        int numPlayersAlive = 0;
        foreach (var player in players)
        {
            if (player.GetComponent<PlayerManager>().CheckAlive())
            {
                numPlayersAlive++;
            }
        }

        if (numPlayersAlive <= 1)
        {
            RunGameOverSequence();
        }
    }

    private void RunGameOverSequence()
    {
        if(!gameover)
        {
            gameover = true;
            Debug.Log("GAME OVER ASDJFKLSDJFFDJSKLDSFKJLDSFJLK");
            //stop all movement
            foreach (var x in GameObject.FindObjectsOfType<CharacterMovement>())
            {
                Destroy(x);
            }


            foreach (var character in GameObject.FindGameObjectsWithTag("Character"))
            {
                
                if(character != null)
                {
                    var charScript = character.GetComponent<Character>();
                    var healthScript = character.GetComponent<CharacterHealth>();
                    if (charScript != null && charScript.IsActive && healthScript != null && !healthScript.isDead)
                    {
                        character.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                        Vector3 spawnPos = character.transform.position;
                        Vector3 camTarget = spawnPos + new Vector3(0, 0, 0);
                        for (int i = 0; i < 10; i++)
                        {
                            this.Send(new PositionUpdate(camTarget + new Vector3(5, 0, 0), Vector3.zero));
                            this.Send(new PositionUpdate(camTarget + new Vector3(-5, 0, 0), Vector3.zero));
                        }
                        spawnPos.y = 10;
                        boulder.transform.position = spawnPos;
                        boulder.SetActive(true);
                    }
                    else
                    {
                        Destroy(character);
                    }
                }
                
            }

        }
    }
        

}
