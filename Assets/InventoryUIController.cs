using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    Weapon weapon;
    private void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
    }

    public void OnInventoryWeaponClick()
    {
        WeaponController.Instance.ChangeWeapon(weapon);
        Destroy(gameObject);
    }
}
