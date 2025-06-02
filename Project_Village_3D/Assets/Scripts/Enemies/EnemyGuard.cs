using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGuard : MonoBehaviour
{
    private NavMeshAgent _navAgent;
    private Animator _animator;
    
    private enum EnemyState { Idle, Chasing, Attacking }
    private EnemyState _currentState = EnemyState.Idle;
    private Vector3 _initialPosition;
    private float _lastAttackTime = 0f;
    
    public Transform player;
    public Transform pointRotation;
    public LayerMask playerLayer;
    
    public float sightRange = 20f;
    public float attackRange = 2f;
    public float rayDistance = 20f;
    public float rotationSpeed = 5f;
    public float attackCooldown = 1.5f;
    
    private bool _isReturning = false;
    private bool _alreadyAttacked = false;
    
    private void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _initialPosition = transform.position;
        ReturnOnPoint();
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
                if (!playerInSightRange && !playerInAttackRange)
                {
                    ReturnOnPoint();
                    Vector3 direction = pointRotation.position - transform.position;
                    direction.y = 0;
                    if (direction.magnitude > 1f)
                    {
                        Quaternion targetRotation = Quaternion.LookRotation(direction);
                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    }
                    
                    _animator.SetBool("Running", true);
                    _animator.SetBool("Idle", false);
                }
                
                else if (playerInSightRange && !playerInAttackRange)
                {
                    Vector3 direction = player.position - transform.position;
                    direction.y = 0;
                    if (direction.magnitude > 1f)
                    {
                        Quaternion targetRotation = Quaternion.LookRotation(direction);
                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    }
                    
                    CallRayCast();
                }
                break;

            case EnemyState.Chasing:
                if (playerInSightRange && distanceToPlayer > attackRange - 2f)
                {
                    ChasePlayer();
                }
                
                else if (distanceToPlayer <= attackRange - 2f)
                {
                    _currentState = EnemyState.Attacking;
                }
                
                else
                {
                    _currentState = EnemyState.Idle;
                }
                
                _animator.SetBool("Running", true);
                _animator.SetBool("Idle", false);
                break;

            case EnemyState.Attacking:
                if (distanceToPlayer > attackRange - 2f || !playerInSightRange)
                {
                    _currentState = EnemyState.Chasing;
                }
                
                else
                {
                    AttackPlayer();
                }
                break;
        }
        
        if (!_navAgent.hasPath || _navAgent.velocity.magnitude < 0.1f)
        {
            _animator.SetBool("Running", false);
            if (!_alreadyAttacked)
            {
                _animator.SetBool("Idle", true);
            }
        }
        
        if (!playerInSightRange && !playerInAttackRange && _currentState != EnemyState.Idle && !_isReturning)
        {
            ReturnOnPoint();
            _currentState = EnemyState.Idle;
        }
    }

    private void CallRayCast()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 1.5f, directionToPlayer.normalized, out hit, rayDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                _currentState = EnemyState.Chasing;
            }
        }
    }

    private void ChasePlayer()
    {
        _navAgent.SetDestination(player.position);
        FaceTarget(player.position);
    }

    private void FaceTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    private void ReturnOnPoint()
    {
        _isReturning = true;
        _navAgent.SetDestination(_initialPosition);

        StartCoroutine(CheckArrival());

        _animator.SetBool("Running", true);
        _animator.SetBool("Idle", false);
        _currentState = EnemyState.Idle;
    }

    private IEnumerator CheckArrival()
    {
        while (Vector3.Distance(transform.position, _initialPosition) > 0.5f)
        {
            yield return null;
        }

        _isReturning = false;
        _animator.SetBool("Running", false);
        
        yield break;
    }

   private void AttackPlayer()
   {
       _navAgent.isStopped = true;
       
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

   private void PerformAttackAnimation()
   {
       _animator.SetTrigger("Attack");
   }

   private void OnTriggerEnter(Collider other)
   {
       if (other.CompareTag("Player"))
       {
           AttackPlayer();
       }
   }

   private void OnTriggerExit(Collider other)
   {
       if (other.CompareTag("Player"))
       {
           _navAgent.isStopped = false;
           CancelInvoke("PerformAttackAnimation");
       }
   }
}