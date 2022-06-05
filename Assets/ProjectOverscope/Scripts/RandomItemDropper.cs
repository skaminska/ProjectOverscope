using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RandomItemDropper : MonoBehaviour
{
    [SerializeField] GameObject lootWeapon;
    [SerializeField] GameObject lootMoney;
    [SerializeField] Vector3 dropOffset;
    [SerializeField] RuntimeAnimatorController pistolController, rifleController;
    GenerateRandomItem generateRandomItem;

    private void Start()
    {
        generateRandomItem = FindObjectOfType<GenerateRandomItem>();
    }
    public void DrawLoot()
    {
        Item newItem = null;
        int dropChance = UnityEngine.Random.Range(0, 100);
        if(dropChance > 0)
        {
            int whatToDrop = UnityEngine.Random.Range(0, 100);
            if (whatToDrop < 0)
            {
                PlayerStats.Instance.AddMoney(UnityEngine.Random.Range(5, 10));
            }
            else if (whatToDrop < 100)
            {
                newItem = generateRandomItem.GenerateRandomItemOfType(ItemType.WEAPON);
            }
            else
            {
                newItem = generateRandomItem.GenerateRandomItemOfType(ItemType.SHIRT);
            }
        }
        var loot = Instantiate(lootWeapon, transform.position + dropOffset, Quaternion.Euler(new Vector3(-90, 0, 0)));
        loot.GetComponent<TakeItem>().SetItem(newItem);
    }
}
