using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : Singleton<InventoryController>
{
    [SerializeField] List<GameObject> itemsList;
    [SerializeField] GameObject itemPrefab;
    public void AddToInventrory(Item item)
    {
        GameObject newItem = Instantiate(itemPrefab, transform);
        newItem.GetComponentInChildren<TextMeshProUGUI>().text = item.itemName;
        newItem.GetComponent<InventoryUIController>().SetItem(item);
        itemsList.Add(newItem);
    }

    public void ChangeItem(InventoryUIController itemToChangeController)//Item itemToChange)
    {
        ShowItemOfType(itemToChangeController);

    }

    public void ShowItemOfType(InventoryUIController itemToChangeController)
    {
        foreach(var element in itemsList)
        {
            if (element.GetComponent<InventoryUIController>().GetItemType() != itemToChangeController.GetItem().itemType)
            {
                element.SetActive(false);
            }
            else
            {
                element.SetActive(true);
                element.GetComponent<Button>().onClick.AddListener(() => Test(itemToChangeController, element.GetComponent<InventoryUIController>()));
            }
        }
    }

    void Test(InventoryUIController itemToChange, InventoryUIController controller)
    {
        //Debug.Log(itemToChange.GetItem() + " -> item to changeg");
        //Debug.Log(controller.GetItem() + " -> new Item");

        var tmp = itemToChange.GetItem();
        itemToChange.SetItem(controller.GetItem());
        controller.SetItem(tmp);
        
    }
}
