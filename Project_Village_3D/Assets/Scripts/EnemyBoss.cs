using UnityEngine;
using UnityEngine.AI;
using Color = UnityEngine.Color;


public class EnemyBoss : MonoBehaviour
{
    public int Maxhealth = 100;
    private int CurrentHealth;
    
    public BossHealthBar BossHealthBar;
    
    

    public NavMeshAgent navAgent;
    public Transform player;
    public LayerMask groundLayer, playerLayer;
    public float walkPointRange;
    public float timeBetweenAttacks;
    public float sightRange;
    public float attackRange;
    private Animator _animator;
    // public ParticleSystem hitEffect;
    private Collider collider;
    private int damage = 10;

    public Transform enemyPoint;
    public Transform[] Points;

    public Transform pointRotation; //
    public float rotationSpeed = 5f; // How fast to turn
    public float faceThreshold = 5f; //
    private bool isReturning = false;//
    
    private enum EnemyState { Idle, Chasing, Attacking, Searching } //
    private EnemyState currentState = EnemyState.Idle; //
    
    
    private Vector3 walkPoint;
    private bool walkPointSet;
    private bool alreadyAttacked;
    
    RaycastHit hit;

    public Transform rayCastPosition;
    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        CurrentHealth = Maxhealth;
        collider = GetComponent<Collider>();
    }

    // private void Update()
    // {
    //     bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
    //     bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
    //     
    //     
    //     
    //     if (!playerInSightRange && !playerInAttackRange)
    //     {
    //         ReturnOnPoint();
    //         _animator.SetBool("Running", true);
    //         _animator.SetBool("Idle", false);
    //
    //     }
    //     else if (playerInSightRange && !playerInAttackRange)
    //     {
    //         ChasePlayer();
    //         _animator.SetBool("Running", true);
    //         _animator.SetBool("Idle", false);
    //     }
    //     else if (playerInAttackRange && playerInSightRange)
    //     {
    //         AttackPlayer();
    //         _animator.SetBool("Running", false);
    //         _animator.SetBool("Idle", false);
    //     }
    //     
    //     if (!navAgent.hasPath || navAgent.velocity.magnitude < 0.1f)
    //     {
    //         _animator.SetBool("Running", false);
    //         if (!alreadyAttacked)
    //         {
    //             _animator.SetBool("Idle", true);
    //         }
    //     }
    // }
    
    private void Update() //
    {
        bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        
        
        switch (currentState)
        {
            case EnemyState.Idle:
                if (!playerInSightRange && !playerInAttackRange)
                {
                    ReturnOnPoint();
                    _animator.SetBool("Running", true);
                    _animator.SetBool("Idle", false);
                }
                else if (playerInSightRange && !playerInAttackRange)
                {
                    CallRayCast();
                }
                break;

            case EnemyState.Chasing:
                ChasePlayer();
                _animator.SetBool("Running", true);
                _animator.SetBool("Idle", false);

                if (playerInAttackRange)
                {
                    currentState = EnemyState.Attacking;
                }
                break;

            case EnemyState.Attacking:
                AttackPlayer();
                _animator.SetBool("Running", false);
                _animator.SetBool("Idle", false);

                if (!playerInSightRange || !playerInAttackRange)
                {
                    currentState = EnemyState.Idle; // Return to idle if conditions are not met
                }
                break;

            case EnemyState.Searching:
                SearchForNewPosition();
                _animator.SetBool("Running", true);
                _animator.SetBool("Idle", false);
                break;
        }
        
        if (!navAgent.hasPath || navAgent.velocity.magnitude < 0.1f)
        {
            _animator.SetBool("Running", false);
            if (!alreadyAttacked)
            {
                _animator.SetBool("Idle", true);
            }
        }
        
        if (isReturning) //
        {
            TurnTowardsTarget();

            // Optional: check if facing sufficiently close
            Vector3 directionToTarget = (pointRotation.position - enemyPoint.position).normalized;
            float angle = Vector3.Angle(enemyPoint.forward, directionToTarget);
            if (angle < faceThreshold)
            {
                Debug.Log("Enemy is now facing the object and can stay");
                // Set to stay or idle state here
                isReturning = false; // or keep as needed
            }
        }
    }

    private void TurnTowardsTarget() //
    {
        Vector3 direction = (pointRotation.position - enemyPoint.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyPoint.rotation = Quaternion.Slerp(enemyPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void SearchForNewPosition()
    {
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f) 
        {
            currentState = EnemyState.Idle; // Return to idle after reaching the new position
            _animator.SetBool("Idle", true);
            alreadyAttacked = false; // Reset attack flag after moving
        }
        
        // You can add additional logic here if needed while searching.
        // For example, you might want to play an animation or wait before returning.
    }
    
    private Vector3 SearchPoints(Vector3 enemyPosition)
    {
        if (Points.Length == 0) return Vector3.zero;
    
        Transform nearestPoint = null;
        float minSqrDistance = Mathf.Infinity;
    
        foreach (var point in Points)
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
        float angleRange = 160f; // degrees
        int rayCount = 64; // number of rays within the cone
        for (int i = 0; i < rayCount; i++)
        {
            float angle = -angleRange / 2 + (angleRange / (rayCount - 1)) * i;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Vector3 direction = rotation * rayCastPosition.forward;

            Debug.DrawRay(rayCastPosition.position, direction * rayDistance, Color.red);

            if (Physics.Raycast(rayCastPosition.position, direction, out hit, rayDistance))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    Debug.Log("ENEMY SEE PLAYER");
                    currentState = EnemyState.Chasing;
                    break;
                }
            }
        }
    }
    
    
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
        else //
        {
            currentState = EnemyState.Searching; // Change state to searching for a new position
            Vector3 newPosition = SearchPoints(transform.position); // Find nearest point
            navAgent.SetDestination(newPosition); // Move to that point
            Debug.Log($"Moving to new position at {newPosition}");
        }
        
        BossHealthBar.UpdateBossHealthBar(Maxhealth, CurrentHealth);
    }
    
    
    private void ReturnOnPoint()
    {
        if (!walkPointSet)
        {
            
            walkPoint = enemyPoint.position;
            walkPointSet = true;
            _animator.SetBool("Running", true);
            
        }

        if (walkPointSet)
        {
            navAgent.SetDestination(walkPoint);
        }
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        
        if (distanceToWalkPoint.magnitude < 1f)
        {
            isReturning = true; //
            walkPointSet = false;
            _animator.SetBool("Idle", true);
        }
    }
    
    private void ChasePlayer()
{
    if(navAgent.SetDestination(player.position));
    _animator.SetBool("Running", true);
    //navAgent.isStopped = false; // Add this line
    
}
   
  private void AttackPlayer()
{
    navAgent.SetDestination(transform.position);

    if (!alreadyAttacked)
    {
        transform.LookAt(player.position);
        alreadyAttacked = true;
        
        _animator.SetTrigger("Attack");
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
        RaycastHit hit;
        
        //if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
        if (Physics.Raycast(transform.position, transform.forward, out hit, 40f))
        {
            // if (hit.transform.CompareTag("Player"))
            {
                PlayerHealth.Instance.TakeDamage(damage);
                Debug.Log("ENEMY ATTACK");
            }

            /*
                YOU CAN USE THIS TO GET THE PLAYER HUD AND CALL THE TAKE DAMAGE FUNCTION

            PlayerHUD playerHUD = hit.transform.GetComponent<PlayerHUD>();
            if (playerHUD != null)
            {
               playerHUD.takeDamage(damage);
            }
             */
        }
        _animator.SetBool("Idle", false);
    }
}

    private void ResetAttack()
    {
        alreadyAttacked = false;
        
        _animator.SetBool("Idle", false);
        
        if (!navAgent.hasPath || navAgent.velocity.magnitude < 0.1f)
        {
            _animator.SetBool("Idle", true);
        }
    }
    public void Die()
    {
        _animator.SetTrigger("Death"); 
        Destroy(gameObject, 100f);
        navAgent.isStopped = true;
        collider.enabled = false;
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



// private Vector3 SearchPoints()
// {
//     if (Points.Length == 0) return Vector3.zero;
//     Transform nearstPoint = null;
//     float minSqrDistance = Mathf.Infinity;
//
//     foreach (var point in Points)
//     {
//         Vector3 distance = point.position = enemyPoint.position;
//         float distaceSqr = distance.sqrMagnitude;
//         if (minSqrDistance > distaceSqr)
//         {
//             minSqrDistance = distaceSqr;
//             nearstPoint = point;
//         }
//     }
//
//     return nearstPoint.position;
// }