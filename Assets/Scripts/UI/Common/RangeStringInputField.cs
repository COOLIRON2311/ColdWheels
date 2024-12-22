using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class RangeStringInputField : RangeInputFiled<string>
{
    override protected bool CheckInputValidness(string input, out string value)
    {
        value = defaultValue;
        var succ = input.Length >= min && input.Length <= max;
        if (succ)
        {
            value = input;
        }
        return succ;
    }
}
