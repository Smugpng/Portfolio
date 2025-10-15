using UnityEngine;

public interface IInteractable
{
    public string InteractionPromp { get; }
    public bool Interact(FPInteraction interactor);
    public bool Disengage(FPInteraction interactor);

    public GameObject Test();
}
