using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {

        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        float thickness = 1.5f;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov._viewRadius, thickness);
        Vector3 viewAngleLeft = fov.DirectionFromAngle(-fov._viewAngle / 2, false);
        Vector3 viewAngleRight = fov.DirectionFromAngle(fov._viewAngle / 2, false);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleLeft * fov._viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleRight * fov._viewRadius);

        Handles.color = Color.red;
        foreach (var visibleTarget in fov._targets)
        {
            Handles.DrawLine(fov.transform.position, visibleTarget.transform.position, thickness);
        }

    }

}
