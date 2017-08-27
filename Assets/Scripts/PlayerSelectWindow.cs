using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PlayerSelectWindow : MonoBehaviour
    {
        public PlayerSelectManager psMan;
        public List<Sprite> Characters;
        public Image Display;
        public int PlayerNumber;
        public GameObject Prompt;
        public GameObject Ready;
        [SerializeField]
        private int currCharacter;

        private void Start()
        {
            var control = InputHandlerSingleton.Instance.Player1Move;
            var isReady = psMan.Player1Status.StartWith(0);
            switch (PlayerNumber)
            {
                case 1:
                    control = InputHandlerSingleton.Instance.Player2Move;
                    isReady = psMan.Player2Status.StartWith(0);
                    break;
                case 2:
                    control = InputHandlerSingleton.Instance.Player3Move;
                    isReady = psMan.Player3Status.StartWith(0);
                    break;
                case 3:
                    control = InputHandlerSingleton.Instance.Player4Move;
                    isReady = psMan.Player4Status.StartWith(0);
                    break;
            }
            isReady.DelayFrame(1).Subscribe(i =>
            {
                Prompt.SetActive(i == 0);
                Ready.SetActive(i == 2);
            }).AddTo(this);
            control.DelayFrame(1)
                .Select(i => i > 0 ? 1 : i < 0 ? -1 : 0)
                .DistinctUntilChanged()
                //.Throttle(TimeSpan.FromSeconds(0.1f))
                .CombineLatest(isReady, (c, r) => r == 2 ? 0 : c)
                .Subscribe(i =>
                {
                    currCharacter += i;
                    if (currCharacter >= Characters.Count)
                    {
                        currCharacter = 0;
                    }
                    else if (currCharacter < 0)
                    {
                        currCharacter = Characters.Count - 1;
                    }
                    Display.sprite = Characters[currCharacter];
                }).AddTo(this);
        }
    }
}
