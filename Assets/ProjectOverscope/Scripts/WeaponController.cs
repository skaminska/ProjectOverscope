using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] List<Weapon> equipedWeapons;
    [SerializeField] TextMeshProUGUI weaponInfo; 

    int currentWeapon;

    private void Start()
    {
        currentWeapon = 0;
    }

    public Type GetCurrentWeaponType()
    {
        return equipedWeapons[currentWeapon].weaponType;
    }
    
    public void ChangeWeapon(int change)
    {
        if (change > 0)
        {
            if (currentWeapon == equipedWeapons.Count-1)
                currentWeapon = 0;
            else
                currentWeapon++;
        }
        else
        {
            if (currentWeapon == 0)
                currentWeapon = equipedWeapons.Count-1;
            else
                currentWeapon--;
        }
        SetWeapon();
    }

    private void SetWeapon()
    {
        weaponInfo.text = equipedWeapons[currentWeapon].weaponName;
        PlayerStats.Instance.SetCurrentDamage(equipedWeapons[currentWeapon].damage);
    }
}
