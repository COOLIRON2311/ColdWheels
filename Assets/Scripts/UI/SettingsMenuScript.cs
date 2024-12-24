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
    void Start()
    {
        var soundController = SoundController.Instance;
        musicSlider.value = soundController.MusicVolume;
        soundSlider.value = soundController.EffectsVolume;

        backButton.onClick.AddListener(() => { 
            gameObject.SetActive(false);
        });
        musicSlider.onValueChanged.AddListener((float value) => soundController.MusicVolume = value);
        soundSlider.onValueChanged.AddListener((float value) => soundController.EffectsVolume = value);

    }
}
