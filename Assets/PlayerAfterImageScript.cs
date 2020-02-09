using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageScript : MonoBehaviour
{

    [SerializeField]
    private float activetime = 0.1f;
    private float timeactivated;
    private float alpha;
    [SerializeField]
    private float alphaset = 0.8f;
    private float alphaMultiplier = 1f;

    private Transform player;

    private SpriteRenderer sr;

    private SpriteRenderer playersr;

    private Color color;
    Vector3 localScale;

    private void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playersr = player.GetComponent<SpriteRenderer>();
        localScale = transform.localScale;
        alpha = alphaset;
        sr.sprite = playersr.sprite;

        transform.position = player.position;
        if (player.localScale.x < 0)
        {
            //TURN LEFT
            localScale.x *= -1;
        }
        else
        {
            //TURN RIGHT
            localScale.x *= 1;
        }
        transform.localScale = localScale;
        transform.rotation = player.rotation;
        timeactivated = Time.time;
    }

    private void Update()
    {
        alpha *= alphaMultiplier;
        color = new Color(1f, 1f, 1f, alpha);
        sr.color = color;

        if (Time.time >= (timeactivated + activetime))
        {
            AfterImagePool.Instance.AddToPool(gameObject);
        }
    }


}
