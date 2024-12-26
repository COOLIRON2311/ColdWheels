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

    private float currentSpeed = 0f;
    private Rigidbody rb;
    private bool isLaunched = false;
    private float chargeTime;

    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }

    void Update() 
    {
        HandleImpulseCharge();
        HandleMovement();
    }

    void HandleImpulseCharge()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            currentSpeed += acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
            
        }
        else
        {
            currentSpeed -= deceleration * Time.deltaTime;
            currentSpeed = Mathf.Max(currentSpeed, 0);
        }
        Vector3 forwardMovement = Vector3.forward * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + forwardMovement);
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = Vector3.right * horizontalInput * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);

        float turnAngle = horizontalInput * 30f; 
        Quaternion targetRotation = Quaternion.Euler(0, turnAngle, 0);
        carMesh.transform.rotation = Quaternion.Lerp(carMesh.transform.rotation, targetRotation, Time.deltaTime * 5f); 
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("LoseCollider"))
        {
            Debug.Log("you lose");
        }
    }
}


