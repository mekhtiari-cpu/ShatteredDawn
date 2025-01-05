using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] bool playerInView = false;
    [SerializeField] bool playerObstructed;
    [SerializeField] Transform enemyTransform;
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector3 rayOffset;
    [SerializeField] LayerMask validRayObjects;
    [SerializeField] float rayRange;

    private void Start()
    {
        playerTransform = FindFirstObjectByType<Player_Movement>().transform;    
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, playerTransform.position) < rayRange + 2f)
        {
            RayCheck();
        }
    }

    //Check whether the player is obstructed by anything
    bool RayCheck ()
    {
        Vector3 dirToPlayer = (playerTransform.position) - (enemyTransform.transform.position + rayOffset);
        float distanceToPlayer = Vector3.Distance(enemyTransform.position, playerTransform.position);

        Ray ray = new Ray(enemyTransform.transform.position + rayOffset, dirToPlayer.normalized * distanceToPlayer);
        RaycastHit[] hits = Physics.RaycastAll(ray, rayRange, validRayObjects);

        if (hits.Length > 0)
        {
            if (hits[0].collider.CompareTag("Player"))
            {
                playerObstructed = false;
                return false;
            }
            else
            {
                playerObstructed = true;
                return true;
            }
        }
        else
        {
            playerObstructed = true;
            return true;
        }
    }

    public bool GetConditionsForChase()
    {
        return playerInView && !playerObstructed;
    }

    //Check whether the player is actually in view or not
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInView = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInView = false;
        }
    }
}
