using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


[RequireComponent(typeof(Rigidbody))]
public class EvasiveManeuver : MonoBehaviour
{
    [SerializeField] private float _tiltAngle;
    [SerializeField] private float _smoothing;
    [SerializeField] private float _dodge;



    [SerializeField] private Vector2 _startWait;
    [SerializeField] private Vector2 _maneuverTime;
    [SerializeField] private Vector2 _maneuverWait;

    [SerializeField] private Boundary _boundary;

    [SerializeField] private float _currentSpeed;
    private float _maneuverTarget;

    private Rigidbody _rigidbody;


    private void Start()
    {
        StartCoroutine(Evade());
        _rigidbody = GetComponent<Rigidbody>();
        _currentSpeed = _rigidbody.linearVelocity.y;
    }


    private void FixedUpdate()
    {
        float newManeuver =
            Mathf.MoveTowards(_rigidbody.linearVelocity.x, _maneuverTarget, Time.deltaTime * _smoothing);
        _rigidbody.linearVelocity = new Vector3(newManeuver, 0, _currentSpeed);
        _rigidbody.position = new Vector3
        (
            Mathf.Clamp(_rigidbody.position.x, _boundary.xMin, _boundary.xMax),
            Mathf.Clamp(_rigidbody.position.y, _boundary.yMin, _boundary.yMax),
            0.0f
        );
        _rigidbody.rotation = Quaternion.Euler(-90.0f, 0.0f, _rigidbody.linearVelocity.x * -_tiltAngle);
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(_startWait.x, _startWait.y));

        while (true)
        {
            _maneuverTarget = Random.Range(1, _dodge) * Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(_maneuverTime.x, _maneuverTime.y));
            _maneuverTarget = 0;
            yield return new WaitForSeconds(Random.Range(_maneuverWait.x, _maneuverWait.y));
        }
    }
}