using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items")]
public class Item : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    //public GameObject itemObject;
    public int value;
    public Sprite icon;
    public Color backgroundColor;

}

public enum ItemType { WEAPON, SHIRT}
