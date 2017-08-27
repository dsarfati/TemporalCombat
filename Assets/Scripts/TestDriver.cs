﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class TestDriver : MonoBehaviour
{
    void Awake()
    {
        GameManager.players.Add(new GameSettings() { PlayerId = 1, Helmet = Helmet.Knight });
        GameManager.players.Add(new GameSettings() { PlayerId = 2, Helmet = Helmet.Mohawk });
    }

    void Start()
    {
        foreach (var ctrlInput in FindObjectsOfType<ControllerInput>())
        {
            ctrlInput.enabled = false;
        }

        foreach (var testInput in FindObjectsOfType<TestInput>())
        {
            testInput.enabled = true;
        }
    }
}
