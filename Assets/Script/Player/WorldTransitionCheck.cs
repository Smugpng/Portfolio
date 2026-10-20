using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
[ExecuteInEditMode]
public class WorldTransitionCheck : MonoBehaviour
{

    public bool canUp, canDown;
    public GameObject up, down;

    private void Update()
    {
        canUp = CheckTeleport(up.transform.position);
        canDown = CheckTeleport(down.transform.position);
    }
    private bool CheckTeleport(Vector3 gylp)
    {
        if(Physics.CheckSphere(gylp, 1f,6))
        {
            return false;
        }
        if (!Physics.Raycast(gylp, Vector3.down, 200))
        {
            return false;
        }
        return true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(up.transform.position, 2);
        Gizmos.DrawSphere(down.transform.position, 2);
    }
}
