using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name.Equals("Character") && Player.healthAmount<100)
        {
            StartCoroutine("Heal");
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.name.Equals("Character"))
        {
            StopCoroutine("Heal");
        }
    }

    IEnumerator Heal()
    {
        for(float currentHealth=Player.healthAmount; currentHealth<=100; currentHealth+=0.5f)
        {
            HealthBarScript.health = currentHealth;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Player.healthAmount = 100;
    }
}
