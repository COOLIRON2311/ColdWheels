using System;
using UnityEngine;

[Serializable]
public struct Wheel
{
    public Transform wheelMesh;
    public WheelCollider wheelCollider;
    public bool isForward;

    public void UpdateMeshPosition()
    {
        Vector3 position;
        Quaternion rotation;

        wheelCollider.GetWorldPose(out position, out rotation);

        wheelMesh.position = position;
        wheelMesh.rotation = rotation;
    }
}

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("Wheel objects")]
    [SerializeField] private Wheel[] wheels;

    [Space]

    [Header("Wheel settings")]
    [SerializeField, Range(1, 180)] private int wheelAngle;

    [Space]

    [Header("Car settings")]
    [SerializeField,Range(1,5000)] private int motorForce;

    private const float MIN_SPEED = 1.0f;

    private Rigidbody _carRigidbody;

    private float _verticalInput;
    private float _horizontalInput;
    private float _speed;

    private bool _controls = true;

    private void Start()
    {
        _carRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckSpeedThreshold();
        Move();
        RotateWheels();
    }

    private void CheckSpeedThreshold()
    {
        if (!_controls && _speed <= MIN_SPEED) {
            enabled = false;
            DirectorScript.Instance.EndPlayerTurn(gameObject);
        }
    }

    private void Move()
    {
        _speed = _carRigidbody.velocity.magnitude;

        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        if (!_controls) {
            _verticalInput = 0.0f;
            _horizontalInput = 0.0f;
        }

        foreach (Wheel wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = motorForce * _verticalInput;
            wheel.UpdateMeshPosition();
        }

        // Debug.Log(_speed);
    }

    private void RotateWheels()
    {
        float steeringAngle = _horizontalInput * wheelAngle;

        foreach (Wheel wheel in wheels)
        {
            if (wheel.isForward) {
                wheel.wheelCollider.steerAngle = steeringAngle;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DisableControls"))
        {
            SoundController.Instance.FadeOutMusic();
            _controls = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle") && !gameObject.CompareTag("Obstacle"))
        {
            SoundController.Instance.PlayCrashSound();
        }
    }
}

