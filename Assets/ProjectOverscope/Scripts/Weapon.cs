using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public Type weaponType;
    [SerializeField] public string weaponName;
    //[SerializeField] public int damage;
    [SerializeField] public int minDamage;
    [SerializeField] public int maxDamage;
    [SerializeField] public WeaponClass weaponClass;

    public void SetWeaponStats(WeaponClass weaponClass, Type weaponType, string weaponName, int minDamage, int maxDamage)
    {
        this.weaponClass = weaponClass;
        this.weaponType = weaponType;
        this.weaponName = weaponName;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
    }

    public void SetWeaponStats(WeaponClass weaponClass, Type weaponType, string weaponName, int damage)
    {
        this.weaponClass = weaponClass;
        this.weaponType = weaponType;
        this.weaponName = weaponName;
        this.minDamage = damage - (int)(0.1 * damage);
        this.maxDamage = damage + (int)(0.1 * damage);
        Debug.Log(this.weaponClass + " " + this.weaponType + " " + this.minDamage + "-" + this.maxDamage + "  | Level: " + PlayerStats.Instance.GetCurrentLevel());
    }
}
public enum Type { HANDS=0, MELEE=10, PISTOL=3, RIFLE=6, SNIPER=8 }
public enum WeaponClass { COMMON=1, UNCOMMON=2, RARE=3, EPIC=4, LEGENDARY=5}