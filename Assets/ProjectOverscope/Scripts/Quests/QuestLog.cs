using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] GameObject questDetails;
    [SerializeField] GameObject followedQuestDetails;
    private void Start()
    {
        questDetails.SetActive(false);
        followedQuestDetails.SetActive(false);
    }
    public void AddToQuestLog(Quest quest)
    {
        GameObject newItem = Instantiate(item, transform);
        newItem.GetComponentInChildren<TextMeshProUGUI>().text = quest.GetQuestName();
        newItem.GetComponent<Button>().onClick.AddListener(() => ShowDetails(quest));
    }

    public void ShowDetails(Quest quest)
    {
        questDetails.SetActive(true);
        questDetails.GetComponentInChildren<TextMeshProUGUI>().text = quest.GetQuestName() + "\n" + quest.GetQuestStatus();
        questDetails.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        questDetails.GetComponentInChildren<Button>().onClick.AddListener(() => SetAsFollowed(quest));
    }

    //public void TmpFunc(Quest quest)
    //{
    //       questDetails.SetActive(false);
    //        quest.SetQuestStatus(QuestStatus.COMPLETED);
    //}

    public void SetAsFollowed(Quest quest)
    {
        questDetails.SetActive(true);
        PlayerStats.Instance.SetFollowedQuest(quest);
        UpdateFollowInfo();
    }


    public void UpdateFollowInfo()
    {
        followedQuestDetails.GetComponent<TextMeshProUGUI>().text = PlayerStats.Instance.GetFollowedQuest().GetQuestDescription() + "\n" + PlayerStats.Instance.GetFollowedQuest().GetQuestRequirements().GetQuestProgress();
        if(!followedQuestDetails.activeInHierarchy)
            followedQuestDetails.SetActive(true);
    }
    public void HideFollowInfo()
    {
        followedQuestDetails.SetActive(false);
    }
}
