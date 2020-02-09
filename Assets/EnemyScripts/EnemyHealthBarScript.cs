using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarScript : MonoBehaviour {

    Vector3 localScale;
    Enemy script;
    string parentname;
    // Use this for initialization
    void Start () {
        parentname = transform.root.gameObject.name;
        script = GameObject.Find(parentname).GetComponent<Enemy>();

        localScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        localScale.x = script.healthAmount;
        transform.localScale = localScale;
        
	}
}
