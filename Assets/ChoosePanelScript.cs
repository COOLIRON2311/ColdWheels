using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoosePanelScript : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI maxPlayerText;
    [SerializeField]
    RangeIntInputField inputField;
    [SerializeField]
    Button applyButton, backButton;
    [SerializeField]
    PlayersManager playersManager;
    
    void Start()
    {

        maxPlayerText.text += $" {playersManager.MaxPlayers}";

        inputField.minValue = 1;
        inputField.maxValue = playersManager.MaxPlayers;

        inputField.onValueChanged += () => { applyButton.enabled = inputField.HasCorrectValue; };
        applyButton.onClick.AddListener(() => {
            playersManager.PlayersCount = inputField.Value;
            SceneManager.LoadScene("Game");
        });

        backButton.onClick.AddListener(()=> { gameObject.SetActive(false); });

        Clear();
    }

    public void Clear()
    {
        applyButton.enabled = false;
        inputField.Clear();
    }
}
