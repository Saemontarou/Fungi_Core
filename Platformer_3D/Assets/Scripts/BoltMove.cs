using System;
using UnityEngine;

public class BoltMove : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _speed;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.linearVelocity = transform.up * _speed;
    }
}