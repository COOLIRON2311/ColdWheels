using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

abstract public class RangeInputFiled<T> : MonoBehaviour
{
    public int min, max;
    public T defaultValue = default(T);
    [SerializeField]
    protected TMP_InputField inputField;

    protected string previousInput = "";
    protected T correctValue;
    protected bool previousValueCorrectness = true;

    public Action onValueChanged;

    public bool HasCorrectValue { get => previousValueCorrectness; }
    public T Value { get => correctValue; }

    protected void Awake()
    {
        correctValue = defaultValue;

        inputField = GetComponent<TMP_InputField>();
        inputField.onValueChanged.AddListener((string value) =>
        {
            ValidateInput(value);
            onValueChanged?.Invoke();
        }
        );

        previousValueCorrectness = CheckInputValidness($"{defaultValue}", out _);
    }

    protected void ValidateInput(string input) {
        int previousLength = input.Length;
        input = input.Trim();
        if (CheckInputValidness(input, out var value))
        {
            if (previousLength != input.Length)
            {
                inputField.text = input;
            }
            previousInput = input;
            previousValueCorrectness = true;
            correctValue = value;
        }
        else if (input.Length == 0)
        {
            previousInput = "";
            previousValueCorrectness = false;
            correctValue = defaultValue;
        }
        else
        {
            inputField.text = previousInput;
        }
    }

    abstract protected bool CheckInputValidness(string input, out T value);

    public void Clear()
    {
        inputField.text = $"{defaultValue}";
    }
}
