using System;
using TMPro;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _jumpForce;
    private Vector3 _playerMovePoint;
    
    public bool isGround;
    public bool doubleJump;
    public float rayDistance;

    private SpriteRenderer _sprite;
    private Rigidbody2D _rb;
    
    void Start()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        Move();
        Jump();
    }
    
    private void Move()
    {
        _playerMovePoint = Vector3.right * Input.GetAxisRaw("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, _playerMovePoint + transform.position,
            _speed * Time.deltaTime);
        
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
            
            Debug.DrawRay(transform.position, Vector2.down * rayDistance, Color.magenta);

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
            else if (!doubleJump && _rb.linearVelocity.y < 0)
            {
                doubleJump = true;
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
            }
        }
    }
}
