using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateShatter : MonoBehaviour {

    private Rigidbody2D[] childrenRbs;
    private float randomTorque, randomDirX, randomDirY;

	// Use this for initialization
	void Start () {
        childrenRbs = GetComponentsInChildren<Rigidbody2D>();

        foreach (Rigidbody2D rigbody2D in childrenRbs)
        {
            randomTorque = Random.Range(-100f, 100f);
            randomDirX = Random.Range(-200f, 200f);
            randomDirY = Random.Range(300f, 300f);

            rigbody2D.AddTorque(randomTorque);
            rigbody2D.AddForce(new Vector2(randomDirX, randomDirY));


        }

        Destroy(gameObject, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
