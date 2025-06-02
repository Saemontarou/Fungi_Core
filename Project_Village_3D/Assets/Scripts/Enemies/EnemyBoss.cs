using UnityEngine;
using UnityEngine.AI;
using Color = UnityEngine.Color;

public class EnemyBoss : MonoBehaviour
{
    private NavMeshAgent _navAgent;
    private Collider _collider;
    private Animator _animator;
    private int _damage = 100;
    
    private int _maxHealth = 100;
    private int _currentHealth;
    
    private enum EnemyState { Idle, Chasing, Attacking, Searching }
    private EnemyState _currentState = EnemyState.Idle;
    private Vector3 _walkPoint;
    
    private bool _walkPointSet;
    private bool _alreadyAttacked;
    
    public Transform player;
    public Transform rayCastPosition;
    public Transform pointRotation;
    public Transform enemyPoint;
    public Transform[] points;
    
    public LayerMask groundLayer, playerLayer;
    public Animator flyStones;
    public BossHealthBar BossHealthBar;
    
    public float sightRange;
    public float attackRange;
    public float timeBetweenAttacks;
    public float rotationSpeed = 5f;
    
    public AudioSource takeDamage;
    public AudioSource deadGolem;
    
    [SerializeField] private GameObject playerHealth;
    private PlayerHealth _takeDamage;
    
    RaycastHit hit;
    
    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        _collider = GetComponent<Collider>();
        
        if (playerHealth != null)
        {
            _takeDamage = playerHealth.GetComponent<PlayerHealth>();
        }
    }
    
    private void Update()
    {
        bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        
        switch (_currentState)
        {
            case EnemyState.Idle:
                if (!playerInSightRange && !playerInAttackRange)
                {
                    ReturnOnPoint();
                    {
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
                ChasePlayer();
                _animator.SetBool("Running", true);
                _animator.SetBool("Idle", false);

                if (playerInAttackRange)
                {
                    _currentState = EnemyState.Attacking;
                }
                break;

            case EnemyState.Attacking:
                AttackPlayer();
                _animator.SetBool("Running", false);
                _animator.SetBool("Idle", false);

                if (!playerInSightRange || !playerInAttackRange)
                {
                    _currentState = EnemyState.Idle;
                }
                break;

            case EnemyState.Searching:
                SearchPosition();
                _animator.SetBool("Running", true);
                _animator.SetBool("Idle", false);
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
    }

    private void SearchPosition()
    {
        if (!_navAgent.pathPending && _navAgent.remainingDistance < 0.5f) 
        {
            _currentState = EnemyState.Idle;
            _animator.SetBool("Idle", true);
            _alreadyAttacked = false;
        }
    }
    
    private Vector3 SearchPoints(Vector3 enemyPosition)
    {
        if (points.Length == 0) return Vector3.zero;
    
        Transform nearestPoint = null;
        float minSqrDistance = Mathf.Infinity;
    
        foreach (var point in points)
        {
            float distanceSqr = (point.position - enemyPosition).sqrMagnitude;
    
            if (minSqrDistance > distanceSqr)
            {
                minSqrDistance = distanceSqr;
                nearestPoint = point;
            }
        }
        
        return nearestPoint != null ? nearestPoint.position : Vector3.zero;
    }
    
    private void CallRayCast()
    {
        float rayDistance = sightRange;
        float angleRange = 160f;
        int rayCount = 64;
        for (int i = 0; i < rayCount; i++)
        {
            float angle = -angleRange / 2 + (angleRange / (rayCount - 1)) * i;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Vector3 direction = rotation * rayCastPosition.forward;

            //Debug.DrawRay(rayCastPosition.position, direction * rayDistance, Color.red);

            if (Physics.Raycast(rayCastPosition.position, direction, out hit, rayDistance))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    _currentState = EnemyState.Chasing;
                    break;
                }
            }
        }
    }
    
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
        
        else
        {
            _currentState = EnemyState.Searching;
            takeDamage.Play();
            Vector3 newPosition = SearchPoints(transform.position);
            _navAgent.SetDestination(newPosition);
        }
        
        BossHealthBar.UpdateBossHealthBar(_maxHealth, _currentHealth);
    }
    
    private void ReturnOnPoint()
    {
        if (!_walkPointSet)
        {
            _walkPoint = enemyPoint.position;
            _walkPointSet = true;
            _animator.SetBool("Running", true);
        }

        if (_walkPointSet)
        {
            _navAgent.SetDestination(_walkPoint);
        }
        
        Vector3 distanceToWalkPoint = transform.position - _walkPoint;
        
        if (distanceToWalkPoint.magnitude < 1f)
        {
            _walkPointSet = false;
            _animator.SetBool("Idle", true);
        }
    }
    
    private void ChasePlayer()
    {
        if (_navAgent.SetDestination(player.position))
        {
            _animator.SetBool("Running", true); 
        }
    }
   
  private void AttackPlayer()
  {
    _navAgent.SetDestination(transform.position);

    if (!_alreadyAttacked)
    {
        transform.LookAt(player.position);
        _alreadyAttacked = true;
        _animator.SetTrigger("AttackTwo");
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, 40f))
        {
            {
                _takeDamage.TakeDamage(_damage);
            }
        }
        
        _animator.SetBool("Idle", false);
      }
  }

    private void ResetAttack()
    {
        _alreadyAttacked = false;
        _animator.SetBool("Idle", false);
        
        if (!_navAgent.hasPath || _navAgent.velocity.magnitude < 0.1f)
        {
            _animator.SetBool("Idle", true);
        }
    }
    
    private void Die()
    {
        _animator.SetTrigger("Death");
        _navAgent.isStopped = true;
        _collider.enabled = false;
        flyStones.Play("FallingStones");
        deadGolem.Play();
        Destroy(gameObject, 5f);
        ActionManager.CreepBossDeath();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}