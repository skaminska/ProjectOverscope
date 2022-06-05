using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapons")]
public class Weapon : Item
{
    [SerializeField] public Type weaponType;
    [SerializeField] public int minDamage;
    [SerializeField] public int maxDamage;
    [SerializeField] RuntimeAnimatorController weaponAnimController;

    public void SetWeaponStats(ItemClass weaponClass, Type weaponType, string weaponName, int damage)
    {
        this.itemClass = weaponClass;
        this.weaponType = weaponType;
        this.itemName = weaponName;
        this.minDamage = damage - (int)(0.1 * damage);
        this.maxDamage = damage + (int)(0.1 * damage);
        this.itemType = ItemType.WEAPON;
        Debug.Log(this.itemClass + " " + this.weaponType + " " + this.minDamage + "-" + this.maxDamage + "  | Level: " + PlayerStats.Instance.GetCurrentLevel());
    }

    public void SetAnimationController(RuntimeAnimatorController controller)
    {
        weaponAnimController = controller;
    }
    public RuntimeAnimatorController GetWeaponAnimatorController()
    {
        return weaponAnimController;
    }

    public override string GetItemInfo()
    {
        return "Damage: " + minDamage + " - " + maxDamage;
    }
}
public enum Type { HANDS=0, MELEE=10, PISTOL=3, RIFLE=6, SNIPER=8 }
