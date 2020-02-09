using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDustCloud : MonoBehaviour {

    [SerializeField]
    GameObject dustCloud;

    bool coroutineAllowed, grounded;
    public CameraShake cameraShake;
    public GameObject player;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag.Equals("Ground"))
        {
            grounded = true;
            coroutineAllowed = true;
            Player.LandSound.Play();
            Instantiate(dustCloud,transform.position,Quaternion.identity);
          
        }
        if (col.gameObject.tag.Equals("Platform"))
        {
           // Player.OnPlatform = true;
            grounded = true;
            coroutineAllowed = true;
            Player.LandSound.Play();
            Instantiate(dustCloud, transform.position, Quaternion.identity);

            player.transform.parent = col.gameObject.transform;

        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Ground"))
        {
            grounded = false;
            coroutineAllowed = false;
        }
        if (col.gameObject.tag.Equals("Platform"))
        {
            //Player.OnPlatform = false;
            player.transform.parent = null;
        }

    }
    void Update()
    {
        if(grounded && Player.rb.velocity.x!=0 && coroutineAllowed)
        {
            StartCoroutine("SpawnCloud");
            coroutineAllowed = false;
        }
        if(Player.rb.velocity.x==0 || !grounded)
        {
            StopCoroutine("SpawnCloud");
            coroutineAllowed = true;
        }
    }
    IEnumerator SpawnCloud()
    {

        while (grounded)
        {
            Instantiate(dustCloud, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
