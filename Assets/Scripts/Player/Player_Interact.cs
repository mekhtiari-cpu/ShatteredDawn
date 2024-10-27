using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{

    [SerializeField] float interactionRadius;
    [SerializeField] LayerMask interactableLayer;

    private HashSet<Interactable> currentlyInteracting = new HashSet<Interactable>();

    private float interactionCheckInterval = 0.1f;
    private float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interactionCheckInterval)
        {
            DetectNearbyInteractables();
            timer = 0;
        }
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
