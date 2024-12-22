using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameScript : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI maxPlayerText;
    [SerializeField]
    RangeStringInputField inputField;
    [SerializeField]
    Button applyButton, backButton;
    [SerializeField]
    TextMeshProUGUI applyButtonText;

    public Action OnApplyButtonClick, OnBackButtonClick;

    private void Awake()
    {
        var playersManager = PlayersManager.Instance;

        applyButton.interactable = inputField.HasCorrectValue;

        maxPlayerText.text += $" {playersManager.MaxPlayerNameLength}";
        
        inputField.min = playersManager.MinPlayerNameLength;
        inputField.max = playersManager.MaxPlayerNameLength;

        inputField.onValueChanged += () => {
            applyButton.interactable = inputField.HasCorrectValue;
        };

        applyButton.onClick.AddListener(() => {
            playersManager.AddPlayer(inputField.Value);
            OnApplyButtonClick?.Invoke();
        }
        );
        backButton.onClick.AddListener(() => {
            playersManager.RemovePlayer();
            OnBackButtonClick?.Invoke();
        }
        );
    }

    public void SetActive()
    {
        applyButton.interactable = inputField.HasCorrectValue;
        gameObject.SetActive(true);
    }

    public void SetApplyButtonText(string text)
    {
        applyButtonText.text = text;
    }
}
