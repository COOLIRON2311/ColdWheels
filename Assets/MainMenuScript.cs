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
    
    void Start()
    {
        startButton.onClick.AddListener(() => { SceneManager.LoadScene("Game"); });
        settingsButton.onClick.AddListener(() => { 
            gameObject.SetActive(false);
            settingsMenu.previousMenu = gameObject;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
