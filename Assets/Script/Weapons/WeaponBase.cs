using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public interface IWeaponBase
{
    [Header("Weapon Details")]
    public string Name { get; }
    public string Description { get; }
    public Transform weaponTip { get; }

    public Transform playerCamera { get; }

    [Header("Weapon Stats")]
    public int maxAmmo { get; }
    public int currentAmmo { get; set; }
    public float reloadTime { get; }
    public float timeBetweenShots { get; }
    public float damage {  get; }
    public float swapTime { get; }
    public float weaponSpread { get; }
    public float weaponRange { get; }

    [Header("Cosmetics")]
    public TrailRenderer bulletTrail { get; }

    [Header("Other Types")]
    public bool canShoot { get; set;  }
    
    public void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            --currentAmmo;
            RaycastHit hit;
            Vector3 fwd = //WeaponSpread
            playerCamera.transform.forward + playerCamera.TransformDirection(new Vector3(Random.Range(-weaponSpread, weaponSpread), Random.Range(-weaponSpread, weaponSpread)));
            if(Physics.Raycast(playerCamera.transform.position, fwd, out hit, weaponRange))
            {
                BulletTrail(hit);
            }
            else
            {
                Vector3 endPoint = playerCamera.transform.position + fwd * 100;
                hit.point = endPoint;
                BulletTrail(hit);
            }
            
            
            ShootDelay();
        }
        else
        {
            Debug.Log("Wait a bit");
        }
    }
    public void ShootDelay();

    public void Reload()
    {
        currentAmmo = maxAmmo;
    }
    public void BulletTrail(RaycastHit hit);
}
