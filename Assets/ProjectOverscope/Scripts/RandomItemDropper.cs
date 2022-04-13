using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RandomItemDropper : MonoBehaviour
{
    [SerializeField] GameObject lootWeapon;
    [SerializeField] GameObject lootMoney;
    public void DrawLoot()
    {


        int dropChance = UnityEngine.Random.Range(0, 100);
        if(dropChance > 0)
        {
            int whatToDrop = UnityEngine.Random.Range(0, 100);
            if (whatToDrop < 0)
            {
                PlayerStats.Instance.AddMoney(UnityEngine.Random.Range(5, 10));
            }
            else if (whatToDrop < 50)
            {
                DropWeapon();
            }
            else
            {
                DropArmor();
            }
        }
        //Instantiate(loot, transform.position, Quaternion.identity);
    }

    private void DropArmor()
    {
        Armor newArmor = ScriptableObject.CreateInstance<Armor>();
        newArmor.SetStats();
        AssetDatabase.CreateAsset(newArmor, "Assets/Items/Armors/" + newArmor.itemName + ".asset");
        var loot = Instantiate(lootWeapon, transform.position, Quaternion.identity);
        loot.GetComponent<TakeItem>().SetItem(newArmor);
    }

    private void DropWeapon()
    {
        WeaponClass weaponClass = WeaponClass.COMMON;
        Type weaponType = Type.MELEE;


        int rarityPossibility = UnityEngine.Random.Range(0, 100);
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

        int typePosibility = UnityEngine.Random.Range(0, 4);
        switch (typePosibility)
        {
            case 0:
                weaponType = Type.MELEE;
                break;
            case 1:
                weaponType = Type.PISTOL;
                break;
            case 2:
                weaponType = Type.RIFLE;
                break;
            case 3:
                weaponType = Type.SNIPER;
                break;
        }

        int damage = (PlayerStats.Instance.GetCurrentLevel() * (int)weaponClass)*(int)weaponType;
        string name = weaponClass + " " + weaponType;

        Weapon newWeapon = ScriptableObject.CreateInstance<Weapon>();
        newWeapon.SetWeaponStats(weaponClass, weaponType, name, damage);
        AssetDatabase.CreateAsset(newWeapon, "Assets/Items/Weapons/" + newWeapon.itemName + ".asset");
        var loot = Instantiate(lootWeapon, transform.position, Quaternion.identity);
        loot.GetComponent<TakeItem>().SetItem(newWeapon);
    }
}
