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
    [SerializeField] QuestRequirements questRequirements;
    [SerializeField] Quest nextQuest;
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

    public QuestType GetQuestType()
    {
        return questRequirements.GetQuestType();
    }

    public QuestRequirements GetQuestRequirements()
    {
        return questRequirements;
    }

    public void CheckIfQuestCompleted()
    {
        PlayerStats.Instance.UpdateFollowedQuest();
        if (questRequirements.completed)
            this.SetQuestStatus(QuestStatus.COMPLETED);
    }

    public void GiveRewardToPlayer()
    {
        PlayerStats.Instance.AddExperiencePoint(questRewardXP);
    }

    public void SetQuestStatus(QuestStatus newQuestStatus)
    {
        questStatus = newQuestStatus;
        switch (questStatus)
        {
            case QuestStatus.AVAILABLE:
                questGiver.CheckIfQuestAvailable();
                break;
            case QuestStatus.COLLECTED:
                questRequirements.QuestSetUp();
                break;
            case QuestStatus.COMPLETED:
                questGiver.TurnOnInteractions();
                break;
            case QuestStatus.FINISHED:
                if(nextQuest!=null)
                    nextQuest.SetQuestStatus(QuestStatus.AVAILABLE);
                if (PlayerStats.Instance.GetFollowedQuest() == this)
                    PlayerStats.Instance.RemoveFollowedQuest();
                break;
            default:
                break;
        }
    }

}

public enum QuestStatus { HIDDEN, AVAILABLE, COLLECTED, COMPLETED, FINISHED}
