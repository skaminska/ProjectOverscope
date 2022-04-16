using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QuestTypes/Eliminate")]
public class QuestType_Eliminate : QuestRequirements
{
    [SerializeField] string targetToEliminateTag;
    [SerializeField] int currentAmount;
    [SerializeField] int requiredAmount;

    public string GetTargetToEliminateTag()
    {
        return targetToEliminateTag;
    }

    public void TargetEliminated()
    {
        currentAmount++;
        if (currentAmount >= requiredAmount)
        {
            base.completed = true;
        }
    }
}
