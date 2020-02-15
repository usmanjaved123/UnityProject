using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{

    //ENEMY KUNAI COLLISION WITH PLAYER SCRIPT
    public GameObject WallCloud;
    void OnTriggerEnter2D(Collider2D col)
    {
        //Check if enemy is attacking
        if (col.CompareTag("Player"))
        {
            var parentname = col.gameObject.transform.root.gameObject.name;
            Player script = GameObject.Find(parentname).GetComponent<Player>();
            //IF ENEMY IS ARMORED NO KUNAI DAMAGE
            if (HealthBarScript.health <= 0)
            {
                //do nothing
            }
            else
            {

                HealthBarScript.health -= 10f;
            }

            Destroy(gameObject);
        }
        if (col.CompareTag("Ground"))
        {
            Instantiate(WallCloud, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (col.CompareTag("FireArrow"))
        {
            Instantiate(WallCloud, transform.position, Quaternion.identity);
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
