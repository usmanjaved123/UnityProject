using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Rubies;
    public  float Dashcooldown;

    public PlayerData()
    {
        Rubies = RubyCounterScript.Rubies;
        Dashcooldown = Player.dashcooldown;
    }
}
