using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject carMesh;
    [Header("Impuls Configuration")]
    public float acceleration = 10f; 
    public float maxSpeed = 20f; 
    public float deceleration = 5f; 

    [Header("Movement Settings")]
    public float speed = 5f; 

    private bool controls = true;
    private float currentSpeed = 0f;
    private Rigidbody rb;

    private Vector3 lastPos;
    private float lastTurnAngle;
    private float turnAngle;

    void Start()
    {
        lastPos = transform.position;
        rb = GetComponentInChildren<Rigidbody>();
    }

    void Update()
    {
        var lerpValue = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;
        carMesh.transform.position = Vector3.Lerp(lastPos, transform.position, lerpValue);
        var lastTurnAngleQuat = Quaternion.Euler(0, lastTurnAngle, 0);
        var turnAngleQuat = Quaternion.Euler(0, turnAngle, 0);
        carMesh.transform.localRotation = Quaternion.Lerp(lastTurnAngleQuat, turnAngleQuat, lerpValue); 
    }

    void FixedUpdate() 
    {
        if (!controls && currentSpeed <= 0)
        {
            DirectorScript.Instance.EndPlayerTurn(gameObject);
            enabled = false;
            return;
        }
        HandleImpulseCharge();
        HandleMovement();
        lastPos = transform.position;
    }

    void HandleImpulseCharge()
    {
        if (controls && Input.GetKey(KeyCode.Space))
        {
            currentSpeed += acceleration * Time.fixedDeltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
            
        }
        else
        {
            currentSpeed -= deceleration * Time.fixedDeltaTime;
            currentSpeed = Mathf.Max(currentSpeed, 0);
        }
        Vector3 forwardMovement = Vector3.forward * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMovement);
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = Vector3.right * horizontalInput * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        lastTurnAngle = turnAngle;
        turnAngle = horizontalInput * 30f; 
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("DisableControls"))
        {
            controls = false;
        }
    }

    private void OnDisable()
    {
        carMesh.transform.localPosition = Vector3.zero;
        carMesh.transform.localRotation = Quaternion.identity;
    }
}


