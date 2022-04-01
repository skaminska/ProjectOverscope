using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] GameObject inventory;

    private void Start()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = item.itemName;
    }

    public void OnEquipmentItemClick()
    {
        Debug.Log(item);
        inventory.SetActive(true);
        InventoryController.Instance.ChangeItem(this);
    }

    public ItemType GetItemType()
    {
        return item.itemType;
    }

    public void SetItem(Item item)
    {
        this.item = item;
        GetComponentInChildren<TextMeshProUGUI>().text = item.itemName;
    }

    public Item GetItem()
    {
        return item;
    }

    

}
