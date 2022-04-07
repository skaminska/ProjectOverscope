using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest")]
public class Quest : ScriptableObject
{
    [SerializeField] string questName;
    [SerializeField] string questDescription;
    [SerializeField] int questRewardXP;
    [SerializeField] int questRewardMoney;
    [SerializeField] Item questRewardItem;
    //List of quest that must be FINISHED in order to unlock this Quest
    [SerializeField] List<Quest> questRequirements;
    [SerializeField] QuestStatus questStatus;

    public QuestStatus GetQuestStatus()
    {
        return questStatus;
    }
    public string GetQuestName()
    {
        return questName;
    }
    public string GetQuestDescription()
    {
        return questDescription;
    }
    public string GetQuestReward()
    {
        return "Money: " + questRewardMoney + "     XP: " + questRewardXP + "       Item: " + questRewardItem.itemName ;
    }
    public void SetQuestStatus(QuestStatus newQuestStatus)
    {
        questStatus = newQuestStatus;
    }
}

public enum QuestStatus { HIDDEN, AVAILABLE, COLLECTED, COMPLETED, FINISHED}
