using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Character : MonoBehaviour
{
    private bool _isActive = false;

    void Awake()
    {
        this.transform.parent.Receive<MoveInput>().Where(_ => _isActive).Subscribe(input =>
        {
            print("Received input");
            this.transform.position = new Vector3(this.transform.position.x + input.XValue * Time.deltaTime, 0, 0);
        }).AddTo(this);
    }

    public void Activate()
    {
        _isActive = true;
        this.Send(new CharacterActivated(true));
    }

    public void Deactivate()
    {
        _isActive = false;
        this.Send(new CharacterActivated(false));
    }
}
