using System;
using System.Collections.Generic;
using UnityEngine;

namespace WaypointSpace
{
   public class WaypointMovement : MonoBehaviour
   {
      // [SerializeField] private List<Transform> _moveWaypoints;
      [SerializeField] private float _moveSpeed = 3.0f;
      [SerializeField] private float _rotationSpeed = 2.0f;
      [SerializeField] private float _minDistance = 0.1f;

      [SerializeField] private Vector3 _waypointDestination;

      private int _currentWaypointIndex;

      // private Rigidbody _rb;
      private CharacterController _cc;


      internal bool _reachedDestination;



      private void Start()
      {
         // _rb = GetComponent<Rigidbody>();
         _cc = GetComponent<CharacterController>();
         // _currentWaypointIndex = _moveWaypoints.Count - 1;
      }

      private void Update()
      {
         // if (_currentWaypointIndex < _moveWaypoints.Count && _currentWaypointIndex >= 0)
         // {
         //    MoveNextPoint();
         // }

         if (transform.position != _waypointDestination)
         {
            Vector3 direction = _waypointDestination - transform.position;
            direction.y = 0.0f;
            float distance = direction.magnitude;
            if (distance > _minDistance)
            {
               _reachedDestination = false;
               Quaternion targetRotation = Quaternion.LookRotation(direction);
               transform.rotation =
                  Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
               transform.Translate(Vector3.forward * (_moveSpeed * Time.deltaTime));
            }
            else
            {
               _reachedDestination = true;
            }
         }
      }

      // private void MoveNextPoint()
      // {
      //    Transform targetPoint = _moveWaypoints[_currentWaypointIndex];
      //    Vector3 moveDirection = (targetPoint.position - transform.position).normalized;
      //    // _rb.MovePosition(transform.position + moveDirection * (_moveSpeed * Time.deltaTime));
      //    _cc.Move(moveDirection * (_moveSpeed * Time.deltaTime));
      //
      //
      //    if (Vector3.Distance(transform.position, targetPoint.position) < 1f)
      //    {
      //       _currentWaypointIndex--;
      //    }
      // }

      public void SetDestination(Vector3 destination, Transform waypointTransform)
      {
         // Transform parrentTransform = waypointTransform.parent;

         this._waypointDestination = destination; //+ parrentTransform.position;
         _reachedDestination = false;
      }
   }
}