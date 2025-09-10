<<<<<<< HEAD
=======
﻿using System.Collections;
>>>>>>> c205ac635a9db72025274cd06e36e72980917165
using UnityEngine;

public class Enemy : MonoBehaviour
{
<<<<<<< HEAD
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
=======
    public bool PigEnemy = false;
    public bool TurtleEnemy = false;

    public float speed = 2f;
    public float kc = 2f;

    private int direction = 1;
    private Transform originalPosition;
    private bool canMove = false;
    private float leftBound;
    private float rightBound;
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer spRder;

    private bool isWaiting = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition = transform;
        spRder = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        leftBound = transform.position.x - kc;
        rightBound = transform.position.x + kc; 
        Invoke("CanMove", Random.Range(0.1f, 1f));
        InvokeRepeating("PigRunning", 5f, Random.Range(4f, 8f));

>>>>>>> c205ac635a9db72025274cd06e36e72980917165
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        
    }
=======
        if (canMove)
        {
            MoveLR();
        }
    }
    void CanMove()
    {
        canMove = true;
    }
    void MoveLR()
    {
        if(isWaiting) { return; }
        transform.position += new Vector3(speed * Time.deltaTime * direction, 0f, 0f);
        if(transform.position.x >= rightBound)
        {
            StartCoroutine(Waiting());
            spRder.flipX = true;

            direction = -1;
        }else if(transform.position.x <= leftBound)
        {
            StartCoroutine(Waiting());
            spRder.flipX = false;
            direction = 1;
        }
        else
        {
            if (PigEnemy)
            {
                if (!anim.GetBool("IsRun"))
                {
                    anim.SetBool("IsWalk", true);
                }
            }
        }
    }
    IEnumerator Waiting()
    {
        if (PigEnemy)
        {
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsRun",false);
            anim.SetTrigger("Idle");
            isWaiting = true;

            yield return new WaitForSeconds(1.2f);
            speed = 2f;
            isWaiting = false;
        }
    }

    void PigRunning()
    {
        if (PigEnemy)
        {
            speed = 5f;
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsRun", true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("BỊ player đạp");
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Debug.Log("Xét Contact y =" + contact.normal.y);
                if (contact.normal.y <= 0.5f)
                {
                    StartCoroutine(PlayDeathAnim());
                    
                    break;
                }
            }
        }
    }
    IEnumerator PlayDeathAnim()
    {
        Debug.Log("PlayDeathAnim called");

        if (PigEnemy)
        {
            Debug.Log("Triggering LastHit");
            anim.SetTrigger("LastHit");
        }

        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

>>>>>>> c205ac635a9db72025274cd06e36e72980917165
}
