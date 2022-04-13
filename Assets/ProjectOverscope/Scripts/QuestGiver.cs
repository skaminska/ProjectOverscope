using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour, IInteractible
{
    [SerializeField] List<Quest> quests;
    [SerializeField] GameObject interactionInfo;
    [SerializeField] GameObject availableQuest;
    [SerializeField] QuestWindow questWindow;
    [SerializeField] NPCInfo NPCInfo;
    bool haveInteractionActive;


    private void Start()
    {
        questWindow = FindObjectOfType<QuestWindow>();
        interactionInfo.SetActive(false);
        CheckIfQuestAvailable();
        foreach(var quest in quests)
        {
            quest.SetQuestGiver(this);
        }
        NPCInfo.SetQuestGiver(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && haveInteractionActive)
        {
            interactionInfo.SetActive(true);
            interactionInfo.transform.LookAt(2 * transform.position - Camera.main.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactionInfo.SetActive(false);
        }
    }

    public void Interact()
    {
        if (quests[0].GetQuestStatus() == QuestStatus.AVAILABLE && haveInteractionActive)
        {
            Debug.Log("Hello adventurer!");
            questWindow.ShowQuestInfo(quests[0], this);
            interactionInfo.SetActive(false);
        }
        else if (quests[0].GetQuestStatus() == QuestStatus.COMPLETED)
        {
            quests[0].SetQuestStatus(QuestStatus.FINISHED);
            quests[0].GiveRewardToPlayer();
            quests.Remove(quests[0]);
            availableQuest.SetActive(false);
            TurnOffInteractions();
        }
        else if (PlayerStats.Instance.GetColletedQuests() != null)
        {
            foreach(var quest in PlayerStats.Instance.GetColletedQuests())
            {
                if(quest.GetQuestRequirements().GetQuestType() == QuestType.TALKTO)
                {
                    //TODO check if list contains this npc first
                    ((QuestType_TalkTo)quest.GetQuestRequirements()).Talked(NPCInfo);
                    quest.CheckIfQuestCompleted();
                    TurnOffInteractions();
                }
            }
        }
    }

    public void CheckIfQuestAvailable()
    {
        if (quests[0].GetQuestStatus() == QuestStatus.AVAILABLE)
        {
            TurnOnInteractions();
            quests[0].SetQuestGiver(this);
        }
        else
        {
            TurnOffInteractions();
        }
    }

    public void TurnOnInteractions()
    {
        Debug.Log("Hey you! Over there!");
        availableQuest.SetActive(true);
        haveInteractionActive = true;
    }

    public void TurnOffInteractions()
    {
        availableQuest.SetActive(false);
        haveInteractionActive = false;
        interactionInfo.SetActive(false);
    }
}
