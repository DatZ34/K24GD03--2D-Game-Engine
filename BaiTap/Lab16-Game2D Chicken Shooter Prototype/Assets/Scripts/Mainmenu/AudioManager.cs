using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public AudioSource soundEFX, music;
    public AudioClip[] sfxAudioClip, musicAudioClip;
    public Slider musicSlider;
    public Slider soundEFX_Slider;
    public Toggle soundEFX_Toggle;
    public Toggle musicToggle;
    public static AudioManager instance;

    public float currentValueMusic;
    public float currentValueSoundEFX;

    public float toggleValueMusic;
    public float toggleValueSoundEFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentValueSoundEFX = soundEFX.volume;
        currentValueMusic = music.volume;
        SetValueSlider();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        PlayIndexMusic(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayIndexMusic(int index)
    {
        if (index < musicAudioClip.Length)
        {
            music.clip = musicAudioClip[index];
            music.PlayOneShot(music.clip);
        }
    }
    public void ToggleSoundEFX()
    {
        soundEFX.mute = !soundEFX.mute;
        if (soundEFX.mute)
        {
            toggleValueSoundEFX = soundEFX.volume;
            soundEFX_Slider.value = 0;
            soundEFX_Slider.onValueChanged.AddListener(SetChangeVolumeSoundEFX);

        }
        else
        {
            soundEFX_Slider.value = toggleValueSoundEFX;
            soundEFX_Slider.onValueChanged.AddListener(SetChangeVolumeSoundEFX);


        }
    }
    public void ToggleMusic()
    {
        music.mute = !music.mute;
        if (music.mute)
        {
            toggleValueMusic = music.volume;
            musicSlider.value = 0;
            musicSlider.onValueChanged.AddListener(SetChangeVolumeMusic);

        }
        else
        {
            musicSlider.value = toggleValueMusic;
            musicSlider.onValueChanged.AddListener(SetChangeVolumeMusic);

        }
    }
    public void SetValueSlider()
    {
        if(musicSlider != null)
        {
            musicSlider.value = music.volume;
            musicSlider.onValueChanged.AddListener(SetChangeVolumeMusic);
        }
        else
        {
            currentValueMusic = music.volume;
        }
        if(soundEFX != null)
        {
            soundEFX_Slider.value = soundEFX.volume;
            soundEFX_Slider.onValueChanged.AddListener(SetChangeVolumeSoundEFX);
        }
        else
        {
            currentValueSoundEFX = soundEFX.volume;
        }
    }
    public void SetChangeVolumeMusic(float volume)
    {
        currentValueMusic = volume;
        music.volume = volume;
        if (volume != toggleValueMusic)
        {
            musicToggle.isOn = volume <= 0.1f;
        }
    }
    public void SetChangeVolumeSoundEFX(float volume)
    {
        currentValueSoundEFX = volume;
        soundEFX.volume = volume;
        if (volume != toggleValueSoundEFX)
        {
            soundEFX_Toggle.isOn = volume <= 0.1f;
        }
    }
}
