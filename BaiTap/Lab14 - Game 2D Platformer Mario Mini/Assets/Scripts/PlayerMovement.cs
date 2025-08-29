using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumForce = 5f;
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
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }
    // Update is called once per frame
    void Update()
    {
        flip();
        Jump();
    }
    void flip()
    {
        if(horizontal > 0)
        {
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
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach(ContactPoint2D contact in  collision.contacts)
            {
                Debug.Log("[Player] Ground");
                if (contact.normal.y > 0.5f)
                {
                    Debug.Log("[Player] On the Ground");
                    isGround = true;
                }
            }
            
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
            Debug.Log("[isGroud(Player)] = " + isGround);
        }
    }
}
