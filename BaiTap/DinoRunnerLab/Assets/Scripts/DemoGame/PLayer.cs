using UnityEngine;

public class PLayer : MonoBehaviour
{
    public float speed = 5f;
    public float JumpForce = 10f;
    private Rigidbody2D rb;
    public float h;
    public bool isGround = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(h * speed, rb.linearVelocity.y);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            isGround = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Flatform"))
        {
            isGround = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            foreach (ContactPoint2D contacts in collision.contacts)
            {
                if (contacts.normal.y > 0.5f)
                {
                    Debug.Log("Tiêu diệt enemy");

                    Destroy(collision.gameObject);
                    break;
                }
            }
        }

    }
}
