using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    private NavMeshAgent _navAgent;
    private Animator _animator;
    
    private enum State { Patrolling, Chasing, Attacking }
    private State _currentState = State.Patrolling;
    private int _currentWaypointIndex = 0;
    private float _lastAttackTime = 0f;
    
    public float sightRange = 10f;
    public float attackRange = 2f;
    public float fieldOfViewAngle = 110f;
    public float attackCooldown = 1.5f;
    
    public Transform[] waypoints;
    public Transform player;
    
    private void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _navAgent.destination = waypoints[_currentWaypointIndex].position;
    }
    
    private void Update()
    {
        if (player == null)
            return;
    
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        bool playerInFOV = PlayerInFOV();
    
        switch (_currentState)
        {
            case State.Patrolling:
                Patrol();
                if (playerInFOV && distanceToPlayer <= sightRange)
                {
                    _currentState = State.Chasing;
                }
                break;
    
            case State.Chasing:
                Chase();
                if (distanceToPlayer <= attackRange)
                {
                    _currentState = State.Attacking;
                    _animator.SetBool("Running", false);
                }
                else if (!playerInFOV || distanceToPlayer > sightRange + 5f)
                {
                    _currentState = State.Patrolling;
                    _animator.SetBool("Running", true);
                    _navAgent.destination = waypoints[_currentWaypointIndex].position;
                }
                break;
    
            case State.Attacking:
                Attack();
                if (distanceToPlayer > attackRange)
                {
                    _currentState = State.Chasing;
                    _animator.SetBool("Running", true);
                }
                break;
        }
    }
    
    private void Patrol()
    {
        _navAgent.isStopped = false;
        _animator.SetBool("Running", true);
        
        if (!_navAgent.pathPending && _navAgent.remainingDistance < 0.5f)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
            _navAgent.destination = waypoints[_currentWaypointIndex].position;
        }
    }
    
    private void Chase()
    {
        _navAgent.isStopped = false;
       
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Vector3 targetPosition = player.position - directionToPlayer * 4f;
        
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, 1.0f, NavMesh.AllAreas))
        {
            _navAgent.destination = hit.position;
        }
    }
    
    private void Attack()
    {
        _navAgent.isStopped = true;
        transform.LookAt(player);
        
        Vector3 direction = (player.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        
        if (Time.time - _lastAttackTime >= attackCooldown)
        {
            InvokeRepeating("PerformAttackAnimation", 0f, 1f);
            _lastAttackTime = Time.time;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Attack();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CancelInvoke("PerformAttackAnimation");
        }
    }
    
    private void PerformAttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }
    
    private bool PlayerInFOV()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
    
        if (angleToPlayer < fieldOfViewAngle * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 1.5f, directionToPlayer.normalized, out hit, sightRange))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        
        return false;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
        Gizmos.color = Color.blue;
        Vector3 origin = transform.position + Vector3.up * 1.5f;
        Vector3 forward = transform.forward;
        float halfFOV = fieldOfViewAngle * 0.5f;

        Vector3 leftDir = Quaternion.Euler(0, -halfFOV, 0) * forward;
        Vector3 rightDir = Quaternion.Euler(0, halfFOV, 0) * forward;

        Gizmos.DrawRay(origin, leftDir * sightRange);
        Gizmos.DrawRay(origin, rightDir * sightRange);
    }
}