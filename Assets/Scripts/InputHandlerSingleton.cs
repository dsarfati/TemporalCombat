using System;
using UnityEngine;
using UniRx;

namespace Assets.Scripts
{
    public class InputHandlerSingleton : MonoBehaviour
    {
        private static InputHandlerSingleton _instance;

        public static InputHandlerSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("InputHandler");
                    _instance = go.AddComponent<InputHandlerSingleton>();
                }
                return _instance;
            }
        }
        /// <summary>
        /// positive val for stick right
        /// </summary>
        public IObservable<float> Player1Move;
        /// <summary>
        /// 0: button up 1: high attack, 2: mid attack, 3: low attack
        /// </summary>
        public IObservable<int> Player1Attack;
        /// <summary>
        /// 0: button up, other numbers to selected shadow
        /// </summary>
        public IObservable<int> Player1Shadow;
        public IObservable<bool> Player1Jump;

        /// <summary>
        /// positive val for stick right
        /// </summary>
        public IObservable<float> Player2Move;
        /// <summary>
        /// 1: high attack, 2: mid attack, 3: low attack
        /// </summary>
        public IObservable<int>   Player2Attack;
        /// <summary>
        /// 0: button up, other numbers to selected shadow
        /// </summary>
        public IObservable<int>   Player2Shadow;
        public IObservable<bool>  Player2Jump;

        /// <summary>
        /// positive val for stick right
        /// </summary>
        public IObservable<float> Player3Move;
        /// <summary>
        /// 1: high attack, 2: mid attack, 3: low attack
        /// </summary>
        public IObservable<int>   Player3Attack;
        /// <summary>
        /// 0: button up, other numbers to selected shadow
        /// </summary>
        public IObservable<int>   Player3Shadow;
        public IObservable<bool>  Player3Jump;

        /// <summary>
        /// positive val for stick right
        /// </summary>
        public IObservable<float> Player4Move;
        /// <summary>
        /// 1: high attack, 2: mid attack, 3: low attack
        /// </summary>
        public IObservable<int>   Player4Attack;
        /// <summary>
        /// 0: button up, other numbers to selected shadow
        /// </summary>
        public IObservable<int>   Player4Shadow;
        public IObservable<bool>  Player4Jump;


        private void Awake()
        {
            //if (!_instance)
            //    _instance = this;

            Player1Attack = this.ObserveEveryValueChanged(_ => Input.GetButton("p1AttackHigh")).Select(h => h ? 1 : 0)
            .Merge(this.ObserveEveryValueChanged(_ => Input.GetButton("p1AttackMid"))
                .Select(h => h ? 2 : 0)).Merge(this.ObserveEveryValueChanged(_ => Input.GetButton("p1AttackLow")).Select(h => h ? 3 : 0));
            Player1Shadow = this.ObserveEveryValueChanged(_ => Input.GetButton("p1Shadow1")).Select(h => h ? 1 : 0)
                .Merge(this.ObserveEveryValueChanged(_ => Input.GetButton("p1Shadow2")).Select(h => h ? 2 : 0))
                .Merge(this.ObserveEveryValueChanged(_ => Input.GetAxis("p1Shadow3")).Select(h => h > 0.1f ? 3 : 0))
                .Merge(this.ObserveEveryValueChanged(_ => Input.GetAxis("p1Shadow4")).Select(h => h > 0.1f ? 4 : 0));
            Player1Jump = this.ObserveEveryValueChanged(_ => Input.GetButton("p1Jump")).Where(h => h);
            Player1Move = Observable.EveryUpdate().Select(_ => Input.GetAxis("p1Horizontal"));


            //Player1Attack.Subscribe(i => Debug.Log("p1 " + i));
            //Player1Shadow.Subscribe(i => Debug.Log("p1 " + i));
            //Player1Move.Subscribe(i => Debug.Log("p1 " + i));
            //Player1Jump.Subscribe(i => Debug.Log("p1 " + i));

            Player2Attack = this.ObserveEveryValueChanged(_ => Input.GetButton("p2AttackHigh")).Select(h => h ? 1 : 0)
            .Merge(this.ObserveEveryValueChanged(_ => Input.GetButton("p2AttackMid"))
                .Select(h => h ? 2 : 0)).Merge(this.ObserveEveryValueChanged(_ => Input.GetButton("p2AttackLow")).Select(h => h ? 3 : 0));
            Player2Shadow = this.ObserveEveryValueChanged(_ => Input.GetButton("p2Shadow1")).Select(h => h ? 1 : 0)
                .Merge(this.ObserveEveryValueChanged(_ => Input.GetButton("p2Shadow2")).Select(h => h ? 2 : 0))
                .Merge(this.ObserveEveryValueChanged(_ => Input.GetAxis("p2Shadow3")).Select(h => h > 0.1f ? 3 : 0))
                .Merge(this.ObserveEveryValueChanged(_ => Input.GetAxis("p2Shadow4")).Select(h => h > 0.1f ? 4 : 0));
            Player2Jump = this.ObserveEveryValueChanged(_ => Input.GetButton("p2Jump")).Where(h => h);
            Player2Move = Observable.EveryUpdate().Select(_ => Input.GetAxis("p2Horizontal"));


            //Player2Attack.Subscribe(i => Debug.Log("p2 " + i));
            //Player2Shadow.Subscribe(i => Debug.Log("p2 " + i));
            //Player2Move.Subscribe(i => Debug.Log("p2 " + i));
            //Player2Jump.Subscribe(i => Debug.Log("p2 " + i));

            Player3Attack = this.ObserveEveryValueChanged(_ => Input.GetButton("p3AttackHigh")).Select(h => h ? 1 : 0)
            .Merge(this.ObserveEveryValueChanged(_ => Input.GetButton("p3AttackMid"))
                .Select(h => h ? 2 : 0)).Merge(this.ObserveEveryValueChanged(_ => Input.GetButton("p3AttackLow")).Select(h => h ? 3 : 0));
            Player3Shadow = this.ObserveEveryValueChanged(_ => Input.GetButton("p3Shadow1")).Select(h => h ? 1 : 0)
                .Merge(this.ObserveEveryValueChanged(_ => Input.GetButton("p3Shadow2")).Select(h => h ? 2 : 0))
                .Merge(this.ObserveEveryValueChanged(_ => Input.GetAxis("p3Shadow3")).Select(h => h > 0.1f ? 3 : 0))
                .Merge(this.ObserveEveryValueChanged(_ => Input.GetAxis("p3Shadow4")).Select(h => h > 0.1f ? 4 : 0));
            Player3Jump = this.ObserveEveryValueChanged(_ => Input.GetButton("p3Jump")).Where(h => h);
            Player3Move = Observable.EveryUpdate().Select(_ => Input.GetAxis("p3Horizontal"));


            //Player3Attack.Subscribe(i => Debug.Log("p3 " + i));
            //Player3Shadow.Subscribe(i => Debug.Log("p3 " + i));
            //Player3Move.Subscribe(i => Debug.Log("p3 " + i));
            //Player3Jump.Subscribe(i => Debug.Log("p3 " + i));

            Player4Attack = this.ObserveEveryValueChanged(_ => Input.GetButton("p4AttackHigh")).Select(h => h ? 1 : 0)
            .Merge(this.ObserveEveryValueChanged(_ => Input.GetButton("p4AttackMid"))
                .Select(h => h ? 2 : 0)).Merge(this.ObserveEveryValueChanged(_ => Input.GetButton("p4AttackLow")).Select(h => h ? 3 : 0));
            Player4Shadow = this.ObserveEveryValueChanged(_ => Input.GetButton("p4Shadow1")).Select(h => h ? 1 : 0)
                .Merge(this.ObserveEveryValueChanged(_ => Input.GetButton("p4Shadow2")).Select(h => h ? 2 : 0))
                .Merge(this.ObserveEveryValueChanged(_ => Input.GetAxis("p4Shadow3")).Select(h => h > 0.1f ? 3 : 0))
                .Merge(this.ObserveEveryValueChanged(_ => Input.GetAxis("p4Shadow4")).Select(h => h > 0.1f ? 4 : 0));
            Player4Jump = this.ObserveEveryValueChanged(_ => Input.GetButton("p4Jump")).Where(h => h);
            Player4Move = Observable.EveryUpdate().Select(_ => Input.GetAxis("p4Horizontal"));


            //Player4Attack.Subscribe(i => Debug.Log("p4 " + i));
            //Player4Shadow.Subscribe(i => Debug.Log("p4 " + i));
            //Player4Move.Subscribe(i => Debug.Log("p4 " + i));
            //Player4Jump.Subscribe(i => Debug.Log("p4 " + i));

        }
    }
}
