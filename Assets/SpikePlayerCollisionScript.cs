using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePlayerCollisionScript : MonoBehaviour {

    [SerializeField]
    private GameObject blood;
    void OnTriggerEnter2D(Collider2D col)
    {
        //Check if enemy is attacking
        if (col.CompareTag("Player"))
        {
            //Show Blood
            Instantiate(blood, col.gameObject.transform.position, Quaternion.identity);
            HealthBarScript.health -= 100f;
        }
    }
}
