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
    [SerializeField] Player_Movement pm;
    [SerializeField] LayerMask walkablePath;
    [SerializeField] float[] movementSpeeds;
    [SerializeField] AnimationClip hit;
    [SerializeField] bool isStaggered;
    [SerializeField] bool isDead;
    [SerializeField] ZombieAudio myAudio;
    [SerializeField] GameObject myHurtbox;
    [SerializeField] Collider myCollider;
    bool hasPlayedSeenAudio;
    bool hasPlayedDeathAudio;
    bool hasPlayedIdleAudio;
    [SerializeField]bool hasPlayedChaseAudio;

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
    [SerializeField] float patrolTimeout;
    [SerializeField] float patrolTimeoutTime;
    bool hasSetPatrolTime;
    Vector3 randomPoint;

    [Header("Scan Variables")]
    [SerializeField] EnemyVision vision;
    [SerializeField] bool hasAlreadyScanned;
    [SerializeField] float minScanTime;
    [SerializeField] float maxScanTime;
    [SerializeField] float minScanSpeed;
    [SerializeField] float maxScanSpeed;

    [Header("Chase Variables")]
    [SerializeField] float deAggroDistance;

    int toPlayerOrRand;
    float scanSpeed;
    float randomScanTime;
    float endScanTime;

    void Start()
    {
        myNav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        myState = EnemyState.Patrol;
        pm = FindFirstObjectByType<Player_Movement>();
        player = FindAnyObjectByType<Player_Movement>().gameObject.transform;
        hasPlayedSeenAudio = false;
        hasPlayedDeathAudio = false;
        myAudio.PlayIdleAudio();
        patrolTimeoutTime = patrolTimeout;
    }

    private void FixedUpdate()
    {
        animatorinfo = animator.GetCurrentAnimatorClipInfo(0);
        current_animation = animatorinfo[0].clip.name;
        if(current_animation == "Hit")
        {
            return;
        }
        if(!pm.GetIsCrouched() && Vector3.Distance(player.position, transform.position) < 8f)
        {
            myState = EnemyState.Chase;
        }
        HandleState();
    }

    //Logic for when the zombie dies
    public void Die()
    {
        if(!hasPlayedDeathAudio)
        {
            myAudio.StopPlayingChaseAudio();
            myAudio.PlayDeathAudio();
            hasPlayedDeathAudio = true;
        }
        animator.SetInteger("rand",Random.Range(1, 3));
        animator.SetBool("IsDead", true);
        myHurtbox.SetActive(false);
        myCollider.enabled = false;
        isDead = true;
        myNav.speed = movementSpeeds[2];

        PlayerController pc = FindFirstObjectByType<PlayerController>();
        if (pc)
        {
            pc.zombiesKilled++;
            PlayerQuestHandler questHandler = GameManager.instance.playerQuest;
            if (questHandler != null)
            {
                foreach (Quest quest in questHandler.activeQuests)
                {
                    if (quest.isCompleted || quest.turnedIn)
                    {
                        continue;
                    }

                    foreach (QuestCompletionCondition condition in quest.completionConditions)
                    {
                        if (condition.completionType == QuestCompletionType.KillEnemies)
                        {
                            condition.RegisterEnemyKilled(pc.zombiesKilled);

                            questHandler.UpdateQuestDisplay();
                            questHandler.CheckQuestCompletionConditions();
                        }
                    }
                }
            }
        }

        Destroy(gameObject, 5f);
    }

    //Logic for when the zombie is hit
    public void Hit()
    {
        isStaggered = true;
        myAudio.PlayHurtAudio();
        myAudio.StopPlayingChaseAudio();
        StartCoroutine(WaitForHitDuration());
        animator.Play("Hit");
        myNav.speed = movementSpeeds[2];
    }

    //Stagger time when hit
    IEnumerator WaitForHitDuration()
    {
        myState = EnemyState.Chase;
        yield return new WaitForSeconds(hit.length);
        if(!isDead)
            myAudio.PlayChaseAudio();
        hasPlayedChaseAudio = false;
        isStaggered = false;
    }

    //Based on enemy state, behave accordingly
    void HandleState()
    {
        if(!isDead)
        {
            CheckEyesight();
            switch (myState)
            {
                case (EnemyState.Patrol):
                    //Check whether enemy has finished patrolling
                    if (Patrol())
                    {
                        myState = EnemyState.Scan;
                    }
                    break;

                case (EnemyState.Chase):
                    Chase();
                    break;

                case (EnemyState.Scan):
                    //Check whether enemy has finished scanning
                    if (Scan())
                    {
                        myState = EnemyState.Patrol;
                    }
                    break;
            }

            if (myState == EnemyState.Chase)
            {
                animator.SetBool("isChasing", true);
                animator.SetBool("isWalking", false);
            }
            else
            {
                animator.SetBool("isWalking", myState == EnemyState.Patrol || myState == EnemyState.Chase);
            }
        }
    }

    //Constantly check whether the player is in the enemy's vision
    void CheckEyesight()
    {
        if(vision.GetConditionsForChase())
        {
            if(!hasPlayedSeenAudio)
            {
                myAudio.PlayPlayerSeenAudio();
                myAudio.StopPlayingIdleAudio();
                hasPlayedSeenAudio = true;
                
            }
            hasPlayedIdleAudio = false;
            myState = EnemyState.Chase;
        }
        else
        {
            if (Vector3.Distance(player.position, transform.position) > deAggroDistance && myState == EnemyState.Chase)
            {
                myState = EnemyState.Patrol;
                hasPlayedChaseAudio = false;
                hasPlayedSeenAudio = false;
                animator.SetBool("isChasing", false);
                myAudio.StopPlayingChaseAudio();
                if(!hasPlayedIdleAudio)
                {
                    myAudio.PlayIdleAudio();
                    hasPlayedIdleAudio = true;
                }
            }
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
            if (!hasSetPatrolTime)
            {
                patrolTimeoutTime = patrolTimeout;
                hasSetPatrolTime = true;
            }
            else
            {
                patrolTimeoutTime -= Time.deltaTime;
            }

            if(Vector3.Distance(transform.position, randomPoint) <= 5f || patrolTimeoutTime <= 0f)
            {
                patrolTimeoutTime = patrolTimeout;
                isPatrolling = false;
                isMovingTowardsPoint = false;
                Destroy(lastPoint);
                myNav.speed = 0f;
                hasSetPatrolTime = false;
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
                hasAlreadyScanned = false;
                return true;
            }
            else
            {
                if(toPlayerOrRand % 2 == 0)
                {
                    transform.Rotate(Vector3.up * scanSpeed, Space.World);
                }
                else
                {
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
        if (!isStaggered)
        {
            if(!hasPlayedChaseAudio)
            {
                if(!isDead)
                {
                    myAudio.PlayChaseAudio();
                    hasPlayedChaseAudio = true;
                }
            }

            GameSettingsManager gsm = GameSettingsManager.Instance;
            float modifier = 1f;
            if (gsm)
            {
                switch (gsm.Settings.Difficulty)
                {
                    case "Easy":
                        modifier = 0.8f;
                        break;

                    case "Normal":
                        modifier = 1;
                        break;

                    case "Hard":
                        modifier = 1.25f;
                        break;

                    default:
                        modifier = 1;
                        break;

                }
            }

            myNav.speed = movementSpeeds[1] * modifier;
            myNav.acceleration = 1000;
            myNav.SetDestination(player.position);
        }
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
