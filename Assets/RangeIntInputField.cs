using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class RangeIntInputField : MonoBehaviour
{
    public int minValue, maxValue;
    public int defaultValue = 1;
    [SerializeField]
    TMP_InputField inputField;

    string previousInput = "";
    int correctValue = 0;
    bool previousValueCorrectness = true;

    public Action onValueChanged;

    public bool HasCorrectValue { get => previousValueCorrectness; }
    public int Value { get => correctValue; }

    void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onValueChanged.AddListener(ValidateInput);
    }


    private void ValidateInput(string input)
    {
        int previousLength = input.Length;
        input = input.Trim();
        if (int.TryParse(input, out var value) && value >= minValue && value <= maxValue)
        {
            if (previousLength != input.Length)
            {
                inputField.text = input;
            }
            previousInput = input;
            previousValueCorrectness = true;
            correctValue = value;
        } else if (input.Length == 0) {
            previousInput = "";
            previousValueCorrectness = false;
            correctValue = defaultValue;
        }
        else
        {
            inputField.text = previousInput;
        }

        onValueChanged?.Invoke();
    }

    public void Clear()
    {
        inputField.text = $"{defaultValue}";
    }
}
