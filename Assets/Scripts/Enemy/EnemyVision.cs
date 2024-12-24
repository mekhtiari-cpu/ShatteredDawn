using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public bool playerInView = false;
    [SerializeField] bool playerNotObstructed;
    [SerializeField] Transform enemyTransform;
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector3 rayOffset;

    private void Update()
    {
        RayCheck();
    }

    void RayCheck ()
    {
        Ray ray = new Ray(enemyTransform.transform.position + rayOffset, (playerTransform.position) - (enemyTransform.transform.position + rayOffset));
        Debug.DrawRay(enemyTransform.transform.position + rayOffset, (playerTransform.position) - (enemyTransform.transform.position + rayOffset), Color.red);
        RaycastHit[] hits = Physics.RaycastAll(ray, 200f);
        //Debug.Log(hits.Length);
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
