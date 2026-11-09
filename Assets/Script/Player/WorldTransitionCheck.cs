using Player;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
[ExecuteInEditMode]
public class WorldTransitionCheck : MonoBehaviour
{
    public GameObject player;
    public enum WorldState //Player world states
    {
        Real,
        Dream,
    }

    public WorldState CurrentState
    {
        get
        {
            float pos = gameObject.transform.parent.position.y;
            if (pos > 500)
            {
                return WorldState.Dream;
            }
            else return WorldState.Real;
                
        }
        
    }

    [SerializeField] private LayerMask walls;

    private Vector3 nextLocation;
    public bool canSwap() //Checks teleport location based off world state returns true if can teleport
    {
        
        if (CurrentState == WorldState.Dream)
        {
            nextLocation = gameObject.transform.position + new Vector3(0,-10000,0);
        }
        else
        {
            nextLocation = gameObject.transform.position + new Vector3(0, 10000, 0);
        }
        
        if (Physics.OverlapSphere(nextLocation, .5f, walls).Length != 0)
        {
            return false;
        }
        else return true;
    }

    

    public void SwapWorlds() //Teleports to uppder and lower worlds based of world state
    {
        if(CurrentState == WorldState.Dream)
        {
            player.transform.position = new Vector3(player.transform.position.x,player.transform.position.y - 10000, player.transform.position.z);
            FPControler.instance.isDreaming = false;
            WorldShifting.instance.ToRealWorld();
        }
        else if(CurrentState == WorldState.Real) 
        {
            player.transform.position = new Vector3(player.transform.position.x,player.transform.position.y + 10000, player.transform.position.z);
            FPControler.instance.isDreaming = true;
            WorldShifting.instance.ToDreamWorld();
        }
    }
    /* private void OnDrawGizmos()
    {
        if(canSwap())
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        
        Gizmos.DrawSphere(nextLocation, .5f);
    } */
}
