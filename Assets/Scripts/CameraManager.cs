using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

namespace Assets.Scripts
{
    public class CameraManager : MonoBehaviour
    {
        public Transform CameraTrans;
        public Vector3 TargetPos;
        public float CamSpeed = 25f;

        private void Awake()
        {
            var lowerConst = Mathf.Sin(20f * Mathf.Deg2Rad);
            TargetPos = CameraTrans.position;
            this.ReceiveAll<PositionUpdate>().Buffer(10).Subscribe(poss =>
            {
                var avg = Vector3.zero;
                var minX = float.MaxValue;
                var maxX = float.MinValue;
                var minY = float.MaxValue;
                var maxY = float.MinValue;
                for (int i = 0; i < poss.Count; i++)
                {
                    if (poss[i].pos.x > maxX)
                        maxX = poss[i].pos.x;
                    else if (poss[i].pos.x < minX)
                        minX = poss[i].pos.x;

                    if (poss[i].pos.y > maxY)
                        maxY = poss[i].pos.y;
                    else if (poss[i].pos.y < minY)
                        minY = poss[i].pos.y;
                }
                avg.x = (minX + maxX) * 0.5f;
                minY = Mathf.Clamp(minY, -5f, 10f);
                avg.y =  (minY+ maxY) * 0.5f;
                avg.z = Mathf.Clamp(-0.5f * Vector2.Distance(new Vector2(minX, minY), new Vector2(maxX, maxY)) / lowerConst, -25f, -8f);
                //CameraTrans.position = Vector3.MoveTowards(CameraTrans.position, avg, CamSpeed * Time.deltaTime);
                TargetPos = avg;
            });
        }

        private void FixedUpdate()
        {
            CameraTrans.position = Vector3.MoveTowards(CameraTrans.position, TargetPos, CamSpeed * Time.fixedDeltaTime);
        }

    }
}
