using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Armors")]
public class Armor : Item
{
    public void SetStats()
    {
        itemType = ItemType.SHIRT;
        itemName = "newArmor" + Random.Range(1.0f, 5.0f).ToString();
    }
}
