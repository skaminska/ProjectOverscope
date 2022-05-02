using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapons")]
public class Weapon : Item
{
    [SerializeField] public Type weaponType;
    [SerializeField] public int minDamage;
    [SerializeField] public int maxDamage;
    [SerializeField] public WeaponClass weaponClass;
    [SerializeField] RuntimeAnimatorController weaponAnimController;

    public void SetWeaponStats(WeaponClass weaponClass, Type weaponType, string weaponName, int damage)
    {
        this.weaponClass = weaponClass;
        this.weaponType = weaponType;
        this.itemName = weaponName;
        this.minDamage = damage - (int)(0.1 * damage);
        this.maxDamage = damage + (int)(0.1 * damage);
        this.itemType = ItemType.WEAPON;
        Debug.Log(this.weaponClass + " " + this.weaponType + " " + this.minDamage + "-" + this.maxDamage + "  | Level: " + PlayerStats.Instance.GetCurrentLevel());
    }

    public void SetAnimationController(RuntimeAnimatorController controller)
    {
        weaponAnimController = controller;
    }
    public RuntimeAnimatorController GetWeaponAnimatorController()
    {
        return weaponAnimController;
    }
}
public enum Type { HANDS=0, MELEE=10, PISTOL=3, RIFLE=6, SNIPER=8 }
public enum WeaponClass { COMMON=1, UNCOMMON=2, RARE=3, EPIC=4, LEGENDARY=5}