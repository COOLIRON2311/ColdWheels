using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite : MonoBehaviour
{
    [SerializeField] Camera cam;

    void Update()
    {
        Vector3 directionToCamera = cam.transform.position - transform.position;
        directionToCamera.y = 0;
        transform.rotation = Quaternion.LookRotation(directionToCamera);
    }
}
