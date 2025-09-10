using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI score_txt;
    public TextMeshProUGUI scorePanelLose_txt;
    public TextMeshProUGUI Speed_txt;
    public TextMeshProUGUI time_txt;
    public TextMeshProUGUI timePlay_txt;
    public TextMeshProUGUI timeLose_txt;
    private DinoController dinor;
    private Spawner spawner;
    private DateTime CurrentTimeS;
    public bool startgame = false;
    public bool firstStart = true;
    public float StartTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        dinor = FindFirstObjectByType<DinoController>();
        spawner = FindFirstObjectByType<Spawner>();
    }
    private void Start()
    {
        CurrentTimeS = DateTime.Now;
        score_txt.text = "Coin: " + dinor.score;
        Speed_txt.text = "SpeedTree: " + spawner.SpeedTree;
    }

    // Update is called once per frame
    void Update()
    {


        TimeSpan s = DateTime.Now - CurrentTimeS;
        int minus = s.Minutes;
        int secon = s.Seconds;

        time_txt.text = string.Format("Time {0:00}:{1:00}", minus, secon);
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
        if (startgame)
        {
            startTimePlay();
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
    void startTimePlay()
    {

        float time = Time.time - StartTime;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int miniSeconds = Mathf.FloorToInt((time * 100 )% 100);
        timePlay_txt.text = String.Format("Play Time: {0:00}:{1:00}:{2:00}",minutes,seconds,miniSeconds);
        timeLose_txt.text = String.Format("Play Time: {0:00}:{1:00}:{2:00}",minutes,seconds,miniSeconds);
    }

}
