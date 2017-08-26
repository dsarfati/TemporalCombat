using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 1;

    [SerializeField]
    private float _jumpSpeed = 1;

    private bool _isActive = false;

    /// <summary>
    /// Velocity when the character was deactivated
    /// </summary>
    private Vector2 _deactivatedVelocity;

    void Awake()
    {
        var rigidbody = GetComponent<Rigidbody2D>();

        this.transform.parent.Receive<MoveInput>().Where(_ => _isActive).Subscribe(input =>
        {
            rigidbody.velocity = new Vector2(input.XValue * _moveSpeed, rigidbody.velocity.y);
        }).AddTo(this);

        this.transform.parent.Receive<JumpInput>().Where(_ => _isActive).Subscribe(_ =>
        {
            rigidbody.AddForce(Vector2.up * _jumpSpeed);
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
