using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items")]
public class Item : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    //public GameObject itemObject;
    public int value;
    public Image icon;
    public Color backgroundColor;
    public ItemClass itemClass;
    public int itemLevel;

    public Color ItemBackground()
    {
        switch (itemClass)
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

    public virtual string GetItemInfo()
    {
        return "";
    }
}

public enum ItemType { WEAPON, SHIRT}
public enum ItemClass { COMMON = 1, UNCOMMON = 2, RARE = 3, EPIC = 4, LEGENDARY = 5 }

