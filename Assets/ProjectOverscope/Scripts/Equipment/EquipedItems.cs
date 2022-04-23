using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipedItems : Singleton<EquipedItems>
{
    [SerializeField] List<InventoryUIController> equipmentSlots;
    [SerializeField] WeaponController weaponController;

    //List<Item> equipedItems;

    public void UpdateEquipedElements()
    {
        weaponController.ChangeWeapon((Weapon)equipmentSlots[0].GetItem(), (Weapon)equipmentSlots[1].GetItem());
    }

}
