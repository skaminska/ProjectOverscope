using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;
using UnityEngine.UIElements;

public class TakeLoot : MonoBehaviour
{
    //[SerializeField] GameObject pickedLoot;
    [SerializeField] Weapon weapon;
    [SerializeField] AddContenToScrollView scrollViewContent;
    //[SerializeField] InstancePickItemDisplayer display;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("test");
        if (other.tag == "Player" && StarterAssetsInputs.Instance.interact) 
        {
            //Debug.Log(GetComponent<Weapon>().weaponName);
            scrollViewContent.AddContent(weapon.weaponName);
            InventoryController.Instance.AddToInventrory(weapon);
            //var content = Instantiate(pickedLoot);
            //content.GetComponent<TextMeshProUGUI>().text = GetComponent<Weapon>().weaponName;
            //scrollViewContent.AddContent(content);
            //content.transform.SetParent(scrollViewContent.GetTransform());
            StarterAssetsInputs.Instance.interact = false;
            Destroy(gameObject);
        }
    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }
}
