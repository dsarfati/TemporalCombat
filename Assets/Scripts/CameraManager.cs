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
        public float ViewportMinOffset;

        private void Awake()
        {
            var vertSin = Mathf.Sin(20f * Mathf.Deg2Rad);
            var radAngle = 40 * Mathf.Deg2Rad;
            var halfRadHFOV = Mathf.Atan(Mathf.Tan(radAngle / 2) * (16f/9f));
            var horiSin = Mathf.Sin(halfRadHFOV);
            //var FrustrumMin = 
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
                    if (poss[i].pos.x + 1f > maxX)
                        maxX = poss[i].pos.x + 1f;
                    else if (poss[i].pos.x - 1f < minX)
                        minX = poss[i].pos.x - 1f;

                    if (poss[i].pos.y + 1f > maxY)
                        maxY = poss[i].pos.y + 1f;
                    else if (poss[i].pos.y - 1f < minY)
                        minY = poss[i].pos.y - 1f;
                }

                var hSpread = maxX - minX;
                var hPosZ = Mathf.Clamp(-0.5f * hSpread / horiSin, -25f, -8f);
                var vSpread = maxY - minY;
                var vPosZ = Mathf.Clamp(-0.5f * vSpread / vertSin, -25f, -8f);
                float posZ;

                if (vPosZ > hPosZ)
                {
                    posZ = hPosZ;
                    vSpread = posZ * vertSin * -2;
                }
                else
                {
                    posZ = vPosZ;
                }
                avg.x = (minX + maxX) * 0.5f;

                //minY = Mathf.Clamp(minY,vSpread * 0.5f + ViewportMinOffset, 10f);
                avg.y = Mathf.Clamp((minY + maxY) * 0.5f, vSpread * 0.5f + ViewportMinOffset, 10f);
                avg.z = posZ;
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
