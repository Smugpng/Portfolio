using Player;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    public static Blinking instance;
    [SerializeField] private Animator Animator;
    [SerializeField] private GameObject postProcess;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void BlinkCompleted()
    {
        FPControler.instance.Teleport();
        if (postProcess.activeInHierarchy != true)
        {
            postProcess.gameObject.SetActive(true);
        }
        else
        {
            postProcess.gameObject.SetActive(false);
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
