using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] protected float speed = 5;

    [Header("Component Add")]
    [SerializeField] protected Rigidbody2D rb;

    [Header("Dynamic Information")]
    [SerializeField] protected float horizontal;
    [SerializeField] protected float vertical;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
    }
    protected virtual void Move()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        // Chuyển input sang hướng isometric
        Vector2 isoMovement = new Vector2(
            input.x - input.y,       // X hướng isometric
            (input.x + input.y) / 2  // Y hướng isometric
        );
        rb.MovePosition(rb.position + isoMovement * speed * Time.fixedDeltaTime);

    }
}
