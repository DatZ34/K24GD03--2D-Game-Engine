using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    
    public GameObject PanelLose;
    public GameObject PanelWin;
    public TextMeshProUGUI score_Txt;
    public TextMeshProUGUI scoreLose_Txt;
    public TextMeshProUGUI scoreWin_Txt;
    private int score_Num = 0;

    private Animator anim;
    public AudioClip deadSound;
    private AudioSource ado;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ado = GetComponent<AudioSource>();
        ado.clip = deadSound;
        anim = GetComponent<Animator>();
        score_Txt.text = "Score: " + score_Num;
        scoreLose_Txt.text = "Score: " + score_Num;
        scoreWin_Txt.text = "Score: " + score_Num;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            score_Num += 10;
            score_Txt.text = "Score: " + score_Num;
            scoreLose_Txt.text = "Score: " + score_Num;
            scoreWin_Txt.text = "Score: " + score_Num;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            foreach(ContactPoint2D contact in collision.contacts)
            {
                

                if(contact.normal.y >= 0.5)
                {
                    score_Num += 10;
                    score_Txt.text = "Score: " + score_Num;
                    scoreLose_Txt.text = "Score: " + score_Num;
                    scoreWin_Txt.text = "Score: " + score_Num;

                    break;
                }
                else
                {
                    HitAnim();
                    Invoke("GameLose", 0.3f);
                }
            }
        }
        if (collision.gameObject.CompareTag("Spikes"))
        {
            HitAnim();
            Invoke("GameLose", 0.3f);

        }
        if (collision.gameObject.CompareTag("Cup"))
        {
            PanelWin.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    private void HitAnim()
    {
        ado.Play();
        anim.SetTrigger("Hit");
    }
    private void GameLose()
    {
        Time.timeScale = 0f;
        PanelLose.SetActive(true);
        Destroy(gameObject);
        score_Num = 0;
    }
}
