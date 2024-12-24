using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class RangeIntInputField : RangeInputFiled<int>
{

    override protected bool CheckInputValidness(string input, out int value)
    {
        var succ = int.TryParse(input, out value) && value >= min && value <= max;
        return succ;
    }
}
