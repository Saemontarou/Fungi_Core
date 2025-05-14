using UnityEngine;
using UnityEngine.AI;
using Color = UnityEngine.Color;


public class EnemyAI_GPT : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public Transform player;
    public LayerMask groundLayer, playerLayer;

    public float walkPointRange;
    public float timeBetweenAttacks;
    public float sightRange;
    public float attackRange;

    private Animator _animator;

    public Transform enemyPoint;

    private Vector3 walkPoint;
    private bool walkPointSet;
    private bool alreadyAttacked;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            ReturnOnPoint();
            _animator.SetBool("isWalking", true); // Set walking animation
        }
        else if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
            _animator.SetBool("isWalking", true); // Set walking animation
        }
        else if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
            _animator.SetBool("isWalking", false); // Stop walking animation
        }
        
        // If not moving, set idle animation
        if (!navAgent.hasPath || navAgent.velocity.magnitude < 0.1f)
        {
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isIdle", true); // Set idle animation
        }
    }

    private void ReturnOnPoint()
    {
        if (!walkPointSet)
        {
            walkPoint = enemyPoint.position + new Vector3(Random.Range(-walkPointRange, walkPointRange), 0, Random.Range(-walkPointRange, walkPointRange));
            walkPointSet = true;
        }

        navAgent.SetDestination(walkPoint);

        if (Vector3.Distance(transform.position, walkPoint) < 1f)
        {
            walkPointSet = false; // Reset walk point when reached
            _animator.SetBool("isIdle", true); // Set idle animation when at the point
        }
    }

    private void ChasePlayer()
    {
        navAgent.SetDestination(player.position);
        
        // Ensure the enemy is facing the player while chasing
        transform.LookAt(player.position);
        
        _animator.SetBool("isWalking", true); // Set walking animation
    }

    private void AttackPlayer()
    {
        navAgent.SetDestination(transform.position); // Stop moving

        if (!alreadyAttacked)
        {
            transform.LookAt(player.position);
            alreadyAttacked = true;

            _animator.SetTrigger("Attack"); // Trigger attack animation
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
            {
                PlayerHealth playerHealth = hit.transform.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(10); // Example damage value
                    Debug.Log("ENEMY ATTACK");
                }
            }
            
            _animator.SetBool("isIdle", false); // Ensure idle is not set during attack
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        
        // Reset animations after attack
        _animator.SetBool("isIdle", false);
        
         // If not moving after attack, set to idle state.
         if (!navAgent.hasPath || navAgent.velocity.magnitude < 0.1f)
         {
             _animator.SetBool("isIdle", true);
         }
     }

     private void OnDrawGizmosSelected()
     {
         Gizmos.color = Color.red;
         Gizmos.DrawWireSphere(transform.position, attackRange);
         
         Gizmos.color = Color.cyan;
         Gizmos.DrawWireSphere(transform.position, sightRange);
         
         Gizmos.color = Color.green; // For visualizing walk points
         Gizmos.DrawWireSphere(enemyPoint.position, walkPointRange);
     }
}






































// public class EnemyAI_GPT : MonoBehaviour
// {
//     
//     public NavMeshAgent navAgent;
//     public Transform player;
//     public LayerMask groundLayer, playerLayer;
//     
//     public float walkPointRange;
//     public float timeBetweenAttacks;
//     public float sightRange;
//     public float attackRange;
//     
//     private Animator _animator;
//     
//     public Transform enemyPoint;
//     
//     
//     private Vector3 walkPoint;
//     private bool walkPointSet;
//     private bool alreadyAttacked;
//     
//     private void Awake()
//     {
//         _animator = GetComponentInChildren<Animator>();
//         player = GameObject.FindGameObjectWithTag("Player").transform;
//         navAgent = GetComponent<NavMeshAgent>();
//     }
//     
//     private void Update()
//     {
//         bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
//         bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
//
//         if (!playerInSightRange && !playerInAttackRange)
//         {
//             ReturnOnPoint();
//         }
//         else if (playerInSightRange && !playerInAttackRange)
//         {
//             ChasePlayer();
//         }
//         else if (playerInAttackRange && playerInSightRange)
//         {
//             AttackPlayer();
//         }
//     }
//     
//     private void ReturnOnPoint()
//     {
//         if (!walkPointSet)
//         {
//             
//             walkPoint = enemyPoint.position;
//             walkPointSet = true;
//             
//         }
//
//         if (walkPointSet)
//         {
//             navAgent.SetDestination(walkPoint);
//         }
//
//         Vector3 distanceToWalkPoint = transform.position - walkPoint.normalized;
//         
//         if (distanceToWalkPoint.magnitude < 1f)
//         {
//             walkPointSet = false;
//         }
//     }
//     
//
//    private void ChasePlayer()
// {
//     if(navAgent.SetDestination(player.position));
//     
// }
//
//
// private void AttackPlayer()
// {
//     navAgent.SetDestination(transform.position);
//
//     if (!alreadyAttacked)
//     {
//         transform.LookAt(player.position);
//         alreadyAttacked = true;
//
//         _animator.SetTrigger("Attack");
//         Invoke(nameof(ResetAttack), timeBetweenAttacks);
//         RaycastHit hit;
//
//         if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
//         {
//
//             Debug.Log("ENEMY ATTACK"); // //
//
//         }
//     }
// }
//
// private void ResetAttack()
//     {
//         alreadyAttacked = false;
//         _animator.SetBool("Attack", false);
//     }
//
//    
//     
//
//     private void OnDrawGizmosSelected()
//     {
//         Gizmos.color = Color.red;
//         Gizmos.DrawWireSphere(transform.position, attackRange);
//         Gizmos.color = Color.cyan;
//         Gizmos.DrawWireSphere(transform.position, sightRange);
//     }
// }