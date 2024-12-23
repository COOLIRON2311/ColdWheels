using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite : MonoBehaviour
{
    [SerializeField] Camera cam;
    private Vector3 scale;
    void Start()
    {
        scale = new(0, 1, 1);
    }

    void Update()
    {
        transform.LookAt(cam.transform.position);
        Vector3 rotation = transform.eulerAngles;
        rotation.Scale(scale);
        transform.eulerAngles = rotation;
    }
}
