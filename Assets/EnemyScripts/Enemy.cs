using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject ruby, fatalitytext;
    [SerializeField]
    public Transform barrel;
    public Rigidbody2D fire;
    public float throwspeed = 500f;
    public CameraShake cameraShake;
    public Image Healthbar;
    public bool IsArmored;
    public bool IsRanged;
    //camera
    private CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin noise;

  

    public Transform player;
    public float chaseRange;
    public GameObject blood;
    public GameObject HitEffect;
    //SOUNDS
    public AudioSource[] sounds;
    public AudioSource AttackSound;
    public AudioSource DieSound;

    public float moveSpeed = 5f;
    public float leftboundary = 2f;
    public float rightboundary = 5.5f;
    public static float AttackDamage = 5f;
    public Rigidbody2D rb;
    Collider2D enemboxcol;
    CircleCollider2D enemcirclecol;
    private Vector2 movement;
    public bool PlayerDead = false;
    public bool PlayerDetected = false;
    public bool IsDead = false;
    public float dirX;
    public float healthAmount;
    float maxHealth = 100f;

    public bool facingRight = true;
    Vector3 localScale;
    public bool IsAttacking = false;
    public bool AttachFromBehind = false;
    public bool PlayerTooClose = false;
    private bool EnemyDead = false;
    Animator anim;
    public static EnemyHealthBarScript hp;

    public float shakeDuration = 0.3f;
    // Use this for initialization
    void Start()
    {
        //camerasettings
        vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        hp = FindObjectOfType<EnemyHealthBarScript>();

        healthAmount = maxHealth;

        localScale = transform.localScale;
        rb = this.GetComponent<Rigidbody2D>();
        enemboxcol = GetComponent<Collider2D>();
        enemcirclecol = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("isWalking", true);
        sounds = GetComponents<AudioSource>();

        AttackSound = sounds[0];
        DieSound = sounds[1];
        dirX = -1f;
    }
   

    // Update is called once per frame
    void Update()
    {

        Healthbar.fillAmount = healthAmount / maxHealth;
        if (!IsRanged)
        {
            float distanceToTarget = Vector3.Distance(transform.position, player.position);
            //PlayerDetected = false;
            //Player Too Close
            if (distanceToTarget <= 1)
            {
                PlayerTooClose = true;
            }
            else
            {
                PlayerTooClose = false;
            }
            if (distanceToTarget <= chaseRange && !PlayerDead)
            {
                if (!IsAttacking)
                {

                    //IF PLAYER IS ON THE RIGHT SIDE
                    if (player.position.x > transform.position.x)
                    {
                        if ((player.localScale.x < 0 && transform.localScale.x < 0) && !PlayerTooClose)
                        {
                            //DONT DETECT
                            anim.SetBool("isRunning", false);
                            PlayerDetected = false;
                            AttachFromBehind = true;
                        }
                        else
                        {
                            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), moveSpeed * Time.deltaTime * 2);
                            anim.SetBool("isRunning", true);
                            PlayerDetected = true;
                            AttachFromBehind = false;
                        }
                    }
                    //IF PLAYER IS ON THE LEFT SIDE
                    else if (player.position.x < transform.position.x)
                    {

                        if ((player.localScale.x > 0 && transform.localScale.x > 0) && !PlayerTooClose)
                        {
                            //DONT DETECT
                            anim.SetBool("isRunning", false);
                            PlayerDetected = false;
                            AttachFromBehind = true;
                        }
                        else
                        {
                            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), moveSpeed * Time.deltaTime * 2);
                            anim.SetBool("isRunning", true);
                            PlayerDetected = true;
                            AttachFromBehind = false;
                        }
                    }


                }


                //transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), moveSpeed * Time.deltaTime);
            }
            else
            {

                anim.SetBool("isRunning", false);
                PlayerDetected = false;
            }

            if (!PlayerDead && !PlayerDetected)
            {
                if (transform.position.x < leftboundary)
                {
                    dirX = 1f;
                    anim.SetBool("isWalking", true);
                }
                else if (transform.position.x > rightboundary)
                {
                    dirX = -1f;
                    anim.SetBool("isWalking", true);
                }
            }
            if (IsAttacking)
            {
                anim.SetBool("isAttacking", true);
                //anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                if (!PlayerDead)
                {
                    Invoke("PlayAttackSound", 0.25f);
                }


            }
            else
            {
                anim.SetBool("isAttacking", false);
            }
            //Check if player is dead
            if (Player.IsDead)
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isAttacking", false);
                PlayerDead = true;
            }
        }
        else
        {
            
        }
        //Enemy is Dead
        if (healthAmount <= 0)
        {
            //initiate blood
            //Instantiate(blood, transform.position, Quaternion.identity);
            if (!EnemyDead)
            {
                //add screen shake
                //StartCoroutine(cameraShake.Shake(0.125f, 1f));
                StartCoroutine(Noise(2f, 2f, 1f));
                EnemyDead = true;
            }
            anim.Play("Dead_Animation");
           
            PlayerDead = true;
            Invoke("EnableLayerCollision", 1.5f);
        }

    }
    public IEnumerator Throw()
    {
        var firedkunai = Instantiate(fire, barrel.position, barrel.rotation);
        firedkunai.AddForce(barrel.up * throwspeed);
         yield return new WaitForSeconds(2f);
    }
    public IEnumerator Noise(float amplitudeGain, float frequencyGain, float ShakeDuration)
    {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;
        yield return new WaitForSeconds(shakeDuration);
        noise.m_AmplitudeGain = 0f;
    }
    private void FixedUpdate()
    {
        if (!IsRanged)
        {
            if (!IsAttacking && !PlayerDead && !PlayerDetected)
            {
                //moveCharacter(movement);
                rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = Vector2.zero;

            }
        }

    }
    void EnableLayerCollision()
    {
        Physics2D.IgnoreLayerCollision(8, 9, false);
        Destroy(gameObject);
    }
    void LateUpdate()
    {
        if (!IsRanged)
        {
            if (PlayerDetected)
            {
                //IF PLAYER IS ON THE RIGHT SIDE
                if (player.position.x > transform.position.x)
                {
                    if (!facingRight && localScale.x < 0)
                    {
                        localScale.x *= -1;
                    }

                    if (localScale.x < 0)
                    {
                        localScale.x *= -1;
                    }

                }
                //IF PLAYER IS ON THE LEFT SIDE
                else if (player.position.x < transform.position.x)
                {
                    if (facingRight && localScale.x > 0)
                    {
                        localScale.x *= -1;
                    }
                    if (localScale.x > 0)
                    {
                        localScale.x *= -1;
                    }

                }
                else
                {
                    //IF PLAYER IS ON TOP OF ENEMY MOVE IT AWAY LEFT/RIGHT
                    if (facingRight)
                    {
                        transform.position = new Vector2(transform.position.x + 2f, transform.position.y);
                    }
                    else
                    {
                        transform.position = new Vector2(transform.position.x - 2f, transform.position.y);
                    }

                }
            }
            else
            {
                if (dirX > 0)
                {
                    facingRight = true;
                }
                else if (dirX < 0)
                {
                    facingRight = false;
                }

                if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
                {
                    localScale.x *= -1;
                }
            }

            transform.localScale = localScale;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //Check if enemy is attacking
        if (col.gameObject.name.Equals("PlayerHand"))
        {
            if (!IsRanged)
            {
                //Show Hit effect
                Instantiate(HitEffect, transform.position, transform.rotation);
                //add screen shake
                //StartCoroutine(cameraShake.Shake(0.1f, 1f));
                StartCoroutine(Noise(2f, 2f, 1f));
                //HealthBarScript.health += 30f;
                SpecialMoveScript.SpecialMove += 5f;
                if (healthAmount != 0)
                {
                    if (AttachFromBehind)
                    {
                        healthAmount = 0f;
                    }
                    healthAmount -= Player.AttackDamage;

                }

                if (facingRight)
                {
                    rb.AddForce(Vector2.left * 2000);
                }
                else
                {
                    rb.AddForce(Vector2.right * 2000);
                }
                //initiate blood
                if (healthAmount <= 0)
                {
                    //Player death sound
                    DieSound.Play();
                    healthAmount = 0f;
                    Physics2D.IgnoreLayerCollision(8, 9, true);

                    //Show Blood
                    Instantiate(blood, transform.position, Quaternion.identity);

                    //Drop ruby
                    Instantiate(ruby, transform.position, Quaternion.identity);

                    //add screen shake
                    //StartCoroutine(cameraShake.Shake(0.125f, 1f));
                    StartCoroutine(Noise(2f, 2f, 1f));

                }
                IsAttacking = false;
            }
            else
            {
                //Show Hit effect
                Instantiate(HitEffect, transform.position, transform.rotation);
                //add screen shake
                //StartCoroutine(cameraShake.Shake(0.1f, 1f));
                StartCoroutine(Noise(2f, 2f, 1f));
                healthAmount -= Player.AttackDamage * 2;
            }
        }
    }
    public void spawnCollectables()
    {
        //Player death sound
        DieSound.Play();
        healthAmount = 0;
        Physics2D.IgnoreLayerCollision(8, 9, true);

        //Show Blood
        Instantiate(blood, transform.position, Quaternion.identity);
        
        //Show FatalityText
        Instantiate(fatalitytext, transform.position+new Vector3(0f,2f,0f), Quaternion.identity);

        //Drop ruby
        Instantiate(ruby, transform.position, Quaternion.identity);

        //add screen shake
        //StartCoroutine(cameraShake.Shake(0.125f, 1f));
        StartCoroutine(Noise(2f, 2f, 1f));
    }
    private void PlayAttackSound()
    {
        if(!AttackSound.isPlaying)
        {
            AttackSound.Play();
        }
        
    }
}
