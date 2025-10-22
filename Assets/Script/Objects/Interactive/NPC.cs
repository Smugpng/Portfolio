using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactPrompt;
    [SerializeField] private GameObject position;
    public string InteractionPromp => interactPrompt;

    public Transform UIPosition => position.transform;

    [SerializeField][TextArea] private string[] npcDialouge;
    [SerializeField][TextArea] private string npcName;
    private int currLine = 0;

    public bool Disengage(FPInteraction interactor)
    {
        DialougeText.instance.DisableUI();

        return true;
    }

    public bool Interact(FPInteraction interactor)
    {
        Debug.LogWarning("WORKING");
        Talk();
        return true;
    }
    
    public void Talk()
    {
        if(currLine >= npcDialouge.Length)
        {
            DialougeText.instance.DisableUI();
            currLine = 0;
        }
        else
        {
            DialougeText.instance.ActivateText(npcDialouge[currLine], npcName);
            currLine++;
        }
    }

    public GameObject Test()
    {
        return this.gameObject;
    }

   
}
