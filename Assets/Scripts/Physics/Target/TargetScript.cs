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
        var playerCreator = PlayerCreator.Instance;
        switch (type)
        {
            case TargetType.Easy:
                Debug.Log("Collision");
                playerCreator.AddActivePlayerScore(pointsEasy);
                break;
            case TargetType.Medium:
                Debug.Log("Collision");
                playerCreator.AddActivePlayerScore(pointsMedium);
                break;
            case TargetType.Hard:
                Debug.Log("Collision");
                playerCreator.AddActivePlayerScore(pointsHard);
                break;
            default:
                break;
        }
    }
}
