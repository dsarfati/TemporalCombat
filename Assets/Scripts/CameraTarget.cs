using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Assets.Scripts
{
    public class CameraTarget : MonoBehaviour
    {
        private new Transform transform;
        private List<CharacterManager> Players;

        private void Awake()
        {
            transform = GetComponent<Transform>();
        }

        private void Start()
        {
            //Players = new List<CharacterManager>();
            //var pObjs = GameObject.FindGameObjectsWithTag("Player");
            //IObservable<Vector3> allPlayersPos;
            //Players.Add(pObjs[0].GetComponent<CharacterManager>());
            //var obsStream = this.ObserveEveryValueChanged(_=>Players[0].ActiveCharacterTransform).Take(4).Select(t=>t.position).StartWith(Vector3.zero);
            //for (int i = 1; i < pObjs.Length; i++)
            //{
            //    Players.Add(pObjs[i].GetComponent<CharacterManager>());
            //    obsStream =
            //        obsStream.CombineLatest(
            //            this.ObserveEveryValueChanged(_ => Players[i].ActiveCharacterTransform)
            //                .Select(t => t.position)
            //                .StartWith(Vector3.zero),
            //            (v1, v2) => (v1 + v2) * 0.5f);
            //}

            this.ReceiveAll<PositionUpdate>().Buffer(4).Subscribe(pos =>
            {
                var avg = Vector3.zero;
                foreach (var positionUpdate in pos)
                {
                    avg += positionUpdate.pos;
                }
                avg *= 0.25f;
                transform.position = avg;
            }).AddTo(this);


            //Players[3].ActiveCharacterPositionStream.StartWith(Vector3.zero)
            //    .SkipWhile(_ => Players.Count < 4)
            //    .CombineLatest(Players[2].ActiveCharacterPositionStream.StartWith(Vector3.zero),
            //        (v1, v2) => (v1 + v2) * 0.5f)
            //    .SkipWhile(_ => Players.Count < 3)
            //    .CombineLatest(Players[1].ActiveCharacterPositionStream.StartWith(Vector3.zero),
            //        (v1, v2) => (v1 + v2) * 0.5f)
            //    .CombineLatest(Players[0].ActiveCharacterPositionStream.StartWith(Vector3.zero),
            //        (v1, v2) => (v1 + v2) * 0.5f).Subscribe(avg => transform.position = avg).AddTo(this);
        }
    }
}
