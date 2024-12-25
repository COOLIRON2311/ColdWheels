using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public enum TargetType { Easy, Medium, Hard }
    public int pointsEasy = 50;
    public int pointsMedium = 100;
    public int pointsHard = 200;

    public void CollisionDetected(TargetType type)
    {
        switch (type)
        {
            case TargetType.Easy:
                print($"{pointsEasy} points");
                break;
            case TargetType.Medium:
                print($"{pointsMedium} points");
                break;
            case TargetType.Hard:
                print($"{pointsHard} points");
                break;
            default:
                break;
        }
    }
}
