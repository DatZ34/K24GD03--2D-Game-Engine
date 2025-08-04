using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI score_txt;
    public TextMeshProUGUI scorePanelLose_txt;
    public TextMeshProUGUI Speed_txt;

    private DinoController dinor;
    private Spawner spawner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        dinor = FindFirstObjectByType<DinoController>();
        spawner = FindFirstObjectByType<Spawner>();
    }
    private void Start()
    {
        score_txt.text = "Coin: " + dinor.score;
        Speed_txt.text = "SpeedTree: " + spawner.SpeedTree;
    }

    // Update is called once per frame
    void Update()
    {
        if(dinor != null && dinor.score > 10 && dinor.score < 20)
        {
            spawner.repeatingTimeTree = 4f;
            spawner.countDownSpawnTree = 7f;
            spawner.SpeedTree = 10;
            Speed_txt.text = "SpeedTree: " + spawner.SpeedTree;

        }
        else if(dinor != null && dinor.score >= 20)
        {
            spawner.repeatingTimeTree = 2f;
            spawner.countDownSpawnTree = 5f;
            spawner.SpeedTree = 15;
            Speed_txt.text = "SpeedTree: " + spawner.SpeedTree;

        }else
        {
            spawner.repeatingTimeTree = 5f;
            spawner.countDownSpawnTree = 10f;
            spawner.SpeedTree = 5;
            Speed_txt.text = "SpeedTree: " + spawner.SpeedTree;
        }
    }
    public void GetScore(int score)
    {
        
        score_txt.text = "Coin: " + score;
        scorePanelLose_txt.text = "Score: " + score;
    }
    public void ResetScore()
    {
        dinor.score = 0;
        
        score_txt.text = "Coin: " + dinor.score;
        Speed_txt.text = "Speed: " + spawner.SpeedTree;

        
    }
}
