using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] GameObject inventory;

    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemValue;
    [SerializeField] TextMeshProUGUI itemLevel;
    [SerializeField] TextMeshProUGUI itemAddictionalInfo;
    [SerializeField] Image backgrounColor;

    private void Start()
    {
        if(item!=null)
            SetItemInfo();
    }

    public void OnEquipmentItemClick()
    {
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
        SetItemInfo();
    }

    public Item GetItem()
    {
        return item;
    }

    void SetItemInfo()
    {
        itemIcon = item.icon;
        itemName.text = item.itemName;
        itemValue.text = item.value.ToString() + "$";
        itemLevel.text = item.itemLevel.ToString();
        itemAddictionalInfo.text = item.GetItemInfo();
        backgrounColor.color = item.ItemBackground();
    }

}
