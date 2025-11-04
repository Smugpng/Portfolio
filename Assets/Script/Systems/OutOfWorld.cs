using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfWorld : MonoBehaviour
{
    private void OnValidate()
    {
        if(player == null) player = FPControler.instance.gameObject;
    }
    void Start()
    {
        InvokeRepeating(nameof(PlayerLocationCheck), 5, 15);   
    }
    public GameObject player;

    void PlayerLocationCheck()
    {
        if(player.transform.position.y < this.transform.position.y)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else Debug.Log("Player in safe Area");
    }
}
