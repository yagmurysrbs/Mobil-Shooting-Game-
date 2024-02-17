using UnityEngine;
using UnityEngine.UI;

public class SoundSettings: MonoBehaviour
{
    private LevelManager levelManager;
    private int index;

    //public AudioSource helicopterSound;
    public AudioSource gunSound;

    //public Slider helicopterVolumeSlider;
    public Slider gunVolumeSlider;

    public AudioSource backgroundMusic; 

    public Slider musicVolumeSlider; 

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>(); // LevelManager bileþenini bul

        int index = levelManager.levelIndex; // levelIndex deðerine eriþ
                                             // Burada levelIndex deðerini kullanabilirsiniz
                                             // Baþlangýçta slider deðerlerini mevcut ses seviyelerine ayarla
                                             //helicopterVolumeSlider.value = helicopterSound.volume;
        gunVolumeSlider.value = gunSound.volume;
        musicVolumeSlider.value = backgroundMusic.volume;

        if (index == 6)
        {
            backgroundMusic.Stop();
        }
    }

    // Helikopter ses seviyesini ayarla
    //public void SetHelicopterVolume(float volume)
    //{
    //    helicopterSound.volume = volume;
    //}

    // Ateþ sesi seviyesini ayarla
    public void SetGunVolume(float volume)
    {
        gunSound.volume = volume;
    }


    public void SetMusicVolume(float volume)
    {
        backgroundMusic.volume = volume;
    }
}

