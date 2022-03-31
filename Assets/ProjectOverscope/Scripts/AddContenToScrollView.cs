using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddContenToScrollView : MonoBehaviour
{
    [SerializeField] GameObject pickedLoot;
    public Transform parentTransform;
    public void AddContent(string lootName)
    {
        var loot = Instantiate(pickedLoot, parentTransform);
        loot.GetComponent<TextMeshProUGUI>().text = lootName;
    }

}
