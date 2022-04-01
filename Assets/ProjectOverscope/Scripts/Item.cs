using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items")]
public class Item : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
}

public enum ItemType { WEAPON, SHIRT}
