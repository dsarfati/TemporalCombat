﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CharacterAnimation : MonoBehaviour {

    Animator anim;

	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
        if(anim != null)
        {
            transform.parent.parent.parent.Receive<MoveInput>()
                .Subscribe(Input =>
                {
                    anim.speed = 1;
                    if (Input.XValue < -0.1f)
                    {
                        anim.speed = 2 * Mathf.Abs(Input.XValue);

                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else if (Input.XValue > 0.1f)
                    {
                        anim.speed = 2 * Mathf.Abs(Input.XValue);

                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    anim.SetFloat("Movement", Mathf.Abs(Input.XValue));

                }).AddTo(this);

            transform.parent.parent.parent.Receive<AttackInput>()
                .Subscribe(Input =>
                {
                    
                    anim.SetTrigger("Attacking");
                    

                }).AddTo(this);
        }


    }

}