using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    // Use this for initialization
    private Transform playerTransform;

    public float smoothSpeed;
    public float smoothTime = 0.15f;
    public Vector3 offset;

    Vector3 velocity = Vector3.zero;

    public bool bounds;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;
    void Start () {
        playerTransform = GameObject.Find("Character").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //we store original postion

        Vector3 originalPos = playerTransform.position + offset;
        originalPos.x = Mathf.Clamp(playerTransform.position.x, minCameraPos.x, maxCameraPos.x);

        //Vector3 smoothPosition = Vector3.Lerp(transform.position, originalPos, smoothSpeed);
        //transform.position = smoothPosition;
        transform.position = Vector3.SmoothDamp(transform.position, originalPos, ref velocity, smoothTime);
        
	}
}
