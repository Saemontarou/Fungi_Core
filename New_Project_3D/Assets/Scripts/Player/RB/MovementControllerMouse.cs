using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class MovementControllerMouse : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Camera _camera;
    
    [SerializeField] private float _jumpHeight = 2f;
    [SerializeField] private float _gravity = -9.81f;

    private Rigidbody _rb;

    private NavMeshAgent _agent;
    
    private Vector3 _targetPosition;
    private bool _isMoved;

    private bool _isGrounded;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HandleMouseInput();
        }
        
        HandleJump();
    }

    private void FixedUpdate()
    {
        if (_isMoved)
        {
            // HandleMovement();
        }
        
    }
    
    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rb.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
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
        Vector3 direction = (_targetPosition - transform.position).normalized;
        Vector3 velocity = direction * _moveSpeed;

        if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
        {
            _isMoved = false;
            _rb.linearVelocity = Vector3.zero;
            return;
        }

        _rb.linearVelocity = new Vector3(velocity.x, _rb.linearVelocity.y, velocity.z);

        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        _rb.rotation = Quaternion.RotateTowards(_rb.rotation, targetRotation, _moveSpeed * Time.deltaTime);

    }

    private void HandleMouseInput()
    {
        // if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            {
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    // _targetPosition = hit.point;

                    _agent.SetDestination(hit.point);
                    
                    _isMoved = true;
                }
            }
        }
    }
}
