using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {

   //ector3 localScale;
    Image healthBar;
    float maxHealth = 100f;
    public static float health;

    // Use this for initialization
    void Start()
    {
        healthBar = GetComponent<Image>();
        health = maxHealth;
      //localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //calScale.x = MovePlayer.healthAmount;
        //ransform.localScale = localScale;
        healthBar.fillAmount = health / maxHealth;
    }
}
