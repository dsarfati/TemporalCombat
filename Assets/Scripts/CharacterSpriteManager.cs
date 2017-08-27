using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

public class CharacterSpriteManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _characterSprite;

    [SerializeField]
    private Material _ghostMaterial;

    [SerializeField]
    private Material _normalMaterial;

    [SerializeField]
    private GameObject _characterInfo;

    private Animator _animator;

    void Awake()
    {
        var animatedSprite = Instantiate(_characterSprite, transform);
        _animator = animatedSprite.GetComponentInChildren<Animator>();

        this.Receive<CharacterActivated>().Subscribe(c =>
        {
            if (c.IsActivated)
            {
                _characterInfo.SetActive(true);
                _animator.enabled = true;

                this.gameObject.SetMaterial(_normalMaterial);
            }
            else
            {
                _characterInfo.SetActive(false);
                _animator.enabled = false;

                this.gameObject.SetMaterial(_ghostMaterial);
            }

        }).AddTo(this);
    }
}
