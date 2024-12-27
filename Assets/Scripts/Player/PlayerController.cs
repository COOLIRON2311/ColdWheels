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
    [SerializeField] LayerMask surfaceLayer;
    [Header("Impulse Configuration")]
    public float acceleration = 10f;
    public float maxSpeed = 20f;
    public float maxTurnSpeed = 45f;

    [Header("Movement Settings")]
    public float turnSpeed = 5f;
    public float maxTurnAngle = 60f;
    /// <summary>
    /// Steering wheel angle
    /// </summary>
    private float currentAngle = 0f;
    private bool controls = true;
    private float currentSpeed = 0f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        rb.maxLinearVelocity = maxSpeed;
        rb.maxAngularVelocity = maxTurnSpeed;
        // var newCenterOfMass = rb.centerOfMass;
        // newCenterOfMass += new Vector3(0, -0.2f, 0);
        // rb.centerOfMass = newCenterOfMass;
    }

    void Update()
    {
        HandleSteering();
        HandleMovement();
        HandleImpulseCharge();
        //AdjustFrictionBasedOnPhysicsMaterial();
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
    }

    void UpdateSteeringWheelAngle()
    {
        float delta = 0 - currentAngle;
        if (Mathf.Abs(delta) < 1e-3)
        {
            currentAngle = 0;
            return;
        }
        currentAngle += delta * turnSpeed * Time.deltaTime;
    }

    void HandleSteering()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput == 0)
        { // Nobody touches the steering wheel. It returns to default position
            UpdateSteeringWheelAngle();
            return;
        }
        if (Mathf.Abs(currentAngle) < maxTurnAngle)
            currentAngle += Mathf.Sign(horizontalInput) * turnSpeed * Time.deltaTime;
    }

    void HandleImpulseCharge()
    {
        if (controls && Input.GetKey(KeyCode.W))
        {
            Vector3 force = transform.forward * rb.mass * acceleration;
            rb.AddForce(force, ForceMode.Force);
        }
    }

    void HandleMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        var frontLeftWheel = wheelColliders[0];
        var frontRightWheel = wheelColliders[1];
        var rearLeftWheel = wheelColliders[2];
        var rearRightWheel = wheelColliders[3];

        frontLeftWheel.steerAngle = currentAngle;
        frontRightWheel.steerAngle = currentAngle;

        // float torque = verticalInput * rb.mass * 10f;
        // rearLeftWheel.motorTorque = torque;
        // rearRightWheel.motorTorque = torque;
    }
    void AdjustFrictionBasedOnPhysicsMaterial()
    {
        foreach (var wheel in wheelColliders)
        {
            if (Physics.Raycast(wheel.transform.position, -wheel.transform.up, out RaycastHit hit, wheel.suspensionDistance + wheel.radius, surfaceLayer))
            {
                PhysicMaterial surfaceMaterial = hit.collider.material;
                float friction = surfaceMaterial == null ? 1f : surfaceMaterial.dynamicFriction;

                var forwardFriction = wheel.forwardFriction;
                forwardFriction.stiffness = friction;
                wheel.forwardFriction = forwardFriction;

                var sidewaysFriction = wheel.sidewaysFriction;
                sidewaysFriction.stiffness = friction;
                wheel.sidewaysFriction = sidewaysFriction;

                // Debug.Log("Friction changed");
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


