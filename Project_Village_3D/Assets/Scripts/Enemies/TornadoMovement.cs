using UnityEngine;
using Random = UnityEngine.Random;

public class TornadoMovement : MonoBehaviour
{
    public float moveRadius = 20f;
    public float moveSpeed = 10f;
    private Vector3 targetPoint;
    public Collider actionLimit;

    void Start()
    {
        SetNewPoint();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            SetNewPoint();
        }
    }
    
    void SetNewPoint()
    {
        Vector2 randomDir = Random.insideUnitCircle * moveRadius;
        Vector3 newTarget = new Vector3(transform.position.x + randomDir.x, transform.position.y + 0f, transform.position.z + randomDir.y);
        
        RaycastHit hit;
        if (Physics.Raycast(newTarget, Vector3.down, out hit, 10f))
        {
            if (actionLimit.bounds.Contains(hit.point))
            {
                newTarget.y = hit.point.y;
                targetPoint = newTarget;
            }
            
            else
            {
                SetNewPoint();
            }
        }
        
        else
        {
            newTarget.y = transform.position.y;
            targetPoint = newTarget;
        }
    }
}