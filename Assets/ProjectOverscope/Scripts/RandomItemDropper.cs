using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemDropper : MonoBehaviour
{
    [SerializeField] GameObject drop;
    private void Awake()
    {
        for(int i =0; i<10; i++)
        {
           Weapon weapon = DropWeapon();
            Debug.Log(weapon.weaponClass + " " + weapon.weaponType + " " + weapon.weaponName + " " + weapon.minDamage + " " + weapon.maxDamage);
        }
        int dropChance = Random.Range(0, 100);
        if(dropChance > 60)
        {
            int whatToDrop = Random.Range(0, 100);
            if (whatToDrop < 60)
            {
                Debug.Log("Here's your money");
            }
            else
            {
                Weapon weapon = DropWeapon();
                drop.GetComponent<Weapon>().weaponName = weapon.weaponName;

                //newWeapon = DropWeapon();
                //Instantiate(drop, transform.position, Quaternion.identity);
            }
        }
    }

    private Weapon DropWeapon()
    {
        WeaponClass weaponClass = WeaponClass.COMMON;
        Type weaponType = Type.MELEE;


        int rarityPossibility = Random.Range(0, 100);
        if (rarityPossibility < 65)
            weaponClass = WeaponClass.COMMON;
        else if (rarityPossibility < 85)
            weaponClass = WeaponClass.UNCOMMON;
        else if (rarityPossibility < 95)
            weaponClass = WeaponClass.RARE;
        else if (rarityPossibility < 99)
            weaponClass = WeaponClass.EPIC;
        else if (rarityPossibility == 99)
            weaponClass = WeaponClass.LEGENDARY;

        int typePosibility = Random.Range(0, 3);
        switch (typePosibility)
        {
            case 0:
                weaponType = Type.MELEE;
                break;
            case 1:
                weaponType = Type.PISTOL;
                break;
            case 2:
                weaponType = Type.RIFFLE;
                break;
            case 3:
                weaponType = Type.SNIPER;
                break;
        }

        int damage = (PlayerStats.Instance.GetCurrentLevel() * (int)weaponClass)*(int)weaponType;
        string name = weaponClass + " " + weaponType;

        Debug.Log(PlayerStats.Instance.GetCurrentLevel()); 
                
        return new Weapon(weaponClass, weaponType, name, damage);
    }
}
