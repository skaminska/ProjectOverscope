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

    [SerializeField] QuestGiver questGiver;

    public void SetQuestGiver(QuestGiver questGiver)
    {
        this.questGiver = questGiver; 
    }

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

    public void GiveRewardToPlayer()
    {
        PlayerStats.Instance.AddExperiencePoint(questRewardXP);
    }

    public void SetQuestStatus(QuestStatus newQuestStatus)
    {
        questStatus = newQuestStatus;
        if(questStatus == QuestStatus.COMPLETED)
        {
            questGiver.QuestCompleted();
        }
    }
}

public enum QuestStatus { HIDDEN, AVAILABLE, COLLECTED, COMPLETED, FINISHED}
