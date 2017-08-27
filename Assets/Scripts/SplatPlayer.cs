using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class SplatPlayer : MonoBehaviour {
    

	// Use this for initialization
	void Start () {
        var trigger = this.GetComponent<ObservableTrigger2DTrigger>();
        trigger
            .OnTriggerEnter2DAsObservable()
            .Where(coll => coll.gameObject.tag == "Character") //in case we use triggers for other things
            .Subscribe(coll =>
            {
                Debug.Log(gameObject.name + " Colliding(gameover) with " + coll.gameObject.name);
                Destroy(coll.gameObject);
                Destroy(this.GetComponent<Rigidbody2D>());

                Assets.Scripts.InputHandlerSingleton.Instance.Player1Jump
                .Merge(Assets.Scripts.InputHandlerSingleton.Instance.Player2Jump)
                .Merge(Assets.Scripts.InputHandlerSingleton.Instance.Player3Jump)
                .Merge(Assets.Scripts.InputHandlerSingleton.Instance.Player4Jump)
                .Subscribe(_ =>
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Arena");
                })
                .AddTo(this);

                Assets.Scripts.InputHandlerSingleton.Instance.Player1Attack
                    .Merge(Assets.Scripts.InputHandlerSingleton.Instance.Player2Attack)
                    .Merge(Assets.Scripts.InputHandlerSingleton.Instance.Player3Attack)
                    .Merge(Assets.Scripts.InputHandlerSingleton.Instance.Player4Attack)
                    .Where(i=> i != 0)
                    .Subscribe(_ =>
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
                    })
                    .AddTo(this);


            }).AddTo(this);
    }

}
