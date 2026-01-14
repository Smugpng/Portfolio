using System.Collections;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;

public class IW_Sword : MonoBehaviour, IWeapon
{
    public string weaponName;

    public string weaponDescription;

    public Transform bladeTip;

    public float timeBetweenSwings;

    public float damage;

    public float timeToSwap;

    public bool canSwing = true;
    public Transform mainCamera;
    #region Interface
    public string Name => weaponName;

    public string Description => weaponDescription;

    public Transform weaponTip => bladeTip;

    public float attackCooldown => timeBetweenSwings;

    public float healthAffectNum => damage;

    public float swapTime => timeToSwap;

    public Transform playerCamera => mainCamera;
    #endregion

    [Header("Sword Properties")]
    public float attackDistance = 3f;
    public float attackDelay = 1;
    public bool isAttacking;
    int attackCount;

    [Header("Cosmetic/Audio Properties")]
    public GameObject hitEffect;
    public AudioClip swordSwing, hitSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Animator swordAnim;

    private void Awake()
    {
        mainCamera = GetComponentInParent<CinemachineCamera>().transform;
    }
    //-------------------//
    //Make Sure to Change//
    //from a raycast     //
    //To an animation w  //
    // Collision         //
    //-------------------//
    public void Attack()
    {
        Debug.Log("Sup");
        if (canSwing)
        {
            canSwing = false;
            isAttacking = true;
            Debug.Log("Swinging Sword!");

            Invoke(nameof(DelayBetweenShots), timeBetweenSwings);
            //CHANGE BELOW TO ANIMATIONS
            //Invoke(nameof(AttackRaycast), attackDelay);
            swordAnim.SetTrigger("Attack");
            audioSource.pitch = Random.Range(0.9f,1.1f);
            audioSource.PlayOneShot(swordSwing);
        }
        else
        {
            Debug.LogWarning("Can't Attack Right Now");
        }
    }

    public void DelayBetweenShots()
    {
        canSwing = true;
        isAttacking = false;
        Debug.Log("Can Swing");
    }

    public void AttackRaycast()
    {
        if(Physics.SphereCast(mainCamera.transform.position + mainCamera.transform.forward * attackDistance,1f,Vector3.forward, out RaycastHit hit))
        {
            IHealth placeHolder = hit.transform.gameObject.GetComponent<IHealth>();
            if (placeHolder != null) placeHolder.DMGTaken(damage);
        }

    }
    public void HitObject(GameObject target)
    {
        if (isAttacking)
        {
            target.GetComponent<IHealth>().DMGTaken(damage);
        }
    }
    public float drawDistance = 2;

    public void Reload()
    {
        throw new System.NotImplementedException();
    }
}
