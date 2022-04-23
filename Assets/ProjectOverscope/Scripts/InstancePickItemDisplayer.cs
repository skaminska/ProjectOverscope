using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstancePickItemDisplayer : MonoBehaviour
{
    [SerializeField] GameObject pickedItemDisplayer;
    [SerializeField] GameObject pickedLoot;

    Transform parentTransform;

    void Start()
    {
        var content = Instantiate(pickedItemDisplayer, transform);
        pickedItemDisplayer.GetComponent<AddContentToScrollView>().parentTransform = content.transform;
    }
}
