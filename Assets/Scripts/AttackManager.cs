using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class AttackManager : MonoBehaviour
{
    [SerializeField]
    private int _attackFrames = 1;

    private bool _isAttacking = false;
    [SerializeField] private SpriteRenderer swoosh;

    public AudioEvent attackSfx;
    public AudioEvent hitSfx;

    private AudioSource attackSrc;
    private AudioSource hitSrc;
    void Awake()
    {
        attackSrc = GetComponent<AudioSource>();
        hitSrc = GetComponentInChildren<AudioSource>();
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

        var trigger = this.GetComponent<ObservableTrigger2DTrigger>();
        var stayTrigger = trigger.OnTriggerStay2DAsObservable();

        trigger
            .OnTriggerEnter2DAsObservable()
            .Merge(stayTrigger)
            .Where(coll => coll.gameObject.layer == LayerMask.NameToLayer("PlayerHurtbox")) //in case we use triggers for other things
            .Subscribe(coll =>
            {
                Debug.Log("BLERJSKLDFJKLDS");
                hitSfx.Play(hitSrc);
            }).AddTo(this);
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
            attackSfx.Play(attackSrc);

            Observable.TimerFrame(_attackFrames).Subscribe(_ => AttackComplete()).AddTo(this);
        }
    }

    void AttackComplete()
    {
        this.gameObject.SetActive(false);
        this._isAttacking = false;
    }
}
