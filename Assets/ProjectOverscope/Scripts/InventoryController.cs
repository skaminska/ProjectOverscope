using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryController : Singleton<InventoryController>
{
    [SerializeField] List<GameObject> itemsList;
    [SerializeField] GameObject itemPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddToInventrory(Weapon weapon)
    {
        GameObject newItem = Instantiate(itemPrefab, transform);
        newItem.GetComponentInChildren<TextMeshProUGUI>().text = weapon.weaponName;
        itemsList.Add(newItem);
    }
}
