using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour
{
    [SerializeField]
    Slider musicSlider, soundSlider;
    [SerializeField]
    Button backButton;
    [SerializeField]
    AudioSource musicAudioSource, soundAudioSource;
    
    public GameObject previousMenu;
    void Awake()
    {
        musicSlider.value = musicAudioSource.volume;
        soundSlider.value = soundAudioSource.volume;

        backButton.onClick.AddListener(() => { 
            gameObject.SetActive(false);
            previousMenu.SetActive(true);
        });
        musicSlider.onValueChanged.AddListener((float value) => musicAudioSource.volume = value);
        soundSlider.onValueChanged.AddListener((float value) => soundAudioSource.volume = value);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
