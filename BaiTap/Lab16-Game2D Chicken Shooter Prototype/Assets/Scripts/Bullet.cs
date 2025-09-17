using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Thông số")]
    [SerializeField] protected float speed;
    [SerializeField] public int dame;

    [Header("Thành phần Unity gắn kèm")]
    [SerializeField] protected Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {

        rb.linearVelocity = transform.rotation * Vector2.up * speed;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //transform.position += Vector3.up * speed * Time.deltaTime;
        //transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("BackGround") || collision.gameObject.CompareTag("Enemy"))
        {
            PlayerController.instance.energyShoot++;
            Destroy(gameObject);
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
