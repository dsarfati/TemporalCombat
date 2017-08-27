using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CharacterAnimation : MonoBehaviour
{

    Animator anim;

    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        if (anim != null)
        {
            transform.parent.parent.Receive<MoveInput>().Where(_ => anim.enabled)
                .Subscribe(Input =>
                {
                    anim.speed = 1;
                    if (Input.XValue < 0)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                        if(Input.XValue < -0.1f)
                        {

                            anim.speed = 2 * Mathf.Abs(Input.XValue);
                        }
                        
                    }
                    else if (Input.XValue > 0)
                    {
                        transform.localScale = new Vector3(1, 1, 1);

                        if(Input.XValue > 0.1f)
                        {
                            anim.speed = 2 * Mathf.Abs(Input.XValue);

                        }

                        
                    }
                    anim.SetFloat("Movement", Mathf.Abs(Input.XValue));

                }).AddTo(this);

            transform.parent.parent.Receive<AttackInput>()
            .Where(_ => anim.enabled)
                .Subscribe(Input =>
                {

                    anim.SetTrigger("Attacking");


                }).AddTo(this);
        }


    }

}
