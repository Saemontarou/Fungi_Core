using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint PreviousWaypoint;
    public Waypoint NextWaypoint;
    [Range(0f, 5f)] public float Widht = 3f;

    public List<Waypoint> Waypoints = new List<Waypoint>();
    
    [Range(0f, 1f)] public float BranchRatio = 0.5f;

    public Vector3 GetPositionWaypoint()
    {
        Vector3 minBounds = transform.position + transform.right * Widht / 2;
        Vector3 maxBounds = transform.position - transform.right * Widht / 2;
        return Vector3.Lerp(minBounds, maxBounds, Random.Range(0f, 1f));
    }
}