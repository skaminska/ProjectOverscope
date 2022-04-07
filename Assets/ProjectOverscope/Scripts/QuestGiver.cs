using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour, IInteractible
{
    [SerializeField] List<Quest> quests;
    [SerializeField] GameObject interactionInfo;
    [SerializeField] GameObject availableQuest;
    [SerializeField] QuestWindow questWindow;
    bool haveActiveQuest;


    private void Start()
    {
        questWindow = FindObjectOfType<QuestWindow>();
        interactionInfo.SetActive(false);
        CheckIfQuestAvailable();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && haveActiveQuest)
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
        if (haveActiveQuest)
        {
            Debug.Log("Hello adventurer!");
            questWindow.ShowQuestInfo(quests[0], this);
        }

    }

    public void CheckIfQuestAvailable()
    {
        if (quests[0].GetQuestStatus() == QuestStatus.AVAILABLE)
        {
            availableQuest.SetActive(true);
            haveActiveQuest = true;
        }
        else
        {
            availableQuest.SetActive(false);
            haveActiveQuest = false;
        }
    }
}
