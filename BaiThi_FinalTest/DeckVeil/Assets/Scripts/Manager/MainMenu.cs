using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
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
    public void PlayScene()
    {
        StartCoroutine(LoadSceneWithDelay("GamePlay"));
    }

    IEnumerator LoadSceneWithDelay(string sceneName)
    { 

        // Chờ animation chạy (0.5s hoặc thời gian bạn cần)
        yield return new WaitForSeconds(0.2f);

        // Load scene được truyền vào
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Game is quitting...");
        Application.Quit();
    }
    public void CountinueGame()
    {
        StartCoroutine(LoadSceneWithDelay("Level-Select-Example"));
    }
    public void BackToMain()
    {
        if(Time.timeScale == 0f) Time.timeScale = 1f;
        StartCoroutine(LoadSceneWithDelay("Main-Menu-Example"));

    }
    public void PauseGame()
    {
        Time.timeScale = 0f; 
        Debug.Log("Game paused");
    }
    public void ContinueInGame()
    {
        Time.timeScale = 1f; // khôi phục tốc độ bình thường
        Debug.Log("Game resumed");
    }


}
