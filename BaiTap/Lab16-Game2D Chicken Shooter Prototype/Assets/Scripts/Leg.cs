using UnityEngine;

public class Leg : MonoBehaviour
{
    public int point = 10;
    int bounceCount = 0;
    public Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bouncing();
        Destroy(gameObject, 12f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void bouncing()
    {
        float Direction = Random.value < 0.5f ? -1f : 1f;
        float spenSpeed = Random.Range(200f, 400f);
        Vector2 force = new Vector2(Direction * Random.Range(0.5f, 1.5f), Random.Range(1.5f, 3f));
        rb.AddForce(force, ForceMode2D.Impulse);
        rb.angularVelocity = spenSpeed * Direction;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            bounceCount++;
            if (bounceCount >= 3)
            {
                rb.linearVelocity = Vector2.zero;
                rb.gravityScale = 0f;
                rb.freezeRotation = true;
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.instance.GetPoint(point);
            Destroy(gameObject);
        }
    }
    
}
