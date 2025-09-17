using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("mobile UI")]
    public GameObject panelUIMobile;


    [Header("")]
    public GameObject panelLose;
    public GameObject panelWin;

    public GameObject playerShipPrefab;
    public Slider energy_Slider;
    public TextMeshProUGUI point_txt;

    public float valueSlider;
    public float currentValueSlider;

    private bool isGameEnded = false;

    [Header("Script gắn thêm")]
    public EnemySpawner spawnerControler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
#if UNITY_ANDROID
        Debug.Log("Android");
#elif UNITY_IOS
        Debug.Log("iOS");
#elif UNITY_STANDALONE_WIN
        Debug.Log("Windows PC");
#elif UNITY_EDITOR_WIN
        Debug.Log("Windows PC run by unity");
#elif UNITY_WEBGL
        Debug.Log("WebGL");
#endif
#if UNITY_ANDROID || UNITY_IOS
        panelUIMobile.SetActive(true);
#else
        panelUIMobile.SetActive(false);
#endif
        valueSlider = (float)PlayerController.instance.energyShoot / PlayerController.instance.maxEnergy;
        energy_Slider.value = valueSlider;
        point_txt.text = "Point: " + PlayerController.instance.point;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameEnded) return;
        PointCount(PlayerController.instance.point);
        OnValueSliderChange();

        if (!PlayerController.instance.isAlive)
        {
            GameLose();
            isGameEnded = true;
        }
        else if (!spawnerControler.isStage1)
        {
            GameWin();
            isGameEnded = true;
        }

    }

    void PointCount(int Point)
    {
        point_txt.text = "Point: " + Point;

    }
    void OnValueSliderChange()
    {
        currentValueSlider = (float)PlayerController.instance.energyShoot / PlayerController.instance.maxEnergy;
        if (valueSlider != currentValueSlider)
        {
            energy_Slider.value = currentValueSlider;
        }
    }
    void GameLose()
    {
        if (!PlayerController.instance.isAlive && Time.timeScale > 0)
        {
            Time.timeScale = 0;
            Debug.Log("TimeScale : " + Time.timeScale);

            panelLose.SetActive(true);
            panelUIMobile.SetActive(false);
        }
    }
    void GameWin()
    {
        if (!spawnerControler.isStage1 && Time.timeScale > 0)
        {
            Time.timeScale = 0;
            Debug.Log("TimeScale : " + Time.timeScale);
            panelWin.SetActive(true);
            panelUIMobile.SetActive(false);
        }
    }
}
