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
        targetScript.CollisionDetected(targetType);
    }
}
