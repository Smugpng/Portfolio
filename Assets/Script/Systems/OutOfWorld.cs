using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfWorld : MonoBehaviour
{
    [SerializeField]private Vector3 safePos;
    private void OnValidate()
    {
        if(player == null) player = FPControler.instance.gameObject;
    }
    void Start()
    {
        InvokeRepeating(nameof(PlayerLocationCheck), 1, 5);   
    }
    public GameObject player;

    void PlayerLocationCheck()
    {
        if(player.transform.position.y < this.transform.position.y)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            if (safePos != null)
            {
                player.transform.position = safePos;
                Debug.Log("Teleporting");
            }
            else
            {
               
            }  
        }
        else
        {
            Debug.Log("Player in safe Area");
            safePos = player.transform.position;
        }
    }
}
