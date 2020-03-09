using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using Cinemachine;
using System;

[Serializable]

public class Player : MonoBehaviour
{
    Material material;
    bool isDissolving = false;
    float fade = 1f;
    private CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin noise;
    public CameraShake cameraShake;

    public static Rigidbody2D rb;
    public Animator anim;
    float dirX, dirY;
    bool isMoving = false;
    //SOUNDS
    public AudioSource[] sounds;
    public AudioSource StepSound;
    public AudioSource AttackSound;
    public AudioSource JumpSound;
    public static AudioSource LandSound;
    public AudioSource DashSound;
    public AudioSource ThrowSound;
    public AudioSource DeathMusic;
    public AudioSource DeathSound;

    //LIVES
    private LiveCounterScript LifeSystem;
    //Kunais
    public static bool KunaiFinished = false;
    private KunaiCounterScript KunaiSystem;
    TextMesh Playerlivestxt;

    [SerializeField]
    public float movespeed = 5f, jumpforce = 600f, throwspeed = 500f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public static float AttackDamage = 50f;
    public GameObject dustCloud;
    //SpawnPoint
    public Transform spawnPoint;
    public float DashSpeed;

    public static float healthAmount;

    public bool facingRight = true;
    bool CandoubleJump;
    public static bool IsDead = false;
    public static bool GameOver = false;
    bool respawnonce;
    Vector3 localScale;

    public Transform barrel;
    public Rigidbody2D kunai;
    //va
    private Vector2 boostSpeed = new Vector2(50, 0);
    private bool canBoost = true;
    public static float boostCooldown = 10f;

    //GAMEOVER UI
    public GameObject gameOverUI;

    //Hit Effect
    public GameObject HitEffect;

    public static bool OnPlatform = false;

    //test
    bool takelife = false;
    private bool IsDashing;
    public float dashtime;
    public float distancebetweenimages;
    public static float dashcooldown = 10f;
    private float dashtimeleft;
    private float lastiamgexpos;
    private float lastdash = -100f;
    //cooldown button images
    public Image DashCooldownImage;
    //attack cooldown
    float attackcooldowntimer;
    float attackcooldown = 0.5f;

    //throw cooldown
    float throwcooldowntimer;
    float throwcooldown = 0.5f;

    //hangtime
    public float hangtime = 0.2f;
    private float hangCounter;

    // Use this for initialization
    void Start()
    {
        // Make the game run as fast as possible
        Application.targetFrameRate = 60;
        //Load data
        if (SaveSystem.FileExits())
        {
            PlayerData data = SaveSystem.LoadRubies();
            if (data.Dashcooldown != 0)
            {
                dashcooldown = data.Dashcooldown;
            }
        }
        GameOver = false;
        respawnonce = false;
        // Make the game run as fast as possible
        Application.targetFrameRate = 60;
        //camerasettings
        vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();


        //player settings
        healthAmount = 100;
        IsDead = false;
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        LifeSystem = FindObjectOfType<LiveCounterScript>();
        KunaiSystem = FindObjectOfType<KunaiCounterScript>();
        KunaiSystem.Kunai = 10;
        KunaiFinished = false;
        sounds = GetComponents<AudioSource>();
        material = GetComponent<SpriteRenderer>().material;

        StepSound = sounds[0];
        AttackSound = sounds[1];
        LandSound = sounds[2];
        JumpSound = sounds[3];
        DashSound = sounds[4];
        ThrowSound = sounds[5];
        DeathMusic = sounds[6];
        DeathSound = sounds[7];
        //StepSound.Play();

        localScale = transform.localScale;

    }
    public IEnumerator Noise(float amplitudeGain, float frequencyGain, float ShakeDuration)
    {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;
        yield return new WaitForSeconds(ShakeDuration);
        noise.m_AmplitudeGain = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        //Run
        Run();
        //Check if dashing
        //Show Button Cooldown
        if (Time.time <= (lastdash + dashcooldown))
        {
            DashCooldownImage.enabled = true;
            DashCooldownImage.fillAmount -= 1 / dashcooldown * Time.deltaTime;

            if (DashCooldownImage.fillAmount <= 0)
            {
                DashCooldownImage.fillAmount = 1;
                DashCooldownImage.enabled = false;
            }
        }
        CheckDash();
        //Dash 
        Dash();
        //Attack
        CheckAttack();
        //Jump
        Jump();
        //Shoot Kunai
        CheckThrow();
        //Check if dead
        PlayerDeath();

    }
    private void FixedUpdate()
    {
        if (!IsDead)
        {
            rb.velocity = new Vector2(dirX * movespeed, rb.velocity.y);
        }

    }
    private void LateUpdate()
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
            localScale.x *= -1;

