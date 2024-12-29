using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CharacterMovement : MonoBehaviour
{
   [SerializeField] private float _movementSpeed = 5f;
   [SerializeField] private float _rotationSpeed = 5f;
   [SerializeField] private float _jumpForce = 5f;
   [SerializeField] private float _gravity = -9.81f;

   [SerializeField] private Camera _camera;

   private CharacterController _controller;
   private Vector3 _velocity;
   private bool _isGrounded;

   private void Awake()
   {
      _controller = GetComponent<CharacterController>();
   }

   private void Update()
   {
      HandleJump();
   }


   private void HandleJump()
   {
      _isGrounded = _controller.isGrounded;
      if(_isGrounded && _velocity.y < 0)
      {
         _velocity.y = -2f;
      }

      if (Input.GetButtonDown("Jump") && _isGrounded)
      {
         _velocity.y = Mathf.Sqrt(_jumpForce * -2f * _gravity);
      }
   }


   private void FixedUpdate()
   {
      HandleMovement();
   }

   private void HandleMovement()
   {
      float horizontal = Input.GetAxis("Horizontal");
      float vertical = Input.GetAxis("Vertical");

      Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;
      if (movement.magnitude > 0.1f && _isGrounded)
      {
         Vector3 moveDirection = Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0) * movement;

         Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
         transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

         _controller.Move(moveDirection * (_movementSpeed * Time.deltaTime));
         
      }

      if (!_isGrounded)
      {
         _velocity.y += _gravity * Time.deltaTime;
      }

      _controller.Move(_velocity * Time.deltaTime);
      
   }
}
