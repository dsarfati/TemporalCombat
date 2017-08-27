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
            InputHandlerSingleton.Instance.Player1Jump
                .Merge(InputHandlerSingleton.Instance.Player2Jump)
                .Merge(InputHandlerSingleton.Instance.Player3Jump)
                .Merge(InputHandlerSingleton.Instance.Player4Jump)
                .Subscribe(_ =>
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(LevelToLoad);
                })
                .AddTo(this);

        }

    }
}

