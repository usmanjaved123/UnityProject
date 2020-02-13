using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class EnemyRanged : MonoBehaviour
{
    [SerializeField]
    private GameObject ruby, fatalitytext;
    public CameraShake cameraShake;
    public bool IsArmored;
    //camera
    private CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin noise;

    public GameObject blood;
    public GameObject HitEffect;
    //SOUNDS
    public AudioSource[] sounds;
    public AudioSource AttackSound;
    public AudioSource DieSound;
    public float healthAmount;
    bool EnemyDead = false;

    public Rigidbody2D rb;
    Vector3 localScale;
    Animator anim;

    public float shakeDuration = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        //camerasettings
        vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        //enemy settings
        localScale = transform.localScale;
        rb = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sounds = GetComponents<AudioSource>();

        AttackSound = sounds[0];
        DieSound = sounds[1];
        healthAmount = 100;

    }

    // Update is called once per frame
    void Update()
    {
       

    }
    public IEnumerator Noise(float amplitudeGain, float frequencyGain, float ShakeDuration)
    {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;
        yield return new WaitForSeconds(shakeDuration);
        noise.m_AmplitudeGain = 0f;
    }
    void EnableLayerCollision()
    {
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //Check if enemy is attacking
        if (col.gameObject.name.Equals("PlayerHand"))
        {

            //Show Hit effect
            Instantiate(HitEffect, transform.position, transform.rotation);


            healthAmount -= Player.AttackDamage;
            //add screen shake
            //StartCoroutine(cameraShake.Shake(0.1f, 1f));
            StartCoroutine(Noise(2f, 2f, 1f));
          
            //initiate blood
            if (healthAmount <= 0)
            {
                //Player death sound

                anim.Play("DeadRangedAnimation");
                DieSound.Play();
                healthAmount = 0;
                Physics2D.IgnoreLayerCollision(8, 9, true);

                //Show Blood
                Instantiate(blood, transform.position, Quaternion.identity);

                //Drop ruby
                Instantiate(ruby, transform.position, Quaternion.identity);

                //add screen shake
                //StartCoroutine(cameraShake.Shake(0.125f, 1f));
                StartCoroutine(Noise(2f, 2f, 1f));

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
        Instantiate(fatalitytext, transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity);

        //Drop ruby
        Instantiate(ruby, transform.position, Quaternion.identity);

        //add screen shake
        //StartCoroutine(cameraShake.Shake(0.125f, 1f));
        StartCoroutine(Noise(2f, 2f, 1f));
    }
    private void PlayAttackSound()
    {
        if (!AttackSound.isPlaying)
        {
            AttackSound.Play();
        }

    }
}
