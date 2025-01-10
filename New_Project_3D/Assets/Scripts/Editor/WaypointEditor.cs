using Codice.CM.Common;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WaypointEditor : EditorWindow
{

    [MenuItem("Waypoint Tools/Waypoint Editor")]

    public static void Open()
    {
        GetWindow<WaypointEditor>();
    }

    public Transform WaypointRoot;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);
        EditorGUILayout.PropertyField(obj.FindProperty("WaypointRoot"));
        if (!WaypointRoot)
        {
            EditorGUILayout.HelpBox("Root transform must be selected", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButton();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }

    private void DrawButton()
    {
        if (GUILayout.Button("Add Waypoint"))
        {
            CreateWaypoint();
        }

        if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Waypoint>())
        {
            if (GUILayout.Button("Next Waypoint"))
            {
                CreateWaypointAfter();
            }
            if (GUILayout.Button("Previous Waypoint"))
            {
                CreateWaypointBefore();
            }

            if (GUILayout.Button("Delete Waypoint"))
            {
                RemoveWaypoint();
            }

            if (GUILayout.Button("Add Branch Waypoint"))
            {
                BranchWaypoint();
            }
        }
    }

    private void BranchWaypoint()
    {
        GameObject waypointObj = new GameObject("Waypoint" + WaypointRoot.childCount, typeof(Waypoint));
        waypointObj.transform.SetParent(WaypointRoot, false);
        Waypoint waypoint = waypointObj.GetComponent<Waypoint>();
        Waypoint branchsFrom = Selection.activeGameObject.GetComponent<Waypoint>();
        branchsFrom.Waypoints.Add(waypoint);
        waypoint.transform.position = branchsFrom.transform.position;
        waypoint.transform.forward = branchsFrom.transform.forward;

        Selection.activeGameObject = waypoint.gameObject;
    }

    void RemoveWaypoint()
    {
        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
        if (selectedWaypoint.NextWaypoint)
        {
            selectedWaypoint.NextWaypoint.PreviousWaypoint = selectedWaypoint.PreviousWaypoint;
        }

        if (selectedWaypoint.PreviousWaypoint)
        {
            selectedWaypoint.PreviousWaypoint.NextWaypoint = selectedWaypoint.NextWaypoint;
            Selection.activeGameObject = selectedWaypoint.PreviousWaypoint.gameObject;
        }
        DestroyImmediate(selectedWaypoint.gameObject);
    }

    private void CreateWaypointAfter()
    {
        GameObject waypointObj = new GameObject("Waypoint" + WaypointRoot.childCount, typeof(Waypoint));
        waypointObj.transform.SetParent(WaypointRoot, false);
        Waypoint waypoint = waypointObj.GetComponent<Waypoint>();
        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
        waypointObj.transform.position = selectedWaypoint.transform.position;
        waypointObj.transform.forward = selectedWaypoint.transform.forward;
        waypoint.PreviousWaypoint = selectedWaypoint;
        
        if (selectedWaypoint.NextWaypoint)
        {
            selectedWaypoint.NextWaypoint.PreviousWaypoint = waypoint;
            waypoint.NextWaypoint = waypoint;
        }

        selectedWaypoint.NextWaypoint = waypoint;
        waypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        Selection.activeGameObject = waypoint.gameObject;
        
    }

    private void CreateWaypointBefore()
    {
        GameObject waypointObj = new GameObject("Waypoint" + WaypointRoot.childCount, typeof(Waypoint));
        waypointObj.transform.SetParent(WaypointRoot, false);
        Waypoint waypoint = waypointObj.GetComponent<Waypoint>();
        
        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
        waypointObj.transform.position = selectedWaypoint.transform.position;
        waypointObj.transform.forward = selectedWaypoint.transform.forward;

        if (selectedWaypoint.PreviousWaypoint)
        {
            waypoint.PreviousWaypoint = selectedWaypoint.PreviousWaypoint;
            selectedWaypoint.PreviousWaypoint.NextWaypoint = waypoint;
            
        }

        waypoint.NextWaypoint = selectedWaypoint;
        selectedWaypoint.PreviousWaypoint = waypoint;
        
        waypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        Selection.activeGameObject = waypoint.gameObject;
    }


    private void CreateWaypoint()
    {
        GameObject waypointObj = new GameObject("Waypoint" + WaypointRoot.childCount, typeof(Waypoint));
        waypointObj.transform.SetParent(WaypointRoot, false);
        Waypoint waypoint = waypointObj.GetComponent<Waypoint>();
        if (WaypointRoot.childCount > 1)
        {
            waypoint.PreviousWaypoint = WaypointRoot.GetChild(WaypointRoot.childCount - 2).GetComponent<Waypoint>();
            waypoint.PreviousWaypoint.NextWaypoint = waypoint;
            waypoint.transform.position = waypoint.PreviousWaypoint.transform.position;
            waypoint.transform.forward = waypoint.PreviousWaypoint.transform.forward;
            
        }

        Selection.activeObject = waypoint.gameObject;

    }

}
