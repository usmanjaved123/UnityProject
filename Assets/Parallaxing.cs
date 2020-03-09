using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{

    //public Transform[] backgrounds;
    //private float[] parallaxScales;
    //public float smoothing = 1f;

    //private Transform cam; //reference to main camera
    //private Vector3 previousCamPos;

    private float length, StartPos;
    public GameObject cam;
    public float parallaxEffect;

    //Is called before Start(). Great for references.
    void Awake()
    {
        // set up camera the reference
        // cam = Camera.main.transform;
    }
    // Use this for initialization
    void Start()
    {
        // The previous frame had the current frame's camera position
        //previousCamPos = cam.position;
        //// asigning coresponding parallaxScales
        //parallaxScales = new float[backgrounds.Length];
        //for (int i = 0; i < backgrounds.Length; i++)
        //{
        //    parallaxScales[i] = backgrounds[i].position.z * -1;
        //}
        StartPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;


    }
    void Update()
    {
        float temp = (cam.transform.position.x * (1-parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(StartPos + dist, transform.position.y, transform.position.z);

        if (temp > StartPos + length)
        {
            StartPos += length;
        }
        else if (temp < StartPos - length)
        {
            StartPos -= length;
        }
    }
    // Update is called once per frame
    //void Update()
    //{
    //    //// for each background
    //    //for (int i = 0; i < backgrounds.Length; i++)
    //    //{
    //    //    // the parallax is the opposite of the camera movement because the previous frame multiplied by the scale
    //    //    float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
    //    //    // set a target x position which is the current position plus the parallax
    //    //    float backgroundTargetPosX = backgrounds[i].position.x - parallax;
    //    //    // create a target position which is the background's current position with it's target x position
    //    //    Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
    //    //    // fade between current position and the target position using lerp
    //    //    backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
    //    //}
    //    //// set the previousCamPos to the camera's position at the end of the frame
    //    //previousCamPos = cam.position;
    //}
}
