using System;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class MovementControllerMouse : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _canvas;

    [SerializeField] private AudioSource _reloadAmmo;
    
    private Rigidbody _rb;
    private NavMeshAgent _agent;

    private Vector3 _targetPosition;
    private bool _isMoved;

    private Shoot _shoot;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                          RigidbodyConstraints.FreezeRotationZ;
        _agent = GetComponent<NavMeshAgent>();
        _shoot = GetComponent<Shoot>();
        _shoot._currentAmmo = _shoot._poolObject.poolSize;
        
        _reloadAmmo = GetComponentInChildren<AudioSource>();
        
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HandleMouseInput();
        }

        if (Input.GetKey(KeyCode.E))
        {
            _shoot.ShootBullet();
        }
      
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("RELOADING POOL");
            _shoot._currentAmmo = _shoot._poolObject.poolSize;
            _reloadAmmo.Play();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!_canvas.activeSelf)
            {
                _canvas.SetActive(true);
            }
            else
            {
                _canvas.SetActive(false);
            }
        }
    }


    private void FixedUpdate()
    {
        if (_isMoved)
        {
            // HandleMovement();
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
