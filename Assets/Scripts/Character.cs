using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Character : MonoBehaviour
{
    public bool IsActive { get; private set; }
    public GameObject SpawnEffect;
    public GameObject DespawnEffect;

    void Awake()
    {
        var rigidBody = GetComponent<Rigidbody2D>();
        var collider = GetComponent<Collider2D>();

        var startingConstraints = rigidBody.constraints;

        this.Receive<CharacterActivated>()
        .Select(x => x.IsActivated)
        .DistinctUntilChanged().Subscribe(isActive =>
        {
            if (isActive)
            {
                collider.enabled = true;
                rigidBody.constraints = startingConstraints;
            }
            else
            {
                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                collider.enabled = false;
            }
        }).AddTo(this);
    }

    public void Activate()
    {
        SpawnEffect.SetActive(true);
        IsActive = true;
        this.Send(new CharacterActivated(true));
    }

    public void Deactivate()
    {
        DespawnEffect.SetActive(true);
        IsActive = false;
        this.Send(new CharacterActivated(false));
    }
}
