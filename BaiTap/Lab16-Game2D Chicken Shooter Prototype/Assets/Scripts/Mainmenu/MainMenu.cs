using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PLayGame()
    {
        Time.timeScale = 1.0f;
        Debug.Log("TimeScale : " + Time.timeScale);
        if (PlayerController.instance != null)
        {
            PlayerController.instance.isAlive = true;
        }
        SceneManager.LoadSceneAsync("GamePlay");
    }
    public void Exit()
    {
        Debug.Log("Exitting...");
        Application.Quit();
    }
    public void StopGame()
    {
        Time.timeScale = 0;
    }
    public void ContinueGame()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1.0f;
        }
    }
    public void Backhome()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void ResetGame()
    {
        Time.timeScale = 1.0f;
        if (PlayerController.instance != null)
        {
            PlayerController.instance.ResetStats();
        }
    }
}
