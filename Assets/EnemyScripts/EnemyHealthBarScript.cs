using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarScript : MonoBehaviour {

    //Vector3 localScale;
    //Enemy script;
    //string parentname;
    Image healthBar;
    float maxHealth = 100f;
    public float health;
    // Use this for initialization
    void Start () {
        //parentname = transform.root.gameObject.name;
        //script = GameObject.Find(parentname).GetComponent<Enemy>();

        //localScale = transform.localScale;
        healthBar = GetComponent<Image>();
        health = maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
        //localScale.x = script.healthAmount;
        //transform.localScale = localScale;
        healthBar.fillAmount = health / maxHealth;

    }
}
