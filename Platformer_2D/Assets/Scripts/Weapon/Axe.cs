using System;
using Unity.VisualScripting;
using UnityEngine;

public class Axe : MonoBehaviour
{

    public float _speed = 5f;
    public Rigidbody2D _rb;
    void Start()
    {
        _rb.linearVelocity = transform.right * _speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
            
            Destroy(gameObject);
    }
}