using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent NavAgent;

    public AudioSource _golemLostTarget;
    public AudioSource _golemFindTarget;
    
    private GameObject Player;
    
    public Transform[] WayPoints;
    public int CurrentPatch;

    public enum State {Patrol, Stay, Chase};
    public State EnemyState;

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
            if (EnemyState == State.Patrol)
            {
                gameObject.GetComponent<Animator>().SetBool("Move", true);
                NavAgent.SetDestination(WayPoints[CurrentPatch].transform.position);
                float patchDistance = Vector3.Distance(WayPoints[CurrentPatch].transform.position, gameObject.transform.position);
                if (patchDistance < 2)
                {
                    CurrentPatch++;
                    CurrentPatch = CurrentPatch % WayPoints.Length;
                }
            }

            if (EnemyState == State.Stay)
            {
                gameObject.GetComponent<Animator>().SetBool("Move", false);
            }

            if (EnemyState == State.Chase)
            {
                gameObject.GetComponent<Animator>().SetBool("Move", true);
                if (gameObject.GetComponent<FieldOfView>().canSeePlayer == false)
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
                EnemyState = State.Patrol;
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