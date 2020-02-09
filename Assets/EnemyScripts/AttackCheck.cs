using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{

    //Use this for initialization

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Character"))
        {
            var parentname = transform.root.gameObject.name;
            Enemy script = GameObject.Find(parentname).GetComponent<Enemy>();
            script.IsAttacking = true;
            //Enemy_Walking.IsAttacking = true;

        }
    }

    // Update is called once per frame 
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Character"))
        {
            var parentname = transform.root.gameObject.name;
            Enemy script = GameObject.Find(parentname).GetComponent<Enemy>();
            script.IsAttacking = false;
            //Enemy_Walking.IsAttacking = false;

        }
    }

}
