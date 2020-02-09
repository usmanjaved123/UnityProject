using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHitSlash : MonoBehaviour {
    [SerializeField]
    private GameObject ruby;
    public GameObject blood;

    public CameraShake cameraShake;
    Enemy script;
    string parentname;
    void Start()
    {
        parentname = transform.root.gameObject.name;
        script = GameObject.Find(parentname).GetComponent<Enemy>();

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Check if enemy is attacking
        if (col.CompareTag("Enemy"))
        {
            var parentname = col.gameObject.transform.root.gameObject.name;
            Enemy script = GameObject.Find(parentname).GetComponent<Enemy>();
            if (script.healthAmount <= 0)
            {
                //do nothing
            }
            else
            {
                script.healthAmount = 0f;
                script.rb.isKinematic = true;
                script.spawnCollectables();
                //SpecialMoveScript.SpecialMove += 25f;
                Physics2D.IgnoreLayerCollision(8, 9, true);
            }

        }
    }
}
