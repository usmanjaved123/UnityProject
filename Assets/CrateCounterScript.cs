using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrateCounterScript : MonoBehaviour
{
    public static int Crates=0;
    public int LevelCrates;
    Text text;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        Crates = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Crates + " / " + LevelCrates;
    }
}
