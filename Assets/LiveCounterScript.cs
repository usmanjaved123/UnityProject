using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveCounterScript : MonoBehaviour {

    public int Lives;
    private int LifeCounter;
    Text text;

    //GAMEOVER UI
    public GameObject gameOverUI;

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        LifeCounter = Lives;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(LifeCounter<=0)
        {

            RubyCounterScript.Rubies = 0;
            Player.IsDead = true;
            Player.rb.velocity = Vector2.zero;
            gameOverUI.SetActive(true);

        }
        text.text = "X " + LifeCounter;
	}
    public void GiveLife()
    {
        LifeCounter++;
    }
    public void TakeLife()
    {
        LifeCounter--;
    }
}
