using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KunaiCounterScript : MonoBehaviour
{
    public int Kunai;
    public int TotalKunai;
    private int KunaiCounter;
    Text text;

    //GAMEOVER UI
    public GameObject gameOverUI;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        KunaiCounter = Kunai;
    }

    // Update is called once per frame
    void Update()
    {
        if (KunaiCounter <= 0)
        {
            Player.KunaiFinished = true;
            text.text = 0 + " / " + TotalKunai;
            //Cant throw kunai

        }
        text.text = KunaiCounter +" / "+ TotalKunai;
    }
    public void GiveLife()
    {
        KunaiCounter++;
    }
    public void TakeLife()
    {
        KunaiCounter--;
    }
}
