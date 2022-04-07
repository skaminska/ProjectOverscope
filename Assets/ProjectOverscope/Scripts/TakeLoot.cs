using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;
using UnityEngine.UIElements;

public class TakeLoot : MonoBehaviour, IInteractible
{
    [SerializeField] Item item;
    [SerializeField] AddContenToScrollView scrollViewContent;

    public void Interact()
    {
        scrollViewContent.AddContent(item.itemName);
        InventoryController.Instance.AddToInventrory(item);
        Destroy(gameObject);
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }
}
