using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMove : MonoBehaviour

{

    public int Speed = 1;
    public Transform _point1;
    public Transform _point2;
    private Rigidbody2D _rb2D;
    private SpriteRenderer _sprite;
    bool OnRight;


    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();

    }

    void Update()
    {
        if (gameObject.transform.position.x <= _point1.position.x)
        {
            OnRight = true;
            _sprite.flipX = true;
        }

        if (gameObject.transform.position.x >= _point2.position.x)
        {
            OnRight = false;
            _sprite.flipX = false;
        }


        MakePosition();
    }

    void MakePosition()
    {
        if (OnRight)
        {
            _rb2D.linearVelocity = new Vector2(Speed, _rb2D.linearVelocity.y);
        }
        else
        {
            _rb2D.linearVelocity = new Vector2(-Speed, _rb2D.linearVelocity.y);
        }
    }
}