using UnityEngine;

public class StoneManipulate : MonoBehaviour
{
    public float grabPower = 10f;
    public float throwPower = 10f;
    public float rayDistance = 5f;
    
    public Camera playerCamera;
    public Transform takePoint;
    public AudioSource throwStones;
    public AudioSource takeStones;
    
    private ThrowStones _throw;
    
    private bool Grab = false;
    private bool Throw = false;
    
    RaycastHit hit;

    private void OnEnable()
    {
        ActionManager.SmallStones += TakeSmallStones;
    }

    private void Awake()
    {
        _throw = GetComponent<ThrowStones>();
        _throw._currentStones = _throw._poolObject.poolSize;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(ray, out hit, rayDistance);
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
                hit.rigidbody.linearVelocity = playerCamera.ScreenPointToRay(Input.mousePosition).direction * throwPower;
                throwStones.Play();
                Throw = false;
            }
        }
        
        //Debug.DrawRay(ray.origin, ray.direction * 10, UnityEngine.Color.red);
    }

    private void TakeSmallStones()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, rayDistance, LayerMask.GetMask("Rock")))
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _throw._currentStones = _throw._poolObject.poolSize;
                takeStones.Play();
            }
        }
    }
    
    private void OnDisable()
    {
        ActionManager.SmallStones -= TakeSmallStones;
    }
}