using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CharacterSpriteManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _activeSprite;

    [SerializeField]
    private GameObject _inActiveSprite;

    void Awake()
    {
        this.Receive<CharacterActivated>().Subscribe(CharacterActivated).AddTo(this);
    }

    private void CharacterActivated(CharacterActivated message)
    {
        if (message.IsActivated)
        {
            _activeSprite.gameObject.SetActive(true);
            _inActiveSprite.gameObject.SetActive(false);
        }
        else
        {
            _activeSprite.gameObject.SetActive(false);
            _inActiveSprite.gameObject.SetActive(true);
        }
    }
}
