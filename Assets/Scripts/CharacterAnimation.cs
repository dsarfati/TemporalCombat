using System.Collections;
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
            transform.parent.parent.Receive<MoveInput>()
                .Subscribe(Input =>
                {
                    if (Input.XValue == 0)
                    {
                        anim.SetTrigger("Idling");
                    }
                    else
                    {
                        if(Input.XValue < 0)
                        {
                            transform.localScale = new Vector3(-1, 1, 1);
                        }
                        else if(Input.XValue > 0)
                        {
                            transform.localScale = new Vector3(1, 1, 1);
                        }
                        anim.SetTrigger("Walking");
                        anim.speed = Input.XValue;
                    }

                }).AddTo(this);

            transform.parent.parent.Receive<AttackInput>()
                .Subscribe(Input =>
                {
                    
                    anim.SetTrigger("Attacking");
                    

                }).AddTo(this);
        }


    }

}
