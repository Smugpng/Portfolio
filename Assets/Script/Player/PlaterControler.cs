using UnityEngine;
using UnityEngine.InputSystem;
public class PlaterControler : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 move, mouseLook;
    private Vector3 rotationTarget;
    [SerializeField] private Camera main;
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }
    private void Update()
    {
        RaycastHit hit;
        Ray ray = main.ScreenPointToRay(transform.position);
        if (Physics.Raycast(ray, out hit))
        {
            rotationTarget = hit.point;
        }
        MovePlayerWithAim();
    }
    //Player Movement
  
    public void MovePlayerWithAim()
    {
        var lookPos = rotationTarget - transform.position;
        lookPos.y = 0f;
        var rotation = Quaternion.LookRotation(lookPos);

        Vector3 aimDirection = new Vector3(rotationTarget.x,0f, rotation.z);
        if(aimDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, .25f);
        }
        Vector3 movement = new Vector3(move.x, 0f, move.y);
        transform.Translate(movement*speed*Time.deltaTime,Space.World);
    }

}
