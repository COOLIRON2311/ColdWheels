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
        var playerCreator = PlayerCreator.Instance;

        applyButton.interactable = inputField.HasCorrectValue;

        maxPlayerText.text += $" {playerCreator.MaxPlayerNameLength}";
        
        inputField.min = playerCreator.MinPlayerNameLength;
        inputField.max = playerCreator.MaxPlayerNameLength;

        inputField.onValueChanged += () => {
            applyButton.interactable = inputField.HasCorrectValue;
        };

        applyButton.onClick.AddListener(() => {
            playerCreator.AddPlayer(inputField.Value);
            OnApplyButtonClick?.Invoke();
        }
        );
        backButton.onClick.AddListener(() => {
            playerCreator.RemovePlayer();
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
