using UnityEngine;

public class TakeStoneRay : MonoBehaviour
{
    public float grabPower = 10f;
    public float throwPower = 10f;
    public float RayDistance = 5f;

    public AudioSource _throwStone;

    private bool Grab = false;
    private bool Throw = false;
    
    public Transform takePoint;
    public Camera _camera;
    
    RaycastHit hit;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(ray, out hit, RayDistance);
            if (hit.rigidbody)
            {
                Grab = true;
            }
        }
       
        if (Input.GetMouseButtonDown(1))
        {
            if (Grab)
            {
                Grab = false;
                Throw = true;
            }
        }

        if (Grab)
        {
            if (hit.rigidbody)
            {
                hit.rigidbody.linearVelocity = (takePoint.position - (hit.transform.position + hit.rigidbody.centerOfMass)) * grabPower;
            }
        }

        if (Throw)
        {
            if (hit.rigidbody)
            {
                hit.rigidbody.linearVelocity = _camera.ScreenPointToRay(Input.mousePosition).direction * throwPower;
                _throwStone.Play();
                Throw = false;
            }
        }
        
        Debug.DrawRay(ray.origin, ray.direction * 10, UnityEngine.Color.red);
    }
}