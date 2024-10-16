﻿using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _smoothSpeed = 0.1f;
    [SerializeField] private float _offset = 15f;

    private Transform _target;
    private bool _isTargetNotNull;

    private void Start()
    {
        _isTargetNotNull = _target != null;
    }

    private void LateUpdate()
    {
        if (_isTargetNotNull)
        {
            var desiredPosition = _target.position;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y + _offset,
                smoothedPosition.z - _offset);
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}