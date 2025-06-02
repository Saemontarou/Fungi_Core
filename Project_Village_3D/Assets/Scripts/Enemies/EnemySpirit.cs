using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpirit : MonoBehaviour
{
    private NavMeshAgent _navAgent;
    
    private enum EnemyState { Idle, Patrolling, Chasing, Attacking }
    private EnemyState _currentState = EnemyState.Idle;
    private Vector3 _initialPosition;
    private float _lastAttackTime = -Mathf.Infinity;

    public Animator animator;
    public Transform player;
    public Transform patrolPoint;
    public LayerMask playerLayer;
    
    public float sightRange = 20f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public float rotationSpeed = 5f;
    public float patrolSpeed = 3.5f;
    public float chaseSpeed = 5f;

    private void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        _initialPosition = transform.position;

        SetState(EnemyState.Patrolling);
        _navAgent.speed = patrolSpeed;
        Patrol();
    }

    private void Update()
    {
        bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        
        float distanceToPlayer = Mathf.Infinity;
        
        if (player != null && player.gameObject.activeInHierarchy)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.position);
        }

        switch (_currentState)
        {
            case EnemyState.Idle:
                if (playerInSightRange && !playerInAttackRange)
                {
                    SetState(EnemyState.Chasing);
                }
                
                else
                {
                    Patrol();
                }
                break;

            case EnemyState.Patrolling:
                if (playerInSightRange && !playerInAttackRange)
                {
                    SetState(EnemyState.Chasing);
                }
                
                else if (!_navAgent.pathPending && _navAgent.remainingDistance < 0.5f)
                {
                    Patrol();
                }
                break;

            case EnemyState.Chasing:
                if (!playerInSightRange || distanceToPlayer > sightRange * 1.2f)
                {
                    ReturnToStart();
                }
                
                else if (distanceToPlayer <= attackRange)
                {
                    SetState(EnemyState.Attacking);
                }
                
                else
                {
                    Chase();
                }
                break;

            case EnemyState.Attacking:
                if (distanceToPlayer > attackRange)
                {
                    SetState(EnemyState.Chasing);
                }
                
                else
                {
                    Attack();
                }
                break;
        }
    }

    private void SetState(EnemyState newState)
    {
        _currentState = newState;

        switch (newState)
        {
            case EnemyState.Idle:
                _navAgent.isStopped = true;
                break;

            case EnemyState.Patrolling:
                _navAgent.isStopped = false;
                _navAgent.speed = patrolSpeed;
                Patrol();
                break;

            case EnemyState.Chasing:
                _navAgent.isStopped = false;
                _navAgent.speed = chaseSpeed;
                break;

            case EnemyState.Attacking:
                _navAgent.isStopped = true;
                FaceTarget(player.position);
                animator.SetTrigger("SpiritAttack");
                _lastAttackTime = Time.time;
                break;
        }
    }

    private void Patrol()
    {
        if (patrolPoint != null)
        {
            _navAgent.SetDestination(patrolPoint.position);
        }
        
        else
        {
            _navAgent.SetDestination(transform.position);
        }
    }

    private void Chase()
    {
        _navAgent.SetDestination(player.position);
        FaceTarget(player.position);
    }

    private void FaceTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void ReturnToStart()
    {
        SetState(EnemyState.Idle);
        StartCoroutine(ReturningRoutine());
    }

    private IEnumerator ReturningRoutine()
    {
        _navAgent.isStopped = false;
        _navAgent.speed = patrolSpeed;
        _navAgent.SetDestination(_initialPosition);

        while (Vector3.Distance(transform.position, _initialPosition) > 0.5f)
        {
            yield return null;
        }
        
        animator.Play("SpiritStoneDown");
        SetState(EnemyState.Patrolling);
        
        yield break;
    }

    private void Attack()
    {
       if (Time.time - _lastAttackTime >= attackCooldown)
       {
           animator.SetTrigger("SpiritAttack");
           _lastAttackTime = Time.time;
       }
       
       FaceTarget(player.position); 
   }

   private void OnDrawGizmosSelected()
   {
       Gizmos.color = Color.blue;
       Gizmos.DrawWireSphere(transform.position, sightRange);
       Gizmos.color = Color.red;
       Gizmos.DrawWireSphere(transform.position, attackRange);
   }
}