using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCanvasScript : MonoBehaviour
{
    [SerializeField]
    PauseMenuScript pauseMenuScript;
    [SerializeField]
    FinalScorePanelScript finalScorePanelScript;

    private void Start()
    {
        DirectorScript.Instance.scoreboard = finalScorePanelScript.gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            var paused = pauseMenuScript.gameObject.activeSelf;
            if (paused)
            {
                pauseMenuScript.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                pauseMenuScript.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
            
        }
    }
}
