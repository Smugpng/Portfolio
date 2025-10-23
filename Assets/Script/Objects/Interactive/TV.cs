using Player;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TV : MonoBehaviour, IInteractable
{

    [SerializeField] private string prompt;
    [SerializeField] private CinemachineCamera cam;
    [SerializeField] private GameObject position;
    [SerializeField] private GameObject firstSelection;
    private FPInteraction current;

    [Header("Fast Travel Points and Pos must have the same amount in Array!!")]
    [SerializeField] private GameObject[] fastTravelPoints;
  
    public string InteractionPromp => prompt;
    public Transform UIPosition => position.transform;
   
    public bool Disengage(FPInteraction interactor)
    {
        cam.Priority = 1;
        FPControler.instance.isStunned = false;
        EventSystem.current.SetSelectedGameObject(null);
        current = null;
        return true;
    }

    public bool Interact(FPInteraction interactor)
    {
        cam.Priority = 10;
        FPControler.instance.isStunned = true;
        EventSystem.current.SetSelectedGameObject(firstSelection);
        current = interactor;
        return true;
    }
    public bool Next(FPInteraction interactor)
    {
        Disengage(interactor);
        return true;
    }
    public GameObject Test()
    {
        return this.gameObject;
    }
   
    public void FastTravel(int num)
    {
        FPControler.instance.FastTravel(fastTravelPoints[num]);
        cam.Priority = 1;
        FPControler.instance.isStunned = false;
        current.currentInteraction = null;
        current.ResetInteraction();
        EventSystem.current.SetSelectedGameObject(null);
    }

    
}
