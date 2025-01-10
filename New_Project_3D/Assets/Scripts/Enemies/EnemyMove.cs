using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private Transform _target;
    private NavMeshAgent _agent;

    private float _lookDistance;
    
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _target = PlayerManager.instance.player.transform;
        _lookDistance = gameObject.GetComponent<FieldOfView>()._viewRadius;

    }
  
   
    private void Update()
    {
        float distance = Vector3.Distance(_target.position, transform.position);
        if (distance <= _lookDistance)
        {
            _agent.SetDestination(_target.position);
            if (distance <= _agent.stoppingDistance)
            {
                Debug.Log("OK");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _lookDistance);
    }
}