using UnityEngine;

public class Egg : MonoBehaviour
{
    public Animator anim;
    public float timeDestroy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, timeDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Va chạm với: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("platform"))
        {
            Debug.Log("chạm đất");
            anim.SetTrigger("isGround");

        }
    }
}
