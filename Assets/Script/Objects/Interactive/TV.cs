using Player;
using Unity.Cinemachine;
using UnityEngine;

public class TV : MonoBehaviour, IInteractable
{

    [SerializeField] private string prompt;
    [SerializeField] private CinemachineCamera cam;
    [SerializeField] private GameObject position;
    public string InteractionPromp => prompt;
    public Transform UIPosition => position.transform;

    public bool Disengage(FPInteraction interactor)
    {
        cam.Priority = 1;
        FPControler.instance.isStunned = false;
        return true;
    }

    public bool Interact(FPInteraction interactor)
    {
        cam.Priority = 10;
        FPControler.instance.isStunned = true;
        return true;
    }

    public GameObject Test()
    {
        return this.gameObject;
    }
}
