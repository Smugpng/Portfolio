using UnityEngine;

[ExecuteInEditMode]
public class MeleeWeapons : MonoBehaviour
{
    public IWeapon weaponParent;
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
        if (health != null) weaponParent.HitObject(gameObject);
    }
}
