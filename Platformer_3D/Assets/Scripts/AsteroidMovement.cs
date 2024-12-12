using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidMovement : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float _speed;
    private Rigidbody _rigidbody;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        _speed = Random.Range(1f, 10f);
        
        _rigidbody.linearVelocity = Vector3.up * -_speed;

    }
}