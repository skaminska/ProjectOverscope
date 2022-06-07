using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour, IInteractible
{
    [SerializeField] InventoryController inventory;
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject inventoryUI;
    [SerializeField] List<InventoryUIController> itemsInShop;

    GenerateRandomItem generateRandomItem;
    private void Start()
    {
        shopUI.SetActive(false);
        inventory = FindObjectOfType<InventoryController>();
        generateRandomItem = FindObjectOfType<GenerateRandomItem>();
    }


    public void Interact()
    {

        shopUI.SetActive(true);
        inventoryUI.SetActive(true);
        inventory.ShowInventoryInShop();
        foreach(var item in itemsInShop)
        {
            item.SetItem(generateRandomItem.GenerateRandomItemOfType(ItemType.WEAPON));
            item.GetComponent<Button>().onClick.RemoveAllListeners();
            item.GetComponent<Button>().onClick.AddListener(() => BuyItem(item.GetItem(), item.gameObject));
            item.gameObject.SetActive(true);
        }
        Debug.Log("hello");
    }

    void BuyItem(Item item, GameObject itemObject)
    {
        if(PlayerStats.Instance.GetMoneyAmount() >= item.value)
        {
            inventory.AddToInventrory(item);
            PlayerStats.Instance.ChangeMoneyAmount(-item.value);
            itemObject.SetActive(false);
        }

    }




    

}
