using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField]
    string sceneToLoadName;
    [SerializeField]
    Button resumeButton, quitButton;
    void Start()
    {
        quitButton.onClick.AddListener(() => { SceneManager.LoadScene(sceneToLoadName); });
        resumeButton.onClick.AddListener(() => gameObject.SetActive(false));
    }
}
