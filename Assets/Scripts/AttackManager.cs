using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[RequireComponent(typeof(Collider2D))]
public class AttackManager : MonoBehaviour
{
    void Awake()
    {
        var collider = GetComponent<Collider2D>();

        //Assumes the starting position is on the left side of the character
        var startPosition = transform.position.x;

        this.transform.parent.parent.parent.Receive<MoveInput>().Subscribe(m =>
        {
            transform.localPosition = new Vector2(startPosition * Mathf.Sign(m.XValue), transform.localPosition.y);
        }).AddTo(this);
    }
}
