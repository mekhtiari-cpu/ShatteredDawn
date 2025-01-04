using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    [Header("General Variables")]
    NavMeshAgent myNav;
    [SerializeField] GameObject randomPointGo;
    [SerializeField] Transform player;
    [SerializeField] LayerMask walkablePath;
    [SerializeField] float[] movementSpeeds;

    Animator animator;
    private AnimatorClipInfo[] animatorinfo;
    [SerializeField] private string current_animation;


    enum EnemyState { Patrol, Chase, Scan}
    [SerializeField] EnemyState myState = EnemyState.Patrol;

    [Header("Patrol Variables")]
    [SerializeField] float patrolRadius;
    [SerializeField] bool isPatrolling;
    [SerializeField] bool isMovingTowardsPoint = false;
    [SerializeField] GameObject lastPoint;
    Vector3 randomPoint;

    [Header("Scan Variables")]
    [SerializeField] EnemyVision vision;
    [SerializeField] bool hasAlreadyScanned;
    [SerializeField] float minScanTime;
    [SerializeField] float maxScanTime;
    [SerializeField] float minScanSpeed;
    [SerializeField] float maxScanSpeed;

    int toPlayerOrRand;
    float scanSpeed;
    float randomScanTime;
    float endScanTime;

    void Start()
    {
        myNav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        myState = EnemyState.Patrol;
    }

    private void FixedUpdate()
    {
        animatorinfo = animator.GetCurrentAnimatorClipInfo(0);
        current_animation = animatorinfo[0].clip.name;
        if(current_animation == "Hit")
        {
            return;
        }

        HandleState();
    }

    public void Die()
    {
        animator.SetInteger("rand",Random.Range(1, 3));
        animator.SetBool("IsDead", true);
        Destroy(gameObject, 5f);
    }

    public void Hit()
    {
        animator.Play("Hit");
    }

    //Based on enemy state, behave accordingly
    void HandleState()
    {
        CheckEyesight();
        switch (myState)
        {
            case (EnemyState.Patrol):
                //Check whether enemy has finished patrolling
                if (Patrol())
                {
                    myState = EnemyState.Scan;
                    //Debug.Log("Finished patrolling");
                }
                break;

            case (EnemyState.Chase):
                Debug.Log("Chasing");
                Chase();
                break;

            case (EnemyState.Scan):
                //Check whether enemy has finished scanning
                if (Scan())
                {
                    myState = EnemyState.Patrol;
                    //Debug.Log("Scanning");
                }
                break;

            default:
                Debug.Log("Default");
                break;
        }

        if(myState == EnemyState.Chase)
        {
            animator.SetBool("isChasing", true);
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", myState == EnemyState.Patrol || myState == EnemyState.Chase);
        }
    }

    //Constantly check whether the player is in the enemy's vision
    void CheckEyesight()
    {
        if(vision.playerInView)
        {
            myState = EnemyState.Chase;
            Debug.Log("Chasing");
        }
    }

    //Patrol: Enemy walks to a random location in search of player
    bool Patrol()
    {
        if(myState == EnemyState.Chase)
        {
            return true;
        }

        if (isPatrolling)
        {
            if(Vector3.Distance(transform.position, randomPoint) <= 5f)
            {
                isPatrolling = false;
                isMovingTowardsPoint = false;
                Destroy(lastPoint);
                myNav.speed = 0f;
                return true;
            }
            else
            {
                if (!isMovingTowardsPoint)
                {
                    myNav.SetDestination(randomPoint);
                    myNav.speed = movementSpeeds[0];
                    isMovingTowardsPoint = true;
                }
                return false;
            }
        }
        else
        {
            randomPoint = (Random.insideUnitSphere * patrolRadius) + transform.position;
            randomPoint.y = transform.position.y;
            GameObject randomPointInstance = Instantiate(randomPointGo, randomPoint, Quaternion.identity);

            if (Physics.OverlapSphere(randomPointInstance.transform.position, 10, walkablePath).Length == 0)
            {
                while (Physics.OverlapSphere(randomPointInstance.transform.position, 10, walkablePath).Length == 0)
                {
                    Destroy(randomPointInstance.gameObject);
                    randomPoint = (Random.insideUnitSphere * patrolRadius) + transform.position;
                    randomPoint.y = transform.position.y - 3f;
                    randomPointInstance = Instantiate(randomPointGo, randomPoint, Quaternion.identity);
                }
            }

            lastPoint = randomPointInstance;
            isPatrolling = true;
            return false;
        }
    }

    //Scan: Enemy scans the environment in search of the player
    bool Scan()
    {
        if (myState == EnemyState.Chase)
        {
            return true;
        }

        if (!hasAlreadyScanned)
        {
            hasAlreadyScanned = true;
            myNav.speed = movementSpeeds[2];
            randomScanTime = Random.Range(minScanTime, maxScanTime);
            toPlayerOrRand = Random.Range(1, 101);
            scanSpeed = Random.Range(minScanSpeed, maxScanSpeed) * GetRandomDirection();
            endScanTime = Time.time + randomScanTime;
            return false;
        }
        else
        {
            if (Time.time >= endScanTime)
            {
                //Debug.Log("Ending scan: " + (Time.time >= endScanTime));
                hasAlreadyScanned = false;
                return true;
            }
            else
            {
                //Debug.Log("Scanning environment for: " + randomScanTime + "s ");
                if(toPlayerOrRand % 2 == 0)
                {
                    transform.Rotate(Vector3.up * scanSpeed, Space.World);
                }
                else
                {
                    Debug.Log("Rotating towards player");
                    Vector3 dirToPlayer = player.position - transform.position;

                    dirToPlayer.y = 0;

                    float rotationSpeed = 0.65f;
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirToPlayer), Time.deltaTime * rotationSpeed);

                }

                return false;
            }
        }
    }

    //Chase: Chase down the player
    void Chase()
    {
        myNav.speed = movementSpeeds[1];
        myNav.acceleration = 500;
        myNav.SetDestination(player.position);
    }

    int GetRandomDirection()
    {
        int randNum = Random.Range(1, 101) % 2;
        if (randNum % 2 == 0)
            return 1;
        else
            return -1;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }
}
