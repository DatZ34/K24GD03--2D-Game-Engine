using UnityEngine;

public class DinoController : MonoBehaviour
{
    public AudioClip soundJump;
    public AudioClip soundCoin;
    public AudioClip soundDead;
    private AudioSource audioSource;
    public bool isAlive = true;
    public bool isGrounded = true;

    public float speed = 2f;
    public float jumpforce = 10f;
    public int score = 0;
    private Rigidbody2D rb;
    public Animator anim;
    
    private GameManager gameManager;
    private ScoreManager scoreManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gameManager = FindFirstObjectByType<GameManager>();
        scoreManager = FindFirstObjectByType<ScoreManager>();
        audioSource = GetComponent<AudioSource>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Jump();
        }
    }
    void Jump()
    {
        if(isGrounded && Input.GetKey(KeyCode.Space))
        {
            PlaySound(soundJump);
            rb.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Impulse);
            isGrounded = false;
            Debug.Log("isGround: " + isGrounded);
            anim.SetBool("isJump", true);
            anim.SetBool("isRun", false);
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("isGround: " + isGrounded);
            if (anim.GetBool("isJump"))
            {
                anim.SetBool("isJump", false);
                anim.SetBool("isRun", true);
            }
        }
        if (collision.gameObject.CompareTag("Tree"))
        {
            PlaySound(soundDead);
            isAlive = false;
            anim.SetTrigger("dead");
            Debug.Log("Player Die");
            scoreManager.GetScore(score);
            gameManager.GameLose();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            PlaySound(soundCoin);
            score++;
            scoreManager.GetScore(score);
            Destroy(collision.gameObject);
        }
    }
    public void PlaySound(AudioClip sound)
    {
        audioSource.clip = sound;
        audioSource.Play();
    }
}
