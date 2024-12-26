using UnityEngine;

public class SimpleController : MonoBehaviour
{
    public float maxSpeed = 10f; // Максимальная скорость
    public float acceleration = 5f; // Ускорение
    public float turnSpeed = 50f; // Скорость поворота
    public float brakeForce = 10f; // Сила торможения

    private Rigidbody rb;

    void Start()
    {
        // Получаем компонент Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Получаем ввод с клавиатуры
        float moveInput = Input.GetAxis("Vertical"); // W/S или стрелки вверх/вниз
        float turnInput = Input.GetAxis("Horizontal"); // A/D или стрелки влево/вправо

        // Движение вперед/назад
        if (moveInput != 0)
        {
            // Применяем силу для движения
            rb.AddForce(transform.forward * (moveInput * acceleration), ForceMode.Acceleration);
        }

        // Ограничение скорости
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        // Поворот
        if (turnInput != 0)
        {
            // Поворачиваем машину
            transform.Rotate(Vector3.up, turnInput * turnSpeed * Time.fixedDeltaTime);
        }

        // Торможение (пробел)
        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, brakeForce * Time.fixedDeltaTime);
        }
    }
}