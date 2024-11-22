using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float _speed;
    private SpriteRenderer _sprite;
    private Vector3 _playerMovePoint;
    
    private void Start()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }
    void Update()
    {
        Move();
    }
    private void Move()
    {
        Vector3 playerMovePoint = Vector3.right * Input.GetAxisRaw("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, playerMovePoint + transform.position,
            _speed * Time.deltaTime);

        if (playerMovePoint.x < 0)
        {
            _sprite.flipX = true;
        }
        else if (playerMovePoint.x > 0)
        {
            _sprite.flipX = false;
        }
    }
}
