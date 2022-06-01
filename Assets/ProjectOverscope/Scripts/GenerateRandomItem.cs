using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GenerateRandomItem : MonoBehaviour
{
    [SerializeField] RuntimeAnimatorController pistolController, rifleController;
    //make dictionary
    [SerializeField] Sprite pistolIcon, rifleIcon;
    public Item GenerateRandomItemOfType(ItemType itemType)
    {
        Item newItem = null;
        switch (itemType)
        {
            case ItemType.WEAPON:
                newItem = GenerateWeapon();
                break;
            case ItemType.SHIRT:
                newItem = GenerateArmor();
                break;
        }
        
        return newItem;
    }

    Item GenerateWeapon()
    {
        Item newItem = new Item();

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

        int damage = (PlayerStats.Instance.GetCurrentLevel() * (int)weaponClass) * (int)weaponType;
        string name = weaponClass + " " + weaponType;

        Weapon newWeapon = ScriptableObject.CreateInstance<Weapon>();
        newWeapon.SetWeaponStats(weaponClass, weaponType, name, damage);
        //This is TMP
        if (weaponType == Type.PISTOL)
            newWeapon.SetAnimationController(pistolController);
        else if (weaponType == Type.RIFLE)
            newWeapon.SetAnimationController(rifleController);

        AssetDatabase.CreateAsset(newWeapon, "Assets/Items/Weapons/" + newWeapon.itemName + ".asset");
        newItem = newWeapon;
        newItem.backgroundColor = GetRarityColor(weaponClass);
        if (weaponType == Type.PISTOL)
            newItem.icon = pistolIcon;
        else if (weaponType == Type.RIFLE)
            newItem.icon = rifleIcon;
        return newItem;
    }
    Item GenerateArmor()
    {
        Armor newArmor = ScriptableObject.CreateInstance<Armor>();
        newArmor.SetStats();
        AssetDatabase.CreateAsset(newArmor, "Assets/Items/Armors/" + newArmor.itemName + ".asset");
        return newArmor;
    }

    Color GetRarityColor(WeaponClass weaponClass)
    {
        switch (weaponClass)
        {
            case WeaponClass.COMMON:
                return Color.gray;
            case WeaponClass.UNCOMMON:
                return Color.blue;
            case WeaponClass.RARE:
                return Color.green;
            case WeaponClass.EPIC:
                return Color.magenta;
            case WeaponClass.LEGENDARY:
                return Color.yellow;
            default:
                return Color.white;
        }
    }
}
