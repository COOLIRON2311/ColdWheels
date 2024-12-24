using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayersNumberScript : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI maxPlayerText;
    [SerializeField]
    RangeIntInputField inputField;
    [SerializeField]
    Button applyButton, backButton;

    public Action OnApplyButtonClick, OnBackButtonClick;

    void Start()
    {
        var playerCreator = PlayerCreator.Instance;

        maxPlayerText.text += $" {playerCreator.MaxPlayers}";

        inputField.min = 1;
        inputField.max = playerCreator.MaxPlayers;

        inputField.onValueChanged += () => { applyButton.interactable = inputField.HasCorrectValue; };
        applyButton.onClick.AddListener(() => {
            playerCreator.PlayersCount = inputField.Value;
            OnApplyButtonClick?.Invoke();
        });

        backButton.onClick.AddListener(() => { OnBackButtonClick?.Invoke(); });

        Clear();
    }

    public void Clear()
    {
        applyButton.interactable = inputField.HasCorrectValue;
        inputField.Clear();
    }
}
