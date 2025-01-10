using System;
using UnityEngine;
using Random = UnityEngine.Random;


namespace WaypointSpace
{

   public class WaypointNavigation : MonoBehaviour
   {
      [SerializeField] private Waypoint _currentWaypoint;
      private WaypointMovement _controller;

      private void Awake()
      {
         _controller = GetComponent<WaypointMovement>();
      }

      private void Start()
      {
         _controller.SetDestination(_currentWaypoint.GetPositionWaypoint(), _currentWaypoint.transform);
      }

      private void Update()
      {
         if (_controller._reachedDestination)
         {
            bool shouldBranch = false;
            if (_currentWaypoint.Waypoints != null && _currentWaypoint.Waypoints.Count > 0)
            {
               shouldBranch = Random.Range(0f, 1f) <= _currentWaypoint.BranchRatio ? true : false;
            }

            if (shouldBranch)
            {
               _currentWaypoint = _currentWaypoint.Waypoints[Random.Range(0, _currentWaypoint.Waypoints.Count - 1)];
            }
            else
            {
               _currentWaypoint = _currentWaypoint.NextWaypoint;
               _controller.SetDestination(_currentWaypoint.GetPositionWaypoint(), _currentWaypoint.transform);
            }
         }
      }
   }
}
