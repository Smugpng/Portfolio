using System.Collections;
using UnityEngine;

public class BaseGun : MonoBehaviour, IWeaponBase
{
    #region WeaponStats
    public string bgName;

    public string bgDescription;

    public Transform bgWeaponTip;

    public int bgMaxAmmo;

    public int bgCurrAmmo;

    public float bgReloadTime;

    public float bgTimeBetweenShots;

    public float bgDamage;

    public float bgSwapTime;

    public float bgWeaponSpread;

    public float bgWeaponRange;

    public Transform FpCamera;

    public TrailRenderer weaponTrail;

    public bool canShoot = true;
    #endregion
    #region Interface
    public string Name => bgName;

    public string Description => bgDescription;

    public Transform weaponTip => bgWeaponTip;

    public int maxAmmo => bgMaxAmmo;
    public float reloadTime => bgReloadTime;

    public float timeBetweenShots => bgTimeBetweenShots;

    public float damage => bgDamage;

    public float swapTime => bgSwapTime;

    public float weaponSpread => bgWeaponSpread;

    public float weaponRange => bgWeaponRange;

    public Transform playerCamera => FpCamera;

    TrailRenderer IWeaponBase.bulletTrail => weaponTrail;

    bool IWeaponBase.canShoot { get => canShoot; set => canShoot = value; }
    int IWeaponBase.currentAmmo { get => bgCurrAmmo; set => bgCurrAmmo = value; }
    #endregion


    #region Shot Delay
    public void ShootDelay()
    {
        StartCoroutine(ShotDelay());
    }
    private IEnumerator ShotDelay()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }
    #endregion

    public void BulletTrail(RaycastHit hit)
    {
        TrailRenderer trail = Instantiate(weaponTrail, weaponTip.position, Quaternion.identity);
        StartCoroutine(SpawnTrail(trail, hit));
    }
    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPos = trail.transform.position;
        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPos, hit.point, time);
            time += Time.deltaTime/trail.time;

            yield return null;
        }
        trail.transform.position = hit.point;

        Destroy(trail.gameObject,trail.time);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        FpCamera = transform.parent;
    }

    public void HitObject(GameObject obj)
    {
        IHealth health = obj.GetComponent<IHealth>(); //Checks if object is using the health interface and deals the apropiate amount of damage.
        if(health != null)
        {
            health.DMGTaken(damage);
        }
        
    }
    public void RestPos(GameObject gameObject, Vector3 pos)
    {

    }
        


}
