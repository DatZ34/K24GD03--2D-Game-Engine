using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource soundEFX, music;
    public AudioClip[] sfxAudioClip, musicAudioClip;
    public Image CancleMusic_img;
    public Image CancleSoundEFX_img;
    public Slider musicSlider;
    public Slider soundEFX_Slider;
    public static AudioManager instance;

    private float currentValueMusic;
    private float currentValueSoundEFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetValueSlider();
        if(instance == null)
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
        if(index < musicAudioClip.Length)
        {
            music.clip = musicAudioClip[index];
            music.PlayOneShot(music.clip);
        }
    }
    public void PlayIndexSoundEFX(int index)
    {
        if(index < sfxAudioClip.Length)
        {
            soundEFX.clip = sfxAudioClip[index];
            soundEFX.PlayOneShot(soundEFX.clip);
        }
    }
    public void PlayNameMusic(string name)
    {

        AudioClip clip = Array.Find(musicAudioClip, c => c.name == name);
        if (clip != null)
        {
            music.clip = clip;
            music.PlayOneShot(clip);
        }
    }
    public void PlayNameSoundEFX(string name)
    {

        AudioClip clip = Array.Find(sfxAudioClip, c => c.name == name);
        if (clip != null)
        {
            soundEFX.clip = clip;
            soundEFX.PlayOneShot(clip);
        }
    }
    public void ToggleSoundEFX()
    {
        bool on;
        if (soundEFX.mute = !soundEFX.mute)
        {
            
            soundEFX_Slider.value = 0;
            on = true;
            CancleSoundEFX_img.gameObject.SetActive(on);
        }
        else
        {
            soundEFX_Slider.value = currentValueSoundEFX;
            on = false;
            CancleSoundEFX_img.gameObject.SetActive(on);

        }

    }
    public void ToggleMusic()
    {
        
        bool on;
        if (music.mute = !music.mute)
        {
            musicSlider.value = 0;
            on = true;
            CancleMusic_img.gameObject.SetActive(on);
        }
        else
        {
            musicSlider.value = currentValueMusic;
            on = false;
            CancleMusic_img.gameObject.SetActive(on);

        }
        
    }
    public void SetValueSlider()
    {
        if (musicSlider != null)
        {
            musicSlider.value = music.volume;
            musicSlider.onValueChanged.AddListener(SetChangeVolumeMusic);
        }

        if (soundEFX_Slider != null)
        {
            soundEFX_Slider.value = soundEFX.volume;
            soundEFX_Slider.onValueChanged.AddListener(SetChangeVolumeSoundEFX);
        }
    }
    public void SetChangeVolumeMusic(float volume)
    {
        currentValueMusic = music.volume;
        music.volume = volume;
        if(volume > 0)
        {
            CancleMusic_img.gameObject.SetActive(false);
        }
        else
        {
            CancleMusic_img.gameObject.SetActive(true);
        }
    }
    public void SetChangeVolumeSoundEFX(float volume)
    {
        currentValueSoundEFX = soundEFX.volume;
        soundEFX.volume = volume;
        if(volume > 0)
        {
            CancleSoundEFX_img.gameObject.SetActive(false);
        }
        else
        {
            CancleSoundEFX_img.gameObject.SetActive(true);
        }

    }
}
