using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

public class CharacterSpriteManager : MonoBehaviour
{
    [SerializeField]
    private Material _ghostMaterial;

    [SerializeField]
    private Material _normalMaterial;

    [SerializeField]
    private GameObject _characterInfo;

    [SerializeField]
    private Animator _animator;

    void Awake()
    {
        var sprites = this.GetComponentsInChildren<SpriteRenderer>();
        var meshes = this.GetComponentsInChildren<Anima2D.SpriteMeshInstance>();

        this.Receive<CharacterActivated>().Subscribe(c =>
        {
            if (c.IsActivated)
            {
                _characterInfo.SetActive(true);
                _animator.enabled = true;

                foreach (var sprite in sprites)
                {
                    sprite.material = _normalMaterial;
                }

                foreach (var mesh in meshes)
                {
                    mesh.sharedMaterial = _normalMaterial;
                }
            }
            else
            {
                _characterInfo.SetActive(false);
                _animator.enabled = false;

                foreach (var sprite in sprites)
                {
                    sprite.material = _ghostMaterial;
                }

                foreach (var mesh in meshes)
                {
                    mesh.sharedMaterial = _ghostMaterial;
                }
            }

        }).AddTo(this);
    }
}
