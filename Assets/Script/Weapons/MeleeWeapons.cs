using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class MeleeWeapons : MonoBehaviour
{
    public IWeapon weaponParent;
    List<IHealth> enemiesHit = new List<IHealth>();
    public void OnValidate()
    {
        weaponParent = GetComponentInParent<IWeapon>();
    }
    private void Update()
    {
        Debug.Log(weaponParent);
    }
    private void OnTriggerEnter(Collider other)
    {
        IHealth health = other.gameObject.GetComponent<IHealth>();

        if (health != null && !enemiesHit.Contains(health))
        {
            enemiesHit.Add(health);
            weaponParent.HitObject(gameObject);
        }
    }
    public void Resetlist()
    {
        enemiesHit.Clear();
    }
}
