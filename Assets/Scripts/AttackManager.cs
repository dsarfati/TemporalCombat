using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class AttackManager : MonoBehaviour
{
    void Awake()
    {
        this.transform.parent.parent.Receive<MoveInput>().Subscribe(m =>
        {

        }).AddTo(this);
    }
}
