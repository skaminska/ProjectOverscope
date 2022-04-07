using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    [SerializeField] GameObject item;
    public void AddToQuestLog(Quest quest)
    {
        GameObject newItem = Instantiate(item, transform);
        newItem.GetComponentInChildren<TextMeshProUGUI>().text = quest.GetQuestName();
    }
}
