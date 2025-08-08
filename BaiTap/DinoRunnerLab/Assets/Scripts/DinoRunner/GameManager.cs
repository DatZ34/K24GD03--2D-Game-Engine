using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject panelStart;
    public GameObject panelLose;
    public GameObject panelGamePlay;

    private AudioSource audioSource;
    private Spawner spawner;
    private DinoController dino;
    private ScoreManager scoreManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawner = FindFirstObjectByType<Spawner>();
        dino = FindFirstObjectByType<DinoController>();
        scoreManager = FindFirstObjectByType<ScoreManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playGame() // gán nút
    {
        scoreManager.startgame = true;
        panelStart.SetActive(false);
        panelGamePlay.SetActive(true);
        dino.anim.SetBool("isRun", true);
        spawner.StartInVoke();
        audioSource.Play();
    }
    public void GameLose()
    {
        scoreManager.startgame = false;

        scoreManager.StartTime = Time.time;
        panelLose.SetActive(true);
        panelGamePlay.SetActive(false);
        DestroyAllChild(spawner.containerCoin);
        DestroyAllChild(spawner.containerTree);
        spawner.StopInVoke();
        Time.timeScale = 0;

    }
    public void RetryGame() // gán nút
    {
        scoreManager.startgame = true;
        dino.isAlive = true;
        Time.timeScale = 1;
        panelGamePlay.SetActive(true);
        panelLose.SetActive(false);
        dino.anim.SetTrigger("idle");
        spawner.StartInVoke();
        scoreManager.ResetScore();
    }
    void DestroyAllChild(Transform parent)
    {
        foreach(Transform child in parent)
        {
            if (child != null)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
