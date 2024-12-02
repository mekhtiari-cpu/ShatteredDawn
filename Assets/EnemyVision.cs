using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public bool playerInView = false;
    [SerializeField] bool playerNotObstructed;
    [SerializeField] Transform playerTransform;

    private void Update()
    {
        RayCheck();
    }

    void RayCheck ()
    {
        Ray ray = new Ray(transform.position, playerTransform.position - transform.position);
        Debug.DrawRay(transform.position, playerTransform.position - transform.position, Color.red);
        RaycastHit[] hits = Physics.RaycastAll(ray, 100f);
        foreach(RaycastHit hit in hits)
        {
            Debug.Log(hit.collider);
        }
        if(hits.Length == 1 && hits[0].collider.CompareTag("Player"))
        {
            playerNotObstructed = true;
        }
        else
        {
            playerNotObstructed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        playerInView = other.CompareTag("Player") && playerNotObstructed;
    }
}
