using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;


public class SplatPlayer : MonoBehaviour {

    public GameObject giblets;
    public float restartDelay = 1500f;
    public AudioEvent hitSfx;
	// Use this for initialization
	void Start () {
        var trigger = this.GetComponent<ObservableTrigger2DTrigger>();
        trigger
            .OnTriggerEnter2DAsObservable()
            .Where(coll => coll.gameObject.tag == "Character" || coll.gameObject.tag == "Ground") //in case we use triggers for other things
            .Subscribe(coll =>
            {
                hitSfx.Play(GetComponent<AudioSource>());
                if(coll.gameObject.tag == "Character")
                {
                    Instantiate(giblets, coll.transform.position + new Vector3(0, 0, -1.5f), Quaternion.identity);
                }
                Destroy(coll.gameObject);
                Debug.Log(gameObject.name + " Colliding(gameover) with " + coll.gameObject.name);
                //Destroy(coll.gameObject);
                Destroy(this.GetComponent<Rigidbody2D>());


                Vector3 camTarget = transform.position;
                for (int i = 0; i < 10; i++)
                {
                    this.Send(new PositionUpdate(camTarget + new Vector3(5, 0, 0), Vector3.zero));
                    this.Send(new PositionUpdate(camTarget + new Vector3(-5, 0, 0), Vector3.zero));
                }

                Observable.Timer(TimeSpan.FromMilliseconds(restartDelay)).Subscribe(x => {
                    Assets.Scripts.InputHandlerSingleton.Instance.Player1Jump
                .Merge(Assets.Scripts.InputHandlerSingleton.Instance.Player2Jump)
                .Merge(Assets.Scripts.InputHandlerSingleton.Instance.Player3Jump)
                .Merge(Assets.Scripts.InputHandlerSingleton.Instance.Player4Jump)
                .Subscribe(_ =>
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("PlayerSelect");
                })
                .AddTo(this);

                    Assets.Scripts.InputHandlerSingleton.Instance.Player1Attack
                        .Merge(Assets.Scripts.InputHandlerSingleton.Instance.Player2Attack)
                        .Merge(Assets.Scripts.InputHandlerSingleton.Instance.Player3Attack)
                        .Merge(Assets.Scripts.InputHandlerSingleton.Instance.Player4Attack)
                        .Where(i => i != 0)
                        .Subscribe(_ =>
                        {
                            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
                        })
                        .AddTo(this);
                }).AddTo(this);

                


            }).AddTo(this);
    }

}
