using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class RubyCounterScript : MonoBehaviour
{
   
    public static int Rubies;
    Text text;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        LoadRubiesPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Rubies + " ";
    }
    public void LoadRubiesPlayer()
    {
        PlayerData data = SaveSystem.LoadRubies();
        Rubies = data.Rubies;
    }

}
