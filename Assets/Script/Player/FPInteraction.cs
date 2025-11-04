using UnityEngine;

public class FPInteraction : MonoBehaviour
{
    [SerializeField] private Transform interationPoint;
    [SerializeField] private float interationRad = .5f;
    [SerializeField] private LayerMask interactionMask;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;
    public bool isInteracting;
    public GameObject currentInteraction;
    [SerializeField] private bool canInteract = true;
    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interationPoint.position, interationRad, colliders, interactionMask);
        if (numFound > 0)
        {
            
            var interactable = colliders[0].GetComponent<IInteractable>();
            if (canInteract)
            {
                FPInterationUI.instance.SetUp(interactable.UIPosition, interactable.InteractionPromp);
            }
            

            if (interactable != null && isInteracting && currentInteraction == null && canInteract )
            {
                FPInterationUI.instance.HideUI();
                InteractionDelay();
                interactable.Interact(this);
                currentInteraction = interactable.Test();
            }
            else if(interactable != null && currentInteraction != null && isInteracting && canInteract)
            {
                InteractionDelay();
                interactable.Next(this);
                //currentInteraction = null;
                FPInterationUI.instance.HideUI();
            }
        }
        else
        {
            FPInterationUI.instance.HideUI();
            if(currentInteraction!=null)
            {
                var lastInteraction = currentInteraction.GetComponent<IInteractable>();
                lastInteraction.Disengage(this);
                currentInteraction = null;
            }
        }
    }
    public void InteractionDelay()
    {
        canInteract = false;
        Invoke("ResetInteraction", 1);
    }
    public void ResetInteraction()
    {
        canInteract = true;
    }

    public void TryInteract()
    {
        Debug.Log(colliders.ToString());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interationPoint.position, interationRad);
    }
}
