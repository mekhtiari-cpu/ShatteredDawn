using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Interact : MonoBehaviour
{
    public static Player_Interact instance { get; private set; }

    [SerializeField] float interactionRadius;
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] BrokenCar brokenCar;

    private HashSet<Interactable> currentlyInteracting = new HashSet<Interactable>();

    private float interactionCheckInterval = 0.1f;
    private float timer = 0;

    private void Awake()
    {
        //Singleton
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

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

    public void ToggleCarStatusUI(InputAction.CallbackContext context)
    {
        Debug.Log("Toggling car status");
        if(brokenCar.GetPlayerNearCar())
            UI_Manager.instance.GetCarUI().ToggleCarInfoText();
    }

    public bool PlayerNearCar()
    {
        return brokenCar.GetPlayerNearCar();
    }

    public void ReturnKeyItem(KeyItem keyItem)
    {
        brokenCar.ReturnKeyItem(keyItem);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
