using Player;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    public static Blinking instance;
    [SerializeField] private Animator Animator;
    [SerializeField] private GameObject postProcess;
    private FPControler playerCon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        playerCon = FPControler.instance;
    }

    public void BlinkCompleted()
    {
        playerCon.Teleport();
        if (postProcess.activeInHierarchy != true)
        {
            //postProcess.gameObject.SetActive(true);
        }
        else
        {
            //postProcess.gameObject.SetActive(false);
        }
    }
    public void TELEPORT()
    {
        
    }
    public void StartBlink()
    {
        Animator.SetTrigger("blink");
    }
}
