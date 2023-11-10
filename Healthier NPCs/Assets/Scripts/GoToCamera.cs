using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToCamera : MonoBehaviour
{
    [SerializeField] private float _speed = 800f;
    [SerializeField] private float _minDistance = 2f;
    private Transform _cameraTransform;
    private Rigidbody _rb;
    bool _canMove = true;
    void Awake()
    {
        _cameraTransform = Camera.main.transform;
        _rb = GetComponent<Rigidbody>();
        GetComponent<IHealth>().OnDied += Die;
    }

    void Update()
    {
        if (Vector3.Distance(_cameraTransform.position, transform.position) > _minDistance && _canMove)
            _rb.AddForce(Time.deltaTime * _speed * (_cameraTransform.position - transform.position).normalized, ForceMode.VelocityChange);
    }

    private void Die()
    {
        _canMove = false;
    }
}