        transform.localScale = localScale;

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //Check if enemy is attacking
        if (col.gameObject.name.Equals("Hand"))
        {
            //Decrease healthbar
            //HealthBarScript.health -= 5f;

            Instantiate(HitEffect, transform.position, transform.rotation);
            if (facingRight)
            {
                rb.AddForce(Vector2.left * 500);
            }
            else
            {
                rb.AddForce(Vector2.right * 500);
            }
            HealthBarScript.health -= Enemy.AttackDamage;
            healthAmount -= Enemy.AttackDamage;


        }
        //Check if enemy is attacking
        if (col.gameObject.tag.Equals("FlyingEnemy"))
        {
            //Decrease healthbar
            //HealthBarScript.health -= 5f;

            Instantiate(HitEffect, transform.position, transform.rotation);
            if (facingRight)
            {
                rb.AddForce(Vector2.left * 500);
            }
            else
            {
                rb.AddForce(Vector2.right * 500);
            }
            HealthBarScript.health -= Enemy.AttackDamage;
            healthAmount -= Enemy.AttackDamage;


        }
        //Check if Player checkpoints
        if (col.gameObject.tag.Equals("CheckPoint"))
        {
            spawnPoint.position = col.transform.position;
        }

        //Check if Player falls of map
        if (col.gameObject.name.Equals("LevelBorder"))
        {
            //GAME IS OVER
            LifeSystem.TakeLife();
            Respawn();

        }
        if (col.gameObject.name.Equals("ExitPortal"))
        {
            SaveRubiesPlayer();
        }


    }

    //----------------Player functions--------------
    private void Run()
    {
        if (!IsDead)
        {
            dirX = CrossPlatformInputManager.GetAxis("Horizontal");
            //StepSound.Play();

        }

        //Running Animation
        if (Mathf.Abs(dirX) > 0 || Mathf.Abs(dirX) > 0)
        {
            if (!IsDead)
            {
                anim.SetBool("IsRunning", true);
                isMoving = true;
            }
            //StepSound.Play();
        }
        else
        {
            anim.SetBool("IsRunning", false);
            isMoving = false;
            //StepSound.Stop();
        }
        if (isMoving && rb.velocity.y == 0)
        {
            if (!StepSound.isPlaying)
            {
                StepSound.Play();
            }
        }
        else
        {
            StepSound.Stop();
        }

    }
    private void CheckAttack()
    {
        if (attackcooldowntimer > 0)
        {
            attackcooldowntimer -= Time.deltaTime;
        }
        if (attackcooldowntimer < 0)
        {
            attackcooldowntimer = 0;
        }
        Attack();
    }
    private void Attack()
    {
        //Attack Animation
        if (CrossPlatformInputManager.GetButtonDown("Attack") && attackcooldowntimer == 0)
        {
            //If jumping
            
            attackcooldowntimer = attackcooldown;
            anim.SetBool("IsAttacking", true);
            Invoke("PlayAttackSound", 0.25f);


        }
        else
        {
            anim.SetBool("IsAttacking", false);
        }

    }
    private void PlayAttackSound()
    {
        AttackSound.Play();
    }
    private void Jump()
    {
        //hangtime
        if(rb.velocity.y==0)
        {
            hangCounter = hangtime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }
        //Jump Character
       
        if (CrossPlatformInputManager.GetButtonDown("Jump") && !IsDead)
        {
            
            //JumpSound.Play();
            //if on ground
            if (rb.velocity.y == 0 || OnPlatform)
            {
                if(hangCounter > 0f)
                {
                    Debug.Log("SINGLE JUMP");
                    Debug.Log(rb.velocity.y);
                    CandoubleJump = true;
                    JumpSound.Play();
                    //rb.AddForce(Vector2.up * jumpforce);
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.velocity += Vector2.up * jumpforce;
                    Instantiate(dustCloud, transform.position, Quaternion.identity);
                }
                
            }
            else
            {
                if (CandoubleJump)
                {
                    Debug.Log("DOUBLE JUMP");
                    Debug.Log(rb.velocity.y);
                    anim.SetBool("IsDoubleJumping", true);
                    CandoubleJump = false;
                    JumpSound.Play();
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.velocity += Vector2.up * jumpforce*1.25f;
                    //rb.AddForce(Vector2.up * jumpforce);
                    Instantiate(dustCloud, transform.position, Quaternion.identity);
                    //add screen shake
                    StartCoroutine(cameraShake.Shake(0.125f, 1f));
                    gameObject.GetComponent<TrailRenderer>().enabled = true;
                    //CandoubleJump = false;
                }
            }

        }
        //Jump Animation
        if (rb.velocity.y == 0 || OnPlatform)
        {
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsFalling", false);
            anim.SetBool("IsDoubleJumping", false);
        }
        if (rb.velocity.y > 1 && !OnPlatform)
        {
            anim.SetBool("IsJumping", true);
        }
        if (rb.velocity.y < -1.25 && !OnPlatform)
        {
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsFalling", true);
        }
    }
    private void CheckThrow()
    {
        if (throwcooldowntimer > 0)
        {
            throwcooldowntimer -= Time.deltaTime;
        }
        if (throwcooldowntimer < 0)
        {
            throwcooldowntimer = 0;
        }
        Throw();
    }
    private void Throw()
    {
        //Throw Animation
        if (CrossPlatformInputManager.GetButtonDown("Throw") && throwcooldowntimer == 0)
        {
            if (!KunaiFinished)
            {
                throwcooldowntimer = throwcooldown;
                anim.SetBool("IsThrowing", true);
                if (!KunaiFinished)
                {
                    var firedkunai = Instantiate(kunai, barrel.position, Quaternion.identity);
                    firedkunai.AddForce(barrel.up * throwspeed);
                    KunaiSystem.TakeLife();
                    ThrowSound.Play();
                }
            }
        }
        else
        {
            anim.SetBool("IsThrowing", false);
        }
    }
    private void Dash()
    {
        //Dask move
        if (CrossPlatformInputManager.GetButtonDown("Dash") && !IsDead)
        {
            if (Time.time >= (lastdash + dashcooldown))
                AttemptToDash();


        }
        if (IsDashing)
        {
            anim.SetBool("IsDashing", true);
        }
        else
        {

            anim.SetBool("IsDashing", false);
        }
    }
    private void AttemptToDash()
    {
        IsDashing = true;
        DashSound.Play();
        dashtimeleft = dashtime;
        lastdash = Time.time;

        AfterImagePool.Instance.GetFromPool();
        lastiamgexpos = transform.position.x;

    }
    private void CheckDash()
    {
        if (IsDashing)
        {
            if (dashtimeleft > 0)
            {
                //rb.velocity = new Vector2(dashspeed * dirX, rb.velocity.y);

                if (facingRight)
                {

                    StartCoroutine(Noise(2f, 2f, dashtimeleft));
                    //cameraShake.ShakeCamera();
                    float x = rb.velocity.x;
                    rb.AddForce(Vector2.right * DashSpeed, ForceMode2D.Force);
                    rb.velocity = Vector2.zero;
                    rb.velocity += new Vector2(x, 0).normalized * DashSpeed;
                }
                else
                {

                    //cameraShake.ShakeCamera();
                    StartCoroutine(Noise(2f, 2f, dashtimeleft));
                    float x = rb.velocity.x;
                    rb.AddForce(Vector2.left * DashSpeed, ForceMode2D.Force);
                    rb.velocity = Vector2.zero;
                    rb.velocity += new Vector2(x, 0).normalized * DashSpeed;

                }
                dashtimeleft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastiamgexpos) > distancebetweenimages)
                {
                    AfterImagePool.Instance.GetFromPool();
                    lastiamgexpos = transform.position.x;
                }
            }
            if (dashtimeleft <= 0)
            {
                IsDashing = false;

            }
        }

    }
    public IEnumerator Respawn()
    {
        // DISSOLVE EFFECT
        fade -= Time.deltaTime / 2f;
        if (fade <= 0f)
        {
            fade = 0f;
        }
        material.SetFloat("_Fade", fade);

        yield return new WaitForSeconds(2f);

        if (!respawnonce)
        {

            Debug.Log("Respawned man wtf");
            IsDead = false;
            takelife = false;
            anim.SetTrigger("IsIdle");
            HealthBarScript.health = 100f;
            healthAmount = 100f;
            Physics2D.IgnoreLayerCollision(8, 9, false);
            fade = 1f;
            material.SetFloat("_Fade", fade);
            transform.position = spawnPoint.position;

        }
        respawnonce = true;



    }
    private void PlayerDeath()
    {
        //
        //check if player dead
        if (HealthBarScript.health <= 0)
        {
            //GAME OVER
            if (!takelife)
            {
                //Debug.Log("IS DEAD");
                //check if respawn is not called multiple times
                IsDead = true;
                respawnonce = false;
                anim.SetTrigger("IsDead");
                LifeSystem.TakeLife();
                DeathMusic.Play();
                DeathSound.Play();

                Physics2D.IgnoreLayerCollision(8, 9, true);
            }

            takelife = true;
            if (!GameOver)
            {
                //Invoke("Respawn", 1f);
                StartCoroutine(Respawn());
            }
            //Respawn();

        }
    }

    public void SaveRubiesPlayer()
    {
        SaveSystem.SaveRubies();
    }

    IEnumerator Boost(float boostDur) //Coroutine with a single input of a float called boostDur, which we can feed a number when calling
    {
        float time = 0; //create float to store the time this coroutine is operating
        canBoost = false; //set canBoost to false so that we can't keep boosting while boosting

        while (boostDur > time) //we call this loop every frame while our custom boostDuration is a higher value than the "time" variable in this coroutine
        {
            time += Time.deltaTime; //Increase our "time" variable by the amount of time that it has been since the last update
            if (facingRight)
            {
                rb.AddForce(Vector2.right * DashSpeed, ForceMode2D.Force);

            }
            else
            {
                rb.AddForce(Vector2.left * DashSpeed, ForceMode2D.Force);

            }
            yield return 0; //go to next frame
        }
        yield return new WaitForSeconds(boostCooldown); //Cooldown time for being able to boost again, if you'd like.
        canBoost = true; //set back to true so that we can boost again.
    }

}
