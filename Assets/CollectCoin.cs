using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class CollectCoin : MonoBehaviour {

    private LiveCounterScript LifeSystem;
    private AudioSource sound;
    private SpriteRenderer sprite;
    private UnityEngine.Experimental.Rendering.Universal.Light2D lights;
    void Start()
    {
        lights = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        sprite = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
        LifeSystem = FindObjectOfType<LiveCounterScript>();
    }
	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name.Equals("Character"))
        {
            sound.Play();
            RubyCounterScript.Rubies += 1;
            sprite.enabled = false;
            lights.enabled = false;
            Destroy(gameObject,0.5f);
        }
    }
}
