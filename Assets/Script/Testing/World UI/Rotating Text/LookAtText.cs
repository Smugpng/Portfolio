using Player;
using UnityEngine;

public class LookAtText : MonoBehaviour
{
    [SerializeField]private Transform player;
   

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - player.position);
        }
    }
}
