<<<<<<< HEAD
=======
﻿
>>>>>>> c205ac635a9db72025274cd06e36e72980917165
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
<<<<<<< HEAD
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
=======
    private Transform target;
    public float kc = 2f;
    public float speed = 5f;
    private int direction = 1;

    private float leftBound;
    private float rightBound;
    private float topBound;
    private float bottomBound;

    public bool falling_Platform = false;
    public bool moving_Platform = false;
    public bool moveLR = false;
    public bool moveUD = false;
    private bool canMove = false;
    public bool Notphysic = true; //Biến kiểm soát vật lý của platform
    private Rigidbody2D rb;
    private BoxCollider2D box;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = transform;
        box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        if (Notphysic)
        {
            rb.bodyType = RigidbodyType2D.Kinematic; //platform ko thể bị tác động vật lý
        }

        leftBound = transform.position.x - kc;
        rightBound = transform.position.x + kc;
        topBound = transform.position.y + kc;
        bottomBound = transform.position.y - kc;

        Invoke("CanMove", Random.Range(0.1f,1f));
>>>>>>> c205ac635a9db72025274cd06e36e72980917165
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        
=======
        if (canMove)
        {
            MoveLR();
            MoveUD();
        }
    }
    void CanMove()
    {
        canMove = true;
    }
    void MoveUD()
    {
        if(moveUD)
        {
            transform.position += new Vector3(0f, direction * speed * Time.deltaTime, 0f);
            if(transform.position.y >= topBound)
            {
                direction = -1;
            }else if(transform.position.y <= bottomBound)
            {
                direction = 1;
            }
        }
    }
    void MoveLR()
    {
        if (moveLR)
        {
            transform.position += new Vector3(direction * speed * Time.deltaTime, 0f, 0f);
            if(transform.position.x >= rightBound)
            {
                direction = -1;
            }else if(transform.position.x <= leftBound)
            {
                direction = 1;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (falling_Platform)
            {
                rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            }
        }
>>>>>>> c205ac635a9db72025274cd06e36e72980917165
    }
}
