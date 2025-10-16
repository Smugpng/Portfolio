using UnityEngine;

public interface IInteractable
{
    public string InteractionPromp { get; }
    public Transform UIPosition { get; }
    public bool Interact(FPInteraction interactor);
    public bool Disengage(FPInteraction interactor);

    public GameObject Test();
}
