using UnityEngine;

public class Preprocessor : MonoBehaviour
{
    public float speed = 2;
    public float h;
    public float v;
    public Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PcMove();

    }
    void PcMove()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        Vector3 inputDirection = new Vector3(h, 0f, v);
        Vector3 isoDirection = Quaternion.Euler(0, 45, 0) * inputDirection;
        transform.Translate(isoDirection * speed * Time.deltaTime,Space.World);
    }

}
