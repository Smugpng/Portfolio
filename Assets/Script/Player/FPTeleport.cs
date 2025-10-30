using System.Collections;
using Player;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class FPTeleport : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera mainCam;

    [Header("Components")]
    [SerializeField] private GameObject teleportVisual;
    public float teleportDistance = 25;
    public bool isAiming = false;
    [SerializeField] private LayerMask masks;
    [SerializeField] private ParticleSystem warpingEffect;

    [Header("Balance")]
    [SerializeField] private float delay;
    private bool canDash = true;
    private void FixedUpdate()
    {
        if (canDash && isAiming)
        {
            teleportVisual.SetActive(true);
            Vector3 camPos = mainCam.transform.position;
            RaycastHit hit;
            if(Physics.Raycast(camPos,mainCam.transform.forward, out hit, teleportDistance, masks))
            {
                teleportVisual.transform.position = hit.point;
            }
            else
            {
                teleportVisual.transform.position = camPos + mainCam.transform.forward* teleportDistance;
            }
        }
        else
        {
            teleportVisual.SetActive(false);
        }

    }

    [SerializeField] private FPControler player;
    public void Dash()
    {
        Vector3 location = teleportVisual.transform.position;
        if (canDash && isAiming)
        {
            StartCoroutine(DASH(location));
        }
        else
        {
            //PlaySound
        }
    }
    public IEnumerator DASH(Vector3 location)
    {
        warpingEffect.Play();
        player.isDisabled = true;
        canDash = false;
        gameObject.transform.position = location;
        Invoke("ResetDash", delay);
        yield return new WaitForSeconds(.1f);
        
        player.isDisabled = false;
    }
    public void ResetDash()
    {
        canDash = true;

    }
}

