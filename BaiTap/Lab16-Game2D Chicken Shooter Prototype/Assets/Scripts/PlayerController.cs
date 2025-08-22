using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField]private float h;
    [SerializeField]private float v;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal");

        v = Input.GetAxis("Vertical");
        Move();
    }
    // Update is called once per frame
    void Update()
    {
       
    }
    void Move()
    {
        rb.linearVelocity = new Vector2(speed * h, speed * v);
    }

}
