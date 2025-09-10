<<<<<<< HEAD
using UnityEngine;
=======
﻿using UnityEngine;
>>>>>>> c205ac635a9db72025274cd06e36e72980917165

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumForce = 5f;
<<<<<<< HEAD
    [SerializeField] private float horizontal;
    [SerializeField] private int direction = 1;
    [SerializeField] private bool isGround = true;
    private Rigidbody2D rb;
    public Animation anim;
    private SpriteRenderer rbSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animation>();
        rbSprite = GetComponent<SpriteRenderer>();
=======
    [SerializeField] private float boundForce = 5f;
    [SerializeField] private float horizontal;

    [SerializeField] private int doubleJump = 2;
    [SerializeField] private bool isGround = true;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer rbSprite;
    private AudioSource ado;
    public AudioClip jumpSound;
    public AudioClip walkSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ado = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rbSprite = GetComponent<SpriteRenderer>();

>>>>>>> c205ac635a9db72025274cd06e36e72980917165
    }

    private void FixedUpdate()
    {
<<<<<<< HEAD
=======

>>>>>>> c205ac635a9db72025274cd06e36e72980917165
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }
    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
=======
        Fall();
>>>>>>> c205ac635a9db72025274cd06e36e72980917165
        flip();
        Jump();
    }
    void flip()
    {
        if(horizontal > 0)
        {
<<<<<<< HEAD
            rbSprite.flipX = false;
        }
        if(horizontal < 0)
        {
            rbSprite.flipX = true;
        }
    }
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.AddForce(new Vector2(0f, jumForce), ForceMode2D.Impulse);
            Debug.Log("[Player] Jump");
=======
            anim.SetBool("Run", true);
            rbSprite.flipX = false;
        }
        else if(horizontal < 0)
        {
            anim.SetBool("Run", true);
            rbSprite.flipX = true;
        }else
        {
            anim.SetBool("Run", false);
            PlaySound(walkSound);
        }
    }

    void Fall()
    {
        if(rb.linearVelocityY < 0 && anim.GetBool("Jump"))
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Fall", true);
        }
    }
    public void OnTheGround()
    {
        Debug.Log("Groudn");
        anim.SetBool("Fall", false);
        anim.SetTrigger("Idle");
        doubleJump = 2;
        isGround = true;

    }
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround && doubleJump > 0)
        {
            doubleJump -= 1;
            if (doubleJump <= 0)
            {
                isGround = false;
            }
            anim.SetBool("Jump", true);
            rb.AddForce(new Vector2(0f, jumForce), ForceMode2D.Impulse);
            Debug.Log("[Player] Jump");
            PlaySound(jumpSound);
>>>>>>> c205ac635a9db72025274cd06e36e72980917165
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
<<<<<<< HEAD
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach(ContactPoint2D contact in  collision.contacts)
            {
                Debug.Log("[Player] Ground");
                if (contact.normal.y > 0.5f)
                {
                    Debug.Log("[Player] On the Ground");
                    isGround = true;
=======
        if (collision.gameObject.CompareTag("Platform"))
        {
            foreach(ContactPoint2D contact in  collision.contacts)
            {
                if (contact.normal.y >= 0.5f)
                {
                    OnTheGround();
                    break;

>>>>>>> c205ac635a9db72025274cd06e36e72980917165
                }
            }
            
        }
<<<<<<< HEAD
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
            Debug.Log("[isGroud(Player)] = " + isGround);
        }
=======

        if (collision.gameObject.CompareTag("Enemy"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y >= 0.5f)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocityX, boundForce);
                    break;
                }
            }
        }
        if (collision.gameObject.CompareTag("Trampoline"))
        {
            foreach(ContactPoint2D contact in collision.contacts)
            {
                if(contact.normal.y >= 0.5f)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocityX, boundForce * 1.75f );
                    break;
                }
            }
        }

    }
    private void PlaySound(AudioClip clip)
    {
        ado.clip = clip;
        ado.Play();
>>>>>>> c205ac635a9db72025274cd06e36e72980917165
    }
}
