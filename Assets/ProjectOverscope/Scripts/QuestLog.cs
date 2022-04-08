using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] GameObject questDetails;

    private void Start()
    {
        questDetails.SetActive(false);
    }
    public void AddToQuestLog(Quest quest)
    {
        GameObject newItem = Instantiate(item, transform);
        newItem.GetComponentInChildren<TextMeshProUGUI>().text = quest.GetQuestName();
        newItem.GetComponent<Button>().onClick.AddListener(() => ShowDetails(quest));
    }

    public void ShowDetails(Quest quest)
    {
        Debug.Log(quest.name);
        questDetails.SetActive(true);
        questDetails.GetComponentInChildren<TextMeshProUGUI>().text = quest.GetQuestName() + "\n" + quest.GetQuestStatus();
        questDetails.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        questDetails.GetComponentInChildren<Button>().onClick.AddListener(() => TmpFunc(quest));
    }

    public void TmpFunc(Quest quest)
    {
        questDetails.SetActive(false);
        quest.SetQuestStatus(QuestStatus.COMPLETED);
    }
}
