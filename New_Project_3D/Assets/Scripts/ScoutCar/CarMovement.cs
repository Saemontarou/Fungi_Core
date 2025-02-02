using System;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private float _maxMotorTorque = 1500f;
    [SerializeField] private float _breakForce = 3000f;
    [SerializeField] private float _maxSteeringAngle = 45f;
    [SerializeField] private Transform _FL_Transform, _FR_Transform, _RL_Transform, _RR_Transform;

    [SerializeField] private WheelCollider _flWheel, _frWheel;
    [SerializeField] private WheelCollider _rlWheel, _rrWheel;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        float motor = _maxMotorTorque * Input.GetAxis("Vertical");
        float steering = _maxSteeringAngle * Input.GetAxis("Horizontal");

        _flWheel.steerAngle = steering;
        _frWheel.steerAngle = steering;

        _rlWheel.motorTorque = motor;
        _rrWheel.motorTorque = motor;

        if (Input.GetKey(KeyCode.Space))
        {
            _rlWheel.brakeTorque = _breakForce;
            _rrWheel.brakeTorque = _breakForce;
        }
        else
        {
            _rlWheel.brakeTorque = 0;
            _rrWheel.brakeTorque = 0;
        }
        UpdateWheelVisualize(_flWheel, _FL_Transform);
        UpdateWheelVisualize(_frWheel, _FR_Transform);
        UpdateWheelVisualize(_rlWheel, _RL_Transform);
        UpdateWheelVisualize(_rrWheel, _RR_Transform);
    }

    void UpdateWheelVisualize(WheelCollider collider, Transform transform)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        transform.position = pos;
        transform.rotation = rot;
    }
}
