using UniRx;
using UnityEngine;

namespace Assets.Scripts
{
    public class ControllerInput : MonoBehaviour
    {
        public int PlayerNumber;
        private CharacterManager _characterManager;

        private void Awake()
        {
            var ih = InputHandlerSingleton.Instance;
        }

        void Start()
        {
            _characterManager = GetComponent<CharacterManager>();

            var shadow = InputHandlerSingleton.Instance.Player1Shadow;
            var move = InputHandlerSingleton.Instance.Player1Move;
            var attack = InputHandlerSingleton.Instance.Player1Attack;
            var jump = InputHandlerSingleton.Instance.Player1Jump;
            switch (PlayerNumber)
            {
                case 1:
                    shadow = InputHandlerSingleton.Instance.Player2Shadow;
                    move = InputHandlerSingleton.Instance.Player2Move;
                    attack = InputHandlerSingleton.Instance.Player2Attack;
                    jump = InputHandlerSingleton.Instance.Player2Jump;
                    break;
                case 2:
                    shadow = InputHandlerSingleton.Instance.Player3Shadow;
                    move = InputHandlerSingleton.Instance.Player3Move;
                    attack = InputHandlerSingleton.Instance.Player3Attack;
                    jump = InputHandlerSingleton.Instance.Player3Jump;
                    break;
                case 3:
                    shadow = InputHandlerSingleton.Instance.Player4Shadow;
                    move = InputHandlerSingleton.Instance.Player4Move;
                    attack = InputHandlerSingleton.Instance.Player4Attack;
                    jump = InputHandlerSingleton.Instance.Player4Jump;
                    break;
            }
            shadow.Where(i => i > 0).Subscribe(i => _characterManager.ActivateCharacter(i - 1));
            attack.Where(i => i > 0).Subscribe(a =>
            {
                print("attack sent");
                this.Send(new AttackInput());
            });
            move.Subscribe(f => this.Send(new MoveInput(f))).AddTo(this);
            jump.Subscribe(b => this.Send(new JumpInput())).AddTo(this);

        }

    }
}
