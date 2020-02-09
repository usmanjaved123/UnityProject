using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePlayerCollisionScript : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        //Check if enemy is attacking
        if (col.CompareTag("Player"))
        {
            HealthBarScript.health -= 100f;
        }
    }
}
