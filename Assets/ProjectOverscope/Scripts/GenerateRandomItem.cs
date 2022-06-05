using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GenerateRandomItem : MonoBehaviour
{
    [SerializeField] RuntimeAnimatorController pistolController, rifleController;
    //make dictionary
    [SerializeField] Image pistolIcon, rifleIcon;
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

        ItemClass weaponClass = ItemClass.COMMON;
        Type weaponType = Type.MELEE;


        int rarityPossibility = UnityEngine.Random.Range(0, 100);
        if (rarityPossibility < 65)
            weaponClass = ItemClass.COMMON;
        else if (rarityPossibility < 85)
            weaponClass = ItemClass.UNCOMMON;
        else if (rarityPossibility < 95)
            weaponClass = ItemClass.RARE;
        else if (rarityPossibility < 99)
            weaponClass = ItemClass.EPIC;
        else if (rarityPossibility == 99)
            weaponClass = ItemClass.LEGENDARY;

        int typePosibility = Random.Range(1, 4);
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

        newWeapon.itemLevel = PlayerStats.Instance.GetCurrentLevel();
        newWeapon.value = newWeapon.itemLevel * (int)newWeapon.itemClass;
        if (weaponType == Type.PISTOL)
            newWeapon.icon = pistolIcon;
        else if (weaponType == Type.RIFLE)
            newWeapon.icon = rifleIcon;
        AssetDatabase.CreateAsset(newWeapon, "Assets/Items/Weapons/" + newWeapon.itemName + ".asset");

        return newWeapon;
    }
    Item GenerateArmor()
    {
        Armor newArmor = ScriptableObject.CreateInstance<Armor>();
        newArmor.SetStats();
        AssetDatabase.CreateAsset(newArmor, "Assets/Items/Armors/" + newArmor.itemName + ".asset");
        return newArmor;
    }

    Color GetRarityColor(ItemClass weaponClass)
    {
        switch (weaponClass)
        {
            case ItemClass.COMMON:
                return Color.gray;
            case ItemClass.UNCOMMON:
                return Color.blue;
            case ItemClass.RARE:
                return Color.green;
            case ItemClass.EPIC:
                return Color.magenta;
            case ItemClass.LEGENDARY:
                return Color.yellow;
            default:
                return Color.white;
        }
    }
}
