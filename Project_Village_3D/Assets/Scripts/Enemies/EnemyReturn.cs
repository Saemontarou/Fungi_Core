using UnityEngine;
using UnityEngine.AI;

public class EnemyReturn : MonoBehaviour
{
    private NavMeshAgent NavAgent;

    public AudioSource _golemLostTarget;
    public AudioSource _golemFindTarget;
    
    private GameObject Player;
    
    public Transform WayPoint;

    public enum EnemyState {ReturnToPoint, Chase};
    public EnemyState EnemyBehavior;

    private Transform LastPoint;
    public bool CheckLastPoint;
    float EnemyWait;

    void Start()
    {
        NavAgent = gameObject.GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (CheckLastPoint == false)
        {
            if (EnemyBehavior == EnemyState.ReturnToPoint) 
            {
                NavAgent.SetDestination(WayPoint.transform.position);
                float patchDistance = Vector3.Distance(WayPoint.transform.position, gameObject.transform.position);
                if (patchDistance < 2)
                {
                    gameObject.GetComponent<Animator>().SetBool("Move", false);
                    // _golemFindTarget.Play();
                }
            }
            
            if (EnemyBehavior == EnemyState.Chase)
            {
                gameObject.GetComponent<Animator>().SetBool("Move", true);
                if (gameObject.GetComponent<FieldOfViewBeta>().canSeePlayer == false)
                {
                    LastPoint = Player.transform;
                    CheckLastPoint = true;
                    _golemLostTarget.Play();
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
            if (pointDistance < 1 || EnemyWait >= 0)
            {
                CheckLastPoint = false;
                EnemyBehavior = EnemyState.ReturnToPoint;
                EnemyWait = 0;
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("Move", true);
            }
        }

        float distanceToPlayer = Vector3.Distance(Player.transform.position, gameObject.transform.position);
        if (distanceToPlayer < 2)
        {
            Player.SetActive(false);
            GameManager.Instance.Lose();
        }
    }
}