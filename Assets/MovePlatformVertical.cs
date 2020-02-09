using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformVertical : MonoBehaviour {
    [SerializeField]
    public float UpperBoundary = 5f, LowerBoundary = 1f, MoveSpeed = 5f;
    float dirY;
    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody2D>();
        dirY = 1f;
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y > UpperBoundary)
        {
            dirY = -1f;
        }
        else if (transform.position.y < LowerBoundary)
        {
            dirY = 1f;
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x, dirY * MoveSpeed);
       
    }
    
}
