using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour, IInteractible
{
    [SerializeField] List<Quest> quests;
    [SerializeField] GameObject interactionInfo;
    [SerializeField] GameObject availableQuest;
    [SerializeField] QuestWindow questWindow;
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
        else if(quests[0].GetQuestStatus() == QuestStatus.COMPLETED)
        {
            quests[0].SetQuestStatus(QuestStatus.FINISHED);
            quests[0].GiveRewardToPlayer();
            quests.Remove(quests[0]);
            availableQuest.SetActive(false);
            interactionInfo.SetActive(false);
            haveInteractionActive = false;
        }

    }

    public void CheckIfQuestAvailable()
    {
        if (quests[0].GetQuestStatus() == QuestStatus.AVAILABLE)
        {
            availableQuest.SetActive(true);
            haveInteractionActive = true;
            quests[0].SetQuestGiver(this);
        }
        else
        {
            availableQuest.SetActive(false);
            haveInteractionActive = false;
        }
    }

    public void QuestCompleted()
    {
        Debug.Log("Hey. Come here!");
        availableQuest.SetActive(true);
        haveInteractionActive = true;
    }
}
