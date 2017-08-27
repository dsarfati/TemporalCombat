﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class AttackManager : MonoBehaviour
{
    [SerializeField]
    private int _attackFrames = 1;

    private bool _isAttacking = false;
    [SerializeField] private SpriteRenderer swoosh;
    void Awake()
    {
        //Assumes the starting position is on the left side of the character
        var startPosition = transform.localPosition.x;

        var character = this.GetComponentInParent<Character>();
        var player = character.transform.parent;

        player.transform.Receive<MoveInput>().Where(_ => character.IsActive).Subscribe(m =>
          {
              if (this != null && m.XValue != 0)
              {
                  transform.localPosition = new Vector2(startPosition * Mathf.Sign(m.XValue), transform.localPosition.y);
                  swoosh.flipX =  m.XValue > 0;
              }
          }).AddTo(this);

        player.transform.Receive<AttackInput>().Where(_ => character.IsActive).Subscribe(_ => AttackStart()).AddTo(this);

        AttackComplete();
    }
    void AttackStart()
    {
        if(this != null)
        {
            print("Attack");
            if (_isAttacking)
                return;

            this.gameObject.SetActive(true);
            this._isAttacking = true;

            Observable.TimerFrame(_attackFrames).Subscribe(_ => AttackComplete()).AddTo(this);
        }
    }

    void AttackComplete()
    {
        this.gameObject.SetActive(false);
        this._isAttacking = false;
    }
}
