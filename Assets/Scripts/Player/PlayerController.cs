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
    [SerializeField] List<WheelCollider> wheelColliders;
    [SerializeField] List<Transform> wheelModels;
    [SerializeField] LayerMask surfaceLayer;
    [Header("Impuls Configuration")]
    public float acceleration = 10f; 
    public float maxSpeed = 20f;
    public float maxTurnSpeed = 20f;
    public float deceleration = 5f; 

    [Header("Movement Settings")]
    public float speed = 5f;
    public float turnSpeed = 5f;

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
        rb.maxLinearVelocity = maxSpeed;
        rb.maxAngularVelocity = maxTurnSpeed;
        var newCenterOfMass = rb.centerOfMass;
        newCenterOfMass += new Vector3(0, -0.2f, 0);
        rb.centerOfMass = newCenterOfMass;
    }

    void Update()
    {
        // OLD
        //var lerpValue = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;
        //carMesh.transform.position = Vector3.Lerp(lastPos, transform.position, lerpValue);
        //var lastTurnAngleQuat = Quaternion.Euler(0, lastTurnAngle, 0);
        //var turnAngleQuat = Quaternion.Euler(0, turnAngle, 0);
        //carMesh.transform.localRotation = Quaternion.Lerp(lastTurnAngleQuat, turnAngleQuat, lerpValue); 
    }

    void FixedUpdate() 
    {
        currentSpeed = rb.velocity.magnitude;
        if (!controls && currentSpeed <= 0.1)
        {
            DirectorScript.Instance.EndPlayerTurn(gameObject);
            enabled = false;
            return;
        }
        HandleImpulseCharge();
        HandleMovement();
        AdjustFrictionBasedOnPhysicsMaterial();
        //SyncWheelModels();
        lastPos = transform.position;
    }

    void HandleImpulseCharge()
    {
        // OLD
        //if (controls && Input.GetKey(KeyCode.Space))
        //{
        //    currentSpeed += acceleration * Time.fixedDeltaTime;
        //    currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);

        //}
        //else
        //{
        //    currentSpeed -= deceleration * Time.fixedDeltaTime;
        //    currentSpeed = Mathf.Max(currentSpeed, 0);
        //}
        //Vector3 forwardMovement = Vector3.forward * currentSpeed * Time.fixedDeltaTime;
        //rb.MovePosition(rb.position + forwardMovement);

        if (controls && Input.GetKey(KeyCode.W))
        {
            Vector3 force = transform.forward * rb.mass * acceleration;
            rb.AddForce(force, ForceMode.Force);
        }
        else
        {
            //Vector3 breakForce = -rb.velocity;
            ////breakForce.y = 0;  // Игнорируем вертикальную скорость
            //breakForce.Normalize();  // Приводим к единичному вектору
            //breakForce *= deceleration * rb.mass;
            //rb.AddForce(breakForce, ForceMode.Force);
        }

        
    }

    void HandleMovement()
    {
        // OLD
        //float horizontalInput = Input.GetAxis("Horizontal");
        //Vector3 movement = Vector3.right * horizontalInput * speed * Time.fixedDeltaTime;
        //rb.MovePosition(rb.position + movement);

        //lastTurnAngle = turnAngle;
        //turnAngle = horizontalInput * 30f; 


        float horizontalInput = Input.GetAxis("Horizontal");
        // Угол поворота колёс
        turnAngle = horizontalInput * turnSpeed;

        // Применение момента вращения для поворота
        Vector3 torque = transform.up * horizontalInput * rb.mass * turnSpeed;
        rb.AddTorque(torque, ForceMode.Force);
    }

    void AdjustFrictionBasedOnPhysicsMaterial()
    {
        foreach (var wheel in wheelColliders)
        {
            if (Physics.Raycast(wheel.transform.position, -wheel.transform.up, out RaycastHit hit, wheel.suspensionDistance + wheel.radius, surfaceLayer))
            {
                PhysicMaterial surfaceMaterial = hit.collider.material;
                float fricton = surfaceMaterial == null ? 1f : surfaceMaterial.dynamicFriction;

                var forwardFriction = wheel.forwardFriction;
                forwardFriction.stiffness = fricton;
                wheel.forwardFriction = forwardFriction;

                var sidewaysFriction = wheel.sidewaysFriction;
                sidewaysFriction.stiffness = fricton;
                wheel.sidewaysFriction = sidewaysFriction;

                Debug.Log("Friction changed");
            }
        }
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


