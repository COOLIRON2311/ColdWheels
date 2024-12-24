using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    Button startButton, settingsButton, quitButton;
    [SerializeField]
    SettingsMenuScript settingsMenu;
    [SerializeField]
    ChoosePanelScript choosePanel;

    void Start()
    {
        startButton.onClick.AddListener(() => { 
            choosePanel.gameObject.SetActive(true);
            choosePanel.Clear();
        });
        settingsButton.onClick.AddListener(() => { 
            settingsMenu.gameObject.SetActive(true);
        });
        quitButton.onClick.AddListener(() => {
#if UNITY_STANDALONE
            Application.Quit();
#endif
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        });

        SoundController.Instance.PlayMainMenuMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
