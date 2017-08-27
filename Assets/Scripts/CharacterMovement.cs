using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 1;

    [SerializeField]
    private float _jumpSpeed = 1;

    public AudioEvent jumpSfx;

    private bool _isActive = false;

    /// <summary>
    /// Velocity when the character was deactivated
    /// </summary>
    private Vector2 _deactivatedVelocity;

    void Awake()
    {
        var rigidbody = GetComponent<Rigidbody2D>();

        var jumpCount = 0;

        this.transform.parent.Receive<MoveInput>().Where(_ => _isActive).Subscribe(input =>
        {
            
            if(this != null)
            {
                rigidbody.velocity = new Vector2(input.XValue * _moveSpeed, rigidbody.velocity.y);
                this.Send(new PositionUpdate(transform.position, rigidbody.velocity));

            }
        }).AddTo(this);

        this.transform.parent.Receive<JumpInput>().Where(_ => _isActive).Subscribe(_ =>
        {
            if(this != null)
            {
                var isGrounded = Physics2D.OverlapPoint(this.transform.position + Vector3.down, LayerMask.NameToLayer("Ground"));

                if (isGrounded != null)
                {
                    jumpCount = 0;
                }
                else if (jumpCount >= 2)
                {
                    return;
                }

                jumpCount++;
                jumpSfx.Play(GetComponent<AudioSource>());
                rigidbody.AddForce(Vector2.up * _jumpSpeed);
            }

        }).AddTo(this);

        this.Receive<CharacterActivated>()
        .Select(msg => msg.IsActivated)
        .DistinctUntilChanged()
        .Subscribe(isActive =>
        {
            _isActive = isActive;

            if (_isActive == false)
            {
                //Save the previous velocity
                _deactivatedVelocity = rigidbody.velocity;

                //Stop the movement
                rigidbody.velocity = Vector2.zero;
            }
            else
            {
                rigidbody.velocity = _deactivatedVelocity;
            }

        }).AddTo(this);
    }
}