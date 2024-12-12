using System;
using UnityEngine;
using Random = UnityEngine.Random;



public class RandomRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.angularVelocity = Random.insideUnitSphere * rotationSpeed;

    }
}
