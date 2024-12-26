using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScoreScript : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI nameText, scoreText;

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void SetScore(int score)
    {
        scoreText.text = $"{score}";
    }

    public void SetNameAndScore(string name, int score)
    {
        SetName(name);
        SetScore(score);
    }

    public void SetTextColor(Color color)
    {
        nameText.color = color;
        scoreText.color = color;
    }
}
