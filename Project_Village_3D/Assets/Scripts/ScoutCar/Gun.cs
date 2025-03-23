using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject CorePrefab;
    public float Power = 200;
    public TrajectoryRender Trajectory;
   
    //[SerializeField] private AudioSource _shoot;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        new Plane(Vector3.up, transform.position).Raycast(ray, out float enter);
        Vector3 mouseInWorld = ray.GetPoint(enter);

        Vector3 speed = (mouseInWorld - transform.position) * Power;
        transform.rotation = Quaternion.LookRotation(speed);
        Trajectory.ShowTrajectory(transform.position, speed);
        if (Input.GetMouseButtonDown(0))
        {
            Rigidbody core = Instantiate(CorePrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            core.AddForce(speed, ForceMode.VelocityChange);
         
            //_shoot.Play();
        }
    }
}
