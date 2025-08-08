using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void loadScene()
    {
        SceneManager.LoadSceneAsync("DinoRunner");
    }
    public void ToggleMusic()
    {
        AudioManager.instance.PlayNameSoundEFX("Coin");
        AudioManager.instance.ToggleMusic();
    }
    public void ToggleSoundEFX()
    {
        AudioManager.instance.PlayNameSoundEFX("Coin");

        AudioManager.instance.ToggleSoundEFX();
    }
    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
