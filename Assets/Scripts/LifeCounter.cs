using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class LifeCounter : MonoBehaviour {

    public int playerId;
    public int maxLives = 4;
    public GameObject heart;

	// Use this for initialization
	void Awake () {
        SetLives();

        this.ReceiveAll<DeathEvent>()
            .Where(de => de.Character.transform.parent.GetComponent<Player>().playerId == playerId)
            .Subscribe(_ =>
            {
                LoseLife();
            });
            
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("losing life");
            LoseLife();
        }
	}

    void SetLives()
    {
        for (int i = 0; i < maxLives; i++)
        {
            Instantiate(heart, transform);
        }
    }

    void LoseLife()
    {
        
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform img = transform.GetChild(i).GetChild(0);
            if (img != null && img.gameObject.activeInHierarchy)
            {
                img.gameObject.SetActive(false);
                return;
            }
        }
    }
    
}
