using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponController : Singleton<WeaponController>
{
    [SerializeField] List<Weapon> equipedWeapons;
    [SerializeField] TextMeshProUGUI weaponInfo;
    [SerializeField] List<TextMeshProUGUI> weapons;
    [SerializeField] Animator animator;

    int currentWeapon;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentWeapon = 0;
        ShowEquipedWeapons();
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

    public void ChangeWeapon(Weapon weapon1, Weapon weapon2)
    {
        equipedWeapons[1] = weapon1;
        equipedWeapons[2] = weapon2;

        ShowEquipedWeapons();
    }

    public void ShowEquipedWeapons()
    {
        weapons[0].text = equipedWeapons[1].itemName;
        weapons[1].text = equipedWeapons[2].itemName;
    }

    private void SetWeapon()
    {
        weaponInfo.text = equipedWeapons[currentWeapon].itemName;
        PlayerStats.Instance.SetCurrentDamage(equipedWeapons[currentWeapon].minDamage, equipedWeapons[currentWeapon].maxDamage);
    }

    public void SetAnimation(bool aiming)
    {
        animator.SetBool("Aiming", aiming);
        if (equipedWeapons[currentWeapon].weaponType == Type.HANDS)
        {
            animator.SetLayerWeight(1, 0f);
        }
        else
        {
            animator.SetLayerWeight(1, 1f);

        }
    }
}
