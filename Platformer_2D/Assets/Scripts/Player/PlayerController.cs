using System;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 6f;
    [SerializeField] private float _jumpForce = 18f;
    private Vector3 _playerMovePoint;
    
    private bool isGround;
    public bool doubleJump;
    public float rayDistance = 0.9f;

    private SpriteRenderer _sprite;
    private Rigidbody2D _rb;
    [SerializeField] private LayerMask _mask;

    public Animator _animator;
    
    void Start()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        Move();
        Jump();
        DrawGizmos();
    }
    private void Move()
    {
        _playerMovePoint = Vector3.right * Input.GetAxisRaw("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, _playerMovePoint + transform.position,
            _speed * Time.deltaTime);
        
        _animator.SetFloat("moveX", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        
        
        if (_playerMovePoint.x < 0)
        {
            _sprite.flipX = true;
        }
        else if (_playerMovePoint.x > 0)
        {
            _sprite.flipX = false;
        }
    }
    
    private void Jump()
    {
        {
            var hit = Physics2D.Raycast(_rb.position, Vector2.down, rayDistance, LayerMask.GetMask("Ground"));
            
            if(isGround)
            {
                _animator.SetBool("Jumping", false);
            }
            else
            {
                _animator.SetBool("Jumping", true);
            }
            
            if (hit.collider != null)
            {
                isGround = true;
                doubleJump = false;
            }
            else
            {
                isGround = false;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround)
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
            }
            else if (!doubleJump)
            {
                doubleJump = true;
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
            }
        }
    }

    private void DrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector2.down * rayDistance, Color.magenta);
    }
}