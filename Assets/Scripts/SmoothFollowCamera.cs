using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowCamera : MonoBehaviour
{
    public Transform target; // Целевой объект, за которым следит камера
    public float smoothSpeed = 0.125f; // Скорость плавного перемещения
    public Vector3 cameraOffset; // Смещение камеры относительно целевого объекта
    
    private void LateUpdate()
    {
        if (!target) return;

        // Вычисляем желаемую позицию камеры
        var desiredPosition = target.position + target.forward * cameraOffset.z + Vector3.up * cameraOffset.y;

        // Плавно интерполируем позицию камеры
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Устанавливаем новую позицию камеры
        transform.position = smoothedPosition;

        // Поворачиваем камеру в сторону направления target.forward
        // var targetRotation = Quaternion.LookRotation(target.forward);

        // Добавляем наклон по оси X
        // var tiltedRotation = targetRotation * Quaternion.Euler(20, 0, 0);
        transform.LookAt(target);

        // Плавно интерполируем вращение камеры
        // transform.rotation = Quaternion.Lerp(transform.rotation, tiltedRotation, smoothSpeed);
    }
}
