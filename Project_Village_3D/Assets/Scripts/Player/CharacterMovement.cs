using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CharacterMovement : MonoBehaviour
{
   [SerializeField] private float _walkingSpeed = 7.5f;
   [SerializeField] private float _runningSpeed = 11.5f;
   [SerializeField] private float _jumpForce = 5f;
   [SerializeField] private float _gravity = -9.81f;
   [SerializeField] private float _rotationSpeed = 5f;
   
   public bool Sprint;

   [SerializeField] private Camera _playerCamera;

   private CharacterController _characterController;
   
   Vector3 _moveDirection = Vector3.zero;
   
   private Vector3 _velocity;
   private bool _isGrounded;
   
   
   [HideInInspector]
   public bool canMove = true;

   private void Awake()
   {
      _characterController = GetComponent<CharacterController>();
   }

   private void Update()
   {
      
      if(Input.GetKey(KeyCode.LeftShift))
      {
         Sprint = true;
      }
      else
      {
         Sprint = false;
      }
      
      Vector3 forward = transform.TransformDirection(Vector3.forward);
      Vector3 right = transform.TransformDirection(Vector3.right);
      
      bool isRunning = Input.GetKey(KeyCode.LeftShift);
      float curSpeedX = canMove ? (isRunning ? _runningSpeed : _walkingSpeed) * Input.GetAxis("Vertical") : 0;
      float curSpeedY = canMove ? (isRunning ? _runningSpeed : _walkingSpeed) * Input.GetAxis("Horizontal") : 0;
      float movementDirectionY = _moveDirection.y;
      _moveDirection = (forward * curSpeedX) + (right * curSpeedY);
      HandleJump();
   }


   private void HandleJump()
   {
      _isGrounded = _characterController.isGrounded;
      if(_isGrounded && _velocity.y < 0)
      {
         _velocity.y = -2f;
      }

      if (Input.GetButtonDown("Jump") && canMove && _characterController.isGrounded)
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
         Vector3 moveDirection = Quaternion.Euler(0, _playerCamera.transform.rotation.eulerAngles.y, 0) * movement;

         Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
         transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

         _characterController.Move(moveDirection * (_walkingSpeed * Time.deltaTime));
         
      }

      if (!_isGrounded)
      {
         _velocity.y += _gravity * Time.deltaTime;
      }

      _characterController.Move(_velocity * Time.deltaTime);
      
   }
}
