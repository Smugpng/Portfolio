using UnityEngine;

public interface IWeapon
{
    [Header("Weapon Details")]
    public string Name { get; }
    public string Description { get; }
    public Transform weaponTip { get; }

    [Header("Weapon Stats")]

    public float attackCooldown { get; }
    public float healthAffectNum { get; } //Named this was as some items might heal instead
    public float swapTime { get; }
    public Transform playerCamera { get; }

    public void Attack();

    public void DelayBetweenShots();

    public void HitObject(GameObject target);

    public void Reload();
}
