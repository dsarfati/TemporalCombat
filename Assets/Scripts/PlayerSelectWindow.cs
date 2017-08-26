using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PlayerSelectWindow : MonoBehaviour
    {
        public List<Sprite> Characters;
        public Image Display;
        public int PlayerNumber;
        private int currCharacter;

        private void Start()
        {
            var control = InputHandlerSingleton.Instance.Player1Move;
            switch (PlayerNumber)
            {
                case 1:
                    control = InputHandlerSingleton.Instance.Player2Move;
                    break;
                case 2:
                    control = InputHandlerSingleton.Instance.Player3Move;
                    break;
                case 3:
                    control = InputHandlerSingleton.Instance.Player4Move;
                    break;
            }
            control.Select(i => i > 0 ? 1 : i < 0 ? -1 : 0).Distinct().Throttle(TimeSpan.FromSeconds(0.1f)).Do(i =>
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
            });
        }
    }
}
