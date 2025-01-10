using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 720f;
    [SerializeField] private float _jumpHeight = 2f;
    [SerializeField] private Camera _camera;

    private Rigidbody _rigidbody;

    private bool _isGrounded;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        HandleJump();
    }


    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
            _isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        foreach (ContactPoint contact in other.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                _isGrounded = true;
                break;
            }
        }
    }


    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;

        if (movement.magnitude >= 0.1f)
        {
            Vector3 moveDirection = Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0) * movement;


            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _rigidbody.rotation = Quaternion.RotateTowards(_rigidbody.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            Vector3 moveVelocity = moveDirection * _moveSpeed;
            _rigidbody.linearVelocity = new Vector3(moveVelocity.x, _rigidbody.linearVelocity.y, moveVelocity.z);

        }

        else
        {
            _rigidbody.linearVelocity = new Vector3(0, _rigidbody.linearVelocity.y, 0);
        }
    }
}
