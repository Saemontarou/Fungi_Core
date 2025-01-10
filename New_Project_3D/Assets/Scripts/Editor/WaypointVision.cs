using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]



public class WaypointVision
{
 [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
 public static void OnDrawGizmo(Waypoint waypoint, GizmoType gizmoType)
 {
  if ((gizmoType & GizmoType.Selected) != 0)
  {
   Gizmos.color = Color.magenta;

  }
  else
  {
   Gizmos.color = Color.magenta * 0.5f;
  }
  
  Gizmos.DrawSphere(waypoint.transform.position, 0.1f);
  
  Gizmos.color = Color.blue;
  Gizmos.DrawLine(waypoint.transform.position + (waypoint.transform.right * waypoint.Widht / 2),
   waypoint.transform.position - (waypoint.transform.right * waypoint.Widht / 2));
  
  
  if (waypoint.PreviousWaypoint != null)
  {
   Gizmos.color = Color.green;
   Vector3 offset = waypoint.transform.right * waypoint.Widht / 2;
   Vector3 offsetTo = waypoint.PreviousWaypoint.transform.right * waypoint.PreviousWaypoint.Widht / 2;
   Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.PreviousWaypoint.transform.position + offsetTo);
  }

  if (waypoint.NextWaypoint != null)
  {
   Gizmos.color = Color.red;
   Vector3 offset = waypoint.transform.right * -waypoint.Widht / 2;
   Vector3 offsetTo = waypoint.NextWaypoint.transform.right * -waypoint.NextWaypoint.Widht / 2;
   Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.NextWaypoint.transform.position + offsetTo);
  }

  if (waypoint.Waypoints != null)
  {
   foreach (Waypoint branch in waypoint.Waypoints)
   {
    Gizmos.color = Color.yellow;
    Gizmos.DrawLine(waypoint.transform.position, branch.transform.position);
    
   }
  }
 }
}

