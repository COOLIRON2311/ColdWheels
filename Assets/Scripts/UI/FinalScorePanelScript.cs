using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalScorePanelScript : MonoBehaviour
{
    [SerializeField]
    string sceneToLoadName;
    [SerializeField]
    Button okButton;
    [SerializeField]
    ScorePanelScript scorePanelScript;

    void Start()
    {
        okButton.onClick.AddListener(() => { SceneManager.LoadScene(sceneToLoadName); });
    }

}
