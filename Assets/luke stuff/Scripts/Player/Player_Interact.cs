using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{

    [SerializeField] float interactionRadius;
    [SerializeField] LayerMask interactableLayer;

    void Update()
    {
        DetectNearbyInteractables();
    }

    void DetectNearbyInteractables()
    {
        Collider[] nearbyInteractables = Physics.OverlapSphere(transform.position, interactionRadius, interactableLayer);
        foreach(Collider obj in nearbyInteractables)
        {
            obj.GetComponent<Interactable>().Interact();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
