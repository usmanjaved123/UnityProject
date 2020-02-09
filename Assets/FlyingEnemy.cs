using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField]
    public float UpperBoundary = 5f, LowerBoundary = 1f, MoveSpeed = 5f;
    float dirY;
    private Rigidbody2D rb;

    public GameObject ruby;
    public GameObject blood;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        dirY = 1f;
    }

    // Update is called once per frame
    void Update()
    {
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
    void OnTriggerEnter2D(Collider2D col)
    {
        //Check if enemy is attacking
        if (col.gameObject.name.Equals("PlayerHand"))
        {
            //Show Blood
            Instantiate(blood, transform.position, Quaternion.identity);

            //Drop ruby
            Instantiate(ruby, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}