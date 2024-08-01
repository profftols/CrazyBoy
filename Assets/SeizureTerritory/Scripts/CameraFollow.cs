using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 0.125f;
    
    private void FixedUpdate()
    {
        var desiredPosition = _target.position;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, smoothedPosition.z);
    }
}