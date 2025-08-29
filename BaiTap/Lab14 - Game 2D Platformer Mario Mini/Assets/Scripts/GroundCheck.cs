using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private PlayerMovement p;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        p = FindFirstObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach(ContactPoint2D contact in collision.contacts)
            {
                if(contact.normal.y <= 0.5f)
                {
                    p.OnTheGround(); break;
                }
            }
        }
    }
}
