using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;
using UnityEngine.UIElements;

public class TakeItem : MonoBehaviour, IInteractible
{
    [SerializeField] Item item;
    [SerializeField] AddContenToScrollView scrollViewContent;

    public void Interact()
    {
        scrollViewContent.AddContent(item.itemName);
        InventoryController.Instance.AddToInventrory(item);
        foreach(var quest in PlayerStats.Instance.GetColletedQuests())
        {
            Debug.Log("Serching for quest...");
            if(quest.GetQuestType() == QuestType.COLLECT)
            {
                if (this.gameObject.tag == ((QuestType_Collect)quest.GetQuestRequirements()).GetObjectToCollectTag())
                {
                    ((QuestType_Collect)quest.GetQuestRequirements()).ObjectCollected();
                    quest.CheckIfQuestCompleted();
                }

            }
        }
        Destroy(gameObject);
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }
}
