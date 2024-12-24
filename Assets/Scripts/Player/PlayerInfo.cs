using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo
{
    public string Name {  get; set; }

    public int Score { get; set; }

    public PlayerInfo(string name, int score)
    {
        Name = name;
        Score = score;
    }
}
