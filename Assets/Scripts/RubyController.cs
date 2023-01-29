//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;


public class RubyController : MonoBehaviour
{
    
    float horizontal;
    float vertical;
    private Rigidbody2D rb2d;
     
    public int maxHealth = 10;
    int currentHealth = 10;

    public float maxSpeed = 5.0f;
    float currentSpeed = 3.0f;

    public int Health
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public float Speed
    {
        get { return currentSpeed; }
        set { currentSpeed = value; }
    }

    public float timeInvincible = 2.0f;

    bool isInvincible;

    float invincibleTimer;

    Vector2 lookDirection = new Vector2(1, 0);
    Animator animator;

    public GameObject cogBullet;

    AudioSource audioSource;

    public AudioClip audioHit;

    public AudioClip audioThrowCog;

    Vector2 move;

    public PlayerInputAction playerInputAction;

    Vector2 currenInput;

    public Joystick joystick;

    // Start is called before the first frame update
    void Start()
   {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();


        playerInputAction.Player.Movement.performed += onMovement;
        playerInputAction.Player.Movement.canceled += onMovement;
    }

    private void onMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currenInput = context.ReadValue<Vector2>();
        }

        if (context.canceled)
        {
            currenInput = Vector2.zero;
        }
    }

    // Update is called once per frame
    void Update()
   {
        //horizontal = Input.GetAxis("Horizontal");
        //vertical = Input.GetAxis("Vertical");
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;

        if (isInvincible) 
        {
            invincibleTimer -= Time.deltaTime;
           
            if (invincibleTimer < 0) 
            {
                isInvincible = false;
            }
        }


        move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Launch();
        //}

    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
   {
        Vector2 position = transform.position;
        position.x = position.x + currentSpeed * horizontal * Time.deltaTime;
        position.y = position.y + currentSpeed * vertical * Time.deltaTime;
        rb2d.MovePosition(position);

        Vector2 movejoystick = new Vector2(currenInput.x, currenInput.y);
        if (!Mathf.Approximately(movejoystick.x, 0.0f) || !Mathf.Approximately(movejoystick.y, 0.0f))
        {
            lookDirection.Set(movejoystick.x, movejoystick.y);
            lookDirection.Normalize();
        }

    }


   // Change Health
   public void ChangeHealth(int amount)
   {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if(isInvincible)
            {
                return;
            }
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        if (amount < 0) 
        {
            PlaySound(audioHit);
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        //Debug.Log(currentHealth + "/" + maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
   }
    
    void Launch()
    {
        GameObject cogBulletObject = Instantiate(cogBullet, rb2d.position + Vector2.up * 0.5f, Quaternion.identity);
        CogBulletController cogBulletController = cogBulletObject.GetComponent<CogBulletController>();
        cogBulletController.Launch(lookDirection, 300f, transform);

        animator.SetTrigger("Launch");
        PlaySound(audioThrowCog);
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void ThrowCogButton()
    {
        Launch();
    }

    public Vector2 GetPosition()
    {
        return rb2d.position;
    }

   //public void ChangeSpeed(float amount)
   //{
   //     currentSpeed = Mathf.Clamp(currentSpeed + amount, 0, maxSpeed);
   //     Debug.Log(currentSpeed + "/" + maxSpeed);
   //}
}
