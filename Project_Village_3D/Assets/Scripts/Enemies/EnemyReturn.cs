using System.Collections;
using UnityEngine;
using UnityEngine.AI;


// public class EnemyReturn : MonoBehaviour
// {
//     private NavMeshAgent NavAgent;
//
//     public AudioSource _golemLostTarget;
//     public AudioSource _golemFindTarget;
//
//     private GameObject Player;
//
//     public Transform WayPoint;
//
//     public enum EnemyState { ReturnToPoint, Chase, Attack }; //
//
//     public EnemyState EnemyBehavior;
//
//     private Transform LastPoint;
//
//     public bool CheckLastPoint;
//     float EnemyWait;
//
//     void Start()
//     {
//         NavAgent = gameObject.GetComponent<NavMeshAgent>();
//         Player = GameObject.FindGameObjectWithTag("Player");
//     }
//
//     void FixedUpdate()
//     {
//         if (CheckLastPoint == false)
//         {
//             if (EnemyBehavior == EnemyState.ReturnToPoint)
//             {
//                 NavAgent.SetDestination(WayPoint.transform.position);
//                 float patchDistance = Vector3.Distance(WayPoint.transform.position, gameObject.transform.position);
//                 if (patchDistance < 2)
//                 {
//                     gameObject.GetComponent<Animator>().SetBool("Move", false);
//                     // _golemFindTarget.Play();
//                 }
//             }
//
//             if (EnemyBehavior == EnemyState.Chase)
//             {
//                 gameObject.GetComponent<Animator>().SetBool("Move", true);
//                 if (gameObject.GetComponent<FieldOfViewBeta>().canSeePlayer == false)
//                 {
//                     LastPoint = Player.transform;
//                     CheckLastPoint = true;
//                     // _golemLostTarget.Play();
//                 }
//                 else
//                 {
//                     NavAgent.SetDestination(Player.transform.position);
//                 }
//             }
//         }
//
//         else
//         {
//             EnemyWait += 1 * Time.deltaTime;
//             float pointDistance = Vector3.Distance(LastPoint.transform.position, gameObject.transform.position);
//             if (pointDistance < 1 || EnemyWait > 0)
//             {
//                 CheckLastPoint = false;
//                 EnemyBehavior = EnemyState.ReturnToPoint;
//                 EnemyWait = 0;
//             }
//             else
//             {
//                 gameObject.GetComponent<Animator>().SetBool("Move", true);
//             }
//         }
//
//         float distanceToPlayer = Vector3.Distance(Player.transform.position, gameObject.transform.position);
//         if (distanceToPlayer < 2)
//         {
//             gameObject.GetComponent<Animator>().SetBool("Move", false); //
//             Player.SetActive(false);
//             GameManager.Instance.Lose();
//         }
//     }
// }

public class EnemyReturn : MonoBehaviour
{
    private NavMeshAgent NavAgent;

    //public AudioSource _golemLostTarget;
    //public AudioSource _golemFindTarget;

    public GameObject Player;
    
    public Transform WayPoint;

    public enum EnemyState { ReturnToPoint, Chase, Attack }; //

    public EnemyState EnemyBehavior;

    private Transform LastPoint;

    public bool CheckLastPoint;
    float EnemyWait;
    
    
    [Header("FIELD OF VIEW SETTINGS")] 
    public float radius;
    [Range(0, 360)] public float angle;
    
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public bool canSeePlayer;
    

    void Start()
    {
        NavAgent = gameObject.GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        
        StartCoroutine(FOVRoutine());
    }
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    void FixedUpdate()
    {
        if (canSeePlayer == true)
        {
            EnemyBehavior = EnemyState.Chase;
        }

        {
            if (CheckLastPoint == false)
            {
                if (EnemyBehavior == EnemyState.ReturnToPoint)
                {
                    NavAgent.SetDestination(WayPoint.transform.position);
                    float patchDistance = Vector3.Distance(WayPoint.transform.position, gameObject.transform.position);
                    if (patchDistance < 2)
                    {
                        //transform.LookAt(Vector3.left); //
                        gameObject.GetComponent<Animator>().SetBool("Running", false);
                        // _golemFindTarget.Play();
                        
                    }
                }

                if (EnemyBehavior == EnemyState.Chase)
                {
                    gameObject.GetComponent<Animator>().SetBool("Running", true);
                    if (canSeePlayer == false)
                    {
                        LastPoint = Player.transform;
                        CheckLastPoint = true;
                        // _golemLostTarget.Play();
                    }
                    else
                    {
                        NavAgent.SetDestination(Player.transform.position);
                    }
                }
            }

            else
            {
                EnemyWait += 1 * Time.deltaTime;
                float pointDistance = Vector3.Distance(LastPoint.transform.position, gameObject.transform.position);
                if (pointDistance < 1 || EnemyWait > 0)
                {
                    CheckLastPoint = false;
                    EnemyBehavior = EnemyState.ReturnToPoint;
                    EnemyWait = 0;
                }
                else
                {
                    gameObject.GetComponent<Animator>().SetBool("Running", true);
                }
            }
        }

        float distanceToPlayer = Vector3.Distance(Player.transform.position, gameObject.transform.position);
        if (distanceToPlayer < 2)
        {
            gameObject.GetComponent<Animator>().SetBool("Running", false); //
            Player.SetActive(false);
            GameManager.Instance.Lose();
        }
    }
    
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    canSeePlayer = true;
                }

                else
                {
                    canSeePlayer = false;
                }
            }
        }
    }
}