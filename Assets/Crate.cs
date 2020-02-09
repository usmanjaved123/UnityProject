using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Crate : MonoBehaviour {
    [SerializeField]
    private GameObject ruby,shatteredCrate;

    public static event Action CreateDestroyed = delegate { };
    private int hitcount;
    private Animator anim;
    private AudioSource breaksound;
    private SpriteRenderer sp;
    private bool broken;

    void Start()
    {
        anim = GetComponent<Animator>();
        breaksound = GetComponent<AudioSource>();
        sp = GetComponent<SpriteRenderer>();
        //Instantiate(shatteredCrate, transform.position, Quaternion.identity);
        hitcount = 1;
        broken = false;
    }
    void Update()
    {
        if(hitcount==0 && !broken)
        {
            CrateCounterScript.Crates += 1;
            broken = true;
            Instantiate(shatteredCrate, transform.position, Quaternion.identity);
            breaksound.Play();
            sp.enabled = false;
            Destroy(gameObject,1f);
            Instantiate(ruby, transform.position, Quaternion.identity);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //Check if enemy is attacking
        if (col.gameObject.name.Equals("PlayerHand"))
        {
            breaksound.Play();
            anim.SetTrigger("CrateHit");
            hitcount -= 1;
        }
        if (col.CompareTag("Kunai"))
        {
            breaksound.Play();
            anim.SetTrigger("CrateHit");
            hitcount -= 1;
            Destroy(col.gameObject);
        }

    }
}
