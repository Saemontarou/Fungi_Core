using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float viewRadius = 5f; // Radius of the field of view
    [Range(0, 360)]
    public float viewAngle = 110f; // Angle of the field of view
    public LayerMask targetMask; // Layer mask for targets (e.g., player)
    public LayerMask obstacleMask; // Layer mask for obstacles

    private Transform player;
    private NavMeshAgent agent;
    private bool playerInSightRange;

    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;

    private Animator _animator;

    private Vector3 patrolPoint;
    public float patrolPointRange = 10f;

    public float attackRange = 1.5f; // Range within which the enemy can attack
    private bool isAttacking = false;
    
    
    public int Maxhealth = 100;
    private int CurrentHealth;

    public BossHealthBar BossHealthBar;
    
    
    public float retreatDistance = 5f;
    [SerializeField] private int damage = 10;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming your player has the tag "Player"
        SetNewPatrolPoint();

        _animator = GetComponentInChildren<Animator>();
        CurrentHealth = Maxhealth;
    }

    private void Update()
    {
        DetectPlayer();

        if (playerInSightRange)
        {
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                AttackPlayer();
            }
            else
            {
                ChasePlayer();
            }
        }
        else
        {
            Patrol();
        }
    }

    private void DetectPlayer()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        foreach (Collider target in targetsInViewRadius)
        {
            Transform targetTransform = target.transform;
            Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);

                // Check if there are no obstacles between the enemy and the player
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    playerInSightRange = true;
                    return; // Player detected
                }
            }
        }

        playerInSightRange = false; // Player not detected
    }

    private void ChasePlayer()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
        
        isAttacking = false; // Reset attacking state when chasing
    }

    private void AttackPlayer()
    {
        if (!isAttacking)
        {
            Debug.Log("Attacking Player!");

            isAttacking = true;
            
            PlayerHealth.Instance.TakeDamage(damage); //
            
            // Optionally stop moving while attacking
            agent.SetDestination(transform.position);
            _animator.SetTrigger("Attack");
        }
        
        // You can add a cooldown or delay here to prevent continuous attacking.
    }

    private void Patrol()
    {
        agent.speed = patrolSpeed;

        if (Vector3.Distance(transform.position, patrolPoint) < 1f)
        {
            SetNewPatrolPoint();
        }

        agent.SetDestination(patrolPoint);
        _animator.SetBool("Run", true);
    }

    private void SetNewPatrolPoint()
    {
        float randomX = Random.Range(-patrolPointRange, patrolPointRange);
        float randomZ = Random.Range(-patrolPointRange, patrolPointRange);

        patrolPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        NavMeshHit hit;

        if (NavMesh.SamplePosition(patrolPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            patrolPoint = hit.position; // Ensure the patrol point is on the NavMesh
        }

        Debug.Log("New Patrol Point: " + patrolPoint);
    }
    
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        
        if (CurrentHealth <= 0)
        {
            Die();
        }

        // if (CurrentHealth > 0)
        // {
        //     ManeuverOnHit();
        // }
        // else
        // {
        //     Die();
        //     
        // }
        
        BossHealthBar.UpdateBossHealthBar(Maxhealth, CurrentHealth);
    }

    // public void ManeuverOnHit()
    // {
    //     //_animator.SetBool("IsRetreating", true); // Set retreating animation state
    //
    //     Vector3 retreatDirection = (transform.position - player.position).normalized; 
    //     Vector3 retreatPosition = transform.position + retreatDirection * retreatDistance;
    //
    //     NavMeshHit hit;
    //    
    //     if (NavMesh.SamplePosition(retreatPosition, out hit, 1.0f, NavMesh.AllAreas))
    //     {
    //         agent.SetDestination(hit.position); // Move to a new position away from the player
    //         Debug.Log("Enemy is retreating!");
    //     }
    //     
    // }

    public void StopAgent()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }


    public void Die()
    {
        _animator.SetTrigger("Death");
        StopAgent();
        ActionManager.CreepBossDeath();
        Destroy(gameObject, 100f);
        //StoneTurret.Instance.TurretHide();
        //StoneAltarRise.Instance.RiseAltar();
    }

   private void OnDrawGizmos()
   {
       Gizmos.color = Color.yellow;
       Gizmos.DrawWireSphere(transform.position, viewRadius);

       Vector3 frontLeft = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward * viewRadius;
       Vector3 frontRight = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward * viewRadius;

       Gizmos.DrawLine(transform.position, transform.position + frontLeft);
       Gizmos.DrawLine(transform.position, transform.position + frontRight);

       Gizmos.color = Color.red;
       Gizmos.DrawWireSphere(transform.position, attackRange); // Visualize attack range

       foreach (Collider target in Physics.OverlapSphere(transform.position, viewRadius))
       {
           if ((obstacleMask & (1 << target.gameObject.layer)) != 0)
           {
               Gizmos.DrawLine(transform.position, target.transform.position);
           }
       }
   }
}