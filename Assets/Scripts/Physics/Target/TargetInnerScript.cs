using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetInnerScript : MonoBehaviour
{
    public TargetScript.TargetType targetType;
    private TargetScript targetScript;

    void Start()
    {
        targetScript = GetComponentInParent<TargetScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetScript.CollisionDetected(targetType, 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetScript.CollisionDetected(targetType, -1);
        }
    }
}
