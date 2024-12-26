using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyVision : MonoBehaviour
{
    public bool playerInView = false;
    [SerializeField] bool playerNotObstructed;
    [SerializeField] Transform enemyTransform;
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector3 rayOffset;
    [SerializeField] LayerMask validRayObjects;
    [SerializeField] float rayRange;
    [SerializeField] TMP_Text objectsHit;

    private void Update()
    {
        RayCheck();
    }

    void RayCheck ()
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

        foreach (RaycastHit hit in hits)
        {
            Debug.Log("Hit: " + hit.collider.name + ", Length: "+hits.Length);
        }

        if (hits.Length > 0)
        {
            if (hits[0].collider.CompareTag("Player"))
            {
                playerNotObstructed = true;
            }
            else
            {
                playerNotObstructed = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            Debug.Log(other.name);

        playerInView = other.CompareTag("Player") && playerNotObstructed;
    }
}
