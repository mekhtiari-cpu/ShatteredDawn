using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] bool playerInView = false;
    [SerializeField] bool playerObstructed;
    [SerializeField] Transform enemyTransform;
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector3 rayOffset;
    [SerializeField] LayerMask validRayObjects;
    [SerializeField] float rayRange;
    [SerializeField] TMP_Text objectsHit;

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.position) < rayRange + 2f)
        {
            RayCheck();
        }
    }

    //Check whether the player is obstructed by anything
    bool RayCheck ()
    {
        Vector3 dirToPlayer = (playerTransform.position) - (enemyTransform.transform.position + rayOffset);
        Ray ray = new Ray(enemyTransform.transform.position + rayOffset, dirToPlayer.normalized * rayRange);
        Debug.DrawRay(enemyTransform.transform.position + rayOffset, dirToPlayer.normalized * rayRange, Color.red);
        RaycastHit[] hits = Physics.RaycastAll(ray, rayRange, validRayObjects);

        string objectsHitText = "Objects hit: ";

        foreach (RaycastHit hit in hits)
        {
            // Append the name of the object hit to the string
            objectsHitText += hit.collider.name + ", ";
        }

        // Remove the trailing comma and space if there are any hits
        if (hits.Length > 0)
        {
            objectsHitText = objectsHitText.TrimEnd(',', ' ');
        }
        else
        {
            objectsHitText += "None";
        }

        objectsHit.text = objectsHitText;


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
