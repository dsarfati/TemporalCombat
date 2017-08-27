using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Assets.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        public string LevelToLoad;
        // Use this for initialization
        void Awake()
        {
            InputHandlerSingleton.Instance.Player1Attack
                .Merge(InputHandlerSingleton.Instance.Player2Attack)
                .Merge(InputHandlerSingleton.Instance.Player3Attack)
                .Merge(InputHandlerSingleton.Instance.Player4Attack)
                .Where(i => i != 0)
                .Subscribe(i =>
                {
                    Debug.Log("Input: " + i);
                    UnityEngine.SceneManagement.SceneManager.LoadScene(LevelToLoad);
                })
                .AddTo(this);

        }

    }
}

