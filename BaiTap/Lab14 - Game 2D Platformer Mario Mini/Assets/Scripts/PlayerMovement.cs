using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumForce = 5f;
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

    }

    private void FixedUpdate()
    {

        horizontal = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }
    // Update is called once per frame
    void Update()
    {
        Fall();
        flip();
        Jump();
    }
    void flip()
    {
        if(horizontal > 0)
        {
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
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            foreach(ContactPoint2D contact in  collision.contacts)
            {
                if (contact.normal.y >= 0.5f)
                {
                    OnTheGround();
                    break;

                }
            }
            
        }

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
    }
}
