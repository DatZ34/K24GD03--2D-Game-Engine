using UnityEngine;

public class PlatFormControll : MonoBehaviour
{
    public float kc = 2f;
    public float speed = 5f;
    private int direction = 1;
    private float leftBound;
    private float rightBound;
    private float upBound;
    private float downBound;
    public float StartDelays = 0f;
    public bool canMove = false;
    private Rigidbody2D rb;
    public bool Moverl = false;
    public bool Moveud = false;
    public bool CanNotPhysics = false;
    //public Transform target;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!CanNotPhysics)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        leftBound = transform.position.x - kc;
        rightBound = transform.position.x + kc;
        downBound = transform.position.y - kc;
        upBound = transform.position.y + kc;
        Invoke("EnableMovement", StartDelays);
    }

    void Update()
    {
        if (canMove)
        {
            MoveLR();
            MoveUD();
        }
        //moveLR2();
    }
    void EnableMovement()
    {
        canMove = true;
    }

    void MoveLR()
    {
        if (Moverl)
        {
            transform.position += new Vector3(direction * speed * Time.deltaTime, 0f, 0f);

            if (transform.position.x >= rightBound)
            {
                direction = -1;
            }
            else if (transform.position.x <= leftBound)
            {
                direction = 1;
            }
        }
    }
    void MoveUD()
    {
        if (Moveud)
        {
            transform.position += new Vector3(0f, direction * speed * Time.deltaTime, 0f);
            if(transform.position.y >= upBound)
            {
                direction = -1;
            }
            else if(transform.position.y <= downBound)
            {
                direction = 1;
            }    
        }
    }
    //public void moveLR2()
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    //    var distanceToTarget = Vector3.Distance(transform.position, target.position);
    //    Debug.Log("DistanveTOTarget : = " + distanceToTarget);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D contacts in collision.contacts)
            {
                if (contacts.normal.y < -0.5f) 
                {
                    Debug.Log("Nhảy lên platform");
                    collision.transform.SetParent(transform);
                    break;
                }
            }
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
