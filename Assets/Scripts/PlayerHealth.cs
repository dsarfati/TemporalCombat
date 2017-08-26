using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers; // need UniRx.Triggers namespace

public class PlayerHealth : MonoBehaviour
{

    public int currHealth = 2;
    public bool isDead
    {
        get { return currHealth <= 0; }
    }

    public void Awake()
    {
        gameObject.AddComponent<ObservableTrigger2DTrigger>()
            .OnTriggerEnter2DAsObservable()
            .Subscribe(coll =>
            {
                print(gameObject.name + " Colliding with " + coll.gameObject.name);
            });
    }

    public void TakeDamage(int dmg)
    {
        currHealth -= dmg;
    }
}