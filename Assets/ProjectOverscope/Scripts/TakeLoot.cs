using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;
using UnityEngine.UIElements;

public class TakeLoot : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] AddContenToScrollView scrollViewContent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && StarterAssetsInputs.Instance.interact) 
        {
            scrollViewContent.AddContent(item.itemName);
            InventoryController.Instance.AddToInventrory(item);
            StarterAssetsInputs.Instance.interact = false;
            Destroy(gameObject);
        }
    }

    public void SetWeapon(Weapon weapon)
    {
        this.item = weapon;
    }
}
