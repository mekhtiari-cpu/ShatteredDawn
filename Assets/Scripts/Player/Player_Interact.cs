using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{

    [SerializeField] float interactionRadius;
    [SerializeField] LayerMask interactableLayer;

    private HashSet<Interactable> currentlyInteracting = new HashSet<Interactable>();

    void Update()
    {
        DetectNearbyInteractables();
    }

    void DetectNearbyInteractables()
    {
        Collider[] nearbyInteractables = Physics.OverlapSphere(transform.position, interactionRadius, interactableLayer);

        HashSet<Interactable> interactablesThisFrame = new HashSet<Interactable>();

        foreach (Collider obj in nearbyInteractables)
        {
            Interactable interactable = obj.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactablesThisFrame.Add(interactable);

                if (!currentlyInteracting.Contains(interactable))
                {
                    interactable.Interact();
                }
            }
        }

        foreach (var interactable in currentlyInteracting)
        {
            if (!interactablesThisFrame.Contains(interactable))
            {
                interactable.StopInteracting();
            }
        }
        currentlyInteracting = interactablesThisFrame;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
