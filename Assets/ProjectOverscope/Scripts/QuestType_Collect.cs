using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QuestTypes/Collect")]
public class QuestType_Collect : QuestRequirements
{
    [SerializeField] string objectToCollectTag;
    [SerializeField] int currentAmount;
    [SerializeField] int requiredAmount;

    public string GetObjectToCollectTag()
    {
        return objectToCollectTag;
    }

    public void ObjectCollected()
    {
        currentAmount++;
        if(currentAmount >= requiredAmount)
        {
            base.completed = true;
        }
    }

    public override string GetQuestProgress()
    {
        if (currentAmount != requiredAmount)
            return currentAmount + "/" + requiredAmount;
        else
            return "Go back to quest giver";
    }
}
