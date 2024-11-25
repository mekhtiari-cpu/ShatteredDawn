using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    NavMeshAgent myNav;

    [SerializeField] GameObject randomPointGo;
    [SerializeField] Transform target;
    [SerializeField] LayerMask walkablePath;

    enum EnemyState { Patrol, Chase, Scan}

    [SerializeField] EnemyState myState = EnemyState.Patrol;

    [Header("Patrol Variables")]
    [SerializeField] float patrolRadius;
    [SerializeField] bool isPatrolling;
    [SerializeField] bool isMovingTowardsPoint = false;
    Vector3 randomPoint;

    void Start()
    {
        myNav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        HandleState();
    }

    //Based on enemy state, behave accordingly
    void HandleState()
    {
        switch (myState)
        {
            case (EnemyState.Patrol):
                Debug.Log("Patrolling");
                Patrol();
                break;
            case (EnemyState.Chase):
                Debug.Log("Chasing");
                break;
            case (EnemyState.Scan):
                Debug.Log("Scanning");
                break;
            default:
                Debug.Log("Default");
                break;
        }
    }

    void Patrol()
    {
        if (isPatrolling)
        {
            if(Vector3.Distance(transform.position, randomPoint) <= 30)
            {
                isPatrolling = false;
                isMovingTowardsPoint = false;
                myNav.speed = 0f;
            }
            else
            {
                if (!isMovingTowardsPoint)
                {
                    myNav.SetDestination(randomPoint);
                    myNav.speed = 140f;
                    isMovingTowardsPoint = true;
                }
            }
        }
        else
        {
            randomPoint = (Random.insideUnitSphere * patrolRadius) + transform.position;
            randomPoint.y = transform.position.y-3f;
            GameObject randomPointInstance = Instantiate(randomPointGo, randomPoint, Quaternion.identity);
            
            while (Physics.OverlapSphere(randomPointInstance.transform.position, 10, walkablePath).Length == 0)
            {
                randomPoint = (Random.insideUnitSphere * patrolRadius) + transform.position;
                randomPoint.y = transform.position.y-3f;
                randomPointInstance = Instantiate(randomPointGo, randomPoint, Quaternion.identity);
            }
            Destroy(randomPointInstance);
            isPatrolling = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }
}
