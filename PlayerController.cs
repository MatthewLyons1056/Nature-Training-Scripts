using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRb;
    public float jumpForce = 10;
    public float gravityModifier;
    public float gravityModifierOriginal;
    public bool isOnGround = true;
    public bool isAirJumping = false;
    public bool gameOver;

    //Set up particle system
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public ParticleSystem fireWorksParticle;

    //declare sound

    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    //set up player controller

    private Animator playerAnim;

    

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        //gravity logic
        

        Physics.gravity = new Vector3(0, gravityModifier * -9.81f, 0);//sets value of gravity
        
    }

   
    void Update()
    {
        //Make player jump when space bar is pressed

        
        if (gameOver == true)
        {
            dirtParticle.Stop();
            
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver) //(&& ! gameOver) == (&& gameOver = false)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            dirtParticle.Stop();
            isOnGround = false;
            isAirJumping = true;
            playerAnim.SetTrigger("Jump_trig");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isAirJumping && !gameOver) //(&& ! gameOver) == (&& gameOver = false)
        {
            playerRb.velocity = Vector3.zero;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAudio.PlayOneShot(jumpSound, 1.0f);//new jump sound?
            fireWorksParticle.Play();
            isAirJumping = false;
            playerAnim.SetTrigger("Jump_trig");
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            playerAudio.PlayOneShot(crashSound, 1.0f);
            explosionParticle.Play();
            dirtParticle.Stop();
        }

    }
}
