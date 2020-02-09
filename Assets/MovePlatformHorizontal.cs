using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformHorizontal : MonoBehaviour {

    public Transform pos1, pos2;
    public float speed;
    public Transform startPos;

    Vector3 nextPos;
    [SerializeField]
    //public float LeftBoundary = 5f, RightBoundary = 1f, MoveSpeed = 5f;
    //float dirX;
    //private Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        //rb = this.GetComponent<Rigidbody2D>();
        //dirX = -1f;
        nextPos = startPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == pos1.position)
        {
            //dirX = 1f;
            nextPos = pos2.position;

        }
        if (transform.position == pos2.position)
        {
            //dirX = -1f;
            nextPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
    

}
