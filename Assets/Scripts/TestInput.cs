using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{
    private CharacterManager _characterManager;

    void Awake()
    {
        _characterManager = GetComponent<CharacterManager>();
    }

    void Update()
    {
        //Switch between players
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _characterManager.ActivateCharacter(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _characterManager.ActivateCharacter(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _characterManager.ActivateCharacter(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _characterManager.ActivateCharacter(3);
        }

        if (Input.GetKey(KeyCode.RightArrow))
            this.Send(new MoveInput(1));
        if (Input.GetKey(KeyCode.LeftArrow))
            this.Send(new MoveInput(-1));
    }
}
