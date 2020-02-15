using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrowCollisionScript : MonoBehaviour
{
    [SerializeField]
    public GameObject WallCloud;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //Check if enemy is attacking
        if (col.CompareTag("Enemy"))
        {
            var parentname = col.gameObject.transform.root.gameObject.name;
            Enemy script = GameObject.Find(parentname).GetComponent<Enemy>();
            //IF ENEMY IS ARMORED NO KUNAI DAMAGE
            if (script.IsArmored == false)
            {
                if (script.healthAmount <= 0)
                {
                    //do nothing
                }
                else
                {
                    if (script.AttachFromBehind)
                    {
                        script.healthAmount = 0f;
                        script.spawnCollectables();
                    }
                    script.healthAmount -= 50f;
                }
            }
            else
            {
                Destroy(gameObject);
            }



            Destroy(gameObject);
        }
        if (col.CompareTag("Ground"))
        {
            Instantiate(WallCloud, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (col.CompareTag("BreakWall"))
        {
            Instantiate(WallCloud, transform.position, Quaternion.identity);
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        if (col.CompareTag("Kunai"))
        {
            Instantiate(WallCloud, transform.position, Quaternion.identity);
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
