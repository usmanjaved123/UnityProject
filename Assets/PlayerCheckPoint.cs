using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckPoint : MonoBehaviour
{
    private AudioSource checkpointsound;
    Animator anim;
    private bool Checkpoint;
    void Start()
    {
        anim = GetComponent<Animator>();
        checkpointsound = GetComponent<AudioSource>();
        Checkpoint = false;
        //checkpointsound= sounds[0];

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //Check if enemy is attacking
        if (col.gameObject.name.Equals("Character"))
        {
            anim.SetTrigger("Checkpoint");
            if(!Checkpoint)
            {
                checkpointsound.Play();
            }
            Checkpoint = true;
            
        }
    }
}
