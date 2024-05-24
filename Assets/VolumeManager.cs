using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    private BGMPlayer bgmPlay;
    [SerializeField] private AudioSource audioSource;
    public Slider volumeSliderBGM, volumeSliderSFX;

    void Start()
    {
        //audioSource = FindObjectOfType<AudioSource>();
        bgmPlay = FindObjectOfType<BGMPlayer>();

        // Set BGM volume slider value from PlayerPrefs
        volumeSliderBGM.value = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        volumeSliderBGM.onValueChanged.AddListener(changeVol);

        // Set SFX volume slider value from BGMPlayer
        volumeSliderSFX.value = bgmPlay.sfxVol;
        volumeSliderSFX.onValueChanged.AddListener(changeVolSFX);
    }

    void Update()
    {
        
    }

    void changeVol(float value)
    {
        bgmPlay.sound.volume = value;
        PlayerPrefs.SetFloat("BGMVolume", value);
        PlayerPrefs.Save();

        // Debug statement to check if changeVol is called
        Debug.Log("BGM Volume Changed: " + value);
    }

    void changeVolSFX(float value)
    {
        bgmPlay.sfxVol = value;

        // Debug statement to check if changeVolSFX is called
        Debug.Log("SFX Volume Changed: " + value);
    }
}
