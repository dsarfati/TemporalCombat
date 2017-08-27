
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers; // need UniRx.Triggers namespace

public class CharacterHealth : MonoBehaviour
{
    public AudioEvent hitSfx;
    public int invincibilityTime = 1000;
    public int currHealth = 2;
    public bool isDead
    {
        get { return currHealth <= 0; }
    }

    public void Awake()
    {
        var trigger = this.GetComponent<ObservableTrigger2DTrigger>();
        var stayTrigger = trigger.OnTriggerStay2DAsObservable();

        trigger
            .OnTriggerEnter2DAsObservable()
            .Merge(stayTrigger)
            .Where(coll => coll.gameObject.layer == LayerMask.NameToLayer("Attack")) //in case we use triggers for other things
            .ThrottleFirst(TimeSpan.FromMilliseconds(invincibilityTime))
            .Subscribe(coll =>
            {
                Debug.Log(gameObject.name + " Colliding with " + coll.gameObject.name);
                TakeDamage(1);
            }).AddTo(this);
    }

    public void TakeDamage(int dmg)
    {
        hitSfx.Play(GetComponent<AudioSource>());
        currHealth -= dmg;

        if(isDead)
        {
            this.Send(new DeathEvent(this.GetComponent<Character>()));

            Destroy(this.gameObject);
        }
        
    }
}